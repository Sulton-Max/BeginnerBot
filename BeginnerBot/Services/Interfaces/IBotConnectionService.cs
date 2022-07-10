using Telegram.Bot;
using Telegram.Bot.Types;

namespace BeginnerBot.Services.Interfaces
{
    public interface IBotConnectionService
    {
        #region Bot initialization

        Task Start();

        Task Stop();

        #endregion

        #region Handling

        Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken);

        Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken);

        void HandleLocalError(Exception exception);

        #endregion
    }
}
