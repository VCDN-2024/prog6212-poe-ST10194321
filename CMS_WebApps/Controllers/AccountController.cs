using CMS_WebApps.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CMS_WebApps.Controllers
{
    public class AccountController : Controller
    {
        // To store the registered users
        private static List<RegisterViewModel> _users = new List<RegisterViewModel>();

        // To store lecturer details for HR management
        public static List<LecturerViewModel> LecturerManagement = new List<LecturerViewModel>();

        // GET: Register
        public IActionResult RegisterView()
        {
            return View("RegisterView");
        }

        [HttpPost]
        public IActionResult RegisterView(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                if (_users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", "User with this email already exists.");
                    return View("RegisterView", model);
                }

                // Validate FullName and PhoneNumber for all roles
                if (string.IsNullOrEmpty(model.FullName) || string.IsNullOrEmpty(model.PhoneNumber))
                {
                    ModelState.AddModelError("", "Full Name and Phone Number are required for all roles.");
                    return View("RegisterView", model);
                }

                // Add lecturer-specific details for HR management
                if (model.Role == "Lecturer")
                {
                    LecturerManagement.Add(new LecturerViewModel
                    {
                        Email = model.Email,
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber
                    });
                }

                // Add user to the user list
                _users.Add(model);

                // Set session details
                HttpContext.Session.SetString("Email", model.Email);
                HttpContext.Session.SetString("Role", model.Role);

                // Redirect to login page after successful registration
                return RedirectToAction("Login", "Account");
            }

            // If validation fails, return the form with error messages
            return View("RegisterView", model);
        }



        // GET: Login
        public IActionResult Login()
        {
            return View("LoginView");
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user in the list
                var user = _users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Set session data
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Role", user.Role);

                    // Redirect based on role
                    if (user.Role == "Lecturer")
                        return RedirectToAction("Create", "Claims");
                    if (user.Role == "ProgramCoordinator" || user.Role == "AcademicManager")
                        return RedirectToAction("Index", "Submission");
                    if (user.Role == "HR")
                        return RedirectToAction("Index", "HR");

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View("LoginView", model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear the session
            return RedirectToAction("Login", "Account"); // Redirect to login page
        }
    }
}


//Code from: https://stackoverflow.com/questions/60520664/how-to-create-a-simple-login-in-asp-net-core-without-database-and-authorize-cont
//https://stackoverflow.com/questions/1304603/asp-net-mvc-roles-without-database-and-without-role-provider
//https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-8.0