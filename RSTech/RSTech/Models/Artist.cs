using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSTech.Models
{
    [Table("Artist")]
    public class Artist : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
