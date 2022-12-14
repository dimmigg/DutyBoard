using System.Threading.Tasks;
using DutyBoard_Models.Extensions;
using DutyBoard_Utility;
using Telegram.Bot;

namespace DutyBoard_Telegram
{
    public class TelegramBot
    {
        private readonly string _token;
        private readonly string _url;
        private TelegramBotClient _botClient;

        public TelegramBot()
        {
            _token = string.Concat(WC.TokenTG0.Decryption(), WC.TokenTG1.Decryption(), WC.TokenTG2.Decryption());
            _url = WC.UrlTG;
        }
        public async Task<TelegramBotClient> GetBot()
        {
            if (_botClient != null)
            {
                return _botClient;
            }

            _botClient = new TelegramBotClient(_token);

            var hook = $"{_url}api/message/update";
            await _botClient.SetWebhookAsync(hook);

            return _botClient;
        }
    }
}
