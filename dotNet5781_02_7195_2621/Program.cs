using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7195_2621
{
    class Program
    {
        static void Main(string[] args)
        {
            BusList Egged = new BusList();
            for (int i = 1; i < 11; i++)
            {
                BusLine _bus = new BusLine(i);
                Egged.Add(_bus);
            }
            for (int i = 1; i < 41; i++)
            {
                BusLineStation _bus = new BusLineStation(i);
            }








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
                    case 1:
                        {
                            Console.WriteLine("enter bus number and area");
                            int busNumber;
                            while (int.TryParse(Console.ReadLine(), out busNumber) == false)
                            {
                                Console.WriteLine("Enter bus number again");///////exc?
                            }
                            AREA busArea;
                            while (int.TryParse(Console.ReadLine(), out busArea) == false)
                            {
                                Console.WriteLine("Enter bus area again again");///////exc?
                            }
                            try
                            {
                                BusLine bus = new BusLine(busNumber, busArea);
                                Egged.Add(bus);
                            }
                            catch (ArgumentOutOfRangeException msg)
                            {
                                Console.WriteLine(msg);
                            }
                        }
                        break;


                    case 2:
                        Console.WriteLine("enter bus number");
                        int busNumber1;
                        while (int.TryParse(Console.ReadLine(), out busNumber1) == false)
                        {
                            Console.WriteLine("Enter bus number again");///////exc?
                        }
                        Console.WriteLine("enter the station number and adress");
                        int stationNumber;
                        while (int.TryParse(Console.ReadLine(), out stationNumber) == false)
                        {
                            Console.WriteLine("Enter station number again");///////exc?
                        }
                        string stationAdress = Console.ReadLine();
                        Console.WriteLine("if you want to add the first station press 1, if you want to add station after other station press 2, and if you want to add the last station enter 3.");
                        int choice2;
                        while (int.TryParse(Console.ReadLine(), out choice2) == false)
                        {
                            Console.WriteLine("Enter your choice again");///////exc?
                        }
                        try
                        {
                            BusLineStation station1 = new BusLineStation(stationNumber, stationAdress);

                            switch (choice2)
                            {
                                case 1:
                                    {
                                        foreach (BusLine item in Egged)
                                        {
                                            if (item.BusLineKey == busNumber1)
                                            {
                                                item.InsertFirst(station1);
                                                break;
                                            }
                                        }
                                        foreach (BusLine item in Egged)
                                        {
                                            if (item.BusLineKey == busNumber1 && item.FirstStation.BusStationKey != station1.BusStationKey)
                                            {
                                                item.InsertLast(station1);
                                                break;
                                            }
                                        }
                                    }
                                    break;

                                case 2:
                                    Console.WriteLine("enter the code of the previous station");
                                    int prevNumber;
                                    while (int.TryParse(Console.ReadLine(), out prevNumber) == false)
                                    {
                                        Console.WriteLine("Enter station number again");///////exc?
                                    }
                                    foreach (BusLine item in Egged)
                                    {
                                        if (item.BusLineKey == busNumber1)
                                        {
                                            item.InsertStation(station1, prevNumber);
                                            break;
                                        }
                                    }
                                    break;
                                case 3:
                                    foreach (BusLine item in Egged)
                                    {
                                        if (item.BusLineKey == busNumber1)
                                        {
                                            item.InsertLast(station1);
                                            break;
                                        }
                                    }
                                    foreach (BusLine item in Egged)
                                    {
                                        if (item.BusLineKey == busNumber1 && item.LastStation.BusStationKey != station1.BusStationKey)
                                        {
                                            item.InsertFirst(station1);
                                            break;
                                        }
                                    }
                                    break;
                            }
                        }
                        catch (Exception msg)
                        {
                            Console.WriteLine(msg);
                        }

                        break;
                    case 3:
                        try
                        {
                            int line3;
                            Console.WriteLine("Enter a line to delete");
                            if (int.TryParse(Console.ReadLine(), out line3) == false)
                            {
                                throw new ArgumentException("it must be a number");
                            }
                            Egged.DeleteLine(line3);////////////
                        }
                        catch (Exception msg)
                        {
                            Console.WriteLine(msg);
                        }
                    case 7:
                        foreach (BusLine item in Egged)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case 8:
                        try
                        {
                            foreach (BusStation item in BusLineStation.allStations)
                            {
                                Console.WriteLine(item.BusStationKey);
                                foreach (BusLine item1 in Egged)
                                {
                                    if (item1.CheckStation(item.BusStationKey))
                                    {
                                        Console.WriteLine(item1.BusLineKey);
                                    }
                                }
                            }
                        }
                        catch (Exception msg)
                        {
                            Console.WriteLine(msg);
                        }
                        break;
                }

            } while (choice != 0);
        }
    }
}
