using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using GondolinBot;
using GondolinBot.Configuration;
using GondolinBot.Controllers;

namespace GondolinBot
{
    /// <summary>
    /// Бот должен иметь две функции:
    ///  ✓   подсчёт количества символов в тексте
    ///  ✓   вычисление суммы чисел, которые вы ему отправляете(одним сообщением через пробел).
    ///  ✓   Выбор одной из двух функций должен происходить на старте в «Главном меню».
    /// То есть в ответ на условное сообщение «сова летит» он должен прислать что-то вроде
    /// «в вашем сообщении 10 символов». А в ответ на сообщение «2 3 15» должен прислать «сумма чисел: 20».
    /// </summary>

    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(appSettings);

            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "7854755560:AAGPXIIPl1b310rsUogMm3QoghdZFYBlwSg"
            };
        }
    }
}
