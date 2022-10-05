﻿using System.ComponentModel.DataAnnotations;

namespace bht_demo.Areas.AdminPanel.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
