using DutyBoard_DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Dapper;
using DutyBoard_DataAccess.Extensions;
using DutyBoard_Models.Models;

namespace DutyBoard_DataAccess.Repository
{
    public class MappingRepository : Repository<Mapping>, IMappingRepository
    {
        private readonly IRosterRepository _rostRepo;
        private readonly IEmployeeRepository _empRepo;
        public MappingRepository(IConfiguration configuration,
                                 IRosterRepository rostRepo,
                                 IEmployeeRepository empRepo) : base(configuration)
        {
            _rostRepo = rostRepo;
            _empRepo = empRepo;
        }
        public new IEnumerable<Mapping> GetAll(int? id)
        {
            var mappings = base.GetAll(id);
            using (var cn = GetConnection())
            {
                foreach (var map in mappings)
                {
                    map.Employee = _empRepo.FirstOrDefault(map.EmployeeId);
                    map.Roster = _rostRepo.FirstOrDefault(map.RosterId);
                }
            }
            return mappings;
        }

        public new Mapping FirstOrDefault(int? id)
        {
            var map = base.FirstOrDefault(id);
            map.Employee = _empRepo.FirstOrDefault(map.EmployeeId);
            map.Roster = _rostRepo.FirstOrDefault(map.RosterId);
            return map;
        }

        public void Update(string mapp, string emp)
        {
            using (var connect = GetConnection())
            {
                var dp = new DynamicParameters();
                dp.Add("@MappId", mapp);
                dp.Add("@EmployeeId", emp);
                connect.ExecuteProcedure<string>("tool.uspDutyBoardEditMapping", dp);
            }
        }
    }
}
