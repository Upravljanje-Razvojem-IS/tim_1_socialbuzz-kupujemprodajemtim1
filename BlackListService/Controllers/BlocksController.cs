using AutoMapper;
using BlackListService.Attributes;
using BlackListService.Dtos;
using BlackListService.Entities;
using BlackListService.Logger;
using BlackListService.Repositories;
using BlackListService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BlackListService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorization]
    public class BlocksController : ControllerBase
    {
        private readonly FakeLogger _logger;
        private readonly IMapper _mapper;
        private readonly IBlockRepository _blockRepository;
        private readonly IUserService _userService;

        public BlocksController(FakeLogger logger, IMapper mapper, IBlockRepository blockRepository, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _blockRepository = blockRepository;
            _userService = userService;
        }

        /// <summary>
        /// Creates a Block
        /// </summary>
        /// <param name="blockCreateDto"></param>
        /// <response code="200">Block created</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Block</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<BlockReadDto> Create(BlockCreateDto blockCreateDto)
        {
            User blockerUser = _userService.Get(blockCreateDto.BlockerId);
            if (blockerUser == null)
            {
                return BadRequest("Blocker user doesn't exist");
            }

            User blockedUser = _userService.Get(blockCreateDto.BlockedId);
            if (blockedUser == null)
            {
                return BadRequest("Blocked user doesn't exist");
            }

            if (blockCreateDto.BlockerId == blockCreateDto.BlockedId)
            {
                return BadRequest("BlockedId and BlockerIds cannot be the same");
            }

            Block block = _blockRepository.Get(blockCreateDto.BlockerId, blockCreateDto.BlockedId);
            if (block != null)
            {
                return BadRequest("This block already exists");
            }

            block = _mapper.Map<Block>(blockCreateDto);
            block.CreateDateTime = DateTime.UtcNow;
            _blockRepository.Create(block);
            _logger.Log("Create a Block");

            return Ok(block);
        }

        /// <summary>
        /// Gets all Blocks
        /// </summary>
        /// <response code="200">Blocks returned</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Blocks</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public ActionResult<IEnumerable<BlockReadDto>> Get()
        {
            IEnumerable<Block> result = _blockRepository.Get();
            _logger.Log("Get all Blocks");

            return Ok(_mapper.Map<IEnumerable<BlockReadDto>>(result));
        }

        /// <summary>
        /// Gets a Block with given Id.
        /// </summary>
        /// <response code="200">Block returned</response>
        /// <response code="400">Block doesn't exist</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <param name="id"></param>
        /// <returns>Block</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public ActionResult<BlockReadDto> Get(int id)
        {
            if (_blockRepository.Get(id) == null)
            {
                return BadRequest("Block doesn't exist");
            }

            Block result = _blockRepository.Get(id);
            _logger.Log("Get a Block");

            return Ok(_mapper.Map<BlockReadDto>(result));
        }

        /// <summary>
        /// Deletes a Block
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Block deleted</response>
        /// <response code="400">Block doesn't exist</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Block block = _blockRepository.Get(id);
            if (block == null)
            {
                return BadRequest("Block doesn't exist");
            }

            _blockRepository.Delete(block);
            _logger.Log("Delete a Block");

            return Ok();
        }
    }
}
