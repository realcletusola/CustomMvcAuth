using CustomAuthMVC.Entities;
using CustomAuthMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CustomAuthMVC.Controllers
{
    public class AccountController : Controller
    {
        // configure DbContext 
        private readonly AppDbContext _context;

        public AccountController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        // add iAction method for registration
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationModel model)
        {
            // custom validation logic
            // validate first name  
            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                ModelState.AddModelError("firstname_value", "First name is required");
            }
            else if (model.FirstName.Length > 30)
            {
                ModelState.AddModelError("firstname_length", "First name cannot exceed 30 characters");
            }

            // validate last name 
            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                ModelState.AddModelError("lastname_value", "Last name is required");
            }
            else if (model.LastName.Length > 30)
            {
                ModelState.AddModelError("lastname_length", "Last name cannot exceed 30 characters");
            }

            // validate email 
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError("email_value", "Email cannot be empty");
            }
            else if (!Regex.IsMatch(model.Email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            {
                ModelState.AddModelError("email_character", "Please provide a valid email address");
            }

            // validate username 
            if (string.IsNullOrWhiteSpace(model.UserName))
            {
                ModelState.AddModelError("username_value", "Username is required");
            }
            else if (model.UserName.Length > 20)
            {
                ModelState.AddModelError("username_length", "Username cannot exceed 20 characters");
            }

            // validate password 
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("password_value", "Password is required");
            }
            else if (!Regex.IsMatch(model.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"))
            {
                ModelState.AddModelError("password_character", "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
            }

            // validate second password 
            if(model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("password_match", "Both passwords must match");
            }

            // create account if modelstate is valid
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Email = model.Email;
                account.UserName = model.UserName;
                account.Password = model.Password;

                _context.UserAccounts.Add(account);
                _context.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = $"Account created successfully for {account.UserName}";

                return View(); 
            }

            // if validation fails return view with model and validation errors 
            return View(model);
        }
    }
}
