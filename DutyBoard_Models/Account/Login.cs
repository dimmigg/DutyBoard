using System.ComponentModel.DataAnnotations;

namespace DutyBoard_Models.Account
{
    public class Login
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
