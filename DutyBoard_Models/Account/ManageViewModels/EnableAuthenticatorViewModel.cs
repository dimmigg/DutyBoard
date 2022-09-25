using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutyBoard_Models.Account.ManageViewModels
{
    public class EnableAuthenticatorViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [StringLength(7, ErrorMessage = "Код верификации должен быть от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Код верификации")]
        public string Code { get; set; }

        [ReadOnly(true)]
        public string SharedKey { get; set; }

        public string AuthenticatorUri { get; set; }
    }
}
