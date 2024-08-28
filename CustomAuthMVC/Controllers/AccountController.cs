using CustomAuthMVC.Entities;
using CustomAuthMVC.Models;
using Microsoft.AspNetCore.Mvc;

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

            return View(model);
        }
    }
}
