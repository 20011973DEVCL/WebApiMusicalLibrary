using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class BandSingerCreateDto
    {
        [Required]
        public string BandSingerName { get; set; } = string.Empty;

        [Required]
        public string Members { get; set; }
        
        [Required]
        public string IdCountry { get; set; }
        
    }
}