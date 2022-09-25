using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutyBoard_Telegram.Commands;
using DutyBoard_Telegram.Interface;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DutyBoard_Telegram
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly List<BaseCommand> _commands;
        private BaseCommand _lastCommand;

        public CommandExecutor(IServiceProvider serviceProvider)
        {
            _commands = serviceProvider.GetServices<BaseCommand>().ToList();
        }

        public async Task Execute(Update update)
        {
            if (update?.Message?.Chat == null && update?.CallbackQuery == null)
                return;

            var message = update.Message?.Text ?? update.CallbackQuery.Data;
            //if (update?.Message?.Chat == null)
            //{
            await ExecuteCommand(message, update);
            //switch (message)
            //{
            //    case CommandNames.StartCommand:
            //        await ExecuteCommand(CommandNames.StartCommand, update);
            //        return;
            //    case CommandNames.WhoDutyCommand:
            //        await ExecuteCommand(CommandNames.WhoDutyCommand, update);
            //        return;
            //    case CommandNames.ListDuty:
            //        await ExecuteCommand(CommandNames.ListDuty, update);
            //        return;
            //    case CommandNames.Admin:
            //        await ExecuteCommand(CommandNames.Admin, update);
            //        return;
            //    case CommandNames.UsersCallback:
            //        await ExecuteCommand(CommandNames.UsersCallback, update);
            //        return;
            //}
            //}

            //switch (_lastCommand?.Name)
            //{
            //    case CommandNames.AddOperationCommand:
            //        {
            //            await ExecuteCommand(CommandNames.SelectCategoryCommand, update);
            //            break;
            //        }
            //    case null:
            //        {
            //            await ExecuteCommand(CommandNames.StartCommand, update);
            //            break;
            //        }
            //}
        }

        private async Task ExecuteCommand(string commandName, Update update)
        {
            _lastCommand = _commands.First(x => x.Name == commandName.Split("_")[0]);
            await _lastCommand.ExecuteAsync(update);
        }
    }
}
