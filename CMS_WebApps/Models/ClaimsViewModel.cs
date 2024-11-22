using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CMS_WebApps.Models
{
    //Model to store Claim attributes
    public class ClaimViewModel
    {
        public int ID { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public int Hours { get; set; }


        public string Modules { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; } = "Pending";


        public string? Document { get; set; }

        public DateTime? Date1 { get; set; }
        public DateTime? Date2 { get; set; }


    }
}
//Title: Pro C 7 with.NET and .NET Core
//Author: Andrew Troelsen; Philip Japikse
// Date: 2017
// Code version: Version 1
//Availability: Textbook / Ebook