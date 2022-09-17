using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Telegram.Interface;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DutyBoard_Telegram.Commands
{
    public class GetAnalyticCommand : BaseCommand
    {
        private readonly ITelegramUserService _telegramUserService;
        private readonly TelegramBotClient _telegramBotClient;

        public GetAnalyticCommand(ITelegramUserRepository telegramRepo, ITelegramUserService telegramUserService, TelegramBot telegramBot) : base(telegramRepo)
        {
            _telegramUserService = telegramUserService;
            _telegramBotClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.GetAnalyticsCommand;
        public override async Task ExecuteAsync(Update update)
        {
            var user = _telegramUserService.GetOrCreate(update);
            var daysString = update.CallbackQuery?.Data?.Replace("analytic-", "") ?? "0";
            var days = int.Parse(daysString);
            var analyticData = "Получить аналитику";

            await _telegramBotClient.SendTextMessageAsync(user.ChatId, analyticData, ParseMode.Markdown);
        }
    }
}
