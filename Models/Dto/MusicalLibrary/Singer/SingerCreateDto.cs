using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class SingerCreateDto
    {
        [Required]
        public string SingerName { get; set; } = string.Empty;

        [Required]
        public string Members { get; set; }
        
        [Required]
        public string IdCountry { get; set; }
        
    }
}