using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Telegram;
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

        public AdminCommand(ITelegramUserService telegramUserService, TelegramBot telegramBot) : base(telegramUserService, telegramBot)
        {
        }

        public override string Name => CommandNames.Admin;
        
        private async Task EditMessage(Update update)
        {
            var cancellationToken = new CancellationTokenSource();
            var sentMessage = await BotClient.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: "Выберите команду",
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken.Token);
        }

        internal override async Task SendMessage(Update update, TelegramUser user)
        {
            if (user.IsAdmin)
            {
                if (update.CallbackQuery != null)
                {
                    var cancellationToken = new CancellationTokenSource();
                    var sentMessage = await BotClient.SendTextMessageAsync(
                        chatId: user.ChatId,
                        text: "Выберите команду",
                        replyMarkup: GetKeyboard(),
                        cancellationToken: cancellationToken.Token);
                }
                else
                    await EditMessage(update);
            }
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
