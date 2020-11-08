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
        private List<BusLineStation> stations;
        private int busLine;
        private BusStation firstStstion;
        private BusStation lastStation;
        private AREA area;

        internal BusStation FirstStstion { get => stations[0]; set => stations[0] = value; }
        internal BusStation LastStation { get => stations[stations.Count-1]; set => stations[stations.Count - 1] = value; }
        internal AREA Area { get => area; set => area = value; }

        //בנאי
        public override string ToString()
        {
            string data;
            data = busLine + " " + area + " ";
            foreach (var item in stations)
            {
                data += item.BusStationKey;
                data += " ";
            }
            return data;
        }
        public bool CheckStation(BusLineStation station)
        {
            for (int i = 0; i < stations.Count; i++)
            {
                if (stations[i].BusStationKey == station.BusStationKey)
                {
                    return true;
                }
            }
            return false;
        }
        public void InsertFirst(BusLineStation newStation)
        {
            stations.Insert(0, newStation);
            firstStstion = newStation;
        }
        public void InsertLast(BusLineStation newStation)
        {
            stations.Add(newStation);
            lastStation = newStation;
        }
        public void InsertStation(BusLineStation newStation, BusLineStation prevStation)
        {
            int i;
            if (!CheckStation(prevStation))
            {
                throw new System.ArgumentException("the previous station not exsit");
            }
            for (i = 0; i < stations.Count; i++)
            {
                if (stations[i].BusStationKey == newStation.BusStationKey)
                {
                    throw new System.ArgumentException("station with the same number already exist");
                }
            }
            for (i = 0; i < stations.Count; i++)
            {
                if (stations[i].BusStationKey == prevStation.BusStationKey)
                {
                    break;
                }
            }
            stations.Insert(++i, newStation);
            if (prevStation.BusStationKey == lastStation.BusStationKey)
            {
                lastStation = newStation;
            }
        }
        public void DleteStation(BusLineStation station)
        {
            if(!CheckStation(station))
            {
                throw new System.ArgumentException("the station not exist");
            }
            int i;
            for (i = 0; i < stations.Count; i++)
            {
                if (stations[i].BusStationKey == station.BusStationKey)
                {
                    break;
                }
            }
            stations.RemoveAt(i);
        }
        public BusLine SubPath(BusLineStation firstStation, BusLineStation secondStation)
        {
            BusLine subBus=new BusLine();
            if (CheckStation(firstStation) && CheckStation(secondStation))
            {
                int i;
                for (i = 0; i < stations.Count; i++)
                {
                    if (stations[i].BusStationKey == firstStation.BusStationKey)
                    {
                        break;
                    }
                }
                subBus.InsertFirst(stations[i]);
                i++;
                while (stations[i].BusStationKey != secondStation.BusStationKey)
                {
                    subBus.InsertLast(stations[i]);
                    i++;
                }
                subBus.InsertLast(secondStation);
                subBus.Area = area;
                subBus.busLine = busLine;
                return subBus;
            }
            else
            {
                throw new System.ArgumentException("the stations or one of them not exist");
            }

        }
        public double getDistance(BusLineStation firstStation, BusLineStation secondStation)
        {
            int i = 0;
            for(;i<stations.Count;i++)
            {
                if (firstStation.BusStationKey == stations[i].BusStationKey)
                    break;
            }
            int j = 0;
            for (; j < stations.Count; j++)
            {
                if (firstStation.BusStationKey == stations[j].BusStationKey)
                    break;
            }
            if(i== stations.Count||j== stations.Count)
                throw new System.ArgumentException("the stations or one of them not exist");
            if(j<i)
            {
                int temp = j;
                j = i;
                i = temp;
            }
            if (i == j)
                throw new System.ArgumentException("the stations are equal");
            double distance=0;
            for (i = i + 1; i <= j; i++)
                distance += stations[i].DistanceFromPrevStat;
            return distance;
        }
        public TimeSpan getTime(BusLineStation firstStation, BusLineStation secondStation)
        {
            int i = 0;
            for (; i < stations.Count; i++)
            {
                if (firstStation.BusStationKey == stations[i].BusStationKey)
                    break;
            }
            int j = 0;
            for (; j < stations.Count; j++)
            {
                if (firstStation.BusStationKey == stations[j].BusStationKey)
                    break;
            }
            if (i == stations.Count || j == stations.Count)
                throw new System.ArgumentException("the stations or one of them not exist");
            if (j < i)
            {
                int temp = j;
                j = i;
                i = temp;
            }
            if (i == j)
                throw new System.ArgumentException("the stations are equal");
            TimeSpan time = new TimeSpan(0,0,0);
            for (i = i + 1; i <= j; i++)
                time += stations[i].TimeFromPrevStat;
            return time;
        }

        public int CompareTo(BusLine obj)
        {
            return getTime(this.FirstStation, lastStation).CompareTo(obj.getTime(obj.FirstStstion, obj.LastStation));
        }
        //public int CompareTimes(BusLineStation firstStation, BusLineStation secondStation)
        //{
        //    BusLine newLine = SubPath(BusLineStation firstStation, BusLineStation secondStation);
            
        //}

    }
}

    
