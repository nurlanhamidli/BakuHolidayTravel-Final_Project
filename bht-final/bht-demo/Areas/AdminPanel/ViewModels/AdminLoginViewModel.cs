using System.ComponentModel.DataAnnotations;

namespace bht_demo.Areas.AdminPanel.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string Username { get; set; }
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
