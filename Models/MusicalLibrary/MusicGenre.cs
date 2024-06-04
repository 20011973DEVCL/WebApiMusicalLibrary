using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models
{
    public class MusicGenre
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdMusicGenre { get; set; }
        [Required]
        [MaxLength(30)]
        public string GenreName { get; set; }
    }
}