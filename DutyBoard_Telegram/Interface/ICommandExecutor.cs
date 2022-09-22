using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DutyBoard_Telegram.Interface
{
    public interface ICommandExecutor
    {
        Task Execute(Update update);
    }
}
