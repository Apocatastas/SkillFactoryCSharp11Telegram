using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using GondolinBot.Configuration;
using GondolinBot.Services;

namespace GondolinBot.Controllers
{
	public class TextMessageController
	{
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"🔤 Подсчет символов в сообщении" , $"text"),
                        InlineKeyboardButton.WithCallbackData($"🔢 Сумма введённых чисел" , $"calc")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> Этот бот может посчитать длину вашего сообщения или сумму введённых чисел.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Если вы выбрали вычисление суммы чисел, введите их через пробел.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    try
                    {
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, DataHandler.ProcessData(message.Text, _memoryStorage.GetSession(message.Chat.Id).userChoise), cancellationToken: ct);
                    }
                    catch (Exception er)
                    {
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, DataHandler.ProcessError(_memoryStorage.GetSession(message.Chat.Id).userChoise), cancellationToken: ct);
                    }
                    break;
            }
        }
    }
}

