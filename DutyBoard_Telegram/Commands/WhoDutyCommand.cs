using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Telegram;
using DutyBoard_Telegram.Interface;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DutyBoard_Telegram.Commands
{
    public class WhoDutyCommand : BaseCommand
    {
        private readonly IExportRepository _exportRepo;

        public WhoDutyCommand(ITelegramUserService telegramUserService, IExportRepository exportRepo, TelegramBot telegramBot) : base(telegramUserService, telegramBot)
        {
            _exportRepo = exportRepo;
        }

        public override string Name => CommandNames.WhoDutyCommand;

        internal override async Task SendMessage(Update update, TelegramUser user)
        {
            var cancellationToken = new CancellationTokenSource();
            var all = _exportRepo.GetAll();
            var duty = all.FirstOrDefault(x => x.DateStart <= DateTime.UtcNow.AddHours(3) && x.DateFinish >= DateTime.UtcNow.AddHours(3));
            if (duty == null)
            {
                var sentMessage1 = await BotClient.SendTextMessageAsync(
                    chatId: user.ChatId,
                    text: "_Дежурный не найден_",
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken.Token);
                var sentMessage2 = await BotClient.SendStickerAsync(
                    chatId: user.ChatId,
                    sticker: "CAACAgIAAxkBAAN1Yu04NMOScokSSVMyDt5nNCPDhX8AAhcAA-I3-SnBo7LllX9XlCkE",
                    cancellationToken: cancellationToken.Token);
            }
            else
            {
                var sentMessage1 = await BotClient.SendTextMessageAsync(
                    chatId: user.ChatId,
                    text: $"\U0001f977_Дежурный:_\n*{duty.Name}*\n\n🛫_Начало дежурства:_\n*{duty.DateStart:dd.MM.yyyy HH:mm}*\n\n🛬_Окончание дежурства:_\n*{duty.DateFinish:dd.MM.yyyy HH:mm}*",
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken.Token);
                var sentMessage2 = await BotClient.SendContactAsync(
                    chatId: user.ChatId,
                    phoneNumber: duty.PhoneNumber,
                    firstName: duty.Name,
                    cancellationToken: cancellationToken.Token);
            }
        }
    }
}
