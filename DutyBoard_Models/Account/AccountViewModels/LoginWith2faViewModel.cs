using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutyBoard_Models.Account.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [StringLength(7, ErrorMessage = "Код аутентификатора должен быть от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Код аутентификатора")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Запомни это устройство")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
