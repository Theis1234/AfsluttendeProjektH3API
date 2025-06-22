using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Domain.Entities
{
    public class Education
    {
        public int Id { get; set; }
        public string Degree { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public int GraduationYear { get; set; }
    }
}
