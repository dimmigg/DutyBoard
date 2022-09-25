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
        private readonly IEmployeeRepository _empRepo;

        public ListDutyCommand(ITelegramUserRepository telegramRepo,
            IExportRepository exportRepo,
            IEmployeeRepository empRepo,
            TelegramBot telegramBot) : base(telegramRepo)
        {
            _botClient = telegramBot.GetBot().Result;
            _exportRepo = exportRepo;
            _empRepo = empRepo;
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
            var all = _empRepo.GetAll().OrderBy(o => o.Name).GroupBy(x => x.Name);
            foreach (var item in all)
            {
                var duty = item.First();
                sb.Append($"_{duty.Name} ");
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
