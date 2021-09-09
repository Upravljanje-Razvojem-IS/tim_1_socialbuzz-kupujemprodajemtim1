using AccountService.Data.Interfaces;
using AccountService.Dtos;
using AccountService.Logger;
using AccountService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserRepository _userRepository;
        private readonly FakeLogger _logger;

        public AccountsController(
            IMapper mapper,
            IAccountRepository accountRepository,
            ICurrencyRepository currencyRepository,
            IUserRepository userRepository,
            FakeLogger logger)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _currencyRepository = currencyRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <response code="200">Account created</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <param name="accountCreateDto"></param>
        /// <returns>New Account</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<AccountReadDto> Create([FromBody] AccountCreateDto accountCreateDto)
        {
            var user = _userRepository.Get(accountCreateDto.UserId);

            if (user == null)
            {
                return BadRequest("User with that id doesn't exist.");
            }

            Account account = _mapper.Map<Account>(accountCreateDto);

            account.CreatedAt = DateTime.UtcNow;

            Currency currency = _currencyRepository.Get(account.CurrencyId);

            if (currency == null)
            {
                return BadRequest("Currency with that id doesn't exist.");
            }

            _accountRepository.Create(account);

            _logger.Log("Create Account");

            return Ok(_mapper.Map<AccountReadDto>(account));
        }

        /// <summary>
        /// Returns all accounts
        /// </summary>
        /// <response code="200">Accounts returned</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Accounts</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public ActionResult<IEnumerable<AccountReadDto>> Get()
        {
            List<Account> result = _accountRepository.Get().ToList();

            _logger.Log("Get Accounts");

            return Ok(_mapper.Map<IEnumerable<AccountReadDto>>(result));
        }

        /// <summary>
        /// Returns an account
        /// </summary>
        /// <response code="200">Account returned</response>
        /// <response code="400">Account doesn't exist</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Account</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public ActionResult<AccountReadDto> Get(int id)
        {
            if (_accountRepository.Get(id) == null)
            {
                return BadRequest("Account with that Id doesn't exist.");
            }

            Account result = _accountRepository.Get(id);

            _logger.Log("Get Account");

            return Ok(_mapper.Map<AccountReadDto>(result));
        }

        /// <summary>
        /// Get users account.
        /// </summary>
        /// <response code="200">Returns an account for user.</response>
        /// <response code="400">User doesn't exist or user doesn't have an account</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <param name="userId"></param>
        /// <returns>Account</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("users/{userId}")]
        public ActionResult<AccountReadDto> GetForUser(int userId)
        {
            User user = _userRepository.Get(userId);

            if (user == null)
            {
                return BadRequest("User with that id doesn't exist.");
            }

            Account account = _accountRepository.Get()
                .FirstOrDefault(account => account.UserId == userId);

            if (account == null)
            {
                return BadRequest("This user doesn't have an account.");
            }

            _logger.Log("Get Account For User");

            return Ok(_mapper.Map<AccountReadDto>(account));
        }

        /// <summary>
        /// Updates account.
        /// </summary>
        /// <response code="200">Account updated</response>
        /// <response code="400">Account doesn't exist or bad request</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <param name="accountCreateDto"></param>
        /// <returns>Account</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<AccountReadDto> Update(AccountUpdateDto accountCreateDto)
        {
            Account account = _accountRepository.Get(accountCreateDto.Id);

            if (account == null)
            {
                return BadRequest("Account with that id doesn't exist.");
            }

            account = _mapper.Map(accountCreateDto, account);

            _accountRepository.Update(account);

            _logger.Log("Update Account");

            return Ok(account);
        }

        /// <summary>
        /// Deletes an account.
        /// </summary>
        /// <response code="200">Account deleted</response>
        /// <response code="400">Account doesn't exist</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public ActionResult<AccountReadDto> Delete(int id)
        {
            Account account = _accountRepository.Get(id);

            if (account == null)
            {
                return BadRequest("Account with that id doesn't exit");
            }

            _accountRepository.Delete(account);

            _logger.Log("Delete Account");

            return Ok();
        }
    }
}
