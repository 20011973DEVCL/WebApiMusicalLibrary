using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class MusicGenreDto
    {
        public int IdGenre { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string GenreName { get; set; }
    }
}