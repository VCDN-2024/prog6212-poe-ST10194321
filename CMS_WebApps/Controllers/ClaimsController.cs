using CMS_WebApps.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace CMS_WebApps.Controllers
{
    public class ClaimsController : Controller
    {
        // A list to store claims
        public static List<ClaimViewModel> claims { get; } = new List<ClaimViewModel>();

        // Displays the list of claims
        public IActionResult Index()
        {
            return View(claims);
        }

        // GET: Create a new claim
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create a new claim and handle file upload
        [HttpPost]
        public IActionResult Create(ClaimViewModel model, IFormFile document)
        {
            // Sets predefined  hourly rate and calculates the total salary
            const decimal hourlyRate = 300.00m;
            model.Total = model.Hours * hourlyRate;
            model.Date1 = DateTime.Now;

            model.Status = "Pending";

            // Assigns a unique ID to the claim
            model.ID = claims.Any() ? claims.Max(c => c.ID) + 1 : 1;

            // Handles the supporting document upload
            if (document != null && document.Length > 0)
            {
                try
                {
                    // Saves the files to upload folder
                    string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                    if (!Directory.Exists(uploadsPath))
                    {
                        Directory.CreateDirectory(uploadsPath);
                    }

                    var filePath = Path.Combine(uploadsPath, document.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        document.CopyTo(stream);
                    }

                    // Stores the path of the documnet as a relative URL
                    model.Document = "/uploads/" + document.FileName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error uploading document: {ex.Message}");
                }
            }

            // Adds the claim to the list
            claims.Add(model);

            // Redirects the user to the Review claims view after the claim is created
            return RedirectToAction("Index", "Claims");
        }
    }
}

//Title: Pro C 7 with.NET and .NET Core
//Author: Andrew Troelsen; Philip Japikse
// Date: 2017
// Code version: Version 1
//Availability: Textbook / Ebook

//Code from: https://www.c-sharpcorner.com/article/upload-files-in-asp-net-mvc-5/
//https://stackoverflow.com/questions/867117/how-to-add-static-list-of-items-in-mvc-html-dropdownlist