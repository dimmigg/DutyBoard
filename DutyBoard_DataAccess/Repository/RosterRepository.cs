using DutyBoard_DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using Dapper;
using DutyBoard_DataAccess.Extensions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using DutyBoard_Models.Models;

namespace DutyBoard_DataAccess.Repository
{
    public class RosterRepository : Repository<Roster>, IRosterRepository
    {
        private readonly IDaysOfWeekRepository _daysRepo;
        public RosterRepository(IConfiguration configuration, IDaysOfWeekRepository daysRepo) : base(configuration)
        {
            _daysRepo = daysRepo;
        }

        public IEnumerable<Roster> GetAll(int? id = null, int? daysOfWeekId = null)
        {
            var dp = new DynamicParameters();
            dp.Add("RosterId", id);
            dp.Add("DaysOfWeekId", daysOfWeekId);

            using (var connect = GetConnection())
            {

                var rosters = connect.ExecuteProcedure<Roster>("tool.uspDutyBoardGetRoster", dp);
                foreach (var ros in rosters)
                {
                    ros.DaysOfWeek = _daysRepo.FirstOrDefault(ros.DaysOfWeekId);
                }

                return rosters;
            }
        }
        public new Roster FirstOrDefault(int? id)
        {
            var ros = base.FirstOrDefault(id);
            if (ros == null)
                return new Roster();
            ros.DaysOfWeek = _daysRepo.FirstOrDefault(ros.DaysOfWeekId);
            return ros;
        }
    }
}
