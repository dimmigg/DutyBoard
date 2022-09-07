using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using DutyBoard_Models.ViewModels;
using DutyBoard_Utility;
using DutyBoard_Utility.Calculate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DutyBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepository _empRepo;
        private readonly IRosterRepository _rostRepo;
        private readonly IWorkdayRepository _workdayRepo;
        private readonly IHolidayRepository _holidayRepo;
        private readonly IMappingRepository _mappRepo;

        public HomeController(ILogger<HomeController> logger,
                              IEmployeeRepository empRepo,
                              IHolidayRepository holidayRepo,
                              IRosterRepository rostRepo,
                              IWorkdayRepository workdayRepo,
                              IMappingRepository mappRepo)
        {
            _logger = logger;
            _empRepo = empRepo;
            _holidayRepo = holidayRepo;
            _rostRepo = rostRepo;
            _workdayRepo = workdayRepo;
            _mappRepo = mappRepo;
        }

        public IActionResult Index()
        {
            var vm = new HomeVM
            {
                MainTable = _mappRepo.GetAll(),
                Employees = _empRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.EmployeeId.ToString()
                })
            };
            var employees = vm.Employees.Select(x => x.Text).Distinct().ToArray();
            var arrDuty = new int[employees.Count()];
            for (int i = 0; i < employees.Count(); i++)
            {
                arrDuty[i] = vm.MainTable.Count(x => x.Employee.FullName == employees[i]);
            }
            ViewBag.ArrEmployee =  JsonConvert.SerializeObject(employees);
            ViewBag.CountsDuty =  JsonConvert.SerializeObject(arrDuty);
            return View(vm);
        }


        public IActionResult Calc()
        {
            var calc = new Calculate(
                _rostRepo.GetAll(),
                _empRepo.GetAll(),
                _holidayRepo.GetAll(),
                _workdayRepo.GetAll()
                );
            var b = calc.StartCalculate();
            _mappRepo.InsertData(b);
            return RedirectToAction(nameof(Index));
        }

        public int[] EditMapping(string mapp, string emp)
        {
            _mappRepo.Update(mapp, emp);          
            var employees = _empRepo.GetAll().Select(x => x.FullName).Distinct().ToArray();
            var arrDuty = new int[employees.Count()];
            var data = _mappRepo.GetAll();
            for (int i = 0; i < employees.Count(); i++)
            {
                arrDuty[i] = data.Count(x => x.Employee.FullName == employees[i]);
            }
            //ViewBag.ArrEmployee = JsonConvert.SerializeObject(employees);
            //ViewBag.CountsDuty = JsonConvert.SerializeObject(arrDuty);
            //TempData[WC.Success] = "Дежурство изменено";
            return arrDuty;
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
