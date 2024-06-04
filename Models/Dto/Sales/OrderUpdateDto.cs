using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models.Sales
{
    public class OrderUpdateDto
    {
        public int IdOrder { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime DateOrder { get; set; }

        [Required]
        public double Total { get; set; }
    }
}