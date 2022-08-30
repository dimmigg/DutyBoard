using DutyBoard_Models.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;


namespace DutyBoard_Models
{
    [HolidayDateComparsion]
    public class Holiday
    {
        [Key]
        public int HolidayId { get; set; }

        public int EmployeeId { get; set; }

        [NotMapped]
        public Employee Employee { get; set; }

        public DateTime DateStart { get; set; } = DateTime.Today;

        public DateTime DateFinish { get; set; } = DateTime.Today;

    }
}
