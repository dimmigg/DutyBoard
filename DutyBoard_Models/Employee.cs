using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyBoard_Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string FullName { get; set; }

        public string LoginName { get; set; }

        public string Phone { get; set; }

        [NotMapped]
        public int CountDuty { get; set; } = 0;

        [NotMapped]
        public string ShortName => $"{FullName.Split()[0]} {FullName.Split()[1][0]}.";
    }
}
