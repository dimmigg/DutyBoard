using System.ComponentModel.DataAnnotations;

namespace DutyBoard_Models.Account.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [EmailAddress(ErrorMessage = "Введи корректный Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [StringLength(100, ErrorMessage = "Пароль должен быть от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Не сооветствует требованиям")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Не сооветствует требованиям")]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
