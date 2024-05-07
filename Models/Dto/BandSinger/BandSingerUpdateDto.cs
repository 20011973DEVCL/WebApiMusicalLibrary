using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class BandSingerUpdateDto
    {
        [Required]
        public int IdBandSinger { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Members { get; set; }
        
        [Required]
        public string IdCountry { get; set; }
    }
}