using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyBoard_Models
{
    public class Roster
    {
        [Dapper.Contrib.Extensions.Key]
        public int RosterId { get; set; }
        [Display(Name = "День недели")]
        [Required(ErrorMessage = "Выбрери день недели!")]
        public int DaysOfWeekId { get; set; }

        [NotMapped]
        public DaysOfWeek DaysOfWeek { get; set; }
        [DataType(DataType.Time), Required(ErrorMessage = "Введи время!")]
        [Display(Name = "Начало дежурства")]
        public TimeSpan StartTime { get; set; }
        [Display(Name = "Длительность дежурства")]
        [Range(1, 24, ErrorMessage = "Введи больше 0 и меньше 24!"), Required(ErrorMessage = "Введи длительность!")]
        public int DurationOfDuty { get; set; }

        [NotMapped]
        public TimeSpan EndTime => StartTime.Add(TimeSpan.FromHours(DurationOfDuty));

        //[NotMapped]
        //public string RosterName => $"{DaysOfWeek.DayOfWeekName}: {StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
    }
}
