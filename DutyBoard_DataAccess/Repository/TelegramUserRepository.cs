using Dapper;
using DutyBoard_DataAccess.Extensions;
using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Telegram;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DutyBoard_DataAccess.Repository
{
    public class TelegramUserRepository : Repository<TelegramUser>, ITelegramUserRepository
    {
        public TelegramUserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public TelegramUser FirstOrDefault(int? id = null, long? chatId = null)
        {
            var dp = new DynamicParameters();
            dp.Add("Id", id);
            dp.Add("ChatId", chatId);

            using (var connect = GetConnection())
            {

                var rosters = connect.ExecuteProcedure<TelegramUser>("tool.uspDutyBoardGetTelegramUser", dp).FirstOrDefault();
                return rosters;
            }
        }
    }
}
