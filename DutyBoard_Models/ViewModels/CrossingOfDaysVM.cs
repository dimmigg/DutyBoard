using System;

namespace DutyBoard_Models.ViewModels
{
    public class CrossingOfDaysVM
    {
        public string FullName { get; set; }

        public DateTime DateRoster { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }

    }
}
