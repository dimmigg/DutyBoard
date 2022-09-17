using DutyBoard_Models.Account;

namespace DutyBoard_DataAccess.Repository.IRepository
{
    public interface ISiteUserRepository : IRepository<SiteUser>
    {
        bool CheckUser(string loginName, string password);
        SiteUser FirstOrDefault(string loginName);
    }
}
