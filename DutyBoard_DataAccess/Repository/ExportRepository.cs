using DutyBoard_DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using DutyBoard_DataAccess.Extensions;
using DutyBoard_Models.Models;

namespace DutyBoard_DataAccess.Repository
{
    public class ExportRepository : Repository<Export>, IExportRepository
    {
        public ExportRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
