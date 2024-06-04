using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class AlbunDto
    {
        public int IdAlbun { get; set; } 
        
        public string AlbunName { get; set; } = string.Empty;

        public int AlbunYear { get; set; }

        [Required]
        public int IdSinger { get; set; }

        [Required]
        public int IdMusicGenre { get; set; }

        [MaxLength(255)]
        public string Notes { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }
    }
}