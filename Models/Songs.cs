using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models
{
    public class Songs
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSong { get; set; } 

        [Required]
        public int IdAlbun { get; set; }

        [ForeignKey("IdAlbun")]
        public Albun Albun { get; set; }

        [Required]
        public int? Track { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}