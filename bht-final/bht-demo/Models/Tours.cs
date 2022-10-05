using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace bht_demo.Models
{
    public class Tours
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(maximumLength: 100, ErrorMessage = "Uzunluq 100-dən yuxarı ola bilməz")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Boş ola bilməz")]
        public double Price { get; set; }
        public string Content { get; set; }
        [Required(ErrorMessage = "Olke secin")]
        public string Country { get; set; }
        public string TourAbout { get; set; }
        [Required]
        public string TourDate { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string YoutubeLink { get; set; }
        public string TourMap { get; set; }
        public string HeroImgUrl { get; set; }
        public string ImgUrl { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Şəkil Yükləyin")]
        public IFormFile HeroImgFile { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Şəkil Yükləyin")]
        public IFormFile ImgFile { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
