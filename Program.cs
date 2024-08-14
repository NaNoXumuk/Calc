using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float CacheResult=0;
            int CountMathIterations = 0, i, DoPlusMinus = 0;

            Console.WriteLine("Введите пример для решения.");
            string Example = Console.ReadLine();

            CountMathIterations = GetCountMathIterations(Example);

            while (CountMathIterations > 0)
            {
                
                string[] Cache = Example.Split(' ');
                for (i = -1; i <= Cache.Length - 1;)

                {
                    i = i + 2;
                    if (i >= Cache.Length)
                    {
                        DoPlusMinus = 1;
                        i = 1;
                    }

                    if ((Cache[i] != "+" && Cache[i] != "-") || (DoPlusMinus != 1))
                        switch (Cache[i])
                        {
                            case "/":
                            case "*":
                                if (Cache[i] == "/")
                                    CacheResult = Calculate('/', Cache[i - 1], Cache[i + 1]);                                
                                else
                                    CacheResult = Calculate('*', Cache[i - 1], Cache[i + 1]);
                                Cache[i] = Convert.ToString(CacheResult);
                                Cache[i - 1] = null;
                                Cache[i + 1] = null;
                                i = Cache.Length;
                                break;
                        }
                    else
                        switch (Cache[i])
                        {
                            case "+":
                            case "-":
                                if (Cache[i] == "+")
                                    CacheResult = Calculate('+', Cache[i - 1], Cache[i + 1]); 
                                else
                                    CacheResult = Calculate('-', Cache[i - 1], Cache[i + 1]);
                                Cache[i] = Convert.ToString(CacheResult);
                                Cache[i - 1] = null;
                                Cache[i + 1] = null;
                                i = Cache.Length;
                                break;
                        }
                  
                }
                CountMathIterations--;
                Example = string.Empty;

                for (i = 0; i < Cache.Length; i++)
                    if (Cache[i] != null)
                        Example = Example + Cache[i] + " ";
            }
            Console.WriteLine($"Ответ: {Example}");
            Console.ReadKey();
        }

        static float Calculate (char operation, string number1, string number2)
        {
            switch (operation)
            {
                case '/': return Convert.ToSingle(number1) / Convert.ToSingle(number2);
                case '*': return Convert.ToSingle(number1) * Convert.ToSingle(number2);
                case '+': return Convert.ToSingle(number1) + Convert.ToSingle(number2);
                case '-': return Convert.ToSingle(number1) - Convert.ToSingle(number2);
            }
            return 0;
        }

        static int GetCountMathIterations (string Example)
        {
            int count = 0;
            for (int i = 0; i < Example.Length; i++)
                if (Example[i] == '/' || Example[i] == '*' || Example[i] == '+' || Example[i] == '-')
                    count++;
            return count;
        }

    }
}
