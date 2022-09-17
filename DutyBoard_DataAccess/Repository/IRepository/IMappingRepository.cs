using DutyBoard_Models.Models;

namespace DutyBoard_DataAccess.Repository.IRepository
{
    public interface IMappingRepository : IRepository<Mapping>
    {
        void Update(string mapp, string emp);
    }
}
