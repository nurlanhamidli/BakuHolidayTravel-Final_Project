using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace bht_demo.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(maximumLength: 50, ErrorMessage = "Uzunluq 50-dən yuxarı ola bilməz")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(maximumLength: 50, ErrorMessage = "Uzunluq 50-dən yuxarı ola bilməz")]
        public string City { get; set; }
        [Required(ErrorMessage = "Boş ola bilməz")]
        [StringLength(maximumLength: 50, ErrorMessage = "Uzunluq 50-dən yuxarı ola bilməz")]
        public string Department { get; set; }
        public string Content { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
