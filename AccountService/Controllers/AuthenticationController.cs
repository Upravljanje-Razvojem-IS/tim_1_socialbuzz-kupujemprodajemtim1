using AccountService.Data.Interfaces;
using AccountService.Dtos;
using AccountService.Logger;
using AccountService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly FakeLogger _logger;

        public AuthenticationController(IConfiguration configuration, IUserRepository userRepository, FakeLogger logger)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="dto"></param>
        /// <response code="200">Logged in</response>
        /// <response code="400">Credentials incorrect</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Token</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<string> Login(LoginDto dto)
        {
            User user = _userRepository.Get(dto.Email);

            if (user == null || user.Password != dto.Password)
            {
                return BadRequest("Email or password incorrect");
            }

            string token = GenerateJwtToken(user);

            _logger.Log("Login");

            return Ok(token);
        }

        private string GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString())
            };

            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            JwtHeader jwtHeader = new JwtHeader(credentials);

            JwtSecurityToken token = new JwtSecurityToken(jwtHeader,
                new JwtPayload(claims));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
