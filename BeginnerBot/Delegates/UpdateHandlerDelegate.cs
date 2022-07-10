using Telegram.Bot;
using Telegram.Bot.Types;

namespace BeginnerBot.Delegates
{
    public delegate Task UpdateHandlerDelegate(ITelegramBotClient client, Update update, CancellationToken cancellationToken);
}
