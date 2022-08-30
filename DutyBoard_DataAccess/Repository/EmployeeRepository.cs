using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using Microsoft.Extensions.Configuration;

namespace DutyBoard_DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        //public void Upsert(Employee entity)
        //{
        //    if(entity.EmployeeId == 0)
        //        Add(entity);
        //    else
        //        Update(entity);
        //}
    }
}
