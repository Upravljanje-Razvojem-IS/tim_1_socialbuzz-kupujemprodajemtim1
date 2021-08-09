using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RankingService.DTOs;
using RankingService.Interfaces;
using System;

namespace RankingService.Controllers
{
    [ApiController]
    [Route("api/transport-rate")]
    [Authorize]
    public class TransportRateController : ControllerBase
    {
        private readonly ITransportRateService _service;

        public TransportRateController(ITransportRateService service)
        {
            _service = service;
        }

        /// <summary>
        /// GetAll transport rates
        /// </summary>
        /// <returns>List of transport rates</returns>
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
        /// Return transport rate by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>transport rate</returns>
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
        /// Post new transport rate
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>new transport rate</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult Post([FromBody] TransportRateCreateDTO dto)
        {
            var entity = _service.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update transport rate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>updated transport rate</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, TransportRateCreateDTO dto)
        {
            var entity = _service.Update(id, dto);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Delete transport rate by id
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
