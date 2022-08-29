using DutyBoard_Models;

namespace DutyBoard_DataAccess.Repository.IRepository
{
    public interface IRosterRepository : IRepository<Roster>
    {
        void Upsert(Roster entity);
    }
}
