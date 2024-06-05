using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models.Sales
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int IdAlbun { get; set; }
        
        [ForeignKey("IdAlbun")]
        public Albun Album { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}