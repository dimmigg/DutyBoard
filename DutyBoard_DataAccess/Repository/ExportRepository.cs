using DutyBoard_DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using DutyBoard_Models.Models;

namespace DutyBoard_DataAccess.Repository
{
    public class ExportRepository : Repository<Export>, IExportRepository
    {
        public ExportRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
