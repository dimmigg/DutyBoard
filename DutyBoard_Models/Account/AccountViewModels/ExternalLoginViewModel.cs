using System.ComponentModel.DataAnnotations;

namespace DutyBoard_Models.Account.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [EmailAddress(ErrorMessage = "Введи корректный Email")]
        public string Email { get; set; }
    }
}
