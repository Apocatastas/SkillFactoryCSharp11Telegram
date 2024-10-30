using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using GondolinBot.Configuration;
using GondolinBot.Services;
using GondolinBot.Models;

namespace GondolinBot.Controllers
{
	public class InlineKeyboardController
	{
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;
            _memoryStorage.GetSession(callbackQuery.From.Id).userChoise = callbackQuery.Data;
            // Генерим информационное сообщение
            string actionType = callbackQuery.Data switch
            {
                "text" => "🔤 Подсчет символов в сообщении",
                "calc" => "🔢 Сумма введённых чисел",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Вы выбрали - {actionType}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять выбор в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);

        }
    }
}

