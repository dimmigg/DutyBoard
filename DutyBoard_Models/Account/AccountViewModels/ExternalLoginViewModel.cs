using System.ComponentModel.DataAnnotations;

namespace DutyBoard_Models.Account.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "���� ����������� ��� �����")]
        [EmailAddress(ErrorMessage = "����� ���������� Email")]
        public string Email { get; set; }
    }
}
