using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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