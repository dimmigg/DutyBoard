using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using DutyBoard_Models.ViewModels;
using DutyBoard_Utility;
using DutyBoard_Utility.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public IActionResult DeleteById(int Id) => PartialView("_Delete", getVM(Id));
        

        [HttpPost]
        public IActionResult Del()
        {
            _workdayRepo.Remove(_workdayRepo.FirstOrDefault(x => x.WorkdayId == Workday.WorkdayId)); ;
            TempData[WC.Success] = "Дежурство удалено";
            return Redirect(nameof(Index));
        }
        public IActionResult EditById(int Id) => PartialView("_Edit", getVM(Id));

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

        public IActionResult GetRosterByDateId(int id)
        {
            WorkdayVM workdayVM = new WorkdayVM();

            if (id == -1)
                workdayVM.Rosters = _rostRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = $"{x.DaysOfWeek.DayOfWeekName}: {x.StartTime:hh\\:mm} - {x.EndTime:hh\\:mm}",
                    Value = x.RosterId.ToString()
                });
            else
            {
                workdayVM.Rosters = _rostRepo.GetAll(x => x.DaysOfWeekId == id).Select(x => new SelectListItem
                {
                    Text = $"{x.DaysOfWeek.DayOfWeekName}: {x.StartTime:hh\\:mm} - {x.EndTime:hh\\:mm}",
                    Value = x.RosterId.ToString()
                });
            }
            return PartialView("_Rosters", workdayVM);
        }

        private WorkdayVM getVM(int Id)
        {
            Workday workday;
            if (Id == 0)
                workday = new Workday();
            else
                workday = _workdayRepo.FirstOrDefault(x => x.WorkdayId == Id);
            WorkdayVM workdayVM = new WorkdayVM
            {
                Workday = workday,
                Employee = _empRepo.FirstOrDefault(e => e.EmployeeId == workday.EmployeeId),
                Roster = _rostRepo.FirstOrDefault(e => e.RosterId == workday.RosterId),
                Employees = _empRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.EmployeeId.ToString()
                })
            };
            if (workday.IsAlways)
                workdayVM.Rosters = _rostRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = $"{x.DaysOfWeek.DayOfWeekName}: {x.StartTime:hh\\:mm} - {x.EndTime:hh\\:mm}",
                    Value = x.RosterId.ToString()
                });
            else
            {
                workdayVM.Rosters = _rostRepo.GetAll(x => x.DaysOfWeekId == workday.DateWork.GetDayOfWeek()).Select(x => new SelectListItem
                {
                    Text = $"{x.DaysOfWeek.DayOfWeekName}: {x.StartTime:hh\\:mm} - {x.EndTime:hh\\:mm}",
                    Value = x.RosterId.ToString()
                });
            }
            return workdayVM;
        }
    }
}
