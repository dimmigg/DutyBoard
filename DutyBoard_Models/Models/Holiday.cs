using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DutyBoard_Models.Extensions;
using System.Globalization;

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
        
        public DateTime DateStart { get; set; } = DateTime.Today;

        [NotMapped]
        [Required(ErrorMessage = "Выбери день!")]
        [Display(Name = "Начало")]
        public string StartDt
        {
            get => DateStart.ToString("dd.MM.yyyy");
            set => DateStart = DateTime.ParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }

        [NotMapped]
        [Required(ErrorMessage = "Выбери день!")]
        [Display(Name = "Окончание")]
        public string FinishDt
        {
            get => DateFinish.ToString("dd.MM.yyyy");
            set => DateFinish = DateTime.ParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
        public DateTime DateFinish { get; set; } = DateTime.Today.AddHours(23).AddMinutes(59);

    }
}
