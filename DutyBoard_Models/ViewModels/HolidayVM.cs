using DutyBoard_Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DutyBoard_Models.ViewModels
{
    public class HolidayVM
    {
        public Holiday Holiday { get; set; }
        public Employee Employee { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}
