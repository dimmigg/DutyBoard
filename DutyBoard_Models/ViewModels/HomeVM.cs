using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutyBoard_Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Mapping> MainTable { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }

        public string ArrEmployee { get; set; }
        public string CountsDuty { get; set; }
        public string Statistics { get; set; } 
    }
}
