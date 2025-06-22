using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Domain.Entities
{
    public class Address
    {
        [MaxLength(100)]
        public string? Street { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
        [MaxLength(50)]
        public string? State { get; set; }
        [MaxLength(20)]
        public string? PostalCode { get; set; }
        [MaxLength(40)]
        public string? Country { get; set; }
    }
}
