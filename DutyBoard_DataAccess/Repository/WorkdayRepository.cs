using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;

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

        public new IEnumerable<Workday> GetAll(int? id)
        {
            var workDays = base.GetAll(id);
            using (var cn = GetConnection())
            {
                foreach (var day in workDays)
                {
                    day.Roster = _rostRepo.FirstOrDefault(day.RosterId);
                    day.Employee = _empRepo.FirstOrDefault(day.EmployeeId);
                }
            }
            return workDays;
        }

        public new Workday FirstOrDefault(int? id)
        {
            var day = base.FirstOrDefault(id);
            using (var cn = GetConnection())
            {
                day.Roster = _rostRepo.FirstOrDefault(day.RosterId);
                day.Employee = _empRepo.FirstOrDefault(day.EmployeeId);
                
            }
            return day;
        }
    }
}
