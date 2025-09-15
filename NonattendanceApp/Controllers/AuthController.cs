using Microsoft.AspNetCore.Mvc;

namespace NonattendanceApp.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}