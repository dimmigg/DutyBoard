using Microsoft.AspNetCore.Mvc;

namespace DutyBoard.Controllers
{
    public class RosterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
