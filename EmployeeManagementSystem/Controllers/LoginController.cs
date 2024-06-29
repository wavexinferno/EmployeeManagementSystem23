using EmployeeManagementSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EmployeeManagementSystem.DataAccess;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserRepository _userRepository;
        public LoginController(IConfiguration configuration, UserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        private string GenerateToken(User user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var cred = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: cred
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User user)
        {
            IActionResult res = Unauthorized();
            var user_ = _userRepository.ValidateUser(user);
            if (user_ != null)
            {
                var token = GenerateToken(user_);
                res = Ok(new { token = token });
            }
            return res;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            try
            {
                bool res = _userRepository.InsertUser(user);
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the user", error = ex.Message });
            }
        }
    }
}
