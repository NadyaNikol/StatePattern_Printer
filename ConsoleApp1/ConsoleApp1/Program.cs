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

            Printer printer = new Printer(new PrintPrinterState(10, 3));
            printer.Print();

            Console.ReadKey();
        }
    }


    interface IStatePrinter
    {
        void  Off(Printer printer);
        void Wait(Printer printer);
        void Print(Printer printer);
        void WithoutLists(Printer printer);
    }

    class Printer
    {
        public IStatePrinter State { set; get; }
        public Printer (IStatePrinter state)
        {
            this.State = state;
        }

        public void Off()
        {
            State.Off(this);
        }

        public void Wait()
        {
            State.Wait(this);
        }

        public void Print()
        {
            State.Print(this);
        }

        public void WithoutLists()
        {
            State.WithoutLists(this);
        }
    }

    class OffState : IStatePrinter
    {
        public void Off(Printer printer)
        {
            Console.WriteLine("Для начала работы с принтером следует его включить");
        }

        public void Print(Printer printer)
        {
            throw new NotImplementedException();
        }

        public void Wait(Printer printer)
        {
            throw new NotImplementedException();
        }

        public void WithoutLists(Printer printer)
        {
            throw new NotImplementedException();
        }
    }


    class WaitPrinterState : IStatePrinter
    {
        public void Off(Printer printer)
        {
            throw new NotImplementedException();
        }

        public void Print(Printer printer)
        {
            throw new NotImplementedException();
        }

        public void Wait(Printer printer)
        {
            throw new NotImplementedException();
        }

        public void WithoutLists(Printer printer)
        {
            throw new NotImplementedException();
        }
    }

    class PrintPrinterState : IStatePrinter
    {
        int allLists;
        int listsForPrint;
        public PrintPrinterState (int allLists, int listsForPrint)
        {
            this.allLists = allLists;
            this.listsForPrint = listsForPrint;
        }

        public void Off(Printer printer)
        {
            Console.WriteLine("Для начала работы с принтером следует его включить");
            printer.Wait();
        }

        public void Print(Printer printer)
        {
            Console.WriteLine($"\nПечать страниц. Пожалуйста подождите...\n");
            for (int i = 0; i < listsForPrint; i++)
            {
                Console.WriteLine($"Печать {i + 1}-ой страницы");
                System.Threading.Thread.Sleep(1000);

                if (allLists - i - 1 <= 0)
                {
                    Console.WriteLine("Невозможно распечатать, нет бумаги в принтере");
                    printer.WithoutLists();
                    break;
                }
            }
        }

        public void Wait(Printer printer)
        {
            Console.WriteLine("Принтер готов к использованию");
            printer.Print();
        }

        public void WithoutLists(Printer printer)
        {
            Console.WriteLine("Пожалуйста, добавте бумагу в принтер");
            printer.Wait();
        }
    }








    //enum StatePrinter
    //{
    //    Off,
    //    Wait,
    //    Print,
    //    WithoutLists
    //}

    //class Printer
    //{
    //    public StatePrinter state;

    //    public void Print(int listsForPrint, int allLists)
    //    {
    //        if (state == StatePrinter.Off)
    //        {
    //            Console.WriteLine("Для начала работы с принтером следует его включить");
    //            state = StatePrinter.Wait;
    //        }

    //         if (state == StatePrinter.Wait && state != StatePrinter.WithoutLists)
    //        {
    //            Console.WriteLine("Принтер готов к использованию");
    //            state = StatePrinter.Print;
    //        }

    //         if (state == StatePrinter.Print)
    //        {
    //            Console.WriteLine($"\nПечать страниц. Пожалуйста подождите...\n");
    //            for (int i = 0; i < listsForPrint; i++)
    //            {
    //                Console.WriteLine($"Печать {i+1}-ой страницы");
    //                System.Threading.Thread.Sleep(1000);

    //                if (allLists - i-1 <= 0)
    //                {
    //                    Console.WriteLine("Невозможно распечатать, нет бумаги в принтере");
    //                    state = StatePrinter.WithoutLists;
    //                    break;
    //                }
    //            }

    //        }

    //         if (state == StatePrinter.WithoutLists)
    //        {
    //            Console.WriteLine("Пожалуйста, добавте бумагу в принтер");
    //            state = StatePrinter.Wait;
    //        }

    //    }


    //}

}
