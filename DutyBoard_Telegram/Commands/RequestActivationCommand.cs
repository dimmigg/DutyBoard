using DutyBoard_DataAccess.Repository.IRepository;
using System.Linq;
using DutyBoard_Models.Telegram;
using Telegram.Bot;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using DutyBoard_Models.Account;

namespace DutyBoard_Telegram.Commands
{
    public  class RequestActivationCommand
    {
        private readonly TelegramBotClient _botClient;
        private readonly ITelegramUserRepository _telegramRepo;
        public RequestActivationCommand(ITelegramUserRepository telegramRepo, TelegramBot telegramBot)
        {
            _telegramRepo = telegramRepo;
            _botClient = telegramBot.GetBot().GetAwaiter().GetResult();
        }

        public async Task RequestActiveTelegramUser(TelegramUser user)
        {
            var recipients = _telegramRepo.GetAll().Where(x => x.IsAdmin);

            foreach (var rec in recipients)
            {
                await SendMessageActiveTelegramUser(rec, user);
            }
        }

        internal async Task SendMessageActiveTelegramUser(TelegramUser recipient, TelegramUser user)
        {
            var cancellationToken = new CancellationTokenSource();
            var sentMessage = await _botClient.SendTextMessageAsync(
                chatId: recipient.ChatId,
                text: $"В Телеграме новый запрос от пользователя {user.Username}",
                replyMarkup: GetAcceptBtnTelegram(user),
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken.Token);
        }

        private static InlineKeyboardMarkup GetAcceptBtnTelegram(TelegramUser user)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Активировать",
                        callbackData: $"{CommandNames.TelegramActivateCommand}_{user.Id}" )
                }
            });
        }



        public async Task RequestActiveSiteUser(ApplicationUser user)
        {
            var recipients = _telegramRepo.GetAll().Where(x => x.IsAdmin);

            foreach (var rec in recipients)
            {
                await SendMessageActiveSiteUser(rec, user);
            }
        }

        internal async Task SendMessageActiveSiteUser(TelegramUser recipient, ApplicationUser user)
        {
            var cancellationToken = new CancellationTokenSource();
            var sentMessage = await _botClient.SendTextMessageAsync(
                chatId: recipient.ChatId,
                text: $"На сайте новый запрос от пользователя {user.Email}",
                replyMarkup: GetAcceptBtnSite(user),
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken.Token);
        }

        private static InlineKeyboardMarkup GetAcceptBtnSite(ApplicationUser user)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Активировать",
                        callbackData: $"{CommandNames.SiteActivateCommand}_{user.Id}" )
                }
            });
        }
    }
}
