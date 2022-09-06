using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.ViewModels;
using DutyBoard_Models;
using DutyBoard_Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DutyBoard.Controllers
{
    public class HolidayController : Controller
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly IHolidayRepository _holidayRepo;
        [BindProperty]
        public HolidayVM HolidayVM { get; set; }
        [BindProperty]
        public Holiday Holiday { get; set; }
        public HolidayController(IEmployeeRepository empRepo, IHolidayRepository holidayRepo)
        {
            _empRepo = empRepo;
            _holidayRepo = holidayRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<HolidayVM> holidays = _holidayRepo.GetAll().Select(x => new HolidayVM
            {
                Holiday = x,
                Employee = _empRepo.FirstOrDefault(e => e.EmployeeId == x.EmployeeId)
            });

            return View(holidays);
        }

        public IActionResult DeleteById(int Id) => PartialView("_Delete", getVM(Id));


        [HttpPost]
        public IActionResult Del()
        {
            _holidayRepo.Remove(_holidayRepo.FirstOrDefault(x => x.HolidayId == Holiday.HolidayId)); ;
            TempData[WC.Success] = "Отпуск удален";
            return Redirect(nameof(Index));
        }
        public IActionResult EditById(int Id) => PartialView("_Edit", getVM(Id));

        [HttpPost]
        public IActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                Holiday.DateFinish = Holiday.DateFinish.AddHours(23).AddMinutes(59);
                _holidayRepo.Upsert(Holiday);
                TempData[WC.Success] = Holiday.HolidayId == 0 ? "Отпуск добавлен" : "Отпуск изменен";
                return Redirect(nameof(Index));
            }
            HolidayVM holidayVM = new HolidayVM
            {
                Holiday = Holiday,
                Employee = _empRepo.FirstOrDefault(e => e.EmployeeId == Holiday.EmployeeId),
                Employees = _empRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.EmployeeId.ToString()
                })
            };
            return PartialView("_Edit", holidayVM);
        }

        private HolidayVM getVM(int Id)
        {
            Holiday holiday;
            if (Id == 0)
                holiday = new Holiday();
            else
                holiday = _holidayRepo.FirstOrDefault(x => x.HolidayId == Id);
            HolidayVM holidayVM = new HolidayVM
            {
                Holiday = holiday,
                Employee = _empRepo.FirstOrDefault(e => e.EmployeeId == holiday.EmployeeId),
                Employees = _empRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.EmployeeId.ToString()
                })
            };
            return holidayVM;
        }
    }
}
