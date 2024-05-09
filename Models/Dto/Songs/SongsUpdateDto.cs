using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class SongsUpdateDto
    {
        public int IdSong { get; set; } 

        [Required]
        public int IdAlbun { get; set; }

        [Required]
        public int Track { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? Disc { get; set; }
    }
}