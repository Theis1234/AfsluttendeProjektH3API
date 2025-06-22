using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Domain.Entities
{
    public class Nationality
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = "Undefined";
        public string CountryCode { get; set; } = "Undefined";
    }
}
