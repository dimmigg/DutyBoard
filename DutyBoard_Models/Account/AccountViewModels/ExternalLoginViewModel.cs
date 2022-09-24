using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutyBoard_Models.Account.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "���� ����������� ��� �����")]
        [EmailAddress(ErrorMessage = "����� ���������� Email")]
        public string Email { get; set; }
    }
}
