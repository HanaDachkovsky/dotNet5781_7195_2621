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
            try
            {
                BusList Egged = new BusList();
                for (int i = 1; i < 11; i++)//insert 10 buses 
                {
                    BusLine _bus = new BusLine(i, 2 * i - 1, 2 * i);//create a new bus, with first and last station, we create in total 20 new stations
                    Egged.Add(_bus);

                }
                Egged.Add(new BusLine(50));
                for (int i = 1; i < 11; i++)//insert to each line 2 new stations, in total we have 40 stations
                {
                    BusLineStation _bus1 = new BusLineStation(2 * i + 19);
                    BusLineStation _bus2 = new BusLineStation(2 * i + 20);
                    try
                    {
                        (Egged[i])[0].InsertLast(_bus1);
                        (Egged[i])[0].InsertLast(_bus2);
                        (Egged[50])[0].InsertFirst(_bus1);// insert to line number 50, 10 exisiting stations
                    }
                    catch (Exception msg)
                    {
                        Console.WriteLine(msg);
                    }

                }
                int choice;
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
                    while (int.TryParse(Console.ReadLine(), out choice) == false)
                    {
                        Console.WriteLine("Enter again");
                    }
                    switch (choice)
                    {
                        case 1://insert a new line 
                            try
                            {
                                {
                                    Console.WriteLine("enter bus number");
                                    int busNumber;
                                    while (int.TryParse(Console.ReadLine(), out busNumber) == false)
                                    {
                                        Console.WriteLine("Enter bus number again");
                                    }
                                    Console.WriteLine("enter the code of the first station");
                                    int first1;
                                    while (int.TryParse(Console.ReadLine(), out first1) == false)
                                    {
                                        Console.WriteLine("Enter the code of the first station again");
                                    }
                                    Console.WriteLine("enter the code of the last station");
                                    int last1;
                                    while (int.TryParse(Console.ReadLine(), out last1) == false)
                                    {
                                        Console.WriteLine("Enter the code of the last station again");
                                    }
                                    double long1 = 0, long2 = 0, lat1 = 0, lat2 = 0;
                                    int found1 = 0;//flag that  signal if the line exist in the list
                                    bool exist1 = false;//flag that signal if the station exist
                                    AREA busArea;
                                    foreach (BusLine item in Egged)
                                    {
                                        if (item.BusLineKey == busNumber)
                                        {
                                            found1++;
                                            if (item.FirstStation.BusStationKey == last1 && item.LastStation.BusStationKey == first1)//if the line to the opposite side exist
                                            {
                                                long1 = item.LastStation.Longitude;
                                                long2 = item.FirstStation.Longitude;
                                                lat1 = item.LastStation.Latitude;
                                                lat2 = item.FirstStation.Latitude;
                                                exist1 = true;
                                            }
                                        }
                                    }
                                    BusLine bus;
                                    BusLineStation firstStat;
                                    BusLineStation lastStat;
                                    if (found1 >= 2)//if the line to both sides exist
                                    {
                                        throw new InvalidOperationException("can not insert the line because this line exist in the both sides ");
                                    }
                                    if (found1 == 1 && exist1 == true)//if thre is line to only one side
                                    {
                                         firstStat = new BusLineStation(first1, lat1, long1);
                                        lastStat = new BusLineStation(last1, lat2, long2);
                                        bus = new BusLine(busNumber,firstStat, lastStat);
                                        Egged.Add(bus);
                                    }
                                    bool flag1=false;
                                    if (exist1 == false)//if there are no lines with this number
                                    {
                                        Console.WriteLine("enter bus area: General, North, South, Center or Jerusalem ");
                                        while (AREA.TryParse(Console.ReadLine(), out busArea) == false)
                                        {
                                            Console.WriteLine("Enter bus area again again");
                                        }
                                        foreach (BusStation item in BusLineStation.allStations)//check if the first station is allready exist
                                        {
                                            if (item.BusStationKey == first1)
                                            {
                                                flag1 = true;
                                                long1 = item.Longitude;
                                                lat1 = item.Latitude;
                                                break;
                                            }
                                        }
                                        if (flag1 == true)//if the station exist
                                        {
                                            firstStat = new BusLineStation(first1, lat1, long1);
                                        }
                                        else
                                        {
                                            firstStat = new BusLineStation(first1);
                                        }
                                        flag1 = false;
                                        foreach (BusStation item in BusLineStation.allStations)//check if the last station is allready exist
                                        {
                                            if (item.BusStationKey == last1)
                                            {
                                                flag1 = true;
                                                long2 = item.Longitude;
                                                lat2 = item.Latitude;
                                                break;
                                            }
                                        }
                                        if (flag1 == true)
                                        {
                                            lastStat = new BusLineStation(last1, lat2, long2);
                                        }
                                        else
                                        {
                                            lastStat = new BusLineStation(last1);
                                        }
                                        bus = new BusLine(busNumber, firstStat, lastStat);
                                        Egged.Add(bus);
                                    }
                                }
                            }
                            catch (Exception msg)
                            {
                                Console.WriteLine(msg);
                            }
                    break;


                        case 2://insert a station
                            int last2, first2;
                    Console.WriteLine("enter bus number");
                    int busNumber1;
                    while (int.TryParse(Console.ReadLine(), out busNumber1) == false)
                    {
                        Console.WriteLine("Enter bus number again");
                    }
                    Console.WriteLine("Enter the code of the first station");//read the first and the last stations code, in order to identify back or forth line
                    if (int.TryParse(Console.ReadLine(), out first2) == false)
                    {

                        throw new ArgumentException("it must be a number");

                    }
                    Console.WriteLine("Enter the code of the last station");

                    if (int.TryParse(Console.ReadLine(), out last2) == false)

                    {

                        throw new ArgumentException("it must be a number");

                    }
                    Console.WriteLine("enter the station number to add ");
                    int stationNumber;
                    double longi = 0;
                    double lat = 0;
                    bool flag2 = false;
                    BusLineStation station1;
                    while (int.TryParse(Console.ReadLine(), out stationNumber) == false)
                    {
                        Console.WriteLine("Enter station number again");
                    }
                    foreach (BusStation item in BusLineStation.allStations)//check if the station is allready exist
                    {
                        if (item.BusStationKey == stationNumber)
                        {
                            flag2 = true;
                            longi = item.Longitude;
                            lat = item.Latitude;
                            break;
                        }
                    }
                    string stationAdress;
                    if (!flag2)//if the station not exist
                    {
                        Console.WriteLine("Enter the station adress");
                        stationAdress = Console.ReadLine();
                        station1 = new BusLineStation(stationNumber, stationAdress);//create a new station
                    }
                    else//if the station exist
                    {
                        station1 = new BusLineStation(stationNumber, lat, longi);//create a new busLineStation with the details of the phisical station
                    }
                    Console.WriteLine("if you want to add the first station press 1, if you want to add station after other station press 2, and if you want to add the last station enter 3.");
                    int choice2;
                    while (int.TryParse(Console.ReadLine(), out choice2) == false)
                    {
                        Console.WriteLine("Enter your choice again");
                    }
                    try
                    {


                        switch (choice2)//choose where to insert the station
                        {
                            case 1://insert in the beginning
                                {
                                    foreach (BusLine item in Egged)
                                    {
                                        if (item.BusLineKey == busNumber1 && item.FirstStation.BusStationKey == first2 && item.LastStation.BusStationKey == last2)//forth line
                                        {
                                            item.InsertFirst(station1);
                                            break;
                                        }
                                    }
                                    foreach (BusLine item in Egged)
                                    {
                                        if (item.BusLineKey == busNumber1 && item.FirstStation.BusStationKey == last2 && item.LastStation.BusStationKey == first2)//back line
                                        {
                                            item.InsertLast(station1);
                                            break;
                                        }
                                    }
                                }
                                break;

                            case 2://insert after other station
                                Console.WriteLine("enter the code of the previous station");
                                int prevNumber;
                                while (int.TryParse(Console.ReadLine(), out prevNumber) == false)
                                {
                                    Console.WriteLine("Enter station number again");
                                }
                                foreach (BusLine item in Egged)
                                {
                                    if (item.BusLineKey == busNumber1 && item.FirstStation.BusStationKey == first2 && item.LastStation.BusStationKey == last2)
                                    {
                                        item.InsertStation(station1, prevNumber);
                                        break;
                                    }
                                }
                                break;
                            case 3://insert in the end
                                foreach (BusLine item in Egged)
                                {
                                    if (item.BusLineKey == busNumber1 && item.FirstStation.BusStationKey == first2 && item.LastStation.BusStationKey == last2)//insert in the forth line
                                    {
                                        item.InsertLast(station1);
                                        break;
                                    }
                                }
                                foreach (BusLine item in Egged)
                                {
                                    if (item.BusLineKey == busNumber1 && item.FirstStation.BusStationKey == last2 && item.LastStation.BusStationKey == first2)//insert in the back line
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
                        case 3://delete a bus line

                            try
                    {
                        int line3, first3, last3;
                        Console.WriteLine("Enter a line to delete");
                        if (int.TryParse(Console.ReadLine(), out line3) == false)
                        {
                            throw new ArgumentException("it must be a number");
                        }
                        Console.WriteLine("Enter the code of the first station");//read the first and the last stations code, in order to identify back or forth line
                        if (int.TryParse(Console.ReadLine(), out first3) == false)
                        {

                            throw new ArgumentException("it must be a number");

                        }
                        Console.WriteLine("Enter the code of the last station");

                        if (int.TryParse(Console.ReadLine(), out last3) == false)

                        {

                            throw new ArgumentException("it must be a number");

                        }
                        Egged.Delete(line3, first3, last3);//delete the line
                    }
                    catch (Exception msg)
                    {
                        Console.WriteLine(msg);
                    }
                    break;
                        case 4://delete a station from a line
                            try
                    {

                        int line4, first4, last4, statDe;

                        Console.WriteLine("Enter the line");
                        if (int.TryParse(Console.ReadLine(), out line4) == false)
                        {
                            throw new ArgumentException("it must be a number");
                        }
                        Console.WriteLine("Enter the code of the first station");//read the first and the last stations code, in order to identify back or forth line
                        if (int.TryParse(Console.ReadLine(), out first4) == false)
                        {

                            throw new ArgumentException("it must be a number");

                        }
                        Console.WriteLine("Enter the code of the last station");

                        if (int.TryParse(Console.ReadLine(), out last4) == false)

                        {

                            throw new ArgumentException("it must be a number");

                        }

                        Console.WriteLine("Enter the code of the station to delete");

                        if (int.TryParse(Console.ReadLine(), out statDe) == false)

                        {

                            throw new ArgumentException("it must be a number");

                        }

                        List<BusLine> list4 = Egged[line4];//get a list of the buses that with this line key

                        foreach (BusLine item in list4)

                        {//check for each bus is it is the exact bus(with same stations)

                            if (item.FirstStation.BusStationKey == first4 && item.LastStation.BusStationKey == last4)
                            {

                                item.DleteStation(statDe);//if it the bus- delete the station
                                break;
                            }

                        }

                    }

                    catch (Exception msg)

                    {

                        Console.WriteLine(msg);

                    }
                    break;
                        case 5://seach line in that pass through a station
                            try
                    {
                        int code5;
                        Console.WriteLine("Enter the station code");
                        if (int.TryParse(Console.ReadLine(), out code5) == false)
                        {
                            throw new ArgumentException("it must be a number");
                        }
                        List<BusLine> list5 = Egged.searchLines(code5);//get a list of the buses in this station
                        foreach (BusLine item in list5)
                        {
                            Console.WriteLine(item.BusLineKey);//print them all
                        }
                    }
                    catch (Exception msg)
                    {
                        Console.WriteLine(msg);
                    }
                    break;
                        case 6://search options to travale and print them sorted(from the fastest option to the slowest option)
                            try
                    {
                        List<BusLine> list6 = new List<BusLine>();//list that contatin the sublines that suit for the travle 
                        int firstcode, lastcode;
                        Console.WriteLine("Enter the first staion code");
                        if (int.TryParse(Console.ReadLine(), out firstcode) == false)
                        {
                            throw new ArgumentException("it must be a number");
                        }
                        Console.WriteLine("Enter the last staion code");
                        if (int.TryParse(Console.ReadLine(), out lastcode) == false)
                        {
                            throw new ArgumentException("it must be a number");
                        }
                        foreach (BusLine item in Egged)
                        {
                            if (item.CheckStation(firstcode) && item.CheckStation(lastcode))
                            {

                                list6.Add(item.SubPath(firstcode, lastcode));//insert to the list the subline from the last to the first station
                            }
                        }
                        list6.Sort();
                        foreach (BusLine item in list6)
                        {
                            Console.WriteLine(item.BusLineKey);
                        }
                    }
                    catch (Exception msg)
                    {
                        Console.WriteLine(msg);
                    }
                    break;
                        case 7://print all lines with their station
                            foreach (BusLine item in Egged)
                    {
                        Console.WriteLine(item.ToString());
                    }
                    break;
                        case 8://print all the stations and the lines that pass through them
                            try
                    {
                        foreach (BusStation item in BusLineStation.allStations)//move all over the stations
                        {
                            Console.WriteLine("station:" + item.BusStationKey);
                            Console.Write("lines: ");
                            foreach (BusLine item1 in Egged)//move all the lines
                            {
                                if (item1.CheckStation(item.BusStationKey))//check if the line pass through this station
                                {
                                    Console.Write(item1.BusLineKey + " ");
                                }
                            }
                            Console.WriteLine();
                        }
                    }
                    catch (Exception msg)
                    {
                        Console.WriteLine(msg);
                    }
                    break;
                }

                } while (choice != 0) ;
        }
            catch(Exception msg)
            {
                Console.WriteLine(msg);
            }
}
    }
}
