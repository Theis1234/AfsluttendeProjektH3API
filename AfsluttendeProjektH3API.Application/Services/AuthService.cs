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
using System.Security.Cryptography;
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
		public async Task<TokenReponseDTO?> LoginAsync(LoginUserDTO userDTO)
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
            return await CreateTokenResponse(user);
        }
        public async Task<LoginUserDTO?> GetUserByUsername(LoginUserDTO userDTO)
        {
            var user = await _userRepository.GetByUsernameAsync(userDTO.Username);
            if (user is null)
            {
                return null;
            }
			return user;
        }

        private async Task<TokenReponseDTO?> CreateTokenResponse(User user)
        {
            return new TokenReponseDTO
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        public async Task<User?> RegisterUserAsync(LoginUserDTO userDTO)
		{
			var user = await _userRepository.AddAsync(userDTO);
			return user;
		}
        public async Task<TokenReponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO refreshTokenDTO)
        {
			var user = await ValidateRefreshTokenAsync(refreshTokenDTO.UserId, refreshTokenDTO.RefreshToken);
			if (user is null)
			{
				return null;
			}
            return await CreateTokenResponse(user);
        }
        private async Task<User?> ValidateRefreshTokenAsync(int userId, string refreshToken)
		{
            var user = await _userRepository.GetByIdAsync(userId);
			if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpirationTime <= DateTime.UtcNow)
			{
				return null;
			}
			return user;
        }
        private string GenerateRefreshToken()
		{
			var randomNumber = new Byte[32];

			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
		{
			var refreshToken = GenerateRefreshToken();
			await _userRepository.GenerateAndSaveRefreshTokenAsync(user, refreshToken);
			return refreshToken;
		}
		private string CreateToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Role, user.Role)
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
