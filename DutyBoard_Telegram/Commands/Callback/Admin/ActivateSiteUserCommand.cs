using DutyBoard_Telegram.Interface;
using System.Threading.Tasks;
using System.Threading;
using DutyBoard_DataAccess.Account;
using Telegram.Bot.Types;
using Telegram.Bot;
using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Account;
using DutyBoard_Models.Telegram;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types.Enums;

namespace DutyBoard_Telegram.Commands.Callback.Admin
{
    public class ActivateSiteUserCommand : BaseCommand
    {
        private readonly IUserStore<ApplicationUser> _userRepo;

        public ActivateSiteUserCommand(IConfiguration configuration,
                ITelegramUserService telegramUserService, 
                TelegramBot telegramBot) : base(telegramUserService, telegramBot)
        {
            _userRepo = new UserStore(configuration);
        }

        public override string Name => CommandNames.SiteActivateCommand;

        internal override async Task SendMessage(Update update, TelegramUser user)
        {
            if (update.CallbackQuery is { Message: { }, Data: { } })
            {
                var activationUser = update.CallbackQuery.Data.Split("_");
                if (activationUser.Length > 1)
                {
                    var newSiteUser = await _userRepo.FindByIdAsync(activationUser[1], CancellationToken.None);
                    newSiteUser.EmailConfirmed = true;
                    await _userRepo.UpdateAsync(newSiteUser, CancellationToken.None);

                    await BotClient.DeleteMessageAsync(user.ChatId, update.CallbackQuery.Message.MessageId);

                    var cancellationToken = new CancellationTokenSource();
                    var sentMessage = await BotClient.SendTextMessageAsync(
                        chatId: user.ChatId,
                        text: "Пользователь активирован",
                        parseMode: ParseMode.Markdown,
                        cancellationToken: cancellationToken.Token);

                }
            }
        }
    }
}
