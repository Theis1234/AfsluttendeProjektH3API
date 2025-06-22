using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.DTOs
{
    public class CreatePublisherDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? ContactEmail { get; set; }
    }
}
