using System.ComponentModel.DataAnnotations;

namespace DemoService.Models
{
    public class MyService
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}