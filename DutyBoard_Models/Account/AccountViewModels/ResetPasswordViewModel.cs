using System.ComponentModel.DataAnnotations;

namespace DutyBoard_Models.Account.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [EmailAddress(ErrorMessage = "Введи корректный Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [StringLength(100, ErrorMessage = "Пароль должен быть от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Не сооветствует требованиям")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Не сооветствует требованиям")]
        [Display(Name = "Повторите пароль")]
        [StringLength(100, ErrorMessage = "Пароль должен быть от {2} до {1} символов", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
