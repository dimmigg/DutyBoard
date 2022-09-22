using DutyBoard_Models.Telegram;
using System.Collections.Generic;

namespace DutyBoard_DataAccess.Repository.IRepository
{
    public interface ITelegramUserRepository : IRepository<TelegramUser>
    {
        TelegramUser FirstOrDefault(int? id = null, long? chatId = null);
    }
}
