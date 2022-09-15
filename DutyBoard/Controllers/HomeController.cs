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

        [BindProperty]
        public ConfHomeVM ConfHomeVM { get; set; }

        public IActionResult Index()
        {
            var vm = new HomeVM
            {
                MainTable = _mappRepo.GetAll(),
                Workdays = _workdayRepo.GetAll().Select(x => new WorkdayVM
                {
                    Workday = x,
                    Employee = _empRepo.FirstOrDefault(e => e.EmployeeId == x.EmployeeId),
                    Roster = _rostRepo.FirstOrDefault(r => r.RosterId == x.RosterId)
                }),
                Holidays = _holidayRepo.GetAll().Select(x => new HolidayVM
                {
                    Holiday = x,
                    Employee = _empRepo.FirstOrDefault(e => e.EmployeeId == x.EmployeeId)
                }),
                Employees = _empRepo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.EmployeeId.ToString()
                }),
                CrossingOfDays = new List<string>()
            };
            var employees = vm.Employees.Select(x => x.Text).Distinct().OrderBy(x => x).ToArray();
            var arrDuty = new int[employees.Count()];
            var arrDutyWork = new int[employees.Count()];
            var arrDutyHoly = new int[employees.Count()];
            for (int i = 0; i < employees.Count(); i++)
            {
                arrDuty[i] = vm.MainTable.Count(x => x.Employee.FullName == employees[i]);
                arrDutyWork[i] = vm.MainTable.Count(x => x.Employee.FullName == employees[i] && (x.Roster.DaysOfWeekId != 7 && x.Roster.DaysOfWeekId != 6));
                arrDutyHoly[i] = vm.MainTable.Count(x => x.Employee.FullName == employees[i] && (x.Roster.DaysOfWeekId == 7 || x.Roster.DaysOfWeekId == 6));
            }
            vm.Statistics = JsonConvert.SerializeObject(new Statistics()
            {
                Employees = employees,
                AllCounts = arrDuty,
                WorkdayCounts = arrDutyWork,
                HolidayCounts = arrDutyHoly
            });



            return View(vm);
        }
        [HttpPost]
        public IActionResult ConfCalc(string fromDate, string toDate)
        {
            if (DateTime.TryParse(fromDate, out DateTime fDate) && DateTime.TryParse(toDate, out DateTime tDate))
            {
                var confVM = new ConfHomeVM()
                {
                    FromDate = fDate,
                    ToDate = tDate
                };
                return PartialView("_Confirmation", confVM);
            }
            return null;
        }


        [HttpPost]
        public IActionResult Calc()
        {

            var calc = new Calculate(
                _rostRepo.GetAll(),
                _empRepo.GetAll(),
                _holidayRepo.GetAll(),
                _workdayRepo.GetAll()
                )
            {
                Start = ConfHomeVM.FromDate,
                Finish = ConfHomeVM.ToDate
            };
            var b = calc.StartCalculate();
            _mappRepo.InsertData(b);

            return RedirectToAction(nameof(Index));
        }

        public string EditMapping(string mapp, string emp)
        {
            _mappRepo.Update(mapp, emp);
            var employees = _empRepo.GetAll().Select(x => x.FullName).Distinct().OrderBy(x => x).ToArray();
            var arrDuty = new int[employees.Count()];
            var arrDutyWork = new int[employees.Count()];
            var arrDutyHoly = new int[employees.Count()];
            var data = _mappRepo.GetAll();
            for (int i = 0; i < employees.Count(); i++)
            {
                arrDuty[i] = data.Count(x => x.Employee.FullName == employees[i]);
                arrDutyWork[i] = data.Count(x => x.Employee.FullName == employees[i] && (x.Roster.DaysOfWeekId != 7 && x.Roster.DaysOfWeekId != 6));
                arrDutyHoly[i] = data.Count(x => x.Employee.FullName == employees[i] && (x.Roster.DaysOfWeekId == 7 || x.Roster.DaysOfWeekId == 6));
            }
            var st = new Statistics()
            {
                Employees = employees,
                AllCounts = arrDuty,
                WorkdayCounts = arrDutyWork,
                HolidayCounts = arrDutyHoly
            };
            return JsonConvert.SerializeObject(st);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult GetCrossingOfDays()
        {
            var holidays = _holidayRepo.GetAll().Select(x => new HolidayVM
            {
                Holiday = x,
                Employee = _empRepo.FirstOrDefault(e => e.EmployeeId == x.EmployeeId)
            });
            var mainTable = _mappRepo.GetAll();
            var vm = new List<CrossingOfDaysVM>();
            foreach (var item in holidays)
            {
                var workdays = mainTable.Where(x => x.Employee.EmployeeId == item.Employee.EmployeeId);
                foreach (var workday in workdays)
                {
                    if (workday.DateStart >= item.Holiday.DateStart && workday.DateStart <= item.Holiday.DateFinish)
                    {
                        var cross = new CrossingOfDaysVM()
                        {
                            FullName = workday.Employee.FullName,
                            DateRoster = workday.DateStart,
                            DateStart = item.Holiday.DateStart,
                            DateFinish = item.Holiday.DateFinish
                        };
                        vm.Add(cross);
                    }
                }
            }

            return PartialView("_CrossingOfDays", vm);
        }
    }
}
