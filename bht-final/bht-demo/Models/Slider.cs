using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bht_demo.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Boş ola bilməz")]
        [StringLength(maximumLength:25, ErrorMessage ="Uzunluq 25-dən yuxarı ola bilməz")]
        public string Title { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "Uzunluq 50-dən yuxarı ola bilməz")]
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImgUrl { get; set; }
        [Required(ErrorMessage ="Birini seçin")]
        public int Type { get; set; }
        [NotMapped]
        [Required(ErrorMessage ="Şəkil Yükləyin")]
        public IFormFile ImageFile { get; set; }
    }
}
