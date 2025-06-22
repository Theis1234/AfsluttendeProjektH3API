using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Domain.Entities
{
    public class UserProfile
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(500)]
        public string? PictureUrl { get; set; }
        [MaxLength(1000)]
        public string? Bio { get; set; }
    }

}
