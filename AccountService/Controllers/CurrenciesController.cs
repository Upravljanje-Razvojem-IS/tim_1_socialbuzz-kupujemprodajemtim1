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

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrenciesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly FakeLogger _logger;

        public CurrenciesController(IMapper mapper, ICurrencyRepository currencyRepository, FakeLogger logger)
        {
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _logger = logger;
        }

        /// <summary>
        /// Creates a Currency
        /// </summary>
        /// <param name="currencyCreateDto"></param>
        /// <response code="200">Currency created</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Currency</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<CurrencyReadDto> Create(CurrencyCreateDto currencyCreateDto)
        {
            Currency currency = _mapper.Map<Currency>(currencyCreateDto);

            currency.CreatedAt = DateTime.UtcNow;

            _currencyRepository.Create(currency);

            _logger.Log("Create Currency");

            return Ok(currency);
        }

        /// <summary>
        /// Gets all Currencies
        /// </summary>
        /// <response code="200">Currencies returned</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Currencies</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public ActionResult<IEnumerable<CurrencyReadDto>> Get()
        {
            IEnumerable<Currency> result = _currencyRepository.Get();

            _logger.Log("Get Currencies");

            return Ok(_mapper.Map<IEnumerable<CurrencyReadDto>>(result));
        }

        /// <summary>
        /// Gets a Currency
        /// </summary>
        /// <response code="200">Currency returned</response>
        /// <response code="400">Currency doesn't exist</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <param name="id"></param>
        /// <returns>Currency</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public ActionResult<CurrencyReadDto> Get(int id)
        {
            if (_currencyRepository.Get(id) == null)
            {
                return BadRequest("Account with that Id doesn't exist.");
            }

            Currency result = _currencyRepository.Get(id);

            _logger.Log("Get Currency");

            return Ok(_mapper.Map<CurrencyReadDto>(result));
        }

        /// <summary>
        /// Updates a Currency
        /// </summary>
        /// <param name="currencyUpdateDto"></param>
        /// <response code="200">Currency updated</response>
        /// <response code="400">Currency doesn't exist or bad request</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Currency</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<CurrencyReadDto> Update(CurrencyUpdateDto currencyUpdateDto)
        {
            Currency currency = _currencyRepository.Get(currencyUpdateDto.Id);

            if (currency == null)
            {
                return BadRequest("Currency with that id doesn't exist.");
            }

            currency = _mapper.Map(currencyUpdateDto, currency);

            _currencyRepository.Update(currency);

            _logger.Log("Update Currency");

            return Ok(currency);
        }

        /// <summary>
        /// Deletes a Currency
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Currency deleted</response>
        /// <response code="400">Currency doesn't exist</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public ActionResult<CurrencyReadDto> Delete(int id)
        {
            Currency currency = _currencyRepository.Get(id);

            if (currency == null)
            {
                return BadRequest("Currency with that id doesn't exit");
            }

            _currencyRepository.Delete(currency);

            _logger.Log("Delete Currency");

            return Ok();
        }
    }
}
