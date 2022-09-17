using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DutyBoard_DataAccess.Extensions;
using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace DutyBoard_DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
