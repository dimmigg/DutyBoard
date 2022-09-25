using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutyBoard_Telegram.Commands;
using DutyBoard_Telegram.Interface;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

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
            await ExecuteCommand(message, update);
        }

        private async Task ExecuteCommand(string commandName, Update update)
        {
            _lastCommand = _commands.First(x => x.Name == commandName.Split("_")[0]);
            await _lastCommand.ExecuteAsync(update);
        }
    }
}
