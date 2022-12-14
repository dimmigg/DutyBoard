using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyBoard_Models.Models
{
    public class DaysOfWeek
    {
        [Key]
        public int DayOfWeekId { get; set; }
        public string DayOfWeekName { get; set; }
        public string DayOfWeekNameShort { get; set; }
        [NotMapped]
        public virtual string Day { get; set; } = "0";
    }
}
