using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DutyBoard_DataAccess.Repository
{
    public class HolidayRepository : Repository<Holiday>, IHolidayRepository
    {
        private readonly IEmployeeRepository _empRepo;
        public HolidayRepository(IConfiguration configuration, IEmployeeRepository empRepo) : base(configuration)
        {
            _empRepo = empRepo;
        }

        public new IEnumerable<Holiday> GetAll(Func<Holiday, bool> filter = null)
        {
            var holiDays = base.GetAll(filter);
            using (SqlConnection cn = GetConnection())
            {
                foreach (var day in holiDays)
                {
                    day.Employee = _empRepo.FirstOrDefault(x => x.EmployeeId == day.EmployeeId);
                }
            }
            return holiDays;
        }

        public new Holiday FirstOrDefault(Func<Holiday, bool> filter = null)
        {
            var day = base.FirstOrDefault(filter);
            day.Employee = _empRepo.FirstOrDefault(x => x.EmployeeId == day.EmployeeId);
            return day;
        }

        //public void Upsert(Holiday entity)
        //{
        //    if (entity.HolidayId == 0)
        //        Add(entity);
        //    else
        //        Update(entity);
        //}
    }
}
