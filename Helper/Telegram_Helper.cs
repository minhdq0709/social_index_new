using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace SocialNetwork_New.Helper
{
	class Telegram_Helper
	{
        private readonly TelegramBotClient BotClient = null;

        public Telegram_Helper(string connectBot)
        {
            BotClient = new TelegramBotClient(connectBot);
        }

        public async Task SendMessageToChannel(string message, long groupOrChannelId, bool isHtmlParseMode = false, bool isWebPreview = false)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            try
            {
                var parseMode = ParseMode.Html;
                await BotClient.SendTextMessageAsync(groupOrChannelId, message, parseMode, disableWebPagePreview: !isWebPreview).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                File.AppendAllText($"{Environment.CurrentDirectory}/Check/error.txt", ex.ToString());
            }
        }

        public async ValueTask<bool> SendFileToGroup(string pathFile, long idGroup, string nameFile)
        {
            try
            {
                using (FileStream sendFileStream = File.Open(pathFile, FileMode.Open))
                {
                    await BotClient.SendDocumentAsync(idGroup,
                        new Telegram.Bot.Types.InputFiles.InputOnlineFile(sendFileStream, nameFile));
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
