using System.Collections.Generic;

namespace DutyBoard_Models.ViewModels
{
    public class WorkdayListVM
    {
        //public Workday Workday { get; set; }
        //public IEnumerable<SelectListItem> Workdays { get; set; }
        public IEnumerable<WorkdayVM> Workdays { get; set; }
    }
}
