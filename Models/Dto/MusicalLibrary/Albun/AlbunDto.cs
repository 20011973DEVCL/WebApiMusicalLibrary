using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class AlbunDto
    {
        public int IdAlbun { get; set; } 
        
        public string AlbunName { get; set; } = string.Empty;

        public int AlbunYear { get; set; }

        [Required]
        public int IdBandSinger { get; set; }

        [Required]
        public int IdGenre { get; set; }

        [MaxLength(255)]
        public string Notes { get; set; }

        [Required]
        public double Price { get; set; }
    }
}