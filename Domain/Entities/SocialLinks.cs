using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Domain.Entities
{
    public class SocialLinks
    {
        [MaxLength(256)]
        public string? Website { get; set; }
        [MaxLength(256)]
        public string? Instagram { get; set; }
        [MaxLength(256)]
        public string? Twitter { get; set; }
    }
}
