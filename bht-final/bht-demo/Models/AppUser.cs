using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace bht_demo.Models
{
    public class AppUser:IdentityUser
    {
        [StringLength(maximumLength: 20)]
        public string Name { get; set; }
        [StringLength(maximumLength: 30)]
        public string Surname { get; set; }
        [StringLength(maximumLength: 100)]
        public bool IsAdmin { get; set; }
    }
}
