using BeginnerBot.Delegates;
using BeginnerBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BeginnerBot.Services
{
    public class UpdateHandlerService : IUpdateHandlerService
    {
        #region Generic handler

        public static int MessageId;

        public UpdateHandlerDelegate GetHandler(UpdateType type)
        {
            UpdateHandlerDelegate handler = type switch
            {
                UpdateType.Message => HandleMessageUpdateAsync,
                UpdateType.Poll => HandlePollUpdateAsync,
                _ => HandleUnknownUpdateAsync
            };

            return handler;
        }


        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Handling specific update
            var handler = GetHandler(update.Type);
            await handler(botClient, update, cancellationToken);
        }

        #endregion

        #region Handlers

        public async Task HandleMessageUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Send text message
            await botClient.SendTextMessageAsync
            (
                chatId: update.Message.Chat.Id,
                text: "Hello world",
                cancellationToken: cancellationToken
            );

            // Send a sticker
            await botClient.SendStickerAsync
            (
                chatId: update.Message.Chat.Id,
                sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp",
                cancellationToken: cancellationToken
            );

            // Send a video message
            await botClient.SendVideoAsync
            (
                chatId: update.Message.Chat.Id,
                video: "https://github.com/TelegramBots/book/raw/master/src/docs/video-bulb.mp4",
                cancellationToken: cancellationToken
            );

            // Send a formatted message with reply markup
            var message = await botClient.SendTextMessageAsync
            (
                chatId: update.Message.Chat.Id,
                text: "Trying <p>all parameters</p> of <h1>sendMessage</h1> method",
                replyToMessageId: update.Message.MessageId,
                replyMarkup: new InlineKeyboardMarkup
                (
                    InlineKeyboardButton.WithUrl
                    (
                        "Github",
                        "https://github.com/"
                    )
                ),
                cancellationToken: cancellationToken,
                parseMode: ParseMode.Markdown,
                disableNotification: true
            );

            // Show sent message
            Console.WriteLine
            (
                $"{message.From.FirstName} sent message {message.MessageId} " +
                $"to chat {message.Chat.Id} at {message.Date} " +
                $"It is a reply to message {message.ReplyToMessage?.MessageId} " +
                $"and has {message.Entities?.Length} message entities"
            );

            // Send a photo
            var photoMessage = await botClient.SendPhotoAsync
            (
                chatId: update.Message.Chat.Id,
                photo: "https://drive.google.com/file/d/1DdYML40LJeyln6YVgLQKsOcpq7c2CPMx/view",
                caption: "<b>BMW M5</b>. <i>See here</i>: <a href=\"https://www.bmwusa.com/build-your-own.html?tl=grp-wdpl-bcom-mix-mn-.-nscf-.-.-#/series/M5/sedan\">Pixabay</a>",
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken
            );

            // Send a photo with fileId
            await botClient.SendPhotoAsync
            (
                chatId: update.Message.Chat.Id,
                photo: "AgACAgQAAxkDAAIBaGLK9JEhwiB6Yr4NLFKmqiCr9irVAAI4rjEbSLxdUoYvzo98WTLyAQADAgADeQADKQQ",
                caption: "Photo sent by <b>file_id</b>",
                parseMode: ParseMode.Html
            );

            // Send a sticker
            var stickerMessage = await botClient.SendStickerAsync
            (
                chatId: update.Message.Chat.Id,
                sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp",
                cancellationToken: cancellationToken
            );

            await botClient.SendStickerAsync
            (
                chatId: update.Message.Chat.Id,
                sticker: stickerMessage.Sticker.FileId,
                cancellationToken: cancellationToken
            );

            // Send an audio
            await botClient.SendAudioAsync
            (
                chatId: update.Message.Chat.Id,
                audio: "https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3"
            );

            // Send a voice 
            using var stream = System.IO.File.OpenRead(@"C:\Users\RSM_DEV\Downloads\src_docs_voice-nfl_commentary.ogg");
            await botClient.SendVoiceAsync
            (
                chatId: update.Message.Chat.Id,
                voice: stream,
                duration: 36,
                cancellationToken: cancellationToken
            );

            // Send a video
            await botClient.SendVideoAsync
            (
                chatId: update.Message.Chat.Id,
                video: "https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-countdown.mp4",
                thumb: "https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg",
                supportsStreaming: true,
                cancellationToken: cancellationToken
            );

            // Send a video note
            using var videoNoteStream = System.IO.File.OpenRead(@"C:\Users\RSM_DEV\Downloads\video-waves.mp4");
            await botClient.SendVideoNoteAsync
            (
                chatId: update.Message.Chat.Id,
                videoNote: videoNoteStream,
                duration: 47,
                length: 360,
                cancellationToken: cancellationToken
            );

            // Send an album
            await botClient.SendMediaGroupAsync
            (
                chatId: update.Message.Chat.Id,
                media: new IAlbumInputMedia[]
                {
                    new InputMediaPhoto("https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg"),
                    new InputMediaPhoto("https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg"),
                },
                cancellationToken: cancellationToken
            );

            // Send a document
            await botClient.SendDocumentAsync
            (
                chatId: update.Message.Chat.Id,
                document: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
                caption: "<i>Ara</i>",
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken
            );

            // Send an animation
            await botClient.SendAnimationAsync
            (
                chatId: update.Message.Chat.Id,
                animation: "https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-waves.mp4",
                caption: "<b>Waves</b>",
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken
            );

            // Sending a poll
            var poll = await botClient.SendPollAsync
            (
                chatId: "-655695240",
                question: "Your cat's name ?",
                options: new[]
                {
                    "Kristina",
                    "Kristi"
                },
                cancellationToken: cancellationToken
            );
            MessageId = poll.MessageId;

            // Send a venue
            await botClient.SendVenueAsync
            (
                chatId: update.Message.Chat.Id,
                latitude: 50.0840172f,
                longitude: 14.418288f,
                title: "Man Hanging out",
                address: "Husova, 110 00 Staré Město, Czechia",
                cancellationToken: cancellationToken
            );

            // Send a location
            await botClient.SendLocationAsync
            (
                chatId: update.Message.Chat.Id,
                latitude: 50.0840172f,
                longitude: 14.418288f,
                cancellationToken: cancellationToken
            );
        }

        public async Task HandlePollUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (!update.Poll.IsClosed)
                await botClient.StopPollAsync
                (
                    chatId: "-655695240",
                    messageId: MessageId,
                    cancellationToken: cancellationToken
                );
        }

        public Task HandleUnknownUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
