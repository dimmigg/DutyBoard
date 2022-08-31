using DutyBoard_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DutyBoard.Controllers
{
    public class RosterController : Controller
    {
        private readonly IRosterRepository _rostRepo;
        public RosterController(IRosterRepository rostRepo)
        {
            _rostRepo = rostRepo;
        }

        public IActionResult Index()
        {
            return View(_rostRepo.GetAll());
        }
    }
}
