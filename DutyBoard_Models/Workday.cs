using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DutyBoard_Models
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
        [HiddenInput(DisplayValue = false)]
        public Roster Roster { get; set; }
        [Display(Name = "Постоянно")]
        public bool IsAlways { get; set; } = false;
        [Required(ErrorMessage = "Выбери день!")]
        [Display(Name = "День")]
        public DateTime DateWork { get; set; } = DateTime.Today;

    }
}
