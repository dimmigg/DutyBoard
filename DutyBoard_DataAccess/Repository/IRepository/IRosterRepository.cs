using DutyBoard_Models;
using System.Collections.Generic;

namespace DutyBoard_DataAccess.Repository.IRepository
{
    public interface IRosterRepository : IRepository<Roster>
    {
        IEnumerable<Roster> GetAll(int? id = null, int? daysOfWeekId = null);
    }
}
