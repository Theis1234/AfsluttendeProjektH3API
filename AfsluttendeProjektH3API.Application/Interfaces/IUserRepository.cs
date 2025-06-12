using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
	public interface IUserRepository
	{
		Task<User?> AddAsync(UserDTO userDTO);
		Task DeleteAsync(int id);
		Task<IEnumerable<User>> GetAllAsync();
		Task GenerateAndSaveRefreshTokenAsync(User user, string refreshToken);

        Task<User?> GetByIdAsync(int id);
		Task<User?> GetByUsernameAsync(string username);
		Task UpdateAsync(User user);
		bool UserExists(int id);
	}
}
