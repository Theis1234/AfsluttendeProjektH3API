using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Domain.Entities
{
    public class Edition
    {
        public int Id { get; set; }
        public string Format { get; set; } = "";
        public DateOnly ReleaseDate { get; set; }
        public int BookId { get; set; }
        [JsonIgnore]
        public Book? Book { get; set; }
    }
}
