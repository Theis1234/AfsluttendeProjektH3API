using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Domain.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ContactEmail { get; set; }
    }
}
