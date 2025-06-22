using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Application.DTOs
{
    public class CreateNationalityDTO
    {
        public string CountryCode { get; set; } = "Undefined";
        public string CountryName { get; set; } = "Undefined";
    }
}
