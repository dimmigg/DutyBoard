using DutyBoard_Telegram.Interface;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot.Types;
using Telegram.Bot;
using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Telegram;
using Telegram.Bot.Types.Enums;

namespace DutyBoard_Telegram.Commands.Callback.Admin
{
    public class ActivateTelegramUserCommand : BaseCommand
    {
        private readonly ITelegramUserRepository _telegramRepo;
        private readonly StartCommand _startCommand;

        public ActivateTelegramUserCommand(ITelegramUserRepository telegramRepo,
                ITelegramUserService telegramUserService, 
                TelegramBot telegramBot) : base(telegramUserService, telegramBot)
        {
            _telegramRepo = telegramRepo;
            _startCommand = new StartCommand(telegramUserService, telegramBot);
        }

        public override string Name => CommandNames.TelegramActivateCommand;

        internal override async Task SendMessage(Update update, TelegramUser user)
        {
            if (update.CallbackQuery is { Message: { }, Data: { } })
            {
                var activationUser = update.CallbackQuery.Data.Split("_");
                if (activationUser.Length > 1)
                {
                    var newTgUser = _telegramRepo.FirstOrDefault(int.Parse(activationUser[1]));
                    newTgUser.RoleId = 1;
                    _telegramRepo.Upsert(newTgUser);

                    await BotClient.DeleteMessageAsync(user.ChatId, update.CallbackQuery.Message.MessageId);

                    var cancellationToken = new CancellationTokenSource();
                    var sentMessage = await BotClient.SendTextMessageAsync(
                        chatId: user.ChatId,
                        text: "Пользователь активирован",
                        parseMode: ParseMode.Markdown,
                        cancellationToken: cancellationToken.Token);


                    await _startCommand.SendMessage(null, newTgUser);
                }
            }
        }
    }
}
