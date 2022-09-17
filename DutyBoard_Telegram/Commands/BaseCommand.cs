using DutyBoard_DataAccess.Repository.IRepository;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DutyBoard_Telegram.Commands
{
    public abstract class BaseCommand
    {
        internal readonly ITelegramUserRepository TelegramRepo;
        protected BaseCommand(ITelegramUserRepository telegramRepo)
        {
            TelegramRepo = telegramRepo;
        }
        public abstract string Name { get; }
        public abstract Task ExecuteAsync(Update update);
    }
}
