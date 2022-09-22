using System;
using System.ComponentModel;
using DutyBoard_Models.Extensions;

namespace DutyBoard_Models.Models
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
        public string Name
        {
            get => FullName.Decryption();
            set => FullName = value.Encryption();
        }
        [Description("Номер телефона")]
        public string PhoneNumber
        {
            get => Phone.Decryption();
            set => Phone = value.Encryption();
        }
        [Description("Логин")]
        public string Login
        {
            get => LoginName.Decryption();
            set => LoginName = value.Encryption();
        }

        public string FullName { get; set; }
        public string Phone { get; set; }
        public string LoginName { get; set; }
    }
}
