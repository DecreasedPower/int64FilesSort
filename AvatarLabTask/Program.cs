using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace AvatarLabTask
{
    class Program
    {
        static void Main(string[] args)
        {
            // пути к входным и выходному файлам
            string inputFile1 = @"InputFile1.txt";
            string inputFile2 = @"InputFile2.txt";
            string outputFile = @"OutputFile.txt";

            SortFiles(inputFile1,inputFile2,outputFile,true);

            Console.ReadLine();
        }
        // функция SortFiles считывает int64 значения из двух файлов, после чего сортирует их и записывает в новый третий файл
        // параметр inputFile1 принимает путь к первому входному файлу
        // параметр inputFile1 принимает путь ко второму входному файлу
        // параметр outputFile принимает путь по которому создать исходный файл
        // параметр formFiles определяет, нужно ли заполнить входные файлы значениями
        //      true - заполнить(потребуется ввести количество заполняемых значений)
        //      false - не заполнять
        static async void SortFiles(string inputFile1, string inputFile2, string outputFile, bool formFiles)
        {
            // Заполняет исходные файлы значениями, если formFiles == true
            if (formFiles)
            {
                long quantity = 0;
                // Считывание количества заполняемых значений
                do
                {
                    Console.WriteLine("Введите количество значений:");
                    try
                    {
                        quantity = Convert.ToInt64(Console.ReadLine());
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                } while (quantity <= 0 || quantity > Int64.MaxValue);
                // заполнение первого файла
                Console.WriteLine("Формирование первого файла...");
                using (StreamWriter sw = new StreamWriter(inputFile1, false, Encoding.Default))
                {
                    for (long l = Int64.MaxValue - quantity; l < Int64.MaxValue; l++)
                    {
                        await sw.WriteLineAsync(l.ToString());
                    }
                }
                // заполнение второго файла
                Console.WriteLine("Формирование второго файла...");
                using (StreamWriter sw = new StreamWriter(inputFile2, false, Encoding.Default))
                {
                    for (long l = Int64.MinValue; l < Int64.MinValue + quantity; l++)
                    {
                        await sw.WriteLineAsync(l.ToString());
                    }
                }
            }
            // коллекция, в которой хранятся, сортируются и извлекаются значения, считанные из файлов
            List<long> numbers = new List<long>();
            // извлечение значений из первого файла
            Console.WriteLine("Считывание первого файла...");
            using (StreamReader sr = new StreamReader(inputFile1, Encoding.Default))
            {
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    try
                    {
                        numbers.Add(Convert.ToInt64(line));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            // извлечение значений из второго файла
            Console.WriteLine("Считывание второго файла...");
            using (StreamReader sr = new StreamReader(inputFile2, Encoding.Default))
            {
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    try
                    {
                        numbers.Add(Convert.ToInt64(line));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            // Сортировка считанных значений
            Console.WriteLine("Сортировка...");
            numbers.Sort();
            // запись исходного файла
            Console.WriteLine("Запись исходного файла...");
            using (StreamWriter sw = new StreamWriter(outputFile, false, Encoding.Default))
            {
                foreach (long l in numbers)
                {
                    await sw.WriteLineAsync(l.ToString());
                }
            }
            Console.WriteLine("Запись завершена");
        }
    }
}
