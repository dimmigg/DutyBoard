using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyBoard_Models
{
    public class Mapping
    {
        public Mapping()
        {

        }
        public Mapping(int rosterId, DateTime dateTime)
        {
            RosterId = rosterId;
            DateStart = dateTime;
        }

        [Dapper.Contrib.Extensions.Key]
        public int MappingId { get; set; }
        public int EmployeeId { get; set; }
        [NotMapped]
        public Employee Employee { get; set; }
        public int RosterId { get; set; }
        [NotMapped]
        public Roster Roster { get; set; }
        public DateTime DateStart { get; set; }
        [NotMapped]
        public DateTime DateEnd => DateStart.AddHours(Roster != null ? Roster.DurationOfDuty : 0);
    }
}
