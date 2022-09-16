using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using DutyBoard_DataAccess.Extensions;

namespace DutyBoard_DataAccess.Repository
{
    public class ExportRepository : Repository<ExportModel>, IExportRepository
    {
        public ExportRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public new IEnumerable<ExportModel> GetAll(Func<ExportModel, bool> filter = null)
        {
            using (SqlConnection cn = GetConnection())
            {
                return cn.ExecuteProcedure<ExportModel>($"tool.uspDutyBoardGetExportTable");
            }
        }
    }
}
