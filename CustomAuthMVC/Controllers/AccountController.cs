using CustomAuthMVC.Entities;
using CustomAuthMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

        // create registration controller 
        [HttpGet]
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
            else if (_context.UserAccounts.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("email_exists", $"Email {model.Email} is not available");
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
            else if (_context.UserAccounts.Any(u => u.UserName == model.UserName))
            {
                ModelState.AddModelError("username_exists", $"Username {model.UserName} is not available");
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
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("password_match", "Both passwords must match");
            }

            // create account if modelstate is valid
            if (ModelState.IsValid)
            {
                // Hash the password 
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                UserAccount account = new UserAccount();
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Email = model.Email;
                account.UserName = model.UserName;
                account.Password = hashedPassword;

                _context.UserAccounts.Add(account);
                _context.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = $"Account created successfully for {account.UserName}";

                return View();
            }

            // if validation fails return view with model and validation errors 
            return View(model);
        }

        // create login controller 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // fetch the user account from database 
                var userAccount = _context.UserAccounts.SingleOrDefault(u => u.UserName == model.UserNameOrEmail || u.Email == model.UserNameOrEmail);

                // check if the password matches 
                if (userAccount != null && BCrypt.Net.BCrypt.Verify(model.Password, userAccount.Password))
                {
                    // create claims for the user (usr identity)
                    var claims = new List<Claim>
                    {
                        // store username in claims
                        new Claim(ClaimTypes.Name, userAccount.UserName),
                        new Claim(ClaimTypes.Email, userAccount.Email)
                    };

                    // create the claims identity 
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // create authentication properties 
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    // sign in the user 
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    // redirect to profile page 
                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login details");
                }
            }

            return View(model);
        }

        // logout controller
        public IActionResult Logout()
        {
            // sign out the user and remove the authentication cookie 
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        } 

        // profile controller 
        [Authorize]
        public IActionResult Profile()
        {
            // get username from user's claim
            var username = User.Identity.Name;

            ViewBag.Username = username;

            return View();
        }
    }
}
