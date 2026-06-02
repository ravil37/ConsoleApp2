using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "1ex.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("- Меня зовут Равиль");
                writer.WriteLine("- Мне 18 лет");
                writer.WriteLine("- Я учусь программированию");
                writer.WriteLine("- Мое хобби - играть");
                writer.WriteLine("- Мечтаю стать разработчиком");
            }
            using (StreamReader reader = new StreamReader(fileName))
            {
                {
                    int LineNumber = 1;
                    string Line;
                    while ((Line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine($" Строка {LineNumber} {Line}");
                        LineNumber++;
                    }
                    Console.WriteLine();
                    Console.WriteLine($"Количество строк в файле {LineNumber - 1}");
                }
            }


            // Задание 3
            var file = @"C:\Users\242425\Desktop\shopping.txt";
            if (!File.Exists(file)) File.Create(file).Close();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Показать список\n2. Добавить покупку\n3. Отметить выполненной\n4. Очистить список\n5. Выход");


                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        using (StreamReader r = new StreamReader(file))
                            for (int i = 1; !r.EndOfStream; i++)
                                Console.WriteLine($"{i}. {r.ReadLine()}");
                        Console.ReadKey();
                        break;

                    case '2':
                        Console.Clear();
                        Console.Write("Покупка: ");
                        using (StreamWriter w = new StreamWriter(file, true))
                            w.WriteLine($"[ ] {Console.ReadLine()}");
                        break;

                    case '3':
                        Console.Clear();
                        string tempFile = Path.GetTempFileName();
                        using (StreamReader r = new StreamReader(file))
                        {
                            for (int i = 1; !r.EndOfStream; i++)
                                Console.WriteLine($"{i}. {r.ReadLine()}");
                        }
                        Console.Write("Номер: ");
                        if (int.TryParse(Console.ReadLine(), out int n))
                        {
                            using (StreamReader r = new StreamReader(file))
                            using (StreamWriter w = new StreamWriter(tempFile))
                            {
                                int j = 1;
                                while (!r.EndOfStream)
                                {
                                    string line = r.ReadLine();
                                    w.WriteLine(j++ == n ? line.Replace("[ ]", "[X]") : line);
                                }
                            }
                            File.Delete(file);
                            File.Move(tempFile, file);
                        }
                        break;

                    case '4':
                        using (StreamWriter w = new StreamWriter(file, false)) { }
                        break;

                    case '5':
                        return;
                }
            }

            //Задание 2

            string fileName = "1ex.txt";
            Console.WriteLine("Оцените свое настроение (1-5?)");
            string mood = Console.ReadLine();
            Console.WriteLine("Введите комментарий:");
            string com = Console.ReadLine();
            string data = $"{DateTime.Now:dd.MM.yyyy} Настроение: {mood}/5 - {com}";
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(data);
            }

            Console.WriteLine("\n Запись добавлена");

            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }

            }
            Console.WriteLine("\nПоследние 3 записи:");

            int start = lines.Count - 3;
            if (start < 0)
                start = 0;

            int number = 1;
            for (int i = lines.Count - 1; i >= start; i--)
            {
                Console.WriteLine($"{number}. {lines[i]}");
                number++;
            }


        }
    }
}
