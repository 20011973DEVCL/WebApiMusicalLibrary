using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class AlbunUpdateDto
    {
        [Required]
        public int IdAlbun { get; set; } 
        
        [Required]
        public string AlbunName { get; set; } = string.Empty;

        public int? AlbunYear { get; set; }

        [Required]
        public int IdSinger { get; set; }

        [Required]
        public int IdMusicGenre { get; set; }

        public string? Notes { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }
    }
}