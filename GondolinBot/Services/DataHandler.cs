using System;
namespace GondolinBot.Services
{
	public static class DataHandler
	{
		public static string ProcessData(string data, string parameter)
		{
			switch (parameter)
			{
				case "text":
					{
						return data.Length.ToString();
					}
				case "calc":
					{
						return CalcNumberString(data);
					}
			}
			return parameter;
		}

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
                        return "Произошла ошибка обработки чисел. Убедитесь, что вы ввели числа, эти числа целые (без разделителя и дробей) и разделены пробелом";
                    }
            }
            return "Произошла ошибка обработки данных, попробуйте ещё раз выбрать вариант в главном меню";
        }

        public static string CalcNumberString(string equation)
		{
            string[] words = equation.Split(' ');
			int result = 0;

            foreach (var word in words)
            {
				result += Convert.ToInt32(word);
            }

            return result.ToString();

		}
    }
}

