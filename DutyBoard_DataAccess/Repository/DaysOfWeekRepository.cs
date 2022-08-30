﻿using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using Microsoft.Extensions.Configuration;

namespace DutyBoard_DataAccess.Repository
{
    public class DaysOfWeekRepository : Repository<DaysOfWeek>, IDaysOfWeekRepository
    {
        public DaysOfWeekRepository(IConfiguration configuration) : base(configuration)
        {
        }

        //public void Upsert(DaysOfWeek entity)
        //{
        //    if(entity.DayOfWeekId == 0)
        //        Add(entity);
        //    else
        //        Update(entity);
        //}
    }
}