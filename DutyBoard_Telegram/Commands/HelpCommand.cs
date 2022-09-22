using DutyBoard_DataAccess.Repository.IRepository;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DutyBoard_Utility;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DutyBoard_Telegram.Commands
{
    public class HelpCommand : BaseCommand
    {
        private readonly TelegramBotClient _botClient;

        public HelpCommand(ITelegramUserRepository telegramRepo, TelegramBot telegramBot) : base(telegramRepo)
        {
            _botClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.Help;

        public override async Task ExecuteAsync(Update update)
        {
            var chatId = update.CallbackQuery?.Message.Chat.Id ?? update.Message.Chat.Id;

            await SendMessage(chatId);

        }

        private async Task SendMessage(long chatId)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Бот создан для поиска текщего дежурного.");
            sb.AppendLine($"Редактирование дежурств происходит на [сайте]({WC.UrlTG})");

            var cancellationToken = new CancellationTokenSource();
            var sentMessage1 = await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: sb.ToString(),
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken.Token);
        }
    }
}
