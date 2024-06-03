using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMusicalLibrary.Models.Sales
{
    public class OrderDetail
    {
        [Key]
        public int IdOrderDetail { get; set; }
       
        public int IdOrder { get; set; }

        [ForeignKey("IdOrder")]
        public Order Order { get; set; }

        [Required]
        public int IdAlbun { get; set; } 

        [ForeignKey("IdAlbun")]
        public Albun Albun { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

    }
}