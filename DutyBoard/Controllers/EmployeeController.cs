using Microsoft.AspNetCore.Mvc;

namespace DutyBoard.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
