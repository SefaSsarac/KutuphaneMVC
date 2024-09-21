using Microsoft.AspNetCore.Mvc;
using KutuphaneMVC.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace KutuphaneMVC.Controllers
{
    public class AuthController : Controller
    {
        // Static list to hold users for demonstration purposes
        private static List<User> _users = new List<User>()
        {
            new User{ Id = 1, Email = ".", Password = "."} // Initial user (for demo)
        };

        private readonly IDataProtector _dataProtector;

        // Constructor to initialize data protector for password encryption
        public AuthController(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }

        // GET: Show the sign-up form
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: Handle the sign-up form submission
        [HttpPost]
        public IActionResult SignUp(SingUpViewModel formData)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            // Check if user with the same email already exists
            var user = _users.FirstOrDefault(x => x.Email.ToLower() == formData.Email.ToLower());
            if (user != null)
            {
                ModelState.AddModelError("Email", "Kullanıcı mevcut"); // Add error to ModelState
                return View(formData);
            }

            // Create new user object
            var newUser = new User()
            {
                Id = _users.Max(x => x.Id) + 1, // Generate new user ID
                Email = formData.Email.ToLower(),
                Password = _dataProtector.Protect(formData.Password), // Protect the password
            };

            _users.Add(newUser); // Add new user to the list

            // Redirect to the login page
            return RedirectToAction("LogIn", "Auth");
        }

        // GET: Show the login form
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        // POST: Handle the login form submission
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel formData)
        {
            // Find the user by email
            var user = _users.FirstOrDefault(x => x.Email.ToLower() == formData.Email.ToLower());

            // If user is not found, show error message
            if (user is null)
            {
                ViewBag.Error = "Kullanıcı adı veya şifre hatalı"; // Username or password is incorrect
                return View(formData);
            }

            try
            {
                // Check if the password is correct
                if (_dataProtector.Unprotect(user.Password) != formData.Password)
                {
                    ViewBag.Error = "Kullanıcı adı veya şifre hatalı"; // Username or password is incorrect
                    return View(formData);
                }
            }
            catch (Exception ex)
            {
                // Show a generic error message if an error occurs
                ViewBag.Error = "Şifre çözme sırasında bir hata oluştu"; // An error occurred during password decryption
                return View(formData);
            }

            // Create claims for the user
            var claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("id", user.Id.ToString())
            };

            // Create claims identity
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Set authentication properties
            var autProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48)) // Set expiration time
            };

            // Sign in the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);

            // Redirect to the main page
            return RedirectToAction("Index", "Home");
        }

        // POST: Handle user logout
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Sign out the user
            return RedirectToAction("Index", "Home"); // Redirect to the main page
        }
    }
}
