using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.DTOs
{
	public class LoginResponse
	{
		public string? Username { get; set; }

		public string? Role { get; set; }
		public string? AccessToken { get; set; }
		public int TokenExpiresIn { get; set; }
	}
}
