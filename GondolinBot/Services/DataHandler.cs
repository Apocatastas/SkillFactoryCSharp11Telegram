using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GondolinBot.Services
{
    /// <summary>
    /// Обработка сообщений с заданиями, поддерживает многострочный ввод как для текста, так и для чисел
    /// </summary>
	public static class DataHandler
	{
        /// <summary>
        /// Если ввод успешный, в зависимости от выбора пользователя обрабатываем текст, либо числа
        /// </summary>
        /// <param name="data">Содержимое сообщения пользователя</param>
        /// <param name="parameter">Данные о кнопке, которую выбрал пользователь</param>
        /// <returns>Строка с результатом (либо длина сообщения, либо сумма чисел)</returns>
		public static string ProcessData(string data, string parameter)
		{
			switch (parameter)
			{
				case "text":
					{
                        return CalcMultilineInput(data);
					}
				case "calc":
					{
						return CalcNumberString(data);
					}
			}
			return parameter;
		}

        /// <summary>
        /// Обработчик ошибок ввода, если данные не соответствуют ожидаемым
        /// </summary>
        /// <param name="parameter">Тип ожидаемого ввода (текст, либо числа)</param>
        /// <returns>Строка сообщения об ошибке</returns>
        public static string ProcessError(string parameter)
        {
            switch (parameter)
            {
                case "text":
                    {
                        return "Произошла ошибка обработки текста.";
                    }
                case "calc":
                    {
                        return "Произошла ошибка обработки чисел. Убедитесь, что вы ввели числа, эти числа целые (без разделителей, спецсимволов и дробей) и разделены одним пробелом";
                    }
            }
            return "Произошла ошибка обработки данных, попробуйте ещё раз выбрать вариант в главном меню";
        }

        /// <summary>
        /// Вычисляем сумму чисел даже в случае многострочного ввода
        /// </summary>
        /// <param name="equation">Строка чисел с разделителем пробелом, либо возвратом каретки</param>
        /// <returns>Сумма чисел</returns>
        public static string CalcNumberString(string equation)
		{
            string[] lines = equation.Split('\n'); //делим на строки на случай ввода с переносом строки
			long result = 0;
            foreach (var line in lines) //разбираем построчно на числа
            {
                if (line.Length > 0)
                {
                    string[] numbers = line.Split(' '); 
                        foreach (var number in numbers)
                            {
                                result += Convert.ToInt64(number);
                            }
                }
            }
            return result.ToString();
		}

        /// <summary>
        /// Вычисляем длину каждой строки посимвольно на случай ввода в несколько строк
        /// </summary>
        /// <param name="input">Введённое пользователем сообщение</param>
        /// <returns>Строка с длиной введённого сообщения</returns>
        public static string CalcMultilineInput(string input)
        {
            string[] words = input.Split('\n'); //разбираем сообщение построчно
            long result = 0;
            foreach (var word in words)
            {
                result += Convert.ToInt64(word.Length);
            }
            return result.ToString();
        }
    }
}

