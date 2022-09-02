using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DutyBoard_Models.ViewModels
{
    public class WorkdayVM
    {
        public Workday Workday { get; set; }
        public Employee Employee { get; set; } 
        public Roster Roster { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> Rosters { get; set; }
    }
}
