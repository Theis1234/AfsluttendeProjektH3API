using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		public static User user = new User();

		public AuthController(IAuthService authService)
		{
            _authService = authService;
		}

        [HttpPost("register")]
		public async Task<ActionResult<User>> Register(LoginUserDTO userDTO)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _authService.RegisterUserAsync(userDTO);
			if (user is null)
			{
				return BadRequest("Username already exists");
			}

			return Ok(user);
		}
		[HttpPost("refresh-token")]
		public async Task<ActionResult<TokenReponseDTO>> RefreshToken(RefreshTokenRequestDTO refreshTokenDTO)
		{
			var result = await _authService.RefreshTokensAsync(refreshTokenDTO);
			if(result is null || result.AccessToken is null || result.RefreshToken is null)
			{
				return Unauthorized("Invalid refresh token.");
			}
			return Ok(result);
		}

		[HttpPost("login")]
		public async Task<ActionResult<TokenReponseDTO>> Login(LoginUserDTO userDTO)
		{
			var response = await _authService.LoginAsync(userDTO);
			if (response is null)
			{
				return BadRequest("Invalid username or password");
			}
			return Ok(response);
		}

	}
}
