using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using GondolinBot.Configuration;
//using GondolinBot.Services;

namespace GondolinBot.Controllers
{
	public class InlineKeyboardController
	{
        private readonly ITelegramBotClient _telegramClient;
      //  private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient)//, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
           // _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
          //  _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

            // Генерим информационное сообщение
            string actionType = callbackQuery.Data switch
            {
                "text" => "🔤 Подсчет символов в сообщении",
                "calc" => "🔢 Вычислить сумму введенных чисел",
                _ => String.Empty
            };

            switch (actionType)
            {
                case "text":
                    // Отправляем в ответ уведомление о выборе
                    await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                        $"<b>Вы выбрали подсчет символов в сообщении. {Environment.NewLine}</b> Введите ваше сообщение ниже." +
                        $"{Environment.NewLine}Можно поменять выбор в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
                    return;
                case "calc":
                    // Отправляем в ответ уведомление о выборе
                    await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                        $"<b>Вы выбрали вычисление суммы введенных чисел.{Environment.NewLine}</b> Введите числа через пробел." +
                        $"{Environment.NewLine}Можно поменять выбор в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
                    return;
            }

            
        }
    }
}

