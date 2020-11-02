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
                Console.WriteLine("Enter your choice:");//the menu
                Console.WriteLine("1: add a bus");
                Console.WriteLine("2: choose a bus to drive");
                Console.WriteLine("3: to care or to refueling");
                Console.WriteLine("4: print the kilometers from the last care, from all the buses in the company");
                Console.WriteLine("0: to exit");
                while (int.TryParse(Console.ReadLine(), out choice) == false) //sread choce from 1 to 4
                {

                }
                switch (choice)
                {
                    case 1://add a bus

                        bool succRead = true;
                        string _numeVeh;
                        int _dayCtor, _monthCtor, _yearCtor ;
                        Console.WriteLine("Enter the vehicle license number");
                        _numeVeh = Console.ReadLine();
                        while (_numeVeh.Length > 8 || _numeVeh.Length < 7)//if the number must have 7 or 8 digits- else, read again
                        {
                            Console.WriteLine("Enter again");
                            _numeVeh = Console.ReadLine();
                        }
                        do
                        {//check if the numner of bus already exits
                            succRead = true;
                            foreach (Bus Item in ourBuses)//go of every bus in the list
                            {
                                if (succRead == true && Item.VehicleNum == _numeVeh)//if we didnt find yet and now the number is equal
                                {
                                    Console.WriteLine("the bus is found, enter again");//the bus exits
                                    _numeVeh = Console.ReadLine();                      //and we have to read again
                                    succRead = false;//stop search because we found
                                }
                            }
                        } while (succRead == false);//if /the bus exits read again
                        Console.WriteLine("press 1 if the vehicle is new and any key if not");//press 1 if the bus is new and didnt drive
                        string ifNew = Console.ReadLine();
                        if (ifNew == "1")//if the bus is new
                        {
                            ourBuses.Add(new Bus(_numeVeh, DateTime.Now, DateTime.Now, 0, 0, 0));//add with ctor- 0 fuel
                            Console.WriteLine("added");
                            break;
                        }

                        //if the bus is not new

                        Console.WriteLine("Enter the activity start date");//read activity start date
                        Console.Write("Day: ");
                        while (int.TryParse(Console.ReadLine(), out _dayCtor) == false || _dayCtor < 0 || _dayCtor > 31)
                        {//if the days are less than 0 or more than 31 or we got a string that is not a number- read again
                            Console.WriteLine("Enter again");
                        }
                        Console.Write("Month: ");
                        while (int.TryParse(Console.ReadLine(), out _monthCtor) == false || _monthCtor < 0 || _monthCtor > 12)
                        {//if the months are less than 0 or more than 12 or we got a string that is not a number- read again
                            Console.WriteLine("Enter again");
                        }
                        Console.Write("Year: ");
                        while (int.TryParse(Console.ReadLine(), out _yearCtor) == false)
                        {
                            Console.WriteLine("Enter again");
                        }


                        DateTime _startDate = new DateTime(_yearCtor, _monthCtor, _dayCtor);//save the datetime for the ctor

                        //read the last care date
                        Console.WriteLine("Enter the last care date");
                        Console.Write("Day: ");
                        while (int.TryParse(Console.ReadLine(), out _dayCtor) == false)
                        {//if the days are less than 0 or more than 31 or we got a string that is not a number- read again
                            Console.WriteLine("Enter again");
                        }
                        Console.Write("Month: ");
                        while (int.TryParse(Console.ReadLine(), out _monthCtor) == false)
                        {//if the months are less than 0 or more than 12 or ew got a string that is not a number- read again
                            Console.WriteLine("Enter again");
                        }
                        Console.Write("Year: ");
                        while (int.TryParse(Console.ReadLine(), out _yearCtor) == false)
                        {
                            Console.WriteLine("Enter again");
                        }
                        DateTime _lastCare = new DateTime(_yearCtor, _monthCtor, _dayCtor);//save datetime for ctor
                        //now read km of last care
                        int _kmsLastCare;
                        Console.WriteLine("Enter the kilometrage of the last care");
                        while (int.TryParse(Console.ReadLine(), out _kmsLastCare) == false)//we need a number
                        {
                            Console.WriteLine("Enter again");
                        }


                        //now read kilometrage
                        int _kilometrage;
                        Console.WriteLine("Enter the total kilometrage");
                        while (int.TryParse(Console.ReadLine(), out _kilometrage) == false)//we need a number
                        {
                            Console.WriteLine("Enter again");
                        }

                        Console.WriteLine("Enter how many kms driven from the last refueling");
                        int _ref;
                        while (int.TryParse(Console.ReadLine(), out _ref) == false||_ref>1200)//we need a number
                        {                                                                      //and a drive is maximun 1200 kms
                            Console.WriteLine("Enter again");
                        }

                        _ref = 1200 - _ref;//the kms left for drive

                        ourBuses.Add(new Bus(_numeVeh, _startDate, _lastCare, _kmsLastCare, _kilometrage, _ref));//add to list
                        Console.WriteLine("added");
                        break;
                    case 2://choose bus for drive
                        Console.WriteLine("Enter the vehicle license number");
                        string vehNum;//the number of chosen bus
                        vehNum = Console.ReadLine();
                        Random rand = new Random(DateTime.Now.Millisecond);
                        int num = rand.Next(1200);//choose kms for drive, at random
                        bool found = false;//if we found the bud
                        for (int i = 0; i < ourBuses.Count; i++)//go over the list to find the bus
                        {
                            if (ourBuses[i].CheckBus(vehNum, num) == true)//if we found the bus stop searching
                            {//the drive updates in the function CheckBus
                                found = true;//we found the bus
                                break;
                            }
                        }
                        if (found == false)//if we didnt find the bus
                        {
                            Console.WriteLine("the bus not exist");
                        }
                        break;
                    case 3://care or refueling
                        Console.WriteLine("Enter the vehicle license number");
                        string vehNum2;//the chosen bus's number
                        vehNum2 = Console.ReadLine();
                        int j;//the index of the chosen bus
                        for (j = 0; j < ourBuses.Count; j++)//first of all, check if the bus is in the list
                        {
                            if (ourBuses[j].VehicleNum == vehNum2)//if we found the bus, stop, and j will save the index
                            {
                                break;
                            }
                        }
                        if (j == ourBuses.Count)//if the bus is not in the list
                        {
                            Console.WriteLine("Error,the bus not exist");
                            break;
                        }
                        Console.WriteLine("enter 1 for care or 2 for refueling");
                        int choice2;//the number of chice
                        bool succ2 = int.TryParse(Console.ReadLine(), out choice2);
                        while (succ2 == false || (choice2 != 1 && choice2 != 2))//if the string is not a number or the number is diffrent of 1 or 2
                        {
                            Console.WriteLine("enter again 1 for care or 2 for refueling");
                            succ2 = int.TryParse(Console.ReadLine(), out choice2);
                        }
                        if (choice2 == 1)//update the care
                        {
                            ourBuses[j].KmsLastCare = ourBuses[j].Kilometrage;//the kilometrage of care is the current
                            ourBuses[j].LastCare = DateTime.Now;//the time of care is noe
                            Console.WriteLine("The care succeeded");
                        }
                        if (choice2 == 2)//update the refueling
                        {
                            ourBuses[j].AvailableKm = 1200;//now we can drive 1200 kms
                            Console.WriteLine("The refueling succeeded");
                        }
                        break;
                    case 4://print the kilometers from the last care, from all the buses in the company
                        Console.WriteLine("The drive of every bus:");
                        foreach (Bus item in ourBuses)//for every bus
                        {
                            Console.Write(item.GetStringVehNum() + "\t");//print the string number
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
