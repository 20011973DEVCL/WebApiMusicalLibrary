using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class SongsDto
    {
        public int IdSong { get; set; } 

        [Required]
        public int IdAlbun { get; set; }

        [Required]
        public int Track { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}