using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace AfsluttendeProjektH3API.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;
		public UserRepository(AppDbContext context) => _context = context;

		public async Task<User?> GetByIdAsync(int id) =>
			await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

		public async Task<User?> GetByUsernameAsync(string username) =>
			await _context.Users.FirstOrDefaultAsync(p => p.Username == username);

		public async Task<IEnumerable<User>> GetAllAsync() =>
			await _context.Users.ToListAsync();

		public async Task GenerateAndSaveRefreshTokenAsync(User user, string refreshToken)
		{
			user.RefreshToken = refreshToken;
			user.RefreshTokenExpirationTime = DateTime.UtcNow.AddDays(7);
			await _context.SaveChangesAsync();
        }

		public async Task<User?> AddAsync(LoginUserDTO userDTO)
		{
			if (await _context.Users.AnyAsync(p => p.Username == userDTO.Username))
			{
				return null;
			}
			var user = new User();
			var hashedPassword = new PasswordHasher<User>().HashPassword(user, userDTO.Password);

			user.Username = userDTO.Username;
			user.PasswordHash = hashedPassword;

			_context.Add(user);
			await _context.SaveChangesAsync();

			return user;
		}

		public async Task UpdateAsync(User user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _context.Users.FindAsync(id);
			if (entity != null)
			{
				_context.Users.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}
		public bool UserExists(int id)
		{
			return _context.Users.Any(c => c.Id == id);
		}
	}
}
