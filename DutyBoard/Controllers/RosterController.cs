using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using DutyBoard_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace DutyBoard.Controllers
{
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


        public IActionResult DeleteById(int Id) => PartialView("_Delete", _rostRepo.FirstOrDefault(x => x.RosterId == Id));
        
        public IActionResult Del()
        {
            _rostRepo.Remove(_rostRepo.FirstOrDefault(x => x.RosterId == Roster.RosterId)); ;
            return Redirect(nameof(Index));
        }
        public IActionResult EditById(int Id)
        {
            Roster roster;
            if (Id == 0)
                roster = new Roster();
            else
                roster = _rostRepo.FirstOrDefault(x => x.RosterId == Id);
            RosterVM rosterVM = new RosterVM()
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
            //RosterVM rosterVM = new RosterVM()
            //{
            //    Roster = ros,
            //    DaysOfWeeks = _dayRepo.GetAll().Select(x => new SelectListItem
            //    {
            //        Text = x.DayOfWeekName,
            //        Value = x.DayOfWeekId.ToString()
            //    })
            //};
            if (ModelState.IsValid)
            {
                _rostRepo.Upsert(RosterVM.Roster);
                return Redirect(nameof(Index));
            }
            return PartialView("_Edit", RosterVM);
        }

        public IActionResult Add()
        {
            return PartialView("_Add");
        }
    }
}
