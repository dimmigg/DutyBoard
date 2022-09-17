using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Telegram.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using DutyBoard_Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DutyBoard_Telegram.Commands
{
    public class StartCommand : BaseCommand
    {
        private readonly TelegramBotClient _botClient;
        private readonly ITelegramUserService _telegramUserService;

        public StartCommand(ITelegramUserRepository telegramRepo, ITelegramUserService telegramUserService, TelegramBot telegramBot) : base(telegramRepo)
        {
            _telegramUserService = telegramUserService;
            _botClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.StartCommand;

        public override async Task ExecuteAsync(Update update)
        {
            var user = _telegramUserService.GetOrCreate(update);
            var inlineKeyboard = new ReplyKeyboardMarkup(CreateButtons(user))
            {
                ResizeKeyboard = true
            };

            await _botClient.SendTextMessageAsync(user.ChatId, "*Добро пожаловать!*\nЯ помогу с поиском дежурного.\n\n_Добавлена панель команд._",
                ParseMode.Markdown, replyMarkup: inlineKeyboard);
        }

        private IEnumerable<List<KeyboardButton>> CreateButtons(TelegramUser user)
        {
            var allButtons = new List<List<KeyboardButton>>();
            var row1 = new List<KeyboardButton>();
            var row2 = new List<KeyboardButton>();
            var end = new List<KeyboardButton>();
            row1.Add(new KeyboardButton(CommandNames.WhoDutyCommand));
            row1.Add(new KeyboardButton(CommandNames.ListDuty));
            row2.Add(new KeyboardButton(CommandNames.File));
            row2.Add(new KeyboardButton(CommandNames.Help));
            allButtons.Add(row1);
            allButtons.Add(row2);
            if (!user.IsAdmin) return allButtons;

            end.Add(new KeyboardButton(CommandNames.Admin));
            allButtons.Add(end);

            return allButtons;
        }
    }
}
