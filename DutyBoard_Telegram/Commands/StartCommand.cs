using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Telegram.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using DutyBoard_Models.Telegram;

namespace DutyBoard_Telegram.Commands
{
    public class StartCommand : BaseCommand
    {

        public StartCommand(ITelegramUserService telegramUserService, TelegramBot telegramBot) : base(telegramUserService, telegramBot)
        {
        }

        public override string Name => CommandNames.StartCommand;


        internal override async Task SendMessage(Update update, TelegramUser user)
        {
            var inlineKeyboard = new ReplyKeyboardMarkup(CreateButtons(user))
            {
                ResizeKeyboard = true
            };
            await BotClient.SendTextMessageAsync(user.ChatId, "*Добро пожаловать!*\nЯ помогу с поиском дежурного.\n\n_Добавлена панель команд._",
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
