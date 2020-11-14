using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7195_2621
{
    class Program
    {
        static void Main(string[] args)
        {
            BusList Egged = new BusList();
            for ()









             int choice;
            while (int.TryParse(Console.ReadLine(), out choice) == false)
            {
                Console.WriteLine("Enter again");///////exc?
            }
            do
            {
                Console.WriteLine("Enter your choice:");//the menu
                Console.WriteLine("1: add a line");
                Console.WriteLine("2: add a station to a line");
                Console.WriteLine("3: delete a line");
                Console.WriteLine("4: delete a station from a line");
                Console.WriteLine("5: search lines that pass through the staion");
                Console.WriteLine("6: search options for a travel");
                Console.WriteLine("7: print all lines");
                Console.WriteLine("8: print the lines that pass through each station");
                Console.WriteLine("0: to exit");
                switch (choice)
                {
                    case 3:
                        try
                        {
                            int line3, first3, last3;
                            Console.WriteLine("Enter a line to delete");
                            if (int.TryParse(Console.ReadLine(),out line3)==false)
                            {
                                throw new ArgumentException("it must be a number");
                            }
                            Console.WriteLine("Enter a key of the first station");
                            if (int.TryParse(Console.ReadLine(), out first3) == false)
                            {
                                throw new ArgumentException("it must be a number");
                            }
                            Console.WriteLine("Enter a key of the last station");
                            if (int.TryParse(Console.ReadLine(), out last3) == false)
                            {
                                throw new ArgumentException("it must be a number");
                            }
                            Egged.DeleteLine(line3, first3, last3);////////////
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    case 4:
                        try
                        {
                            foreach (BusLine item in Egged)
                            {

                            }
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                }

            } while (choice != 0);
        }
    }
}
