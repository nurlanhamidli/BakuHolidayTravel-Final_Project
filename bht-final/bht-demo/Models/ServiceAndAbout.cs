using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bht_demo.Models
{
    public class ServiceAndAbout
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Boş ola bilməz")]
        [StringLength(maximumLength: 35, ErrorMessage = "Uzunluq 35-dən yuxarı ola bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(maximumLength: 150, ErrorMessage = "Uzunluq 150-dən yuxarı ola bilməz")]
        public string Title { get; set; }
        [StringLength(maximumLength: 200, ErrorMessage = "Uzunluq 200-dən yuxarı ola bilməz")]
        public string Desc { get; set; }
        public string Content { get; set; }
        [Required(ErrorMessage = "Birini seçin")]
        public int Type { get; set; }
        public string HeroImgUrl { get; set; }
        public string Icon { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Şəkil Yükləyin")]
        public IFormFile HeroImgFile { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Şəkil Yükləyin")]
        public IFormFile IconFile { get; set; }
        public bool IsDeleted { get; set; }
    }
}
