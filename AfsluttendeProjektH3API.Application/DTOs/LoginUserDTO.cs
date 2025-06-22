using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.DTOs
{
	public class LoginUserDTO
	{
        [Required]
        [MinLength(5, ErrorMessage = "Username must be at least 5 characters long.")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

	}
}
