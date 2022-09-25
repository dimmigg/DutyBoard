using DutyBoard_Telegram.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Telegram;

namespace DutyBoard_Telegram.Commands.Callback.Admin
{
    public class UsersCommand : BaseCommand
    {
        private readonly ITelegramUserRepository _telegramRepo;

        public UsersCommand(ITelegramUserRepository telegramRepo, ITelegramUserService telegramUserService, TelegramBot telegramBot) : base(telegramUserService, telegramBot)
        {
            _telegramRepo = telegramRepo;
        }

        public override string Name => CommandNames.UsersCallback;

        internal override async Task SendMessage(Update update, TelegramUser user)
        {
            var cancellationToken = new CancellationTokenSource();
            var inlineKeyboard = new InlineKeyboardMarkup(GetArrUsers());
            var sentMessage = await BotClient.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: "Список пользователей бота:",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken.Token);
        }


        private IEnumerable<InlineKeyboardButton[]> GetArrUsers()
        {
            var all = _telegramRepo.GetAll();
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
