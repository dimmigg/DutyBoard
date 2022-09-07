using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

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
        public new IEnumerable<Mapping> GetAll(Func<Mapping, bool> filter = null)
        {
            var mappings = base.GetAll(filter);
            using (SqlConnection cn = GetConnection())
            {
                foreach (var map in mappings)
                {
                    map.Employee = _empRepo.FirstOrDefault(x => x.EmployeeId == map.EmployeeId);
                    map.Roster = _rostRepo.FirstOrDefault(x => x.RosterId == map.RosterId);
                }
            }
            return mappings;
        }

        public new Mapping FirstOrDefault(Func<Mapping, bool> filter = null)
        {
            var map = base.FirstOrDefault(filter);
            map.Employee = _empRepo.FirstOrDefault(x => x.EmployeeId == map.EmployeeId);
            map.Roster = _rostRepo.FirstOrDefault(x => x.RosterId == map.RosterId);
            return map;
        }
    }
}
