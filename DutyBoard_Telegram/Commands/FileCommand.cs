using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DutyBoard_Utility.Export;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models.Telegram;
using DutyBoard_Utility.TempFile;
using DutyBoard_Telegram.Interface;

namespace DutyBoard_Telegram.Commands
{
    public class FileCommand : BaseCommand
    {
        private readonly IExportRepository _exportRepo;
        public FileCommand(ITelegramUserService telegramUserService, IExportRepository exportRepo, TelegramBot telegramBot) : base(telegramUserService, telegramBot)
        {
            _exportRepo = exportRepo;
        }

        public override string Name => CommandNames.File;

        internal override async Task SendMessage(Update update, TelegramUser user)
        {
            var cancellationToken = new CancellationTokenSource();
            //var path = @$"{_path}\{Path.GetRandomFileName()}.xlsx";
            var path = @$"{TempFileService.GetSharedPath()}\temp.xlsx";
            FileExport.WriteToExcel(_exportRepo.GetAll(), path);
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var message = await BotClient.SendDocumentAsync(
                    chatId: user.ChatId,
                    document: new InputOnlineFile(fs, "Дежурства.xlsx"),
                    caption: "📝 _Список всех дежурств_",
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken.Token);
                fs.Close();
            }
            System.IO.File.Delete(path);
        }
    }
}
