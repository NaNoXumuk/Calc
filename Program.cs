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
            Console.WriteLine("Введите пример для решения.");
            string Example = Console.ReadLine();
            Example = GetNumberFromBrackets(Example);          
            Example = StringToNumber(GetCountMathIterations(Example), Example);          
            Console.WriteLine($"Ответ: {Example}");
            Console.ReadKey();
        }  
        static string GetNumberFromBrackets (string example)
        {
          int startIndex = 0;
          int endIndex = 0;
          string exampleInBrackets = null;
          string endString = null;
            while (example.Contains('(') || example.Contains(')'))
            {
                for (int i = 0; i < example.Length; i++)
                {
                    if (example[i] == '(')
                        startIndex = i;
                    if (example[i] == ')')
                    {
                        endIndex = i;
                        for (int j = startIndex+1; j < endIndex; j++)
                            exampleInBrackets += example[j];
                        for (int j = 0; j < startIndex-1; j++)
                            endString += example[j];
                        if (Convert.ToSingle(StringToNumber(GetCountMathIterations(exampleInBrackets), exampleInBrackets)) < 0)
                            switch (example[startIndex - 1])
                            {
                                case '+':
                                    endString += StringToNumber(GetCountMathIterations(exampleInBrackets), exampleInBrackets);
                                    break;
                                case '-':                                    
                                    endString += "+" + Convert.ToSingle(StringToNumber(GetCountMathIterations(exampleInBrackets), exampleInBrackets)) * -1;
                                    break;
                                case '/':
                                case '*':
                                    endString += example[startIndex - 1] + StringToNumber(GetCountMathIterations(exampleInBrackets), exampleInBrackets);
                                    break;

                            }
                        else
                        {
                            endString += example[startIndex - 1];
                            endString += StringToNumber(GetCountMathIterations(exampleInBrackets), exampleInBrackets);
                        }    
                        for (int j = endIndex+1; j < example.Length; j++)
                            endString += example[j];
                        break;
                    }
                }              
                example = endString; 
                endString = string.Empty;
                exampleInBrackets = string.Empty;
                startIndex = 0;
                endIndex = 0;
            }
            return example;
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
            if (Example[0] == '-')
                count--;
            for (int i = 0; i < Example.Length; i++)               
                if (Example[i] == '/' || Example[i] == '*' || Example[i] == '+' || Example[i] == '-')
                    if (Example[i+1] != '-')
                    count++;
            else
                    {
                        count++;
                        i++;
                    }
            return count;
        }

        static string StringToNumber(int CountMathIterations, string Example)
        {
            float CacheResult = 0;
            while (CountMathIterations > 0)
            {
                bool CanDoPlusMinus = false;
                string[] Cache = GetSplited(Example);
                for (int i = -1; i <= Cache.Length - 1;)

                {
                    i = i + 2;
                    if (i >= Cache.Length)
                    {
                        CanDoPlusMinus = true;
                        i = 1;
                    }

                    if ((Cache[i] != "+" && Cache[i] != "-") || (CanDoPlusMinus != true))
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
                for (int i = 0; i < Cache.Length; i++)
                    if (Cache[i] != null || Cache[i] != " ")
                        Example = Example + Cache[i];
            }
            return Example;
        }

        static string [] GetSplited (string example)
        {
            string [] Cache = new string[example.Length];
            int index = 0;
            int i = 0;
            if (example[0] == '-')
            {
                Cache[0] += example[0];
                i++;
            }
            for (i=i; i < example.Length;i++)
            {              
                if ((example[i] == '/' ||  example[i] == '*') && example[i+1] == '-')
                {
                    index++;
                    Cache[index] += example[i];
                    index++;
                    Cache[index] += example[i+1];
                    i++;
                }
                else
                if (example[i] == '/' || example[i] == '*' || example[i] == '+' || example[i] == '-')
                {                   
                        index++;
                        Cache[index] += example[i];
                        index++;                  
                }
                else                
                    Cache[index] += example[i];               
            }
         return Cache;
        }
    }
}
