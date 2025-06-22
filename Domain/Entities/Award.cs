using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AfsluttendeProjektH3API.Domain.Entities
{
    public class Award
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateOnly DateReceived { get; set; }
        public string? Description { get; set; }
        public int ArtistId { get; set; }
        [JsonIgnore]
        public Artist? Artist { get; set; }
    }
}
