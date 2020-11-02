using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_7195_2621
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            List<Bus> ourBuses = new List<Bus>();//a list of buses
            do
            {
                Console.WriteLine("Enter your choice:");//the m
                Console.WriteLine("1: add a bus");
                Console.WriteLine("2: choose a bus to drive");
                Console.WriteLine("3: to care or to refueling");
                Console.WriteLine("4: print the kilometers from the last care, from all the buses in the company");
                Console.WriteLine("0: to exit");
                bool succ = int.TryParse(Console.ReadLine(), out choice);
                if (succ == false)
                {
                    choice = 6;//if the string is not a number go to default
                }
                switch (choice)
                {
                    case 1:

                        bool succRead = true;
                        string _numeVeh;
                        int _dayCtor, _monthCtor = 0, _yearCtor = 0;
                        //string _day, _month, _year;

                        Console.WriteLine("Enter the vehicle license number");
                        _numeVeh = Console.ReadLine();
                        while (_numeVeh.Length > 8 || _numeVeh.Length < 7)
                        {
                            Console.WriteLine("Enter again");
                            _numeVeh = Console.ReadLine();
                        }
                        do
                        {
                            succRead = true;
                            foreach (Bus Item in ourBuses)
                            {
                                if (succRead == true && Item.VehicleNum == _numeVeh)
                                {
                                    Console.WriteLine("the bus is found, enter again");
                                    _numeVeh = Console.ReadLine();
                                    succRead = false;
                                }
                            }
                        } while (succRead == false);
                        Console.WriteLine("press 1 if the vehicle is new and any key if not");
                        string ifNew = Console.ReadLine();
                        if (ifNew == "1")
                        {
                            ourBuses.Add(new Bus(_numeVeh, DateTime.Now, DateTime.Now, 0, 0, 0));
                            Console.WriteLine("added");
                            break;
                        }


                        
                        Console.WriteLine("Enter the activity start date");
                        Console.Write("Day: ");
                        while (int.TryParse(Console.ReadLine(), out _dayCtor) == false|| _dayCtor<0|| _dayCtor>31)
                        {
                            Console.WriteLine("Enter again");
                        }
                        Console.Write("Month: ");
                        while (int.TryParse(Console.ReadLine(), out _monthCtor) == false|| _monthCtor<0|| _monthCtor>12)
                        {
                            Console.WriteLine("Enter again");
                        }
                        Console.Write("Year: ");
                        while (int.TryParse(Console.ReadLine(), out _yearCtor) == false)
                        {
                            Console.WriteLine("Enter again");
                        }


                        DateTime _startDate = new DateTime(_yearCtor, _monthCtor, _dayCtor);

                        //
                        Console.WriteLine("Enter the last care date");
                        Console.Write("Day: ");
                        while (int.TryParse(Console.ReadLine(), out _dayCtor) == false)
                        {
                            Console.WriteLine("Enter again");
                        }
                        Console.Write("Month: ");
                        while (int.TryParse(Console.ReadLine(), out _monthCtor) == false)
                        {
                            Console.WriteLine("Enter again");
                        }
                        Console.Write("Year: ");
                        while (int.TryParse(Console.ReadLine(), out _yearCtor) == false)
                        {
                            Console.WriteLine("Enter again");
                        }
                        DateTime _lastCare = new DateTime(_yearCtor, _monthCtor, _dayCtor);
                        //now read km from care
                        int _kmsLastCare;
                        Console.WriteLine("Enter the kilometrage of the last care");
                        while (int.TryParse(Console.ReadLine(), out _kmsLastCare) == false)
                        {
                            Console.WriteLine("Enter again");
                        }


                        //now read kilometrage
                        int _kilometrage;
                        Console.WriteLine("Enter the total kilometrage");
                        while (int.TryParse(Console.ReadLine(), out _kilometrage) == false)
                        {
                            Console.WriteLine("Enter again");
                        }

                        Console.WriteLine("Enter how many kms driven from the last refueling");
                        int _ref;
                        while (int.TryParse(Console.ReadLine(), out _ref) == false)
                        {
                            Console.WriteLine("Enter again");
                        }

                        _ref = 1200 - _ref;

                        ourBuses.Add(new Bus(_numeVeh, _startDate, _lastCare, _kmsLastCare, _kilometrage, _ref));
                        Console.WriteLine("added");
                        break;
                    case 2:
                        Console.WriteLine("Enter the vehicle license number");
                        string vehNum;
                        vehNum = Console.ReadLine();
                        Random rand = new Random(DateTime.Now.Millisecond);
                        int num = rand.Next(2000);
                        bool found = false;
                        for (int i = 0; i < ourBuses.Count; i++)
                        {
                            if (ourBuses[i].CheckBus(vehNum, num) == true)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (found == false)
                        {
                            Console.WriteLine("the bus not exist");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter the vehicle license number");
                        string vehNum2;
                        vehNum2 = Console.ReadLine();
                        int j;
                        for (j = 0; j < ourBuses.Count; j++)//first of all, check if the bus is in the list
                        {
                            if (ourBuses[j].VehicleNum == vehNum2)
                            {
                                break;
                            }
                        }
                        if (j == ourBuses.Count)//if the bus is not in the list
                        {
                            Console.WriteLine("Error,the bus not exist");
                        }
                        Console.WriteLine("enter 1 for care or 2 for  refueling");
                        int choice2;
                        bool succ2 = int.TryParse(Console.ReadLine(), out choice2);
                        while (succ2 == false || (choice2 != 1 && choice2 != 2))//if the string is not a number or the number is diffrent of 1 or 2
                        {
                            Console.WriteLine("enter again 1 for care or 2 for refueling");
                            succ2 = int.TryParse(Console.ReadLine(), out choice2);
                        }
                        if (choice2 == 1)
                        {
                            ourBuses[j].KmsLastCare = ourBuses[j].Kilometrage;
                            ourBuses[j].LastCare = DateTime.Now;
                            Console.WriteLine("The care succeeded");
                        }
                        if (choice2 == 2)
                        {
                            ourBuses[j].AvailableKm = 1200;
                            Console.WriteLine("The refueling succeeded");
                        }
                        break;
                    case 4:
                        Console.WriteLine("The drive of every bus:");
                        foreach (Bus item in ourBuses)
                        {
                            Console.Write(item.GetStringVehNum()+"\t");
                            Console.WriteLine(item.Kilometrage);
                        }
                        break;

                    default:
                        break;
                }

            } while (choice != 0);

            Console.ReadKey();
        }
    }
}
