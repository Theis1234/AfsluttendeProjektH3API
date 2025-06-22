using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.DTOs
{
    public class UpdateNationalityDTO
    {
        public string CountryCode { get; set; } = "Undefined";
        public string CountryName { get; set; } = "Undefined";
    }
}
