using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class CountryCreateDto
    {
        [Required]
        [MaxLength(3)]
        public string IdCountry { get; set; }
        [Required]
        [MaxLength(30)]
        public string CountryName { get; set; }
    }
}