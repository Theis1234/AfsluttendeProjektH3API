using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.DTOs
{
	public class TokenReponseDTO
	{
		public required string AccessToken { get; set; }

		public required string RefreshToken { get; set; }
	}
}
