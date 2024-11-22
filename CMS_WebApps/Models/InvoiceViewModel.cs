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