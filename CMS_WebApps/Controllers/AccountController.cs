using CMS_WebApps.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;


namespace CMS_WebApps.Controllers
{
    public class AccountController : Controller
    {
        //To store the users
        private static List<RegisterViewModel> _users = new List<RegisterViewModel>();

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
                // to check if the user already exists
                if (_users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", "User with this email already exists.");
                    return View("RegisterView", model);
                }

                // Adds the new user to the list
                _users.Add(model);

                // Sets the session for the newly registered user
                HttpContext.Session.SetString("Email", model.Email);
                HttpContext.Session.SetString("Role", model.Role);  // Ensure the role is set as ProgramCoordinator or AcademicManager

                // Redirects the user to the login page after successful registration
                return RedirectToAction("Login", "Account");
            }

            return View("RegisterView", model);
        }



        // GET: Login
        public IActionResult Login()
        {
            return View("LoginView");
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Finds the user in the list
                var user = _users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // if the is User found, sets session 
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Role", user.Role);

                    // Redirecst the user to the home page after a successful login
                    return RedirectToAction("Index", "Home");
                }


                ModelState.AddModelError("", "Invalid email or password.");
            }
            return View("LoginView", model); // Return to the login view with the model
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Redirects the user to the Register View  to allow the user to sign up again
            return RedirectToAction("RegisterView", "Account");
        }

    }
}

//Code from: https://stackoverflow.com/questions/60520664/how-to-create-a-simple-login-in-asp-net-core-without-database-and-authorize-cont
//https://stackoverflow.com/questions/1304603/asp-net-mvc-roles-without-database-and-without-role-provider
//https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-8.0
