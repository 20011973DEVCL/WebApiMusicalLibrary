using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class SingerDto
    {
        public int IdSinger { get; set; }
        public string SingerName { get; set; } = string.Empty;
        public string Members { get; set; }
        
        [Required]
        public string IdCountry { get; set; }

        
    }
}