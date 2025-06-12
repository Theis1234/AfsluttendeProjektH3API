using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
	public interface IAuthService
	{
		Task<User?> RegisterUserAsync(UserDTO userDTO);
		Task<TokenReponseDTO?> LoginAsync(UserDTO userDTO);
        Task<TokenReponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO refreshTokenDTO);
    }
}
