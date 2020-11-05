using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7195_2621
{
    enum AREA { General,North,South,Center,Jerusalem};
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
            data = busLine + " " + area+" ";
            foreach (var item in stations)
            {
                data += item.BusStationKey;
                data += " ";
            }
            return data;
        }
        public bool CheckStation(BusLineStation station)
        {
            for(int i=0;i<stations.Count;i++)
            {
                if(stations[i]==station)
                {
                    return true;
                }
            }
            return false;
        }
        public void InsertStation(BusLineStation station)
        {
            for (int i = 0; i < stations.Count; i++)
            {
                if (stations[i].BusStationKey == station.BusStationKey)
                {
                    //לזרוק חריגה
                }
            }


        }
        public BusLine SubPath(BusLineStation firstStation, BusLineStation secondStation)
        {
            BusLine subBus;
            if(CheckStation(firstStation)&&CheckStation(secondStation))
            {
                int i;
                for ( i = 0; i < stations.Count; i++)
                {
                    if (stations[i] == firstStation)
                    {
                        break;
                    }
                }
                while( stations[i]!=secondStation )
                {
                    subBus.InsertStation(stations[i]);
                }
                subBus.InsertStation(secondStation);
                subBus.FirstStstion=firstStation;
                subBus.LastStation = secondStation;
                subBus.area = area;
            }
            else
            {
                //לזרוק חריגה
            }

        }

    }
}
