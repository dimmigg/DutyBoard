using DutyBoard_Models.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DutyBoard_Models
{
    [HolidayDateComparsion]
    public class Holiday
    {
        [Dapper.Contrib.Extensions.Key]
        [HiddenInput(DisplayValue = false)]
        public int HolidayId { get; set; }

        public int EmployeeId { get; set; }

        [NotMapped]
        public Employee Employee { get; set; }


        [Display(Name = "Начало отпуска")]
        [Required(ErrorMessage = "Необходимо выбрать день")]
        public DateTime DateStart { get; set; } = DateTime.Today;

        [Display(Name = "Окончание отпуска")]
        [Required(ErrorMessage = "Необходимо выбрать день")]
        public DateTime DateFinish { get; set; } = DateTime.Today;

    }
}
