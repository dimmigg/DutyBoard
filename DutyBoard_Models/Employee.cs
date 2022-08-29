using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutyBoard_Models
{
    public class Employee
    {
        [Dapper.Contrib.Extensions.Key]
        [HiddenInput(DisplayValue = false)]
        public int EmployeeId { get; set; }
        [Display(Name = "Полное имя")]
        [Required(ErrorMessage = "Необходимо ввести имя")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 симовлов")]
        public string FullName { get; set; }
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Необходимо ввести логин")]
        [StringLength(20, ErrorMessage = "Логин не должен превышать 20 симовлов")]
        public string LoginName { get; set; }
        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Необходимо ввести телефон")]
        [StringLength(15, ErrorMessage = "Телефон не должен превышать 15 симовлов")]
        public string Phone { get; set; }

        [NotMapped]
        [HiddenInput(DisplayValue = false)]
        public int CountDuty { get; set; } = 0;

        [NotMapped]
        [HiddenInput(DisplayValue = false)]
        public string ShortName => $"{FullName.Split()[0]} {FullName.Split()[1][0]}.";
    }
}
