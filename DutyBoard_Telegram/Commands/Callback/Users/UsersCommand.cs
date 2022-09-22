using DutyBoard_Telegram.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using DutyBoard_DataAccess.Repository.IRepository;

namespace DutyBoard_Telegram.Commands.Callback.Users
{
    public class UsersCommand : BaseCommand
    {
        private readonly TelegramBotClient _botClient;
        private readonly ITelegramUserService _telegramUserService;

        public UsersCommand(ITelegramUserRepository telegramRepo, ITelegramUserService telegramUserService, TelegramBot telegramBot) : base(telegramRepo)
        {
            _telegramUserService = telegramUserService;
            _botClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.UsersCallback;

        public override async Task ExecuteAsync(Update update)
        {
            var user = _telegramUserService.GetOrCreate(update);
            if (user.IsAdmin)
                await SendMessage(update);
        }

        private async Task SendMessage(Update update)
        {
            var cancellationToken = new CancellationTokenSource();
            var inlineKeyboard = new InlineKeyboardMarkup(GetArrUsers());
            var sentMessage = await _botClient.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: "Список пользователей бота:",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken.Token);
        }


        private IEnumerable<InlineKeyboardButton[]> GetArrUsers()
        {
            var all = TelegramRepo.GetAll();
            var result = new InlineKeyboardButton[all.Count() + 1][];
            var i = 0;
            foreach (var userBot in all)
            {
                result[i++] = new[] { InlineKeyboardButton.WithCallbackData(userBot.ToString(), userBot.Id.ToString()) };
            }

            result[all.Count()] = new[] { InlineKeyboardButton.WithCallbackData("Назад", CommandNames.Admin) };

            return result;
        }
    }
}
