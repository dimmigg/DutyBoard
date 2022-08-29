using DutyBoard_Models;

namespace DutyBoard_DataAccess.Repository.IRepository
{
    public interface IHolidayRepository : IRepository<Holiday>
    {
        void Upsert(Holiday entity);
    }
}
