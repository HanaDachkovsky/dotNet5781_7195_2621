using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_7195_2621
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bus>ourBuses;
            int choice;
            Console.WriteLine("Enter your choice:");
            Console.WriteLine("1: add a bus");
            Console.WriteLine("2: choose a bus to drive");
            Console.WriteLine("3: to care or to refueling");
            Console.WriteLine("4: print the kilometers from the last care, from all the buses in the company");
            Console.WriteLine("0: to exit");
            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    break;
                case 2:
                    
                default:
                    break;
            }





            Console.ReadKey();
        }
    }
}
