using DutyBoard_DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DutyBoard_Models.Models;

namespace DutyBoard_DataAccess.Repository
{
    public class HolidayRepository : Repository<Holiday>, IHolidayRepository
    {
        private readonly IEmployeeRepository _empRepo;
        public HolidayRepository(IConfiguration configuration, IEmployeeRepository empRepo) : base(configuration)
        {
            _empRepo = empRepo;
        }

        public new IEnumerable<Holiday> GetAll(int? id)
        {
            var holiDays = base.GetAll(id);
            using (var cn = GetConnection())
            {
                foreach (var day in holiDays)
                {
                    day.Employee = _empRepo.FirstOrDefault(day.EmployeeId);
                }
            }
            return holiDays;
        }

        public new Holiday FirstOrDefault(int? id)
        {
            var day = base.FirstOrDefault(id);
            day.Employee = _empRepo.FirstOrDefault(day.EmployeeId);
            return day;
        }
    }
}
