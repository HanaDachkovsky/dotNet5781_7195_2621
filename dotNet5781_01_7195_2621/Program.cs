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
            List<Bus>ourBuses=new List<Bus>();
            int choice;
            Console.WriteLine("Enter your choice:");
            Console.WriteLine("1: add a bus");
            Console.WriteLine("2: choose a bus to drive");
            Console.WriteLine("3: to care or to refueling");
            Console.WriteLine("4: print the kilometers from the last care, from all the buses in the company");
            Console.WriteLine("0: to exit");
            bool succ = int.TryParse(Console.ReadLine(),out choice);
            if(succ==false)
            {
                choice = 6;//if the string is not a number go to default
            }
            switch (choice)
            {
                case 1:

                    bool succRead = true;
                    string _numeVeh;
                    int _dayCtor,_monthCtor=0,_yearCtor=0;
                    string _day, _month, _year;
                    do {
                        Console.WriteLine("Enter the vehicle license number");
                        _numeVeh = Console.ReadLine();
                        if (_numeVeh.Length > 8 || _numeVeh.Length < 7)
                            succRead = false;
                        foreach(Bus Item in ourBuses)
                        {
                            if (succRead= true&&Item.VehicleNum == _numeVeh)
                            { 
                                Console.WriteLine("the bus is found, enter again");
                                succRead = false;
                                break;
                             }
                        }
                    } while (succRead == false);
                    Console.WriteLine("press 1 if the vehicle is new and any key if not");
                    string ifNew=Console.ReadLine();
                    if(ifNew=="1")
                    {
                        ourBuses.Add(new Bus(_numeVeh, DateTime.Now, DateTime.Now, 0, 0, 0));
                        break;
                    }
                    do
                    {
                        succRead = true;
                        Console.WriteLine("Enter the activity start date");
                        Console.Write("Day: ");
                        _day = Console.ReadLine();
                        succRead = int.TryParse(_day, out _dayCtor);
                        Console.Write("Month: ");
                        _month = Console.ReadLine();
                        if(succRead==true)
                            succRead = int.TryParse(_day, out _monthCtor);
                        Console.Write("Year: ");
                        _year = Console.ReadLine();
                        if (succRead == true)
                            succRead = int.TryParse(_day, out _yearCtor);

                    } while (succRead == false);

                    DateTime _startDate = new DateTime(_yearCtor, _monthCtor, _dayCtor);

                    do//2
                    {
                        succRead = true;
                        Console.WriteLine("Enter the last care date");
                        Console.Write("Day: ");
                        _day = Console.ReadLine();
                        succRead = int.TryParse(_day, out _dayCtor);
                        Console.Write("Month: ");
                        _month = Console.ReadLine();
                        if (succRead == true)
                            succRead = int.TryParse(_day, out _monthCtor);
                        Console.Write("Year: ");
                        _year = Console.ReadLine();
                        if (succRead == true)
                            succRead = int.TryParse(_day, out _yearCtor);

                    } while (succRead == false) ;
                    DateTime _lastCare = new DateTime(_yearCtor, _monthCtor, _dayCtor);
                    //now read km from care
                    int _kmsLastCare;
                    do
                    {
                        succRead = true;
                        succRead = int.TryParse(Console.ReadLine(), out _kmsLastCare);

                    } while(succRead == false);
                    //now read kilometrage
                    int _kilometrage;
                    do
                    {
                        succRead = true;
                        succRead = int.TryParse(Console.ReadLine(), out _kilometrage);

                    } while (succRead == false);

                    Console.WriteLine("Enter how many kms driven from the last refueling");
                    int _ref;
                    do
                    {
                        succRead = true;
                        succRead = int.TryParse(Console.ReadLine(), out _ref);

                    } while (succRead == false);
                    _ref = 1200 - _ref;

                    ourBuses.Add(new Bus(_numeVeh,_startDate,_lastCare,_kmsLastCare,_kilometrage,_ref));
                    break;
                case 2:
                    Console.WriteLine("Enter the vehicle license number");
                    string vehNum;
                    vehNum = Console.ReadLine();
                    Random rand = new Random(DateTime.Now.Millisecond);
                    int num = rand.Next();
                    bool found=false;
                    for(int i=0;i<ourBuses.Count;i++)
                    {
                        if(ourBuses[i].CheckBus(vehNum,num)==true)
                        {
                            found = true;
                            break;
                        }
                    }
                    if(found==false)
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
                    if (j ==ourBuses.Count)//if the bus is not in the list
                    {
                        Console.WriteLine("Error,the bus not exist");
                    }
                    Console.WriteLine("enter 1 for care or 2 for  refueling" );
                    int choice2;
                    bool succ2 = int.TryParse(Console.ReadLine(), out choice2);
                    while (succ2==false || (choice2!=1 && choice!=2) )//if the string is not a number or the number is diffrent of 1 or 2
                    {
                        Console.WriteLine("enter again 1 for care or 2 for refueling");
                        succ2 = int.TryParse(Console.ReadLine(), out choice2);
                    }
                    if(choice==1)
                    {
                        ourBuses[j].KmsLastCare = ourBuses[j].Kilometrage;
                        ourBuses[j].LastCare = DateTime.Now;
                        Console.WriteLine("The care succeeded");
                    }
                    if (choice == 2)
                    {
                        ourBuses[j].AvailableKm = 1200;
                        Console.WriteLine("The refueling succeeded");
                    }
                    break;
                case 4:
                    Console.WriteLine("The drive of every bus:");
                    foreach(Bus item in ourBuses)
                    {
                        Console.Write(item.GetStringVehNum());
                        Console.WriteLine(item.Kilometrage);
                    }
                    break;

                default:
                    break;
            }





            Console.ReadKey();
        }
    }
}
