using Microsoft.AspNetCore.Mvc;

namespace DutyBoard.Controllers
{
    public class WorkdayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
