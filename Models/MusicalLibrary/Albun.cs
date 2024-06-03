using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models
{
    public class Albun
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdAlbun { get; set; } 
        
        public string AlbunName { get; set; } = string.Empty;

        public int? AlbunYear { get; set; }

        [Required]
        public int IdBandSinger { get; set; }

        [ForeignKey("IdBandSinger")]
        public BandSinger BandSinger { get; set; }

        [Required]
        public int? IdGenre { get; set; }

        [ForeignKey("IdGenre")]
        public Genre? Genre { get; set; }

        public byte[]? Cover { get; set; }

        [MaxLength(255)]
        public string? Notes { get; set; }

        public double Price { get; set; }
    }
}