using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DutyBoard_DataAccess.Repository
{
    public class WorkdayRepository : Repository<Workday>, IWorkdayRepository
    {
        private readonly IRosterRepository _rostRepo;
        private readonly IEmployeeRepository _empRepo;
        public WorkdayRepository(IConfiguration configuration, 
                                 IRosterRepository rostRepo,
                                 IEmployeeRepository empRepo) : base(configuration)
        {
            _rostRepo = rostRepo;
            _empRepo = empRepo;
        }

        public new IEnumerable<Workday> GetAll(Func<Workday, bool> filter = null)
        {
            var workDays = base.GetAll(filter);
            using (SqlConnection cn = GetConnection())
            {
                foreach (var day in workDays)
                {
                    day.Roster = _rostRepo.FirstOrDefault(x => x.RosterId == day.RosterId);
                    day.Employee = _empRepo.FirstOrDefault(x => x.EmployeeId == day.EmployeeId);
                }
            }
            return workDays;
        }

        public new Workday FirstOrDefault(Func<Workday, bool> filter = null)
        {
            var day = base.FirstOrDefault(filter);
            using (SqlConnection cn = GetConnection())
            {
                day.Roster = _rostRepo.FirstOrDefault(x => x.RosterId == day.RosterId);
                day.Employee = _empRepo.FirstOrDefault(x => x.EmployeeId == day.EmployeeId);
                
            }
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
