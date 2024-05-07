using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMusicalLibrary.Models
{
    public class Albun
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdAlbun { get; set; } 
        
        public string Name { get; set; }

        public int Year { get; set; }

        [Required]
        public int IdBandSinger { get; set; }

        [ForeignKey("IdBandSinger")]
        public BandSinger BandSinger { get; set; }

        [Required]
        public int IdGenre { get; set; }

        public Genre Genre { get; set; }
    }
}