using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models.Login
{
    public class MenuOptionsUpdateDto
    {
        [Required]
        public string IdOption { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }=string.Empty;

        [Required]
        public int OptionOrder { get; set; }
    }
}