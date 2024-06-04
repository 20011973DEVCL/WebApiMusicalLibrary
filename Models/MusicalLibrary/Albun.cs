using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models
{
    public class Albun
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdAlbun { get; set; } 
        
        [Required]
        public string AlbunName { get; set; } = string.Empty;

        public int? AlbunYear { get; set; }

        [Required]
        public int IdSinger { get; set; }

        [ForeignKey("IdSinger")]
        public Singer Singer { get; set; }

        [Required]
        public int? IdGenre { get; set; }

        [ForeignKey("IdMusicGenre")]
        public MusicGenre? MusicGenre { get; set; }

        public DateTime PublishDate { get; set; }

        [MaxLength(255)]
        public string? Notes { get; set; }

        [Required]
        public double Price { get; set; }
    }
}