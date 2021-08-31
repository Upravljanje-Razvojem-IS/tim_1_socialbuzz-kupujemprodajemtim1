using AutoMapper;
using FinantialService.Data.Interfaces;
using FinantialService.Entities;
using FinantialService.MockServices.AccountService;
using FinantialService.MockServices.ProductService;
using FinantialService.MockServices.TransportService;
using FinantialService.MockServices.UserService;
using FinantialService.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.Controllers
{
    /// <summary>
    /// ReST controller with CRUD operations for transactions
    /// </summary>
    [ApiController]
    [Route("api/transactions")]
    [Produces("application/json", "application/xml")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;
        private readonly IProductMockRepository productMockService;
        private readonly ITransportTypeMockRepository transportTypeMockService;
        private readonly LinkGenerator linkGenerator;
        private readonly IValidator<Transaction> transactionValidator;
        private readonly ILogger logger;
        private readonly IAccountMockRepository accountMockService;
        private readonly IUserMockRepository userMockService;

        public TransactionController(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            IProductMockRepository productMockService,
            ITransportTypeMockRepository transportTypeMockService,
            LinkGenerator linkGenerator,
            IValidator<Transaction> transactionValidator,
            ILogger logger,
            IAccountMockRepository accountMockService,
            IUserMockRepository userMockService)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
            this.productMockService = productMockService;
            this.transportTypeMockService = transportTypeMockService;
            this.linkGenerator = linkGenerator;
            this.transactionValidator = transactionValidator;
            this.logger = logger;
            this.accountMockService = accountMockService;
            this.userMockService = userMockService;
        }


        /// <summary>
        /// Creates new transaction (order)
        /// </summary>
        /// <param name="transaction">Transaction model</param>
        /// <returns>Confirmation of created transaction</returns>
        /// <remarks>
        /// Example of a post request \
        /// POST /api/transactions \
        /// {     \
        ///     "BuyerId": "f9168c5e-ceb2-4faa-b6bf-329bf39fa1e5",\
        ///     "ProductId" : "b6496c4a-f938-4863-b3b7-2db52e4271dc",\
        ///     "ProductsQuantity": 2,\
        ///     "TransportTypeId": "6a411c13-a195-48f7-8dbd-67596c3974c1",\
        ///     "DeliveryAddress" : "Strazilovska 123",\
        ///     "DeliveryCity" : "Novi Sad" \
        ///}
        /// </remarks>
        /// <response code="200">Returns created transaction (order)</response>
        /// <response code="404">Product or transport type does not exist</response>
        /// <response code="406">There is no enough products for order</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TransactionDto> CreateTransaction([FromBody] TransactionCreateDto transaction)
        {
            try
            {
                var product = productMockService.GetProductById(transaction.ProductId);

                if (product == null)
                {

                    return NotFound("Product with specified id does not exist.");

                }

                if (!productMockService.HasEnoughProducts(transaction.ProductId, transaction.ProductsQuantity))
                {

                    return StatusCode(StatusCodes.Status406NotAcceptable, "There is no enough products.");

                }

                if (!transportTypeMockService.TransportTypeExistsById(transaction.TransportTypeId))
                {

                    return NotFound("Transport type with specified id does not exist.");
                }


                Transaction transactionEntity = mapper.Map<Transaction>(transaction);

                transactionEntity.BuyingDateTime = DateTime.Now;

                Transaction createdTransaction = transactionRepository.CreateTransaction(transactionEntity);
                var valid = transactionValidator.Validate(createdTransaction);
                if (!valid.IsValid)
                {
                    return BadRequest(valid.Errors);
                }

                //Trazimo usera da bismo dobili id naloga sa kog treba da skinemo novac
                var user = userMockService.GetAccounByUserId(transaction.BuyerId);

                if (user == null)
                {
                    return NotFound("User with specified id does not exist.");
                }

                //Trazimo nalog sa kog treba da skinemo novac
                var account = accountMockService.getAccountById(user.AccountId);

                //Iznos koji treba da skinemo sa naloga
                decimal amount = product.Price * transaction.ProductsQuantity;

                if (account == null)
                {
                    return NotFound("User does not have account.");
                }

                if (account.AccountBalance < amount)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "You do not have enough money to buy this product.");
                }

                TransactionChargeDto charge = new TransactionChargeDto { AccountId = account.AccountId, Amount = amount };

                //Naplacujemo
                bool charged = accountMockService.charge(charge);

                //Proveravamo da li je naplaceno
                if (!charged)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while charging, contact administration");
                }

                //Smanjujemo broj proizvoda na lageru
                TransactionReduceStockDto purchase = new TransactionReduceStockDto { ProductId = transaction.ProductId, Quantity = transaction.ProductsQuantity };

                bool reducedStock = productMockService.ProductPurchased(purchase);

                if (!reducedStock)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while stock reducing, contact administration");
                }

                transactionRepository.SaveChanges();

                logger.Log(LogLevel.Information, $"requestId: {Request.HttpContext.TraceIdentifier}, previousRequestId:No previous ID, Message: Transaction successfully created.");

                string location = linkGenerator.GetPathByAction("GetTransaction", "Transaction", new { transactionId = createdTransaction.TransactionId });

                return Created(location, mapper.Map<TransactionDto>(createdTransaction));


            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Warning, $"requestId: {Request.HttpContext.TraceIdentifier}, previousRequestId:No previous ID, Message: Transaction unsuccessfully created.", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error " + e.Message);
            }
        }


        /// <summary>
        /// Returns all transaction (orders).
        /// </summary>
        /// <param name="buyerId">Buyer id</param>
        /// <param name="deliveryAddress">The address to which the order is delivered (e.g. Strazilovska 10)</param>
        /// <param name="deliveryCity">The city where the order is delivered  (e.g. Novi Sad)</param>
        /// <returns>List of transactions (orders)</returns>
        /// <response code="200">Returns list of transactions (orders)</response>
        /// <response code="404">There is no transactions(if param specified - there is no transactions with specified param)</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<TransactionDto>> GetTransactions(Guid buyerId, string deliveryAddress, string deliveryCity)
        {
            var transactions = transactionRepository.GetTransactions(buyerId, deliveryAddress, deliveryCity);

            if (transactions == null || transactions.Count == 0)
            {
                return NotFound("There is no transactions.");
            }

            return Ok(mapper.Map<List<TransactionDto>>(transactions));

        }

        /// <summary>
        /// Returns one transaction with specified id.
        /// </summary>
        /// <param name="transactionId">Transaction id</param>
        /// <returns>Transaction with specified id</returns>
        /// <response code="200">Returns transaction with specified id</response>
        /// <response code="404">There is no transaction with specified id</response>
        [HttpGet("{transactionId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<TransactionDto> GetTransaction(Guid transactionId)
        {
            var transaction = transactionRepository.GetTransactionById(transactionId);

            if (transaction == null)
            {
                return NotFound("There is no transaction with specified id.");
            }

            return Ok(mapper.Map<TransactionDto>(transaction));
        }

        /// <summary>
        /// Updates quantity, delivery address or delivery city of one transaction (order)
        /// </summary>
        /// <param name="transaction">Transaction model for updating</param>
        /// <returns>Update confirmation</returns>
        /// <remarks>
        /// Example of a update request \
        /// PUT /api/transactions \
        /// {     \
        ///     "TransactionId": "52b0cbe6-661d-4b79-55c1-08d968d41dcd",\
        ///     "DeliveryAddress" : "Strazilovska 123",\
        ///     "DeliveryCity" : "Novi Sad" \
        ///}
        /// </remarks>
        /// <response code="200">Returns updated transaction</response>
        /// <response code="404">There is no transaction you want to update</response>
        /// <response code="406">There is no enough products for order</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TransactionDto> UpdateTransaction([FromBody] TransactionUpdateDto transaction)
        {
            try
            {
                var oldTransaction = transactionRepository.GetTransactionById(transaction.TransactionId);
                if (oldTransaction == null)
                {
                    return NotFound("There is no transaction with specified id.");
                }

                mapper.Map(transaction, oldTransaction);

                var valid = transactionValidator.Validate(oldTransaction);

                if (!valid.IsValid)
                {
                    return BadRequest(valid.Errors);
                }

                transactionRepository.SaveChanges();

                logger.Log(LogLevel.Information, $"requestId: {Request.HttpContext.TraceIdentifier}, previousRequestId:No previous ID, Message: Transaction successfully updated.");

                return Ok(mapper.Map<TransactionDto>(oldTransaction));

            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Warning, $"requestId: {Request.HttpContext.TraceIdentifier}, previousRequestId:No previous ID, Message: Transaction unsuccessfully updated.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error " + e.Message);
            }
        }

        /// <summary>
        /// Deletes one transaction with specified id
        /// </summary>
        /// <param name="transactionId">Transaction id</param>
        /// <response code="200">Transaction successfully deleted</response>
        /// <response code="404">There is no transaction with specified id</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{transactionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTransaction(Guid transactionId)
        {
            try
            {
                var transaction = transactionRepository.GetTransactionById(transactionId);

                if (transaction == null)
                {
                    return NotFound("There is no transaction with specified id.");
                }

                transactionRepository.DeleteTransaction(transactionId);
                transactionRepository.SaveChanges();

                //Proizvod koji je kupljen
                var product = productMockService.GetProductById(transaction.ProductId);

                //Uvecaj broj dostupnih proizvoda
                TransactionReduceStockDto returnee = new TransactionReduceStockDto { ProductId = product.ProductId, Quantity = (int)transaction.ProductsQuantity };

                bool returned = productMockService.ProductReturned(returnee);

                if (!returned)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while returning products to stock, contact administration");
                }

                //Vrati novac na racun

                //Pronadji kupca koji je izvrsio transakciju
                var user = userMockService.GetAccounByUserId(transaction.BuyerId);

                //Trazimo nalog na koji vracamo novac
                var account = accountMockService.getAccountById(user.AccountId);

                //Iznos koji treba da vratimo
                decimal amount = (decimal)(product.Price * transaction.ProductsQuantity);

                TransactionChargeDto refund = new TransactionChargeDto { AccountId = account.AccountId, Amount = amount };

                bool refunded = accountMockService.refund(refund);

                if (!refunded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while refunding, contact administration");
                }

                logger.Log(LogLevel.Information, $"requestId: {Request.HttpContext.TraceIdentifier}, previousRequestId:No previous ID, Message: Transaction successfully deleted.");
                return Ok("Transaction successfully deleted.");
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Information, $"requestId: {Request.HttpContext.TraceIdentifier}, previousRequestId:No previous ID, Message: Transaction unsuccessfully deleted.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error " + e.Message);
            }
        }

        /// <summary>
        /// Returns allowed options
        /// </summary>
        [HttpOptions]
        public IActionResult GetTransactionOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
