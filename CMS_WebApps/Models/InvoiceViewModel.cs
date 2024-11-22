namespace CMS_WebApps.Models
{
    public class InvoiceViewModel
    {
        public LecturerViewModel Lecturer { get; set; }
        public List<ClaimViewModel> Claims { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
//Title: Pro C 7 with.NET and .NET Core
//Author: Andrew Troelsen; Philip Japikse
// Date: 2017
// Code version: Version 1
//Availability: Textbook / Ebook