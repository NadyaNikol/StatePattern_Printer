using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            printer.Print(5, 3);

            Console.ReadKey();
        }
    }

    enum StatePrinter
    {
        Off,
        Wait,
        Print,
        WithoutLists
    }

    class Printer
    {
        public StatePrinter state;

        public void Print(int listsForPrint, int allLists)
        {
            if (state == StatePrinter.Off)
            {
                Console.WriteLine("Для начала работы с принтером следует его включить");
                state = StatePrinter.Wait;
            }

             if (state == StatePrinter.Wait && state != StatePrinter.WithoutLists)
            {
                Console.WriteLine("Принтер готов к использованию");
                state = StatePrinter.Print;
            }

             if (state == StatePrinter.Print)
            {
                Console.WriteLine($"\nПечать страниц. Пожалуйста подождите...\n");
                for (int i = 0; i < listsForPrint; i++)
                {
                    Console.WriteLine($"Печать {i+1}-ой страницы");
                    System.Threading.Thread.Sleep(1000);

                    if (allLists - i-1 <= 0)
                    {
                        Console.WriteLine("Невозможно распечатать, нет бумаги в принтере");
                        state = StatePrinter.WithoutLists;
                        break;
                    }
                }

            }

             if (state == StatePrinter.WithoutLists)
            {
                Console.WriteLine("Пожалуйста, добавте бумагу в принтер");
            }

        }


    }

}
