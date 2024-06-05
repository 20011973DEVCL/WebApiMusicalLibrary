using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models.Sales
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; }
        
        [ForeignKey("Username")]
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}