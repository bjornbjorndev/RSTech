using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSTech.Models
{
    [Table("Artist")]
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
