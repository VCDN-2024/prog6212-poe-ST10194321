using Microsoft.AspNetCore.Mvc;
using CMS_WebApps.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using CMS_WebApps.Controllers;


namespace CMS_WebApps.Controllers
{
    public class SubmissionController : Controller
    {
        // Referencesthe ClaimController's claims list
        private static List<ClaimViewModel> claims => ClaimsController.claims;

        // Displays the list of claims on the submissions page (only for Program Coordinators and Academic Managers)
        public IActionResult Index()
        {
            // Gets the user role from the session 
            string userRole = HttpContext.Session.GetString("Role");

            // Checks if the user is a Program Coordinator or Academic Manager
            if (userRole == "ProgramCoordinator" || userRole == "AcademicManager")
            {

                return View(claims);
            }
            else
            {

                return Unauthorized();
            }
        }


        [HttpPost]
        public IActionResult UpdateClaimStatus(int id, string status)
        {
            // Find the claim by its unique ID
            var claim = claims.FirstOrDefault(c => c.ID == id);
            if (claim != null)
            {
                // Updates the status of the claim (Approved or Rejected)
                claim.Status = status;
                Console.WriteLine($"Claim with ID {id} has been updated to status: {status}");
            }
            else
            {
                // If there is no matching claim it returns an error message
                Console.WriteLine($"No claim found with ID {id}");
            }

            // After updating the claim status it refreshes the submissions page to view status
            return RedirectToAction("Index");
        }
    }
}
//Title: Pro C 7 with.NET and .NET Core
//Author: Andrew Troelsen; Philip Japikse
// Date: 2017
// Code version: Version 1
//Availability: Textbook / Ebook

//Code from: https://stackoverflow.com/questions/54100268/mvc-approve-or-unapprove-button-clarification