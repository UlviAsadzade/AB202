using System.ComponentModel.DataAnnotations.Schema;

namespace PustokAB202.Models
{
    public class Slider
    {
        public int Id { get; set; }

        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Desc { get; set; }
        public string? Image { get; set; }
        public int Order { get; set; }

        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
