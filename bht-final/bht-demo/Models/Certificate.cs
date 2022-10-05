using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace bht_demo.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
