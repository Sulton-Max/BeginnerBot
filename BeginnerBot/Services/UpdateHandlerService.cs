using BeginnerBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BeginnerBot.Services
{
    public class UpdateHandlerService : IUpdateHandlerService
    {
        public Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
