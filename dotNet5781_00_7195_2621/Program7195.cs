using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_7195_2621
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome7195();
            Console.ReadKey();
        }
        static partial void Welcome2621();
        private static void Welcome7195()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
