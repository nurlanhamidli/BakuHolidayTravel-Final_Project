using System.ComponentModel.DataAnnotations;

namespace bht_demo.Areas.AdminPanel.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
    }
}
