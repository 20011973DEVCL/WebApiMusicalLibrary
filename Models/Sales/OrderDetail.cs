using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models.Sales
{
    public class OrderDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdOrderDetail { get; set; }
       
        public int IdOrder { get; set; }

        [ForeignKey("IdOrder")]
        public Order Order { get; set; }

        [Required]
        public int IdAlbun { get; set; } 

        [ForeignKey("IdAlbun")]
        public Albun Albun { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

    }
}