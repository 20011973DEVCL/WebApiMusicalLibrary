using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class BandSingerDto
    {
        public int IdBandSinger { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Members { get; set; }
        
        [Required]
        public string IdCountry { get; set; }

        
    }
}