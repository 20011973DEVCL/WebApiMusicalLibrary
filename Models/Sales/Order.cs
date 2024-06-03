using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Models.Sales
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }

        [Required]
        public string Username { get; set; }

        [ForeignKey("Username")]
        public UserModel UserModel { get; set; }

        public DateTime DateOrder { get; set; }
        public double Total { get; set; }
    }
}