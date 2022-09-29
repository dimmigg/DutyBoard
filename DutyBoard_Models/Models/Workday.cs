using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;


namespace DutyBoard_Models.Models
{
    public class Workday
    {
        public Workday()
        {
            Employee = new Employee();
            Roster = new Roster();
        }
        [Dapper.Contrib.Extensions.Key]

        public int WorkdayId { get; set; }
        [Display(Name = "Сотрудник")]
        public int EmployeeId { get; set; }

        [NotMapped]
        public Employee Employee { get; set; }
        [Required(ErrorMessage = "Выбери дежурство!")]
        [Display(Name = "Дежурство")]
        public int RosterId { get; set; }
        [NotMapped]
        public Roster Roster { get; set; }
        [Display(Name = "Постоянно")]
        public bool IsAlways { get; set; } = false;
        
        public DateTime DateWork { get; set; } = DateTime.Today;

        [NotMapped]
        [Required(ErrorMessage = "Выбери день!")]
        [Display(Name = "День")]
        public string WorkDt
        {
            get => DateWork.ToString("dd.MM.yyyy");
            set => DateWork = DateTime.ParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
        [NotMapped]
        public DateTime StartDateWork => DateWork + Roster.StartTime;

    }
}
