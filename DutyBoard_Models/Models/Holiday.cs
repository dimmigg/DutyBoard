using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DutyBoard_Models.Models
{
    public class Holiday
    {
        [Dapper.Contrib.Extensions.Key]
        public int HolidayId { get; set; }
        [Required(ErrorMessage = "Выбери дежурного!")]
        [Display(Name = "Дежурный")]
        public int EmployeeId { get; set; }

        [NotMapped]
        public Employee Employee { get; set; }
        [Required(ErrorMessage = "Выбери день!")]
        [Display(Name = "Начало")]
        public DateTime DateStart { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "Выбери день!")]
        [Display(Name = "Окончание")]
        public DateTime DateFinish { get; set; } = DateTime.Today.AddHours(23).AddMinutes(59);

    }
}
