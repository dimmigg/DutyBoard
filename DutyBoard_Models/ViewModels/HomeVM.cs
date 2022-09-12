using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DutyBoard_Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Mapping> MainTable { get; set; }
        public IEnumerable<WorkdayVM> Workdays { get; set; }
        public IEnumerable<HolidayVM> Holidays { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IList<string> CrossingOfDays { get; set; }

        public string ArrEmployee { get; set; }
        public string CountsDuty { get; set; }
        public string Statistics { get; set; } 
    }
}
