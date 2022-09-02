using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using DutyBoard_Models.ViewModels;
using DutyBoard_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace DutyBoard.Controllers
{
    public class WorkdayController : Controller
    {
        private readonly IRosterRepository _rostRepo;
        private readonly IEmployeeRepository _empRepo;
        private readonly IWorkdayRepository _workdayRepo;
        [BindProperty]
        public WorkdayVM WorkdayVM { get; set; }
        public WorkdayListVM WorkdayListVM { get; set; }
        [BindProperty]
        public Workday Workday { get; set; }
        public WorkdayController(IRosterRepository rostRepo, IEmployeeRepository empRepo, IWorkdayRepository workdayRepo)
        {
            _rostRepo = rostRepo;
            _empRepo = empRepo;
            _workdayRepo = workdayRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<WorkdayVM> Workdays = _workdayRepo.GetAll().Select(x => new WorkdayVM
            {
                Workday = x,
                Employee = _empRepo.FirstOrDefault(e => e.EmployeeId == x.EmployeeId),
                Roster = _rostRepo.FirstOrDefault(r => r.RosterId == x.RosterId)
            });
            
            return View(Workdays);
        }

        public IActionResult DeleteById(int Id) => PartialView("_Delete", _rostRepo.FirstOrDefault(x => x.RosterId == Id));

        [HttpPost]
        public IActionResult Del()
        {
            _workdayRepo.Remove(_workdayRepo.FirstOrDefault(x => x.WorkdayId == Workday.WorkdayId)); ;
            TempData[WC.Success] = "Дежурство удалено";
            return Redirect(nameof(Index));
        }
        public IActionResult EditById(int Id)
        {
            Workday workday;
            if (Id == 0)
                workday = new Workday();
            else
                workday = _workdayRepo.FirstOrDefault(x => x.WorkdayId == Id);
            WorkdayVM workdayVM = new WorkdayVM()
            {
                Workday = workday,
                Employee = _empRepo.FirstOrDefault(e => e.EmployeeId == Workday.EmployeeId),
                Roster = _rostRepo.FirstOrDefault(e => e.RosterId == Workday.RosterId),
                Employees = _empRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.EmployeeId.ToString()
                }),
                Rosters = _rostRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.DaysOfWeek.DayOfWeekName + x.StartTime.ToString(),
                    Value = x.RosterId.ToString()
                })
            };
            return PartialView("_Edit", workdayVM);
        }

        [HttpPost]
        public IActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                _workdayRepo.Upsert(Workday);
                TempData[WC.Success] = Workday.WorkdayId == 0 ? "Дежурство добавлено" : "Дежурство изменено";
                return Redirect(nameof(Index));
            }
            return PartialView("_Edit", Workday);
        }
    }
}
