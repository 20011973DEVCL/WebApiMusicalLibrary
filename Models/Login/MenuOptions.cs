using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMusicalLibrary.Models.Login
{
    public class MenuOptions
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(30)]
        public string IdOption { get; set; }

        [Required]
        [MaxLength(70)]
        public string Description { get; set; }=string.Empty;

        [Required]
        public int OptionOrder { get; set; }
    }
}