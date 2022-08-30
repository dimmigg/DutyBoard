using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DutyBoard_Models
{ 
    public class Workday
    {
        [Key]
        public int WorkdayId { get; set; }

        public int EmployeeId { get; set; }

        [NotMapped]
        public Employee Employee { get; set; }

        public int RosterId { get; set; }
        [NotMapped]
        public Roster Roster { get; set; }

        public bool IsAlways { get; set; } = false;

        public DateTime DateWork { get; set; } = DateTime.Today;

    }
}
