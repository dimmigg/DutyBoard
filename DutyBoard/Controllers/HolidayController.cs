using Microsoft.AspNetCore.Mvc;

namespace DutyBoard.Controllers
{
    public class HolidayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
