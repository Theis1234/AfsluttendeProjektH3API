using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AfsluttendeProjektH3API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _config;
		public static User user = new User();

		public AuthController(IConfiguration config)
		{
			_config = config;
		}

		[HttpPost("register")]
		public ActionResult<User> Register(UserDTO userDTO)
		{
			var hashedPassword = new PasswordHasher<User>().HashPassword(user, userDTO.Password);

			user.Username = userDTO.Username;
			user.PasswordHash = hashedPassword;

			return Ok(user);
		}
		[HttpPost("login")]
		public ActionResult<string> Login(UserDTO userDTO)
		{
			if(user.Username != userDTO.Username)
			{
				return BadRequest("User not found");
			}
			if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, userDTO.Password) == PasswordVerificationResult.Failed)
			{
				return BadRequest("Wrong password");
			}
			string token = CreateToken(user);

			return Ok(token);
		}

		private string CreateToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JwtConfig:Key")!));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

			var tokenDescriptor = new JwtSecurityToken(
				issuer: _config.GetValue<string>("JwtConfig:Issuer"), 
				audience: _config.GetValue<string>("JwtConfig:Audience"),
				claims: claims, 
				expires: DateTime.UtcNow.AddMinutes(_config.GetValue<int>("JwtConfig:TokenValidityMins")), 
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
		}

	}
}
