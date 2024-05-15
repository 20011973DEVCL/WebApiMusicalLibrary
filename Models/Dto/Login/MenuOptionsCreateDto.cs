using System.ComponentModel.DataAnnotations;

namespace WebApiMusicalLibrary.Models.Login
{
    public class MenuOptionsCreateDto
    {
        [Required]
        public string IdOption { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }=string.Empty;
    }
}