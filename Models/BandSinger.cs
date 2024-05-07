using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models
{
    public class BandSinger
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdBandSinger { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Members { get; set; }
        
        [Required]
        public string IdCountry { get; set; }

        [ForeignKey("IdCountry")]
        public Country Country { get; set; }

        public DateTime StarDate { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        
    }
}