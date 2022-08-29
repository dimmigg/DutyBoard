using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using DutyBoard_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IDaysOfWeekRepository _daysOfWeekRepo;
        private readonly IEmployeeRepository _empRepo;
        private readonly IHolidayRepository _holidayRepo;

        public HomeController(ILogger<HomeController> logger, 
                              IDaysOfWeekRepository daysOfWeekRepo, 
                              IEmployeeRepository empRepo,
                              IHolidayRepository holidayRepo)
        {
            _logger = logger;
            _daysOfWeekRepo = daysOfWeekRepo;
            _empRepo = empRepo;
            _holidayRepo = holidayRepo;
        }

        public IActionResult Index()
        {

            _holidayRepo.Upsert(new Holiday()
            {
                EmployeeId = 1,
                DateStart = DateTime.Now,
                DateFinish = DateTime.Now
            });

            var d = _holidayRepo.GetAll();
            var d2 = _holidayRepo.FirstOrDefault();
            d2.DateStart = DateTime.UtcNow;
            _holidayRepo.Upsert(d2);



            //TempData[WC.Success] = "ОК";
            var em = _empRepo.FirstOrDefault();
            em.Phone = "12345";
            _empRepo.Upsert(em);

            var emps = _empRepo.GetAll();
            var e = _empRepo.GetAll(x => x.EmployeeId != 2);
            var e2 = _empRepo.FirstOrDefault(x => x.EmployeeId == 3);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
