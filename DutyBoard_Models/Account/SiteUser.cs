using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyBoard_Models.Account
{
    public class SiteUser
    {
        [Dapper.Contrib.Extensions.Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Не указан логин")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        public bool IsActive { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
