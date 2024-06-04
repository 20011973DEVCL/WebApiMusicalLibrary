using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class AlbunCreateDto
    {
        [Required]
        public string AlbunName { get; set; } = string.Empty;


        public int? AlbunYear { get; set; }

        [Required]
        public int IdBandSinger { get; set; }

        [Required]
        public int IdGenre { get; set; }

        public string? Notes { get; set; }
        
        [Required]
        public double Price { get; set; }

    }
}