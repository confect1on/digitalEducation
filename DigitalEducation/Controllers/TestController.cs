using Microsoft.AspNetCore.Mvc;

namespace DigitalEducation.Controllers;

public class TestController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}