using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutyBoard_Models.Account.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [DataType(DataType.Text)]
        [Display(Name = "Код восстановления")]
        public string RecoveryCode { get; set; }
    }
}
