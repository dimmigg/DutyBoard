using DutyBoard_DataAccess.Repository.IRepository;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DutyBoard_Telegram.Commands
{
    public class ListDutyCommand : BaseCommand
    {
        private readonly TelegramBotClient _botClient;
        private readonly IExportRepository _exportRepo;

        public ListDutyCommand(ITelegramUserRepository telegramRepo, IExportRepository exportRepo, TelegramBot telegramBot) : base(telegramRepo)
        {
            _botClient = telegramBot.GetBot().Result;
            _exportRepo = exportRepo;
        }

        public override string Name => CommandNames.ListDuty;

        public override async Task ExecuteAsync(Update update)
        {
            var chatId = update.CallbackQuery?.Message.Chat.Id ?? update.Message.Chat.Id;

            await SendMessage(chatId);

        }

        private async Task SendMessage(long chatId)
        {
            var sb = new StringBuilder();
            sb.AppendLine("📄 *Список дежурных:*");
            var cancellationToken = new CancellationTokenSource();
            var all = _exportRepo.GetAll().OrderBy(o => o.Name).GroupBy(x => x.Name);
            foreach (var item in all)
            {
                var duty = item.First();
                sb.Append($"_{duty.Name} ");
                //var num = Convert.ToInt64(duty.PhoneNumber.Replace(" ", "").Replace("-", ""));
                //sb.AppendLine($"{num:+#(###)###-##-##}_");
                sb.AppendLine($"{duty.PhoneNumber}_");
            }
            var sentMessage1 = await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: sb.ToString(),
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken.Token);
        }
    }
}
