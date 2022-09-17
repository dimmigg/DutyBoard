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
                Employee = _empRepo.FirstOrDefault(x.EmployeeId),
                Roster = _rostRepo.FirstOrDefault(x.RosterId)
            });

            return View(Workdays);
        }

        public IActionResult DeleteById(int Id) => PartialView("_Delete", getVM(Id));
        

        [HttpPost]
        public IActionResult Del()
        {
            _workdayRepo.Remove(_workdayRepo.FirstOrDefault(Workday.WorkdayId)); ;
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

        public IActionResult GetRosterByDate(string id)
        {
            WorkdayVM workdayVM = new WorkdayVM();

            if (id == "-1")
                workdayVM.Rosters = _rostRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = $"{x.DaysOfWeek.DayOfWeekNameShort}: {x.StartTime:hh\\:mm} - {x.EndTime:hh\\:mm}",
                    Value = x.RosterId.ToString()
                });
            else
            {
                if (DateTime.TryParse(id, out DateTime valDate))
                    workdayVM.Rosters = _rostRepo.GetAll(daysOfWeekId : valDate.GetDayOfWeek()).Select(x => new SelectListItem
                    {
                        Text = $"{x.DaysOfWeek.DayOfWeekNameShort}: {x.StartTime:hh\\:mm} - {x.EndTime:hh\\:mm}",
                        Value = x.RosterId.ToString()
                    });
                else
                    return null;
            }
            return PartialView("_Rosters", workdayVM);
        }

        private WorkdayVM getVM(int Id)
        {
            Workday workday;
            if (Id == 0)
                workday = new Workday();
            else
                workday = _workdayRepo.FirstOrDefault(Id);
            WorkdayVM workdayVM = new WorkdayVM
            {
                Workday = workday,
                Employee = _empRepo.FirstOrDefault(workday.EmployeeId),
                Roster = _rostRepo.FirstOrDefault(workday.RosterId),
                Employees = _empRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.EmployeeId.ToString()
                })
            };
            if (workday.IsAlways)
                workdayVM.Rosters = _rostRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = $"{x.DaysOfWeek.DayOfWeekNameShort}: {x.StartTime:hh\\:mm} - {x.EndTime:hh\\:mm}",
                    Value = x.RosterId.ToString()
                });
            else
            {
                workdayVM.Rosters = _rostRepo.GetAll(daysOfWeekId : workday.DateWork.GetDayOfWeek()).Select(x => new SelectListItem
                {
                    Text = $"{x.DaysOfWeek.DayOfWeekNameShort}: {x.StartTime:hh\\:mm} - {x.EndTime:hh\\:mm}",
                    Value = x.RosterId.ToString()
                });
            }
            return workdayVM;
        }
    }
}
