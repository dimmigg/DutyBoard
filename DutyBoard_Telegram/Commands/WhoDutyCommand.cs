using DutyBoard_DataAccess.Repository.IRepository;
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
        private readonly TelegramBotClient _botClient;
        private readonly IExportRepository _exportRepo;

        public WhoDutyCommand(ITelegramUserRepository telegramRepo, IExportRepository exportRepo, TelegramBot telegramBot) : base(telegramRepo)
        {
            _botClient = telegramBot.GetBot().Result;
            _exportRepo = exportRepo;
        }

        public override string Name => CommandNames.WhoDutyCommand;

        public override async Task ExecuteAsync(Update update)
        {
            var chatId = update.CallbackQuery?.Message.Chat.Id ?? update.Message.Chat.Id;

            await SendMessage(chatId);

        }

        private async Task SendMessage(long chatId)
        {
            var cancellationToken = new CancellationTokenSource();
            var all = _exportRepo.GetAll();
            var duty = all.FirstOrDefault(x => x.DateStart <= DateTime.UtcNow.AddHours(3) && x.DateFinish >= DateTime.UtcNow.AddHours(3));
            if (duty == null)
            {
                var sentMessage1 = await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "_Дежурный не найден_",
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken.Token);
                var sentMessage2 = await _botClient.SendStickerAsync(
                    chatId: chatId,
                    sticker: "CAACAgIAAxkBAAN1Yu04NMOScokSSVMyDt5nNCPDhX8AAhcAA-I3-SnBo7LllX9XlCkE",
                    cancellationToken: cancellationToken.Token);
            }
            else
            {
                var sentMessage1 = await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"\U0001f977_Дежурный:_\n*{duty.Name}*\n\n🛫_Начало дежурства:_\n*{duty.DateStart:dd.MM.yyyy HH:mm}*\n\n🛬_Окончание дежурства:_\n*{duty.DateFinish:dd.MM.yyyy HH:mm}*",
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken.Token);
                var sentMessage2 = await _botClient.SendContactAsync(
                    chatId: chatId,
                    phoneNumber: duty.PhoneNumber,
                    firstName: duty.Name,
                    cancellationToken: cancellationToken.Token);
            }
        }
    }
}
