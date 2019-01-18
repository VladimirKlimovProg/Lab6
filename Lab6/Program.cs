using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class Dialog
    {
        public static void PrintMenu1stLevel()
        {
            Console.WriteLine("1. Задача с массивом");
            Console.WriteLine("2. Задача со строкой");
            Console.WriteLine("3. Выход");
        }

        public static void PrintMenu2ndLevelArray()
        {
            Console.WriteLine("1. Сформировать массив");
            Console.WriteLine("2. Напечатать массив");
            Console.WriteLine("3. Удаление строк, в котором есть число, совпадающее с максимальным элементом");
            Console.WriteLine("4. Назад");
        }
        public static void PrintMenu2ndLevelString()
        {
            Console.WriteLine("1. Сформировать строку");
            Console.WriteLine("2. Напечатать строку");
            Console.WriteLine("3. Удаление первого и последнего предложения в строке");
            Console.WriteLine("4. Назад");
        }

        public static void PrintMenu3dLevel()
        {
            Console.WriteLine("1. Создать массив вручную");
            Console.WriteLine("2. Создать массив с помощью ДСЧ");
            Console.WriteLine("3. Назад");
        }
        public static void PrintMenu3dLevelStr()
        {
            Console.WriteLine("1. Создать строку вручную");
            Console.WriteLine("2. Создать строку с помощью массива строк");
            Console.WriteLine("3. Назад");
        }

        /// <summary>
        /// Ввод числа
        /// </summary>
        /// <param name="Text">Сообщение</param>
        /// <param name="sizes">Ограничение на ввод чисел</param>
        /// <returns>Число</returns>
        public static int InputNumber(string Text, params int[] sizes)
        {

            int number = 0;
            bool ok = false;
            do
            {
                Console.WriteLine(Text);
                bool success = Int32.TryParse(Console.ReadLine(), out number);
                if (success)
                {
                    if (sizes.Length == 0)
                    {
                        return number;
                    }
                    ok = number >= sizes[0] && number <= sizes[1];
                }
                else
                {
                    Console.WriteLine("Ошибка ввода");
                }

            } while (!ok);
            return number;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            MakeMenu(exit);
        }

        /// <summary>
        /// Главное меню
        /// </summary>
        /// <param name="exit">Выход из меню</param>
        private static void MakeMenu(bool exit)
        {
            do
            {
                Dialog.PrintMenu1stLevel();
                int menuItemLevel1 = Dialog.InputNumber("Введите пункт меню", 1, 3);
                switch (menuItemLevel1)
                {
                    case 1:
                        MatrMenu();
                        break;
                    case 2:
                        StringMenu();
                        break;
                    case 3:
                        exit = true;
                        break;
                    default:
                        break;
                }

            } while (!exit);
        }

        /// <summary>
        /// Подменю "Строка"
        /// </summary>
        private static void StringMenu()
        {
            bool createString = false;
            int userAnswer, formStr;
            string str = "";
            do
            {
                Dialog.PrintMenu2ndLevelString();
                userAnswer = Dialog.InputNumber("Введите пункт меню", 1, 4);
                switch (userAnswer)
                {
                    case 1:
                        {
                            Dialog.PrintMenu3dLevelStr();
                            formStr = Dialog.InputNumber("Введите пункт меню", 1, 3);
                            switch (formStr)
                            {
                                case 1:
                                    {
                                        createString = FormStringConsole(out str);
                                        break;
                                    }
                                case 2:
                                    {
                                        createString = FormStringArrayOfStrings(out str);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (createString)
                            {
                                PrintStr(str);
                            }
                            else
                            {
                                Console.WriteLine("Строка не сформирована");
                            }
                            break;
                        }
                    case 3:
                        {
                            if (createString)
                            {
                                DeleteFirstAndLastSentence(ref str);
                                
                            }
                            else
                            {
                                Console.WriteLine("Строка не сформирована");
                            }
                            break;
                        }
                    case 4:
                        break;
                    default:
                        break;
                }
            } while (userAnswer != 4);
        }

        /// <summary>
        /// Удаление первого и последнего предолжения в строке
        /// </summary>
        /// <param name="str">Строка</param>
        private static void DeleteFirstAndLastSentence(ref string str)
        {
            str = str.Trim(' ', '.', '!', ',', '?',':', ';');
            int firstIndex = 0;
            int lastIndex = 0;
            //поиск первого разделителя
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] == '.') || (str[i] == '!') || (str[i] == '?')){
                    firstIndex = i;
                    break;
                }
            }
            //поиск второго разделителя
            for (int i = str.Length-1; i >=0 ; i--)
            {
                if ((str[i] == '.') || (str[i] == '!') || (str[i] == '?'))
                {
                    lastIndex = i;
                    break;
                }
            }
            if ((firstIndex == 0)||(lastIndex == 0)){
                Console.WriteLine("Предложение не найдено");
            }
            else
            {
                str = str.Substring(firstIndex + 1, lastIndex - firstIndex);
                str = str.Trim(' ');
                Console.WriteLine("Удаление выполнено");
            }

        }

        /// <summary>
        /// Печать строки
        /// </summary>
        /// <param name="str">Строка</param>
        private static void PrintStr(string str)
        {
            Console.WriteLine(str);
        }

        /// <summary>
        /// Формирование строки вручную
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Строка сформирована</returns>
        private static bool FormStringConsole(out string str)
        {
            do
            {
                Console.WriteLine("Введите строку");
                str = Console.ReadLine();
                if (str == "")
                {
                    Console.WriteLine("Пустая строка. Повторите ввод.");
                }
            } while (str == "");
            return true;
        }

        /// <summary>
        /// Формирование строки с помощью массива строк
        /// </summary>
        /// <param name="str">Переменная для хранения строки</param>
        /// <returns>Массив строк сформирован</returns>
        private static bool FormStringArrayOfStrings(out string str)
        {
            str = "";
            string[] stAr = new string[4];
            for (int i = 0; i < stAr.Length; i++)

                stAr[i] = $"Строка {i+1}. ";

            foreach (string rs in stAr) str = str + rs;
            return true;
        }

        /// <summary>
        /// Подменю "Двумерный массив"
        /// </summary>
        private static void MatrMenu()
        {
            bool createMatr = false;
            int userAnswer, formMatr;
            int[,] matr = new int[0, 0];
            int stringSize = 0, columnSize = 0;
            do
            {
                Dialog.PrintMenu2ndLevelArray();
                userAnswer = Dialog.InputNumber("Введите пункт меню", 1, 4);
                switch (userAnswer)
                {
                    case 1:
                        {
                            Dialog.PrintMenu3dLevel();
                            formMatr = Dialog.InputNumber("Введите пункт меню", 1, 3);
                            switch (formMatr)
                            {
                                case 1:
                                    {
                                        createMatr = ConsoleFormMatr(out stringSize, out columnSize, out matr);
                                        break;
                                    }
                                case 2:
                                    {
                                        createMatr = RandomFormMatr(out stringSize, out columnSize, out matr);
                                        break;
                                    }
                                default: break;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (createMatr)
                            {
                                PrintMatr(stringSize, columnSize, matr);
                            }
                            else
                            {
                                Console.WriteLine("Массив не сформирован");
                            }
                            break;
                        }
                    case 3:
                        {
                            if (createMatr)
                            {
                                DeleteStrings(ref stringSize, columnSize, ref matr);
                            }
                            else
                            {
                                Console.WriteLine("Массив не сформирован");
                            }
                            break;
                        }
                    case 4:
                        break;
                    default:
                        break;
                }
            } while (userAnswer != 4);
        }

        /// <summary>
        /// Создание двумерного массива с помощью ручного ввода
        /// </summary>
        /// <param name="stringSize">Количество строк</param>
        /// <param name="columnSize">Количество столбцов</param>
        /// <param name="matr">Двумерный массив</param>
        /// <returns>Двумерный массив создан</returns>
        private static bool ConsoleFormMatr(out int stringSize, out int columnSize, out int[,] matr)
        {
            int i, j;
            stringSize = Dialog.InputNumber("Введите количество строк матрицы", 1, 10);
            columnSize = Dialog.InputNumber("Введите количество столбцов матрицы", 1, 10);
            matr = new int[stringSize, columnSize];
            for (i = 0; i < stringSize; i++)
                for (j = 0; j < columnSize; j++)
                {
                    matr[i, j] = Dialog.InputNumber("Введите элемент матрицы", 1, 100);
                }
            Console.WriteLine("Массив создан");
            return true;
        }

        /// <summary>
        /// Создание двумерного массива с помощью датчика случайных чисел
        /// </summary>
        /// <param name="stringSize">Количество строк</param>
        /// <param name="columnSize">Количество столбцов</param>
        /// <param name="matr">Двумерный массив</param>
        /// <returns>Двумерный массив создан</returns>
        private static bool RandomFormMatr(out int stringSize, out int columnSize, out int[,] matr)
        {
            int i, j;
            Random rnd = new Random();
            stringSize = Dialog.InputNumber("Введите количество строк матрицы", 1, 10);
            columnSize = Dialog.InputNumber("Введите количество столбцов матрицы", 1, 10);
            matr = new int[stringSize, columnSize];
            for (i = 0; i < stringSize; i++)
                for (j = 0; j < columnSize; j++)
                {
                    matr[i, j] = rnd.Next(1, 100);
                }
            Console.WriteLine("Массив создан");
            return true;
        }

        /// <summary>
        /// Распечатка двумерного массива
        /// </summary>
        /// <param name="stringSize">Количество строк</param>
        /// <param name="columnSize">Количество столбцов</param>
        /// <param name="matr">Двумерный массив</param>
        private static void PrintMatr(int stringSize, int columnSize, int[,] matr)
        {
            int i, j;
            for (i = 0; i < stringSize; i++)
            {
                for (j = 0; j < columnSize; j++)
                    Console.Write(matr[i, j].ToString() + " ");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Удаление строк с максимальным элементом
        /// </summary>
        /// <param name="stringSize">Количество строк</param>
        /// <param name="columnSize">Количество столбцов</param>
        /// <param name="matr">Двумерный массив</param>
        private static void DeleteStrings(ref int stringSize, int columnSize, ref int[,] matr)
        {
            int i, j;
            int maxElement = FindMax2DArray(ref matr);
            int countOfMaxElement = 0;
            //Сколько раз встречается максимальный элемент
            for (i = 0; i < stringSize; i++)
            {
                for (j = 0; j < columnSize; j++)
                {
                    if (matr[i,j] == maxElement)
                    {
                        countOfMaxElement++;
                    }
                }
            }
            //В каких строках встречается максимальный элемент
            int[] stringsMaxElement = new int[countOfMaxElement];
            int count = 0;
            for (i = 0; i < stringSize; i++)
            {
                for (j = 0; j < columnSize; j++)
                {
                    if (matr[i, j] == maxElement)
                    {
                        stringsMaxElement[count] = i;
                        count++;
                    }
                }
            }

            int newStringSize = stringSize - stringsMaxElement.Length;
            int[,] tempMatr = new int[newStringSize, columnSize];
            int t = 0;
            count = 0;
            for (i = 0; i < stringSize; i++)
                if (Array.IndexOf(stringsMaxElement, i)<0)
                {
                    for (j = 0; j < columnSize; j++)
                        tempMatr[t, j] = matr[i, j];
                    t++;
                }
            matr = tempMatr;
            stringSize = newStringSize;
            Console.WriteLine("Удаление выполнено!");
        }

        /// <summary>
        /// Поиск максимального элемента в двумерном массиве
        /// </summary>
        /// <param name="matr">Двумерный массив</param>
        /// <returns>Максимальный элемент</returns>
        private static int FindMax2DArray(ref int[,] matr)
        {
            int currentMaxElement = Int32.MinValue;
            foreach (var element in matr)
            {
                if (element > currentMaxElement)
                {
                    currentMaxElement = element;
                }
            }
            return currentMaxElement;
        }
    }
}
