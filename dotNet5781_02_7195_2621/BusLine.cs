using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7195_2621
{
    enum AREA { General, North, South, Center, Jerusalem };
    class BusLine
    {
        private List<BusLineStation> stations;
        private int busLine;
        private BusStation firstStstion;
        private BusStation lastStation;
        private AREA area;

        internal BusStation FirstStstion { get => firstStstion; set => firstStstion = value; }
        internal BusStation LastStation { get => lastStation; set => lastStation = value; }
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
                if (stations[i] == station)
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
                if (stations[i] == prevStation)
                {
                    break;
                }
            }
            stations.Insert(++i, newStation);
            if (prevStation == lastStation)
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
                if (stations[i] == station)
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
                    if (stations[i] == firstStation)
                    {
                        break;
                    }
                }
                subBus.InsertFirst(stations[i]);
                i++;
                while (stations[i] != secondStation)
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
    }
}

    
