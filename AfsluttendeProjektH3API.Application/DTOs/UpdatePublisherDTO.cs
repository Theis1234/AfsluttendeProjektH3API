using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.DTOs
{
    public class UpdatePublisherDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? ContactEmail { get; set; }
    }
}
