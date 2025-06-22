using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Domain.Entities
{
    public class Biography
    {
        [MaxLength(256)]
        public string? ShortBio { get; set; }
        [MaxLength(1000)]
        public string? FullBio { get; set; }
    }
}
