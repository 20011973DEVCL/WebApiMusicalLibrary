using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models
{
    public class MusicGenreCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string GenreName { get; set; }
    }
}