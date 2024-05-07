using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMusicalLibrary.Models
{
    public class Country
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string IdCountry { get; set; }
        [Required]
        [MaxLength(10)]
        public string CountryName { get; set; }
    }
}