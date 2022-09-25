using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutyBoard_Models.Account.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [EmailAddress(ErrorMessage = "Введи корректный Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [DataType(DataType.Password, ErrorMessage = "Не сооветствует требованиям")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
