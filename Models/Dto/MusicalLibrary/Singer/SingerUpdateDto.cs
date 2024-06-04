using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class SingerUpdateDto
    {
        [Required]
        public int IdBandSinger { get; set; }

        [Required]
        public string SingerName { get; set; } = string.Empty;

        [Required]
        public string Members { get; set; }
        
        [Required]
        public string IdCountry { get; set; }
    }
}