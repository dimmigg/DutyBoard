using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using DutyBoard_Utility;
using Microsoft.AspNetCore.Mvc;

namespace DutyBoard.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _empRepo;

        [BindProperty]
        public Employee Employee { get; set; }
        public EmployeeController(IEmployeeRepository empRepo)
        {
            _empRepo = empRepo;
        }

        public IActionResult Index()
        {
            return View(_empRepo.GetAll());
        }

        public IActionResult DeleteById(int Id) => PartialView("_Delete", _empRepo.FirstOrDefault(Id));

        [HttpPost]
        public IActionResult Del()
        {
            _empRepo.Remove(_empRepo.FirstOrDefault(Employee.EmployeeId)); ;
            TempData[WC.Success] = "Сотрудник удален";
            return Redirect(nameof(Index));
        }
        public IActionResult EditById(int Id)
        {
            Employee employee;
            employee = Id == 0 ? new Employee() : _empRepo.FirstOrDefault(Id); 
            return PartialView("_Edit", employee);
        }

        [HttpPost]
        public IActionResult Edit()
        {
            if (!ModelState.IsValid) return PartialView("_Edit", Employee);
            _empRepo.Upsert(Employee);
            TempData[WC.Success] = Employee.EmployeeId == 0 ? "Сотрудник добавлен" : "Сотрудник изменен";
            return Redirect(nameof(Index));
        }
    }
}
