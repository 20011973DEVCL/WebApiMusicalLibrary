using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models.Sales
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdOrder { get; set; }

        [Required]
        public string Username { get; set; }

        [ForeignKey("Username")]
        public UserModel UserModel { get; set; }

        [Required]
        public DateTime DateOrder { get; set; }

        [Required]
        public decimal Total { get; set; }
    }
}