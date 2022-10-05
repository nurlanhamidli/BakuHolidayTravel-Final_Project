using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace bht_demo.Models
{
    public class Blogs
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(maximumLength: 35, ErrorMessage = "Uzunluq 35-dən yuxarı ola bilməz")]
        public string Title { get; set; }
        [StringLength(maximumLength: 450, ErrorMessage = "Uzunluq 350-dən yuxarı ola bilməz")]
        public string ShortText { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string ImgUrl { get; set; }
        public string DetailImgUrl { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Şəkil Yükləyin")]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Şəkil Yükləyin")]
        public IFormFile DetailImageFile { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
