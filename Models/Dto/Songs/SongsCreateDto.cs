using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class SongsCreateDto
    {
        [Required]
        public int IdAlbun { get; set; }

        [Required]
        public int Track { get; set; }

        [Required]
        [MaxLength(50)]
        public string SongName { get; set; } = string.Empty;

        public int? Disc { get; set; }
    }
}