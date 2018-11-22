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

        public static void PrintMenu3dLevel()
        {
            Console.WriteLine("1. Создать массив вручную");
            Console.WriteLine("2. Создать массив с помощью ДСЧ");
            Console.WriteLine("3. Назад");
        }
        public static int InputNumber(string Text, params int[] sizes)
        {

            int number = 0;
            bool ok = false;
            do
            {
                try
                {
                    Console.WriteLine(Text);
                    number = Convert.ToInt32(Console.ReadLine());
                    if (sizes.Length == 0)
                    {
                        return number;
                    }
                    if (number >= sizes[0] && number <= sizes[1]) ok = true;
                    else ok = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка при вводе числа");
                    ok = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка при вводе числа");
                    ok = false;
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

        //Главное меню
        private static void MakeMenu(bool exit)
        {
            do
            {
                Dialog.PrintMenu1stLevel();
                int menuItemLevel1 = Dialog.InputNumber("Введите пункт меню", 1, 3);
                switch (menuItemLevel1)
                {
                    case 1:
                        Matr();
                        break;
                    case 2:
                        break;
                    case 3:
                        exit = true;
                        break;
                    default:
                        break;
                }

            } while (!exit);
        }

        //Подменю "Двумерный массив"
        private static void Matr()
        {
            bool createMatr = false;
            int userAnswer, formMatr;
            int[,] matr = new int[0, 0];
            int stringSize = 0, columnSize = 0;
            do
            {
                Dialog.PrintMenu2ndLevelArray();
                userAnswer = Convert.ToInt32(Console.ReadLine());
                switch (userAnswer)
                {
                    case 1:
                        {
                            Dialog.PrintMenu3dLevel();
                            formMatr = Convert.ToInt32(Console.ReadLine());
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
                            if (createMatr) PrintMatr(stringSize, columnSize, matr);
                            else Console.WriteLine("Массив не сформирован");
                            break;
                        }
                    case 3:
                        {
                            if (createMatr) DeleteStrings(ref stringSize, columnSize, ref matr);
                            else Console.WriteLine("Массив не сформирован");
                            break;
                        }
                    case 4: break;
                    default: Console.WriteLine("Нет такого пункта в меню"); break;
                }
            } while (userAnswer != 4);
        }

        //Создание двумерного массива с помощью ручного ввода
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

        //Создание двумерного массива с помощью датчика случайных чисел
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

        //Распечатка двумерного массива
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

        //Удаление строк с максимальным элементом
        static void DeleteStrings(ref int stringSize, int columnSize, ref int[,] matr)
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
            //В каких строках встрчается максимальный элемент
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

        //Поиск максимального элемента в двумерном массиве
        static int FindMax2DArray(ref int[,] matr)
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
