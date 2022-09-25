using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Telegram;
using DutyBoard_Telegram.Interface;
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
        private readonly IEmployeeRepository _empRepo;

        public ListDutyCommand(ITelegramUserService telegramUserService,
            IEmployeeRepository empRepo,
            TelegramBot telegramBot) : base(telegramUserService, telegramBot)
        {
            _empRepo = empRepo;
        }

        public override string Name => CommandNames.ListDuty;

        internal override async Task SendMessage(Update update, TelegramUser user)
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
            var sentMessage1 = await BotClient.SendTextMessageAsync(
                chatId: user.ChatId,
                text: sb.ToString(),
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken.Token);
        }
    }
}
