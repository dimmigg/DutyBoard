using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Models;
using Microsoft.Extensions.Configuration;

namespace DutyBoard_DataAccess.Repository
{
    public class DaysOfWeekRepository : Repository<DaysOfWeek>, IDaysOfWeekRepository
    {
        public DaysOfWeekRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
