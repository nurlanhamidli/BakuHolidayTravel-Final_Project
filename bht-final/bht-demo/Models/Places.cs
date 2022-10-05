using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace bht_demo.Models
{
    public class Places
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
