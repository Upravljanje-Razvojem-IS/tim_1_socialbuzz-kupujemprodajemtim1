using AutoMapper;
using FluentValidation;
using FollowingService.Data.Interfaces;
using FollowingService.Entities;
using FollowingService.MockServices.BlackList;
using FollowingService.MockServices.UserService;
using FollowingService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.Controllers
{
    [ApiController]
    [Route("api/follows")]
    [Produces("application/json", "application/xml")]
    public class FollowController : ControllerBase
    {
        private readonly IFollowRepository followRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly IValidator<Follow> followValidator;
        private readonly ILogger logger;
        private readonly IUserMockRepository userMockService;
        private readonly IBlackListMockRepository blackListMockService;

        public FollowController(IFollowRepository followRepository, IMapper mapper, LinkGenerator linkGenerator, IValidator<Follow> followValidator, ILogger logger, IUserMockRepository userMockService, IBlackListMockRepository blackListMockService)
        {
            this.followRepository = followRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.followValidator = followValidator;
            this.logger = logger;
            this.userMockService = userMockService;
            this.blackListMockService = blackListMockService;
        }

        /// <summary>
        /// Creates new follow
        /// </summary>
        /// <param name="follow">Follow model</param>
        /// <returns>Confirmation of created follow</returns>
        /// <remarks>
        /// Example of a post request \
        /// POST /api/follows \
        /// {     \
        ///     "BuyerId": "f9168c5e-ceb2-4faa-b6bf-329bf39fa1e5",\
        ///     "ProductId" : "b6496c4a-f938-4863-b3b7-2db52e4271dc",\
        ///     "TransportTypeId": "6a411c13-a195-48f7-8dbd-67596c3974c1",\
        ///}
        /// </remarks>
        /// <response code="200">Returns created follow</response>
        /// <response code="404">User does not exist</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FollowDto> CreateFollow([FromBody] FollowCreateDto follow)
        {
            try
            {
                if (userMockService.GetAccounByUserId(follow.FollowedId) == null)
                {
                    return NotFound("User with specified id does not exist.");
                }

                if (blackListMockService.IsUserBlocked(follow.FollowerId, follow.FollowedId))
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "You cannot follow users that you have blocked or who have blocked you.");
                }

                Follow followEntity = mapper.Map<Follow>(follow);

                Follow createdFollow = followRepository.CreateFollow(followEntity);

                var valid = followValidator.Validate(createdFollow);
                if (!valid.IsValid)
                {
                    return BadRequest(valid.Errors);
                }

                followRepository.SaveChanges();

                logger.Log(LogLevel.Information, $"requestId: {Request.HttpContext.TraceIdentifier}, previousRequestId:No previous ID, Message: Follow successfully created.");

                string location = linkGenerator.GetPathByAction("GetFollow", "Follow", new { followId = createdFollow.FollowId });

                return Created(location, mapper.Map<FollowDto>(createdFollow));
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Warning, $"requestId: {Request.HttpContext.TraceIdentifier}, previousRequestId:No previous ID, Message: Follow unsuccessfully created.", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error " + e.Message);
            }
        }

        /// <summary>
        /// Returns all follows.
        /// </summary>
        /// <param name="followerId">Buyer id</param>
        /// <returns>List of follows</returns>
        /// <response code="200">Returns list of follows (orders)</response>
        /// <response code="404">There is no follow(if param specified - there is no follow with specified param)</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<FollowDto>> GetFollows(Guid followerId)
        {
            var follows = followRepository.GetFollows(followerId);

            if (follows == null || follows.Count == 0)
            {
                return NotFound("There is no follows.");
            }

            return Ok(mapper.Map<List<FollowDto>>(follows));

        }

        /// <summary>
        /// Returns one follow with specified id.
        /// </summary>
        /// <param name="followId">Follow id</param>
        /// <returns>Follow with specified id</returns>
        /// <response code="200">Returns follow with specified id</response>
        /// <response code="404">There is no follow with specified id</response>
        [HttpGet("{followId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<FollowDto> GetFollow(Guid followId)
        {
            var follow = followRepository.GetFollowById(followId);

            if (follow == null)
            {
                return NotFound("There is no follow with specified id.");
            }

            return Ok(mapper.Map<FollowDto>(follow));
        }

        /// <summary>
        /// Deletes one follow with specified id
        /// </summary>
        /// <param name="followId">Follow id</param>
        /// <response code="200">Follow successfully deleted</response>
        /// <response code="404">There is nofollow with specified id</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{followId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteFollow(Guid followId)
        {
            try
            {
                var follow = followRepository.GetFollowById(followId);

                if (follow == null)
                {
                    return NotFound("There is no follow with specified id.");
                }

                followRepository.DeleteFollow(followId);
                followRepository.SaveChanges();
                logger.Log(LogLevel.Information, $"requestId: {Request.HttpContext.TraceIdentifier}, previousRequestId:No previous ID, Message: Follow successfully deleted.");
                return Ok("Follow successfully deleted.");
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Information, $"requestId: {Request.HttpContext.TraceIdentifier}, previousRequestId:No previous ID, Message: Follow unsuccessfully deleted.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error " + e.Message);
            }
        }
    }
}
