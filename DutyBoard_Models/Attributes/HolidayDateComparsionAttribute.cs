using DutyBoard_Models;
using System.ComponentModel.DataAnnotations;

namespace DutyBoard_Models.Attributes
{
    public class HolidayDateComparsionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is Holiday day)) return false;
            if (day.DateStart <= day.DateFinish) return true;
            ErrorMessage = "Дата начала отпуска должна быть меньше даты окончания.";
            return false;
        }
    }
}
