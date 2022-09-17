using DutyBoard_Models;
using Telegram.Bot.Types;

namespace DutyBoard_Telegram.Interface
{
    public interface ITelegramUserService
    {
        TelegramUser GetOrCreate(Update update);
    }
}
