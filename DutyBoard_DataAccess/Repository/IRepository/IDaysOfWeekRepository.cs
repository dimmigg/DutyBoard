using DutyBoard_Models;

namespace DutyBoard_DataAccess.Repository.IRepository
{
    public interface IDaysOfWeekRepository :IRepository<DaysOfWeek>
    {
        void Upsert(DaysOfWeek entity);
    }
}
