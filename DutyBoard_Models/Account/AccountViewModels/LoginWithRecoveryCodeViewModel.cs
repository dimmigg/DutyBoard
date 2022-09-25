using System.ComponentModel.DataAnnotations;

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
