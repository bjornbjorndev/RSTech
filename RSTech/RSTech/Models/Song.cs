using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSTech.Models
{
    [Table("Song")]
    public class Song
    {
        [Key]
        public int SongId { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public string? Artist { get; set; }
        public string? Shortname { get; set; }
        public int? Bpm { get; set; }
        public int Duration { get; set; }
        public string? Genre { get; set; }
        public string? SpotifyId { get; set; }
        public string? Album { get; set; }
    }
}
