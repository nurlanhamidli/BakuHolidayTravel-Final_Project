using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bht_demo.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        public int Column { get; set; }
        public string ImgUrl { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Şəkil Yükləyin")]
        public IFormFile ImageFile { get; set; }
        public bool IsDeleted { get; set; }
    }
}
