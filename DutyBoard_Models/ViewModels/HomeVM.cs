using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutyBoard_Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Mapping> MainTable { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}
