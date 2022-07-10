using BeginnerBot.Services;

var token = "5445716023:AAE6w8k5mVzlpgG8uM6sGdMm54E8c1ofxdA";

var updateHandlerService = new UpdateHandlerService();
var botConnectionService = new BotConnectionService(token, updateHandlerService);

var cancellationToken = new CancellationTokenSource();
await botConnectionService.Start(cancellationToken.Token);