using System;
using System.ComponentModel.DataAnnotations;

namespace DutyBoard_Models.ViewModels
{
    public class ConfHomeVM
    {
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
    }
}
