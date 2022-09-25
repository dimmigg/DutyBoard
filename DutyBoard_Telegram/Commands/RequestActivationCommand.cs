using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Telegram.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DutyBoard_Models.Telegram;
using Telegram.Bot;
using DutyBoard_Utility;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DutyBoard_Telegram.Commands
{
    public  class RequestActivationCommand
    {
        private readonly TelegramBotClient _botClient;
        private readonly ITelegramUserRepository _telegramRepo;
        public RequestActivationCommand(ITelegramUserRepository telegramRepo, TelegramBot telegramBot)
        {
            _telegramRepo = telegramRepo;
            _botClient = telegramBot.GetBot().Result;
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
                replyMarkup: GetKeyboard(user),
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken.Token);
        }

        private static InlineKeyboardMarkup GetKeyboard(TelegramUser user)
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
    }
}
