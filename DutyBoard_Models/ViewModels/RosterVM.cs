using System.Collections.Generic;
using DutyBoard_Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DutyBoard_Models.ViewModels
{
    public class RosterVM
    {
        public Roster Roster { get; set; }
        public IEnumerable<SelectListItem> DaysOfWeeks { get; set; }
    }
}
