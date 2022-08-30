using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DutyBoard_DataAccess.Repository
{
    public class RosterRepository : Repository<Roster>, IRosterRepository
    {
        private readonly IDaysOfWeekRepository _daysRepo;
        public RosterRepository(IConfiguration configuration, IDaysOfWeekRepository daysRepo) : base(configuration)
        {
            _daysRepo = daysRepo;
        }

        public new IEnumerable<Roster> GetAll(Func<Roster, bool> filter = null)
        {
            var rosters = base.GetAll(filter);
            using (SqlConnection cn = GetConnection())
            {
                foreach (var ros in rosters)
                {
                    ros.DaysOfWeek = _daysRepo.FirstOrDefault(x => x.DayOfWeekId == ros.Days    OfWeekId);
                }
            }
            return rosters;
        }

        public new Roster FirstOrDefault(Func<Roster, bool> filter = null)
        {
            var ros = base.FirstOrDefault(filter);
            ros.DaysOfWeek = _daysRepo.FirstOrDefault(x => x.DayOfWeekId == ros.DaysOfWeekId);
            return ros;
        }

        //public void Upsert(Roster entity)
        //{
        //    if (entity.RosterId == 0)
        //        Add(entity);
        //    else
        //        Update(entity);
        //}
    }
}
