using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DutyBoard_Utility;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using DutyBoard_Telegram.Interface;
using DutyBoard_Models.Telegram;

namespace DutyBoard_Telegram.Commands
{
    public class HelpCommand : BaseCommand
    {

        public HelpCommand(ITelegramUserService telegramUserService, TelegramBot telegramBot) : base(telegramUserService, telegramBot)
        {
        }

        public override string Name => CommandNames.Help;

        internal override async Task SendMessage(Update update, TelegramUser user)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Бот создан для поиска текщего дежурного.");
            sb.AppendLine($"Редактирование дежурств происходит на [сайте]({WC.UrlTG})");

            var cancellationToken = new CancellationTokenSource();
            var sentMessage1 = await BotClient.SendTextMessageAsync(
                chatId: user.ChatId,
                text: sb.ToString(),
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken.Token);
        }
    }
}
