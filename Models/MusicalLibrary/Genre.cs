using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models
{
    public class Genre
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdGenre { get; set; }
        [Required]
        [MaxLength(30)]
        public string GenreName { get; set; }
    }
}