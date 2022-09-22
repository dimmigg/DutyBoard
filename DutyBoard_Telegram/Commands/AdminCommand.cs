using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Telegram.Interface;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DutyBoard_Telegram.Commands
{
    public class AdminCommand : BaseCommand
    {
        private readonly TelegramBotClient _botClient;
        private readonly ITelegramUserService _telegramUserService;

        public AdminCommand(ITelegramUserRepository telegramRepo, ITelegramUserService telegramUserService, TelegramBot telegramBot) : base(telegramRepo)
        {
            _telegramUserService = telegramUserService;
            _botClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.Admin;

        public override async Task ExecuteAsync(Update update)
        {
            var user = _telegramUserService.GetOrCreate(update);
            if (user.IsAdmin)
            {
                if (update.CallbackQuery == null)
                {
                    await SendMessage(user.ChatId);
                }
                else
                {
                    await EditMessage(update);
                }
            }

        }

        private async Task EditMessage(Update update)
        {
            var cancellationToken = new CancellationTokenSource();
            var sentMessage = await _botClient.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: "Выберите команду",
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken.Token);
        }

        private async Task SendMessage(long chatId)
        {
            var cancellationToken = new CancellationTokenSource();
            var sentMessage = await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Выберите команду",
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken.Token);
        }

        private static InlineKeyboardMarkup GetKeyboard()
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                // first row
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: CommandNames.UsersText,
                        callbackData: CommandNames.UsersCallback),
                    InlineKeyboardButton.WithCallbackData(text: "Дежурные", callbackData: "Employees"),
                },
                // second row
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Запуск расчета", callbackData: "Calc"),
                    InlineKeyboardButton.WithCallbackData(text: "Экспорт файла", callbackData: "Export"),
                },
            });
            return inlineKeyboard;
        }
    }
}
