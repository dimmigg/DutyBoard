using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyBoard_Models
{
    public class Roster
    {
        [Key]
        public int RosterId { get; set; }

        public int DaysOfWeekId { get; set; }

        [NotMapped]
        public DaysOfWeek DaysOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }
        
        public int DurationOfDuty { get; set; }

        [NotMapped]
        public TimeSpan EndTime => StartTime.Add(TimeSpan.FromHours(DurationOfDuty));

        [NotMapped]
        public string RosterName => $"{DaysOfWeek.DayOfWeekName}: {StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
    }
}
