using Microsoft.AspNetCore.Mvc;

namespace NonattendanceApp.Controllers;

public class ClassController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}