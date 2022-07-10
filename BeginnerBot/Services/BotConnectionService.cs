using BeginnerBot.Services.Interfaces;
using System.Security.Cryptography;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BeginnerBot.Services
{
    public class BotConnectionService : IBotConnectionService
    {
        protected readonly ITelegramBotClient _botClient;
        protected readonly IUpdateHandlerService _updateHandlerService;

        public BotConnectionService
        (
            string token,
            IUpdateHandlerService updateHandlerService
        )
        {
            _botClient = new TelegramBotClient(token);
            _updateHandlerService = updateHandlerService;
        }

        public async Start(CancellationToken cancellationToken)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            _botClient.StartReceiving
             (
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cancellationToken
             );

            await _botClient.SendTextMessageAsync
            (
                chatId: "760059843",
                text: "Bot connected via polling"
            );
        }

        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Update of type {update.Type} had received");

            // Process the update
            try
            {
                await _updateHandlerService.HandleAsync(client, update, cancellationToken);
            }
            catch (Exception exception)
            {
            }

            Console.WriteLine("Update processed successfully");
        }

        public async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiException => $"Error occured on Telegram API with code {apiException.ErrorCode} and message {apiException.Message}",
                _ => "Unknown error occured"
            };

            Console.WriteLine(errorMessage);
}

        public void HandleLocalError(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
