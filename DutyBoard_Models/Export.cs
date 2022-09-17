using System;
using System.ComponentModel;

namespace DutyBoard_Models
{
    public class Export
    {
        [Description("День недели")]
        public string DayOfWeekName { get; set; }
        [Description("Начало дежурства")]
        public DateTime DateStart { get; set; }
        [Description("Окончание дежурства")]
        public DateTime DateFinish { get; set; }
        [Description("Дежурный")]
        public string FullName { get; set; }
        [Description("Номер телефона")]
        public string Phone { get; set; }
        [Description("Логин")]
        public string LoginName { get; set; }
    }
}
