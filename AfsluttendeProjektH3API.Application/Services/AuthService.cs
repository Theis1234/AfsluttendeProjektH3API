using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _config;
		public AuthService(IUserRepository userRepository, IConfiguration config)
		{
			_userRepository = userRepository;
			_config = config;
		}
		public async Task<string?> LoginAsync(UserDTO userDTO)
		{
			var user = await _userRepository.GetByUsernameAsync(userDTO.Username);
			if (user is null)
			{
				return null;
			}
			if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, userDTO.Password) == PasswordVerificationResult.Failed)
			{
				return null;
			}
			return CreateToken(user);
		}

		public async Task<User?> RegisterUserAsync(UserDTO userDTO)
		{
			var user = await _userRepository.AddAsync(userDTO);
			return user;
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
