using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    // GET: Home page
    public IActionResult Index()
    {
        ViewData["Title"] = "Ana Sayfa"; // Home page title
        return View(); // Return the home page view
    }

    // GET: About page
    public IActionResult About()
    {
        return View(); // Return the about page view
    }

    // POST: Handle user logout
    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Sign out the user
        return RedirectToAction("Index", "Home"); // Redirect to the home page
    }
}
