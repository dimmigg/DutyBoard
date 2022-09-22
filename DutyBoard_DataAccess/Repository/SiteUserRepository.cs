using DutyBoard_DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;
using DutyBoard_DataAccess.Extensions;
using DutyBoard_Models.Account;
using DutyBoard_Models.Models;
using Dapper;
using DutyBoard_Models.Telegram;

namespace DutyBoard_DataAccess.Repository
{
    public class SiteUserRepository : Repository<SiteUser>, ISiteUserRepository
    {
        public SiteUserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public bool CheckUser(string loginName, string password)
        {
            var dp = new DynamicParameters();
            dp.Add("LoginName", loginName);
            dp.Add("Password", password);

            using (var connect = GetConnection())
            {
                return connect.ExecuteProcedure<bool>("tool.uspDutyBoardCheckUser", dp).FirstOrDefault();
            }
        }

        public SiteUser FirstOrDefault(string loginName)
        {
            var dp = new DynamicParameters();
            dp.Add("LoginName", loginName);

            using (var connect = GetConnection())
            {
                return connect.ExecuteProcedure<SiteUser>("tool.uspDutyBoardGetSiteUser", dp).FirstOrDefault();
            }
        }
    }
}
