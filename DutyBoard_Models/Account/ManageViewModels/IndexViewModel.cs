using System.ComponentModel.DataAnnotations;

namespace DutyBoard_Models.Account.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [EmailAddress(ErrorMessage = "Введи корректный Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
