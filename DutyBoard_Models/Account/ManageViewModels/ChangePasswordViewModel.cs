using System.ComponentModel.DataAnnotations;

namespace DutyBoard_Models.Account.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [DataType(DataType.Password, ErrorMessage = "Не сооветствует требованиям")]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [StringLength(100, ErrorMessage = "Пароль должен быть от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Не сооветствует требованиям")]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
