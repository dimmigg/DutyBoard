using DutyBoard_Telegram.Interface;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Telegram;
using DutyBoard_Telegram.Commands;

namespace DutyBoard_Telegram.Services
{
    public class TelegramUserService : ITelegramUserService
    {
        internal readonly ITelegramUserRepository _telegramRepo;
        internal readonly RequestActivationCommand RequestActivationCommand;
        
        public TelegramUserService(ITelegramUserRepository telegramRepo, RequestActivationCommand requestActivationCommand)
        {
            _telegramRepo = telegramRepo;
            RequestActivationCommand = requestActivationCommand;
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

            RequestActivationCommand.RequestActiveTelegramUser(user).GetAwaiter().GetResult();
            return user;
        }
    }
}
