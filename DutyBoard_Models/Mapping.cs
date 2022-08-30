using Dapper.Contrib.Extensions;
using System;

namespace DutyBoard_Models
{
    public class MappingModel
    {
        [Key]
        public int EmployeeId { get; set; }
        public int RosterId { get; set; }
        public DateTime DateStart { get; set; }
    }
}
