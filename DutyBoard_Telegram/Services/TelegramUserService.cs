using DutyBoard_Telegram.Interface;
using System.Threading.Tasks;
using DutyBoard_Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using DutyBoard_DataAccess.Repository.IRepository;

namespace DutyBoard_Telegram.Services
{
    public class TelegramUserService : ITelegramUserService
    {
        internal readonly ITelegramUserRepository _telegramRepo;
        public TelegramUserService(ITelegramUserRepository telegramRepo)
        {
            _telegramRepo = telegramRepo;
        }
        public TelegramUser GetOrCreate(Update update)
        {
            var newUser = update.Type switch
            {
                UpdateType.CallbackQuery => new TelegramUser
                {
                    Username = update.CallbackQuery.From.Username,
                    ChatId = update.CallbackQuery.Message.Chat.Id,
                    FirstName = update.CallbackQuery.Message.From.FirstName,
                    LastName = update.CallbackQuery.Message.From.LastName
                },
                UpdateType.Message => new TelegramUser
                {
                    Username = update.Message.Chat.Username,
                    ChatId = update.Message.Chat.Id,
                    FirstName = update.Message.Chat.FirstName,
                    LastName = update.Message.Chat.LastName
                }
            };
            var user = _telegramRepo.FirstOrDefault(chatId: newUser.ChatId);
            if (user != null) return user;

            _telegramRepo.Upsert(newUser);
            user = _telegramRepo.FirstOrDefault(chatId: newUser.ChatId);

            return user;
        }
    }
}
