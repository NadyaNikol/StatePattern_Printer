﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            printer.State = new OnState();
            printer.Print(10);


            IPrinterState state = new OnState();
            state.AddPages(printer, 10);
            state.PrintPages(printer,9);

            IPrinterState state2 = new OffState();
            state2.Off(printer);
        }
    }


    public class Printer
    {
        public int Pages { set; get; }
        public IPrinterState State { get; set; }

        public void Print(int pages)
        {
            State.PrintPages(this, pages);
        }
    }

    public interface IPrinterState
    {
        void On(Printer printer);
        void Off(Printer printer);
        void PrintPages(Printer printer, int pagesCount);

        void AddPages(Printer printer, int pagesCount);
    }

    public class OnState : IPrinterState
    {
        public void AddPages(Printer printer, int pagesCount)
        {
            printer.Pages += pagesCount; ;
        }

        public void Off(Printer printer)
        {
            Console.WriteLine("Принтер выключен");
            printer.State = new OffState();
        }

        public void On(Printer printer)
        {
            Console.WriteLine("Принтер уже включен");
        }

        public void PrintPages(Printer printer, int pagesCount)
        {
           if (printer.Pages >pagesCount)
            {
                printer.Pages -= pagesCount;
                Console.WriteLine($"Принтер напечатал {pagesCount} страниц");
               // printer.State = new PrintState();
            }
            else
            {
                printer.Pages = 0;
                printer.State = new WithoutPaperState();
            }
        }
    }

    public class OffState : IPrinterState
    {
        public void AddPages(Printer printer, int pagesCount)
        {
            printer.Pages += pagesCount;
        }

        public void Off(Printer printer)
        {
            Console.WriteLine("Принтер уже выключен");
        }

        public void On(Printer printer)
        {
            Console.WriteLine("Принтер включен");
            if (printer.Pages > 0)
                printer.State = new OnState();
            else
                printer.State = new WithoutPaperState();
        }

        public void PrintPages(Printer printer, int pagesCount)
        {
            Console.WriteLine("Перед работой с принтером необходимо его включить");
            printer.State = new OnState();
        }
    }

    public class WithoutPaperState : IPrinterState
    {
        public void AddPages(Printer printer, int pagesCount)
        {
            printer.Pages += pagesCount;
            printer.State = new OnState();
        }

        public void Off(Printer printer)
        {
            Console.WriteLine("Принтер выключен");

        }

        public void On(Printer printer)
        {
            Console.WriteLine("Невозможно использовать принтер без бумаги, необходимо домабить бумагу в принтер");
        }

        public void PrintPages(Printer printer, int pagesCount)
        {
            Console.WriteLine("Для печати недостаточно бумаги, необходимо добавить бумагу в принтер");
        }
    }

    public class PrintState : IPrinterState
    {
        public void AddPages(Printer printer, int pagesCount)
        {
            printer.Pages += pagesCount;
        }

        public void Off(Printer printer)
        {
            Console.WriteLine("Невозможно выключить принтер во время печати");
        }

        public void On(Printer printer)
        {
            Console.WriteLine("Принтер уже включен");

        }

        public void PrintPages(Printer printer, int pagesCount)
        {
            Console.WriteLine("Принтер уже находиться в состоянии печати");

        }
    }
}
