using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DutyBoard_Models.Extensions;

namespace DutyBoard_Models.Models
{
    public class Employee
    {
        [Dapper.Contrib.Extensions.Key]
        public int EmployeeId { get; set; }

        [NotMapped]
        [Display(Name = "Имя")]
        [StringLength(20, ErrorMessage = "Имя не должно превышать 50 симовлов!")]
        public string Name
        {
            get => FullName.Decryption();
            set => FullName = value.Encryption();
        }
        public string FullName { get; set; }

        [NotMapped]
        [Display(Name = "Логин")]
        [StringLength(20, ErrorMessage = "Логин не должен превышать 20 симовлов!")]
        public string Login
        {
            get => LoginName.Decryption();
            set => LoginName = value.Encryption();
        }
        public string LoginName { get; set; }

        [NotMapped]
        [Display(Name = "Телефон")]
        [StringLength(20, ErrorMessage = "Телефон не должен превышать 20 симовлов!")]
        public string PhoneNumber
        {
            get => Phone.Decryption(); 
            set => Phone = value.Encryption();
        }

        public string Phone { get; set; }

        [NotMapped]
        public int CountDuty { get; set; } = 0;
    }
}
