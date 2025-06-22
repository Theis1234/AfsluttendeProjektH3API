using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.DTOs
{
    public class CreateEducationDTO
    {
        public string Degree { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public int GraduationYear { get; set; }
    }
}
