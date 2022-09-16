using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyBoard_Models
{
    public class Roster
    {
        public Roster()
        {
            DaysOfWeek = new DaysOfWeek();
        }
        [Dapper.Contrib.Extensions.Key]
        public int RosterId { get; set; }
        [Display(Name = "День недели")]
        [Required(ErrorMessage = "Выбери день недели!")]
        public int DaysOfWeekId { get; set; } = 1;

        [NotMapped]
        public DaysOfWeek DaysOfWeek { get; set; }
        [DataType(DataType.Time), Required(ErrorMessage = "Введи время!")]
        [Display(Name = "Дежурство с")]
        public TimeSpan StartTime { get; set; }
        [Display(Name = "Длительность")]
        [Range(1, 24, ErrorMessage = "Введи больше 0 и меньше 24!"), Required(ErrorMessage = "Введи длительность!")]
        public int DurationOfDuty { get; set; } = 1;

        [NotMapped]
        public TimeSpan EndTime => StartTime.Add(TimeSpan.FromHours(DurationOfDuty));
    }
}
