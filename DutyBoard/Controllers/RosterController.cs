using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Models;
using DutyBoard_Models.ViewModels;
using DutyBoard_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace DutyBoard.Controllers
{
    [Authorize]
    public class RosterController : Controller
    {
        private readonly IRosterRepository _rostRepo;
        private readonly IDaysOfWeekRepository _dayRepo;
        [BindProperty]
        public RosterVM RosterVM { get; set; }
        [BindProperty]
        public Roster Roster { get; set; }
        public RosterController(IRosterRepository rostRepo, IDaysOfWeekRepository day)
        {
            _rostRepo = rostRepo;
            _dayRepo = day;
        }

        public IActionResult Index()
        {
            return View(_rostRepo.GetAll());
        }

        public IActionResult DeleteById(int Id) => PartialView("_Delete", _rostRepo.FirstOrDefault(Id));

        [HttpPost]
        public IActionResult Del()
        {
            _rostRepo.Remove(_rostRepo.FirstOrDefault(Roster.RosterId)); ;
            TempData[WC.Success] = "Дежурство удалено";
            return Redirect(nameof(Index));
        }
        public IActionResult EditById(int Id)
        {
            Roster roster;
            roster = Id == 0 ? new Roster() : _rostRepo.FirstOrDefault(Id);
            var rosterVM = new RosterVM()
            {
                Roster = roster,
                DaysOfWeeks = _dayRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.DayOfWeekName,
                    Value = x.DayOfWeekId.ToString()
                })
            };
            return PartialView("_Edit", rosterVM);
        }

        [HttpPost]
        public IActionResult Edit()
        {
            if (!ModelState.IsValid) return PartialView("_Edit", RosterVM);
            _rostRepo.Upsert(RosterVM.Roster);
            TempData[WC.Success] = RosterVM.Roster.RosterId == 0 ? "Дежурство добавлено" : "Дежурство изменено";
            return Redirect(nameof(Index));
        }
    }
}
