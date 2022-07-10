using Telegram.Bot;
using Telegram.Bot.Types;

namespace BeginnerBot.Services.Interfaces
{
    public interface IBotConnectionService
    {
        void Start(CancellationToken cancellationToken);

        Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken);

        Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken);

        void HandleLocalError(Exception exception);
    }
}
