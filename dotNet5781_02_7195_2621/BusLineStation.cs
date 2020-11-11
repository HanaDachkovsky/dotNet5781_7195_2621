﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7195_2621
{
    class BusLineStation:BusStation
    {
        static public List<BusStation> allStations = new List<BusStation>();
        private double distanceFromPrevStat;//distance from previous station
        private TimeSpan timeFromPrevStat;//time from the previous station
        public double DistanceFromPrevStat { get => distanceFromPrevStat; set => distanceFromPrevStat = value; }
        public TimeSpan TimeFromPrevStat { get => timeFromPrevStat; set => timeFromPrevStat = value; }
        public BusLineStation():base()
        {
            bool found = false;
            foreach (BusStation item in allStations)
            {
                if (item.BusStationKey == BusStationKey)
                {
                    if (item.Latitude != Latitude || item.Longitude != Longitude || (item.Adress != Adress && Adress != "" && item.Adress != ""))
                    {
                        //throw;
                    }
                    found = true;
                }
            }
            distanceFromPrevStat = rand.NextDouble() * (150 - 0.1) +0.1;
            timeFromPrevStat =new TimeSpan(0,(int)(distanceFromPrevStat*60/50),0);//?
            if(found==false)
                allStations.Add(this);//?
        }
        public BusLineStation(int code, double _latitude, double _longitude, string _adress = ""):base( code, _latitude, _longitude,_adress)
        {
            bool found = false;
            foreach (BusStation item in allStations)
            {
                if (item.BusStationKey == BusStationKey)
                {
                    if (item.Latitude != Latitude || item.Longitude != Longitude || (item.Adress != Adress && Adress != "" && item.Adress != ""))
                    {
                        //throw;
                    }
                    found = false;
                }
            }
            distanceFromPrevStat = rand.NextDouble() * (150 - 0.1) + 0.1;
            timeFromPrevStat = new TimeSpan(0, (int)(distanceFromPrevStat * 60 / 50), 0);
            if (found == false)
                allStations.Add(this);//?
        }
        public BusLineStation(int code, string _adress = ""):base( code, _adress )
        {
            bool found = false;
            foreach (BusStation item in allStations)
            {
                if (item.BusStationKey == BusStationKey)
                {
                    if (item.Latitude != Latitude || item.Longitude != Longitude || (item.Adress != Adress && Adress != "" && item.Adress != ""))
                    {
                        //throw;
                    }
                    found = false;
                }
            }
            distanceFromPrevStat = rand.NextDouble() * (150 - 0.1) + 0.1;
            timeFromPrevStat = new TimeSpan(0, (int)(distanceFromPrevStat * 60 / 50), 0);
            if (found == false)
                allStations.Add(this);//?
        }

    }

    
    
}
