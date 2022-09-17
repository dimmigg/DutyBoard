using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyBoard_Models.Models
{
    public class Employee
    {
        [Dapper.Contrib.Extensions.Key]
        public int EmployeeId { get; set; }
        [Display(Name = "Имя")]
        [StringLength(20, ErrorMessage = "Имя не должно превышать 50 симовлов!")]
        public string FullName { get; set; }

        [Display(Name = "Логин")]
        [StringLength(20, ErrorMessage = "Логин не должен превышать 20 симовлов!")]
        public string LoginName { get; set; }

        [Display(Name = "Телефон")]
        [StringLength(20, ErrorMessage = "Телефон не должен превышать 20 симовлов!")]
        public string Phone { get; set; }

        [NotMapped]
        public int CountDuty { get; set; } = 0;
    }
}
