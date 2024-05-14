using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models
{
    public class Country
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(3)]
        public string IdCountry { get; set; }
        [Required]
        [MaxLength(30)]
        public string CountryName { get; set; }
    }
}