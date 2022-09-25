using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Models;
using Microsoft.Extensions.Configuration;

namespace DutyBoard_DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
