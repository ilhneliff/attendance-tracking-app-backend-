using Microsoft.AspNetCore.Mvc;

namespace NonattendanceApp.Controllers;

public class AttendanceController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}