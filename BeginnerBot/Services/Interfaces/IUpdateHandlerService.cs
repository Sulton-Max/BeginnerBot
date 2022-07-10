using BeginnerBot.Delegates;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BeginnerBot.Services.Interfaces
{
    public interface IUpdateHandlerService
    {
        #region Generic handler

        UpdateHandlerDelegate GetHandler(UpdateType type);

        Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

        #endregion

        #region Handlers

        Task HandleMessageUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

        Task HandlePollUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

        Task HandleUnknownUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

        #endregion
    }
}
