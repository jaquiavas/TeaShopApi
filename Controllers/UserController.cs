using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeaStoreApi.Interfaces;
using TeaStoreApi.Models;

namespace TeaStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IConfiguration _configuration;
        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var isRegistered = await _userRepository.RegisterUser(user);
            if (isRegistered) { return StatusCode(StatusCodes.Status201Created); }
            return BadRequest();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var loggedInUser = await _userRepository.LoginUser(user.Email, user.Password);
            if(loggedInUser == null) 
            {
                return NotFound("User not found");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);
            
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);  
            return Ok(new { 
                token = jwt,
                token_type = "Bearer",
                user_id = user.Id,
                user_name = user.Name
            });

        }
    }
}
