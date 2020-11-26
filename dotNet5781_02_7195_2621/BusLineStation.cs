using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7195_2621
{
    public class BusLineStation:BusStation
    {
        static public List<BusStation> allStations = new List<BusStation>();// list that contain all the stations
        private double distanceFromPrevStat;//distance from previous station
        private TimeSpan timeFromPrevStat;//time from the previous station
        public double DistanceFromPrevStat { get => distanceFromPrevStat; set => distanceFromPrevStat = value; }
        public TimeSpan TimeFromPrevStat { get => timeFromPrevStat; set => timeFromPrevStat = value; }
        //ctor
        public BusLineStation():base()
        {
            bool found = false;
            foreach (BusStation item in allStations)//check if the bus is allready exist 
            {
                if (item.BusStationKey == BusStationKey)//if the station exist
                {
                    if (item.Latitude != Latitude || item.Longitude != Longitude || (item.Adress != Adress && Adress != "" && item.Adress != ""))//if the location of the station is diffrent from the location of the new station
                    {
                        throw new ArgumentException("the station exsit in other location");
                    }
                    found = true;
                }
            }
            distanceFromPrevStat = rand.NextDouble() * (150 - 0.1) +0.1;// random number from 0.1 to 150
            timeFromPrevStat =new TimeSpan(0,(int)(distanceFromPrevStat*60/50),0);//the time is calculated as distance* 60 /50 Kmh 
            if(found==false)
                allStations.Add(this);//if we create a new station add to the list of all stations
        }
        public BusLineStation(int code, double _latitude, double _longitude, string _adress = ""):base( code, _latitude, _longitude,_adress)
        {
            bool found = false;
            foreach (BusStation item in allStations)//check if the bus is allready exist
            {
                if (item.BusStationKey == BusStationKey)//if the station exist
                {
                    if (item.Latitude != Latitude || item.Longitude != Longitude || (item.Adress != Adress && Adress != "" && item.Adress != ""))
                    {
                        throw new ArgumentException("the station exsit in other location");
                    }
                    found = false;
                }
            }
            distanceFromPrevStat = rand.NextDouble() * (150 - 0.1) + 0.1;// random number from 0.1 to 150
            timeFromPrevStat = new TimeSpan(0, (int)(distanceFromPrevStat * 60 / 50), 0);//the time is calculated as distance* 60 /50 Kmh 
            if (found == false)
                allStations.Add(this);//if we create a new station add to the list of all stations
        }
        public BusLineStation(int code, string _adress = ""):base( code, _adress )
        {
            bool found = false;
            foreach (BusStation item in allStations)//check if the bus is allready exist
            {
                if (item.BusStationKey == BusStationKey)//if the station exist
                {
                    if (item.Latitude != Latitude || item.Longitude != Longitude || (item.Adress != Adress && Adress != "" && item.Adress != ""))
                    {
                        throw new ArgumentException("the station exsit in other location");
                    }
                    found = false;
                }
            }
            distanceFromPrevStat = rand.NextDouble() * (150 - 0.1) + 0.1;// random number from 0.1 to 150
            timeFromPrevStat = new TimeSpan(0, (int)(distanceFromPrevStat * 60 / 50), 0);//the time is calculated as distance* 60 /50 Kmh 
            if (found == false)
                allStations.Add(this);//if we create a new station add to the list of all stations
        }

    }

    
    
}
