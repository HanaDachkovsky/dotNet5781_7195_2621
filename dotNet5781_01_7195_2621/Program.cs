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
            List<Bus>ourBuses=new List<Bus>();
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
                    Console.WriteLine("Enter the vehicle license number");
                    string vehNum;
                    vehNum = Console.ReadLine();
                    Random rand = new Random(DateTime.Now.Millisecond);
                    int num = rand.Next();
                    for(int i=0;i<ourBuses.Count;i++)
                    {
                        TimeSpan timeFromLastCare = new TimeSpan();
                        timeFromLastCare = DateTime.Now - ourBuses[i].LastCare;
                        if (ourBuses[i].VehicleNum==vehNum)
                        {
                            if(ourBuses[i].AvailableKm>=num&&ourBuses[i].LastKm<20000&& timeFromLastCare.TotalDays<365)
                            {
                                ourBuses[i].LastKm += num;
                                ourBuses[i].Kilometrage += num;
                                ourBuses[i].AvailableKm -= num;
                            }
                            else
                            {
                                cw
                            }
                        }
                        
                    }

                    break;
                default:
                    break;
            }





            Console.ReadKey();
        }
    }
}
