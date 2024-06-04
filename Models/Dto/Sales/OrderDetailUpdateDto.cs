using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models.Dto.Sales
{
    public class OrderDetailUpdateDto
    {
        public int IdOrderDetail { get; set; }
       
        public int IdOrder { get; set; }

        [Required]
        public int IdAlbun { get; set; } 

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}