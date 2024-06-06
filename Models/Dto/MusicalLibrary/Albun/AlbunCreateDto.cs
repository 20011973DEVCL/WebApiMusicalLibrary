using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class AlbunCreateDto
    {
        [Required]
        public string AlbunName { get; set; } = string.Empty;


        public int AlbunYear { get; set; }

        [Required]
        public int IdSinger { get; set; }

        [Required]
        public int IdMusicGenre { get; set; }

        public string? Notes { get; set; }

    }
}