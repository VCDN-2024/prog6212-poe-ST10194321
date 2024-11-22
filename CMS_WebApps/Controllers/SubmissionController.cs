using CMS_WebApps.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CMS_WebApps.Controllers
{
    public class SubmissionController : Controller
    {
        // Reference the ClaimController's claims list
        private static List<ClaimViewModel> claims => ClaimsController.claims;

        public IActionResult Index(string statusFilter = "All", string searchTerm = "")
        {
            // Get the user role from the session
            string userRole = HttpContext.Session.GetString("Role");

            if (userRole != "ProgramCoordinator" && userRole != "AcademicManager")
            {
                return Unauthorized();
            }

            // Create a list of statuses for the dropdown
            var statuses = new List<string> { "All", "Pending", "Approved", "Rejected" };

            // Filter claims based on the status and search term
            var filteredClaims = claims;

            if (statusFilter != "All")
            {
                filteredClaims = filteredClaims.Where(c => c.Status == statusFilter).ToList();
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredClaims = filteredClaims.Where(c =>
                    (c.FirstName + " " + c.LastName).Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                    c.Modules.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                    c.ID.ToString().Contains(searchTerm)
                ).ToList();
            }

            // Pass filtering data to the view via ViewBag
            ViewBag.Statuses = statuses;
            ViewBag.SelectedStatus = statusFilter;
            ViewBag.SearchTerm = searchTerm;

            return View(filteredClaims);
        }

        [HttpPost]
        public IActionResult UpdateClaimStatus(int id, string status)
        {
            // Find the claim by its unique ID
            var claim = claims.FirstOrDefault(c => c.ID == id);
            if (claim != null)
            {
                // Update the status of the claim
                claim.Status = status;
            }

            // Refresh the submissions page to view the updated status
            return RedirectToAction("Index");
        }
    }
}
//Title: Pro C 7 with.NET and .NET Core
//Author: Andrew Troelsen; Philip Japikse
// Date: 2017
// Code version: Version 1
//Availability: Textbook / Ebook