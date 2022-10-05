using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bht_demo.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string HeaderLogo { get; set; }
        [StringLength(maximumLength: 150)]
        public string FooterLogo { get; set; }
        [StringLength(maximumLength: 50)]
        public string Tel1 { get; set; }
        [StringLength(maximumLength: 50)]
        public string Tel2 { get; set; }
        [StringLength(maximumLength: 50)]
        public string TelWp { get; set; }
        [StringLength(maximumLength: 100)]
        public string SkypeUrl { get; set; }
        [StringLength(maximumLength: 100)]
        public string MessengerUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string FacebookUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string InstagramUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string LinkedinUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string TwitterUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string YoutubeUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string Adress { get; set; }
        [StringLength(maximumLength: 50)]
        public string Email { get; set; }

        [NotMapped]
        public IFormFile HeaderLogoFile { get; set; }
        [NotMapped]
        public IFormFile FooterLogoFile { get; set; }
    }
}
