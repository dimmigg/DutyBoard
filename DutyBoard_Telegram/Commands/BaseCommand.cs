using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Telegram;
using DutyBoard_Telegram.Interface;
using DutyBoard_Utility;
using DutyBoard_Utility.Export;
using DutyBoard_Utility.TempFile;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace DutyBoard_Telegram.Commands
{
    public abstract class BaseCommand
    {
        internal readonly ITelegramUserService TelegramUserService;
        internal readonly TelegramBotClient BotClient;
        protected BaseCommand(ITelegramUserService telegramUserService, TelegramBot telegramBot)
        {
            TelegramUserService = telegramUserService;
            BotClient = telegramBot.GetBot().GetAwaiter().GetResult();
        }
        public abstract string Name { get; }
        internal abstract Task SendMessage(Update update, TelegramUser user);


        public async Task ExecuteAsync(Update update)
        {
            var user = TelegramUserService.GetOrCreate(update);
            if (user.IsActive)
                await SendMessage(update, user);
            else
                await SendMessageNoActive(user.ChatId);
        }

        internal async Task SendMessageNoActive(long chatId)
        {
            var cancellationToken = new CancellationTokenSource();
            var sentMessage = await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Для работы бота необходимо подтверждение Вашей уч. записи.",
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken.Token);
        }
    }
}
