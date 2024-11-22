using Microsoft.AspNetCore.Mvc;
using CMS_WebApps.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Claims;
using System.Text;

namespace CMS_WebApps.Controllers
{
    public class HRController : Controller
    {
        // Reference claims in the ClaimsController
        private static List<ClaimViewModel> claims => ClaimsController.claims;

        // Reference lecturer management data in AccountController
        private static List<LecturerViewModel> lecturers => AccountController.LecturerManagement;

        // GET: HR/Index
        public IActionResult Index()
        {
            // Filter approved claims
            var approvedClaims = claims?.Where(c => c.Status == "Approved").ToList() ?? new List<ClaimViewModel>();

            // Pass data to the view
            ViewBag.ApprovedClaims = approvedClaims;
            ViewBag.Lecturers = lecturers;

            return View();
        }

        // POST: Generate Invoice
        [HttpPost]
        public IActionResult GenerateInvoice()
        {
            // Fetch approved claims
            var approvedClaims = claims.Where(c => c.Status == "Approved").ToList();

            if (!approvedClaims.Any())
            {
                TempData["Message"] = "No approved claims available to generate an invoice.";
                return RedirectToAction("Index");
            }

            // Group claims by lecturer
            var groupedClaims = approvedClaims
                .GroupBy(c => $"{c.FirstName} {c.LastName}")
                .Select(g => new
                {
                    LecturerName = g.Key,
                    Claims = g.ToList(),
                    TotalAmount = g.Sum(c => c.Total)
                });

            // Generate invoice content in HTML format
            var invoiceContent = new StringBuilder();
            invoiceContent.AppendLine("<!DOCTYPE html>");
            invoiceContent.AppendLine("<html lang=\"en\">");
            invoiceContent.AppendLine("<head>");
            invoiceContent.AppendLine("<meta charset=\"UTF-8\">");
            invoiceContent.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            invoiceContent.AppendLine("<title>Invoice Summary</title>");
            invoiceContent.AppendLine("<style>");
            invoiceContent.AppendLine("body { font-family: 'SF Pro', sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px; }");
            invoiceContent.AppendLine(".container { background: white; border-radius: 10px; padding: 20px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); max-width: 800px; margin: auto; }");
            invoiceContent.AppendLine("h1 { text-align: center; color: #043873; font-family: 'Bakbak One', sans-serif; }");
            invoiceContent.AppendLine("table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
            invoiceContent.AppendLine("th, td { padding: 15px; text-align: left; border: 1px solid #ddd; }");
            invoiceContent.AppendLine("th { background-color: #043873; color: white; font-family: 'Bakbak One', sans-serif; }");
            invoiceContent.AppendLine("td { font-family: 'SF Pro', sans-serif; }");
            invoiceContent.AppendLine(".footer { margin-top: 20px; text-align: center; font-size: 14px; color: #666; }");
            invoiceContent.AppendLine(".final-total { text-align: right; font-weight: bold; margin-top: 20px; font-size: 18px; color: #043873; }");
            invoiceContent.AppendLine("</style>");
            invoiceContent.AppendLine("</head>");
            invoiceContent.AppendLine("<body>");
            invoiceContent.AppendLine("<div class='container'>");
            invoiceContent.AppendLine("<h1>Invoice Summary</h1>");

            foreach (var group in groupedClaims)
            {
                // Get lecturer details
                var lecturer = lecturers.FirstOrDefault(l => l.FullName == group.LecturerName);
                var lecturerEmail = lecturer?.Email ?? "N/A";
                var lecturerPhone = lecturer?.PhoneNumber ?? "N/A";

                // Lecturer Section
                invoiceContent.AppendLine("<div class='lecturer-section'>");
                invoiceContent.AppendLine($"<h2>Lecturer: {group.LecturerName}</h2>");
                invoiceContent.AppendLine($"<p>Email: {lecturerEmail}</p>");
                invoiceContent.AppendLine($"<p>Contact: {lecturerPhone}</p>");
                invoiceContent.AppendLine("</div>");

                // Claims Table
                invoiceContent.AppendLine("<table>");
                invoiceContent.AppendLine("<thead>");
                invoiceContent.AppendLine("<tr>");
                invoiceContent.AppendLine("<th>Claim ID</th>");
                invoiceContent.AppendLine("<th>Modules</th>");
                invoiceContent.AppendLine("<th>Date</th>");
                invoiceContent.AppendLine("<th>Total</th>");
                invoiceContent.AppendLine("</tr>");
                invoiceContent.AppendLine("</thead>");
                invoiceContent.AppendLine("<tbody>");

                foreach (var claim in group.Claims)
                {
                    invoiceContent.AppendLine("<tr>");
                    invoiceContent.AppendLine($"<td>{claim.ID}</td>");
                    invoiceContent.AppendLine($"<td>{claim.Modules}</td>");
                    invoiceContent.AppendLine($"<td>{claim.Date1?.ToString("yyyy-MM-dd") ?? "N/A"}</td>");
                    invoiceContent.AppendLine($"<td>{claim.Total:C}</td>");
                    invoiceContent.AppendLine("</tr>");
                }

                invoiceContent.AppendLine("</tbody>");
                invoiceContent.AppendLine("</table>");

                // Final Total for Lecturer
                invoiceContent.AppendLine($"<div class='final-total'>Total Amount: {group.TotalAmount:C}</div>");
            }

            invoiceContent.AppendLine("<div class='footer'>");
            invoiceContent.AppendLine("<p>CMCS Ltd, Address: 12 Radar Rd, Durban North, South Africa</p>");
            invoiceContent.AppendLine("<p>Email: cmcs@contact.com | Phone: 077 764 3671</p>");
            invoiceContent.AppendLine("</div>");
            invoiceContent.AppendLine("</div>");
            invoiceContent.AppendLine("</body>");
            invoiceContent.AppendLine("</html>");

            // Generate invoice file
            var invoiceBytes = Encoding.UTF8.GetBytes(invoiceContent.ToString());
            return File(invoiceBytes, "text/html", "Invoice.html");
        }



        // POST: Update Lecturer Details
        [HttpPost]
        public IActionResult UpdateLecturer(LecturerViewModel model)
        {
            // Find the lecturer by ID
            var lecturer = lecturers.FirstOrDefault(l => l.LecturerID == model.LecturerID);
            if (lecturer != null)
            {
                // Update lecturer details
                lecturer.FullName = model.FullName;
                lecturer.Email = model.Email;
                lecturer.PhoneNumber = model.PhoneNumber;
            }

            return RedirectToAction("Index");
        }
    }
}