using DutyBoard_Telegram.Services;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DutyBoard_Utility.Export;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Utility;
using DutyBoard_Utility.TempFile;

namespace DutyBoard_Telegram.Commands
{
    public class FileCommand : BaseCommand
    {
        private readonly TelegramBotClient _botClient;
        private string _path;
        private readonly IExportRepository _exportRepo;
        public FileCommand(ITelegramUserRepository telegramRepo, IExportRepository exportRepo, TelegramBot telegramBot) : base(telegramRepo)
        {
            _botClient = telegramBot.GetBot().Result;
            _exportRepo = exportRepo;
        }

        public override string Name => CommandNames.File;

        public override async Task ExecuteAsync(Update update)
        {
            var chatId = update.CallbackQuery?.Message.Chat.Id ?? update.Message.Chat.Id;

            await SendMessage(chatId);

        }

        private async Task SendMessage(long chatId)
        {
            _path = TempFileService.GetSharedPath();
            var cancellationToken = new CancellationTokenSource();
            //var path = @$"{_path}\{Path.GetRandomFileName()}.xlsx";
            var path = @$"{TempFileService.GetSharedPath()}\temp.xlsx";
            FileExport.WriteToExcel(_exportRepo.GetAll(), path);
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var message = await _botClient.SendDocumentAsync(
                    chatId: chatId,
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
