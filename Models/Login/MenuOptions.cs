using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models.Login
{
    public class MenuOptions
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(3)]
        public string IdOption { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }=string.Empty;
    }
}