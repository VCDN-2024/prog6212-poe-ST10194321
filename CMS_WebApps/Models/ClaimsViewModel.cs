using System;
using System.ComponentModel.DataAnnotations;

namespace CMS_WebApps.Models
{
    public class ClaimViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Hours must be between 1 and 100")]
        public int Hours { get; set; }

        [Required(ErrorMessage = "Module is required")]
        [StringLength(100, ErrorMessage = "Module name cannot exceed 100 characters")]
        public string Modules { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; } = "Pending";

        public string? Document { get; set; }

        public DateTime? Date1 { get; set; }

    }
}



//Title: Pro C 7 with.NET and .NET Core
//Author: Andrew Troelsen; Philip Japikse
// Date: 2017
// Code version: Version 1
//Availability: Textbook / Ebook