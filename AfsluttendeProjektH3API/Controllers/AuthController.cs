using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using AfsluttendeProjektH3API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserProfileDTO>> GetCurrentUser()
        {
            var usernameClaim = User.FindFirst(ClaimTypes.Name);
            if (usernameClaim == null) return Unauthorized();

            string username = usernameClaim.Value;
            var user = await _authService.GetUserByUsername(username);
            if (user == null) return NotFound();

            var profileDto = new UserProfileDTO
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                FirstName = user.UserProfile.FirstName,
                LastName = user.UserProfile.LastName,
				Age = user.UserProfile.Age,
                PictureUrl = user.UserProfile.PictureUrl,
                Bio = user.UserProfile.Bio,
            };

            return Ok(profileDto);
        }
        [HttpPut("me")]
        public async Task<ActionResult<UserProfileDTO>> UpdateCurrentUser(UserProfileDTO updatedProfile)
        {
            var usernameClaim = User.FindFirst(ClaimTypes.Name);
            if (usernameClaim == null) return Unauthorized();

            string username = usernameClaim.Value;
            var user = await _authService.GetUserByUsername(username);
            if (user == null) return NotFound();

            user.UserProfile.FirstName = updatedProfile.FirstName;
            user.UserProfile.LastName = updatedProfile.LastName;
            user.UserProfile.Age = updatedProfile.Age;
            user.UserProfile.PictureUrl = updatedProfile.PictureUrl;
            user.UserProfile.Bio = updatedProfile.Bio;

            await _authService.UpdateUserAsync(user); 

            return Ok(updatedProfile);
        }
        [HttpPost("register")]
		public async Task<ActionResult<User>> Register(UserDTO userDTO)
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
		public async Task<ActionResult<TokenReponseDTO>> Login(UserDTO userDTO)
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
