﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RankingService.DTOs;
using RankingService.Interfaces;
using System;

namespace RankingService.Controllers
{
    [ApiController]
    [Route("api/user-rate")]
    [Authorize]
    public class UserRateController : ControllerBase
    {
        private readonly IUserRateService _service;

        public UserRateController(IUserRateService service)
        {
            _service = service;
        }

        /// <summary>
        /// GetAll User rates
        /// </summary>
        /// <returns>List of user rates</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetAll()
        {
            var entities = _service.Get();

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Return user rate by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>user rate</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var entity = _service.Get(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Post new user rate
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>new users rate</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult Post([FromBody] UserRateCreateDto dto)
        {
            var entity = _service.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update user rate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>updated user rate</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, UserRateCreateDto dto)
        {
            var entity = _service.Update(id, dto);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Delete user rate by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _service.Delete(id);

            return NoContent();
        }
    }
}
