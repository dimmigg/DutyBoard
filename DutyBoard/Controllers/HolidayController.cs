using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.ViewModels;
using DutyBoard_Utility;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using DutyBoard_Models.Models;
using Microsoft.AspNetCore.Authorization;

namespace DutyBoard.Controllers
{
    [Authorize]
    public class HolidayController : Controller
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly IHolidayRepository _holidayRepo;

        [BindProperty]
        public Holiday Holiday { get; set; }
        public HolidayController(IEmployeeRepository empRepo, IHolidayRepository holidayRepo)
        {
            _empRepo = empRepo;
            _holidayRepo = holidayRepo;
        }

        public IActionResult Index()
        {
            var holidays = _holidayRepo.GetAll().Select(x => new HolidayVM
            {
                Holiday = x,
                Employee = _empRepo.FirstOrDefault(x.EmployeeId)
            });

            return View(holidays);
        }

        public IActionResult DeleteById(int Id) => PartialView("_Delete", GetVM(Id));


        [HttpPost]
        public IActionResult Del()
        {
            _holidayRepo.Remove(_holidayRepo.FirstOrDefault(Holiday.HolidayId)); ;
            TempData[WC.Success] = "Отпуск удален";
            return Redirect(nameof(Index));
        }
        public IActionResult EditById(int Id) => PartialView("_Edit", GetVM(Id));

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
            var holidayVM = new HolidayVM
            {
                Holiday = Holiday,
                Employee = _empRepo.FirstOrDefault(Holiday.EmployeeId),
                Employees = _empRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.EmployeeId.ToString()
                })
            };
            return PartialView("_Edit", holidayVM);
        }

        private HolidayVM GetVM(int Id)
        {
            Holiday holiday;
            holiday = Id == 0 ? new Holiday() : _holidayRepo.FirstOrDefault(Id);
            var holidayVM = new HolidayVM
            {
                Holiday = holiday,
                Employee = _empRepo.FirstOrDefault(holiday.EmployeeId),
                Employees = _empRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.EmployeeId.ToString()
                })
            };
            return holidayVM;
        }
    }
}
