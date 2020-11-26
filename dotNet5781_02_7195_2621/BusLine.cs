using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7195_2621
{
    
    enum AREA { General, North, South, Center, Jerusalem };
    class BusLine:IComparable
    {
       
        private List<BusLineStation> stations=new List<BusLineStation>();//list that contain all the stations in the line
        private int busLineKey;
        private BusLineStation firstStation;
        private BusLineStation lastStation;
        private AREA area;

        
        internal AREA Area { get => area; set => area = value; }
        internal BusLineStation FirstStation { get => firstStation; set => firstStation = value; }
        internal BusLineStation LastStation { get => lastStation; set => lastStation = value; }
        public int BusLineKey { get => busLineKey; private set => busLineKey = value; }
        static Random rand= new Random(DateTime.Now.Millisecond);
        //ctor
        public  BusLine()
        {
            busLineKey = rand.Next(1000);
            area = 0;
            firstStation = new BusLineStation();
            lastStation = new BusLineStation();
            FirstStation.DistanceFromPrevStat = 0;
            FirstStation.TimeFromPrevStat = new TimeSpan(0, 0, 0);
            stations.Add(firstStation);
            stations.Add(lastStation);
        }
        public BusLine(int _busLineKey, int first,int last)
        {
            if (_busLineKey < 0)
            {
                throw new ArgumentOutOfRangeException("the bus number is unvalid");
            }
            busLineKey = _busLineKey;
            area = 0;
            firstStation = new BusLineStation(first);
            lastStation = new BusLineStation(last);
            FirstStation.DistanceFromPrevStat = 0;
            FirstStation.TimeFromPrevStat = new TimeSpan(0, 0, 0);
            stations.Add(firstStation);
            stations.Add(lastStation);
        }
        public BusLine(int _busLineKey, BusLineStation first, BusLineStation last,AREA _area=0)
        {
            if (_busLineKey < 0)
            {
                throw new ArgumentOutOfRangeException("the bus number is unvalid");
            }
            busLineKey = _busLineKey;
            area = _area;
            firstStation = new BusLineStation(first.BusStationKey,first.Latitude, first.Longitude, first.Adress);
            lastStation = new BusLineStation(last.BusStationKey,last.Latitude,last.Longitude,last.Adress);
            FirstStation.DistanceFromPrevStat = 0;
            FirstStation.TimeFromPrevStat = new TimeSpan(0, 0, 0);
            stations.Add(firstStation);
            stations.Add(lastStation);
        }
        public BusLine(int _busLineKey = 0,AREA _area=0)
        {
            if(_busLineKey==0)
            {
                _busLineKey = rand.Next(1000);
            }
            if(_busLineKey<0)
            {
                throw new ArgumentOutOfRangeException("the bus number is unvalid");
            }
            busLineKey = _busLineKey;
            area = _area;
            firstStation = new BusLineStation();
            lastStation = new BusLineStation();
            FirstStation.DistanceFromPrevStat = 0;
            FirstStation.TimeFromPrevStat = new TimeSpan(0, 0, 0);
            stations.Add(firstStation);
            stations.Add(lastStation);
          
        }
        public override string ToString()
        {
            string data;
            data = BusLineKey + " " + area + " ";
            foreach (var item in stations)// move over all the stations in the line
            {
                data += item.BusStationKey;
                data += " ";
            }
            return data;
        }
        public bool CheckStation(int stationCode)//check if the station in the line
        {
            for (int i = 0; i < stations.Count; i++)
            {
                if (stations[i].BusStationKey == stationCode)
                {
                    return true;
                }
            }
            return false;
        }
        public void InsertFirst(BusLineStation newStation)//insert station as the first station in the line
        {

            FirstStation = newStation;
            FirstStation.DistanceFromPrevStat = 0;
            FirstStation.TimeFromPrevStat = new TimeSpan(0, 0, 0);
            stations.Insert(0, firstStation);
            stations[1].DistanceFromPrevStat = rand.NextDouble() * (150 - 0.1) + 0.1;// random number from 0.1 to 150
            stations[1].TimeFromPrevStat = new TimeSpan(0, (int)(stations[1].DistanceFromPrevStat * 60 / 50), 0);//the time is calculated as distance* 60 /50 Kmh

        }
            
        public void InsertLast(BusLineStation newStation)//insert station as the last station in the line
        {
            stations.Add(newStation);
            LastStation = newStation;
        }
        public void InsertStation(BusLineStation newStation, int prevStation)//insert station after other station
        {
            int i;
            if (!CheckStation(prevStation))//if the previous station not exist in the line
            {
                throw new System.ArgumentException("the previous station not exsit");
            }
            for (i = 0; i < stations.Count; i++)// if station with the same number as the new station exist in the line
            {
                if (stations[i].BusStationKey == newStation.BusStationKey)
                {
                    throw new System.ArgumentException("station with the same number already exist");
                }
            }
            for (i = 0; i < stations.Count; i++)//find the index of the privios station in the line
            {
                if (stations[i].BusStationKey == prevStation)
                {
                    break;
                }
            }
            if (prevStation == LastStation.BusStationKey)
            {
                LastStation = newStation;
            }
            else
            {
                stations.Insert(++i, newStation);//insert the new station in the next index
                stations[++i].DistanceFromPrevStat = rand.NextDouble() * (150 - 0.1) + 0.1;
                stations[i].TimeFromPrevStat = new TimeSpan(0, (int)(stations[1].DistanceFromPrevStat * 60 / 50), 0);
            }     
        }
        public void DleteStation(int station)
        {
            if(!CheckStation(station))//if the previous station not exist in the line
            {
                throw new System.ArgumentException("the station not exist");
            }
            if (stations.Count <= 2)//if there aree less than 2 station in the line 
            {
                throw new InvalidOperationException("you cannot delete station,because the are 2 or less stations in the line");
            }
            int i;
            for (i = 0; i < stations.Count; i++)//find the index of the privios station in the line
            {
                if (stations[i].BusStationKey == station)
                {
                    break;
                }
            }
            stations[i+1].DistanceFromPrevStat += stations[i].DistanceFromPrevStat;//the distance of the previous station of the next station is it distanse + the distance of the previos station of the deleted station
            stations[i+1].TimeFromPrevStat += stations[i].TimeFromPrevStat;//the time of the previous station of the next station is it time+ the time of the previos station of the deleted station
            stations.RemoveAt(i);
        }
        public BusLine SubPath(int firstStation, int secondStation)// return the subline from the first to the last station
        {
            BusLine subBus=new BusLine();
            subBus.stations.RemoveAt(0);//remove the first station(random station)
            if (CheckStation(firstStation) && CheckStation(secondStation))//chack if the stations exist in the line
            {
                int i;
                for (i = 0; i < stations.Count; i++)//find the index of the first station
                {
                    if (stations[i].BusStationKey == firstStation)
                    {
                        break;
                    }
                }
                subBus.InsertFirst(stations[i]);//insert the first station to the subline
                subBus.stations.RemoveAt(1);//remove the last station (random station)
                i++;
                while (stations[i].BusStationKey != secondStation)//find the index of the second station
                {
                    subBus.InsertLast(stations[i]);//insert all the stations until the last station
                    i++;
                }
                subBus.InsertLast(stations[i]);//insert the second station
                subBus.Area = area;
                subBus.BusLineKey = BusLineKey;
                return subBus;
            }
            else
            {
                throw new System.ArgumentException("the stations or one of them not exist");
            }

        }
        public double getDistance(BusLineStation firstStation, BusLineStation secondStation)//get the distance between 2 stations
        {
            int i = 0;//the index of the first station
            for(;i<stations.Count;i++)
            {
                if (firstStation.BusStationKey == stations[i].BusStationKey)//find the index of the first station
                    break;
            }
            int j = 0;//the index of the second station
            for (; j < stations.Count; j++)
            {
                if (lastStation.BusStationKey == stations[j].BusStationKey)//find the index of the first station
                    break;
            }
            if(i== stations.Count||j== stations.Count)//if the stations or one of them not exist
                throw new System.ArgumentException("the stations or one of them not exist");
            if(j<i)//if the order of the station is opposite, swap the stations
            {
                int temp = j;
                j = i;
                i = temp;
            }
            if (i == j)//if the are the same
                throw new System.ArgumentException("the stations are equal");
            double distance=0;
            for (i = i + 1; i <= j; i++)
                distance += stations[i].DistanceFromPrevStat;//distance=sum of all the distances between the stations
            return distance;
        }
        public TimeSpan getTime(BusLineStation firstStation, BusLineStation secondStation)//get the time between 2 stations
        {
            int i = 0;//the index of the first station
            for (; i < stations.Count; i++)
            {
                if (firstStation.BusStationKey == stations[i].BusStationKey)//find the index of the first station
                    break;
            }
            int j = 0;//the index of the second station
            for (; j < stations.Count; j++)
            {
                if (lastStation.BusStationKey == stations[j].BusStationKey)//find the index of the first station
                    break;
            }
            if (i == stations.Count || j == stations.Count)//if the stations or one of them not exist
                throw new System.ArgumentException("the stations or one of them not exist");
            if (j < i)//if the order of the station is opposite, swap the stations
            {
                int temp = j;
                j = i;
                i = temp;
            }
            if (i == j)//if the are the same
                throw new System.ArgumentException("the stations are equal");
            TimeSpan time = new TimeSpan(0,0,0);
            for (i = i + 1; i <= j; i++)
                time += stations[i].TimeFromPrevStat;//time=sum of all the time between the stations
            return time;
        }

        public int CompareTo(object obj)
        {
            BusLine line = (BusLine)obj;
            return getTime(FirstStation, LastStation).CompareTo(line.getTime(line.FirstStation, line.LastStation));//compare by the total time
        }



    }
}

    
