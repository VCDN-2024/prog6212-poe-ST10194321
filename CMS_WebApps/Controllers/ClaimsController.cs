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

        [HttpPost]
        public IActionResult Create(ClaimViewModel model, IFormFile document)
        {
            if (ModelState.IsValid)
            {
                const decimal hourlyRate = 400.00m;
                const long fileSizeLimit = 5 * 1024 * 1024; // 5 MB limit
                var allowedExtensions = new[] { ".pdf", ".docx", ".txt" };

                model.Total = model.Hours * hourlyRate;
                model.Date1 = DateTime.Now;
                model.Status = "Pending";

                model.ID = claims.Any() ? claims.Max(c => c.ID) + 1 : 1;

                if (document != null && document.Length > 0)
                {
                    // Validate file size
                    if (document.Length > fileSizeLimit)
                    {
                        ModelState.AddModelError("Document", "File size exceeds the maximum limit of 5 MB.");
                        return View(model);
                    }

                    // Validate file type
                    var fileExtension = Path.GetExtension(document.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("Document", "Invalid file type. Only PDF, DOCX, and TXT files are allowed.");
                        return View(model);
                    }

                    try
                    {
                        string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                        if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

                        string filePath = Path.Combine(uploadsPath, document.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            document.CopyTo(stream);
                        }

                        model.Document = "/uploads/" + document.FileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Error uploading document: {ex.Message}");
                        return View(model);
                    }
                }

                claims.Add(model);
                Console.WriteLine($"Claim submitted with Module: {model.Modules}");
                return RedirectToAction("Index");
            }

            return View(model);
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