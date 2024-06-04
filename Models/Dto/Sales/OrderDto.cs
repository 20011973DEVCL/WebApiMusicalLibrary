using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models.Sales
{
    public class OrderDto
    {
        public int IdOrder { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime DateOrder { get; set; }

        [Required]
        public decimal Total { get; set; }
    }
}