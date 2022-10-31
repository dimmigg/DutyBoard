using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Telegram;
using DutyBoard_Telegram.Interface;
using DutyBoard_Utility.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DutyBoard_Telegram.Commands
{
    public class UserListDutyCommand : BaseCommand
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly IMappingRepository _mappRepo;

        public UserListDutyCommand(ITelegramUserService telegramUserService,
            IEmployeeRepository empRepo,
            IMappingRepository mappRepo,
            TelegramBot telegramBot) : base(telegramUserService, telegramBot)
        {
            _empRepo = empRepo;
            _mappRepo = mappRepo;
        }

        public override string Name => CommandNames.UserListDuty;

        private async Task EditMessage(Update update)
        {
            var cancellationToken = new CancellationTokenSource();
            var sentMessage = await BotClient.EditMessageTextAsync(
                chatId: update.CallbackQuery.Message.Chat.Id,
                messageId: update.CallbackQuery.Message.MessageId,
                text: GetDutyWithLogin(update.CallbackQuery.Data.Split("__")[1]),
                parseMode: ParseMode.Markdown,
                //replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken.Token);
        }

        internal override async Task SendMessage(Update update, TelegramUser user)
        {
            if (update.CallbackQuery == null)
            {
                var cancellationToken = new CancellationTokenSource();
                var sentMessage = await BotClient.SendTextMessageAsync(
                    chatId: user.ChatId,
                    text: "Выберите дежурного:",
                    replyMarkup: GetKeyboard(),
                    cancellationToken: cancellationToken.Token);
            }
            else
                await EditMessage(update);
        }

        private InlineKeyboardMarkup GetKeyboard()
        {
            var all = _empRepo.GetAll().OrderBy(o => o.Name);

            var inlineKeyboards = new List<InlineKeyboardButton[]>();

            foreach (var item in all)
            {
                inlineKeyboards.Add(new[] {InlineKeyboardButton.WithCallbackData(text: item.Name,
                        callbackData: $"{CommandNames.UserListDuty}__{item.Login}") });
            }

            var inlineKeyboard = new InlineKeyboardMarkup(inlineKeyboards);
            return inlineKeyboard;
        }

        private string GetDutyWithLogin(string login)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Дежурства *{_empRepo.GetAll().First(x => x.Login == login).Name}:*");
            sb.AppendLine();

            var duty = _mappRepo.GetAll().Where(x => x.Employee.Login == login);

            foreach (var item in duty)
            {
                var line = new StringBuilder();
                if(item.DateStart.GetDayOfWeek() >= 6)
                    line.Append("🟡 ");
                else
                    line.Append("🟢 ");
                line.Append($"_{item.Roster.DaysOfWeek.DayOfWeekNameShort}_");
                line.Append(item.DateStart.ToString(" dd.MM HH:mm"));
                line.Append(" - ");
                line.Append(item.DateEnd.ToString("HH:mm"));
                sb.AppendLine(line.ToString());
            }
            return sb.ToString();
        }
    }
}
