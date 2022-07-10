using Telegram.Bot;
using Telegram.Bot.Types;

namespace BeginnerBot.Services.Interfaces
{
    public interface IUpdateHandlerService
    {
        Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
