using DLAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DS
{
    public static class DataSource
    {

        public static List<DO.AdjacentStations> ListAdjacentStations;
        public static List<DO.Bus> ListBus;
        public static List<DO.BusOnTrip> ListBusOnTrip;
        public static List<DO.Line> ListLine;
        public static List<DO.LineStation> ListLineStation;
        public static List<DO.LineTrip> ListLineTrip;
        public static List<DO.Station> ListStation;
        public static List<DO.Trip> ListTrip;
        public static List<DO.User> ListUser;
        private static Random rand = new Random(DateTime.Now.Millisecond);
        static DataSource()
        {
            InitAllList();
        }
        static void InitAllList()
        {
            ListAdjacentStations = new List<DO.AdjacentStations>();
            ListBus = new List<DO.Bus>();
            ListBusOnTrip = new List<DO.BusOnTrip>();
            ListLine = new List<DO.Line>();
            ListLineStation = new List<DO.LineStation>();
            ListLineTrip = new List<DO.LineTrip>();
            ListStation = new List<DO.Station>();
            ListTrip = new List<DO.Trip>();
            ListUser = new List<DO.User>();
            for (int i = 0; i < 10; i++)
            {
                DO.Line line = new DO.Line();
                int firstStation = 10 * i + 1;
                int lastStation = 10 * i + 10;
                line.FirstStation = firstStation;
                line.LastStation = lastStation;
                line.Code = i + 1;
                line.Id = ++DO.Counter.LineNum;
                line.Area = DO.AREA.General;
                for (int j = firstStation; j <= lastStation; j++)
                {
                    int index = j % 10;
                    if (index == 0)
                    {
                        index = 10;
                    }
                    ListStation.Add(new DO.Station { Code = j, Londitude = rand.NextDouble() * (35.5 - 34.3) + 34.3, Latitude = rand.NextDouble() * (33.3 - 31) + 31, });
                    ListLineStation.Add(new DO.LineStation { LineId = line.Id, ListLineStation = j, LineStationIndex = index, PrevStation =, NextStation = });

                }
                for (int j = firstStation; j < lastStation; j++)
                {
                    ListAdjacentStations.Add(new DO.AdjacentStations { Station1 = j, Station2 = j + 1, Distance = rand.NextDouble() * (150 - 0.1) + 0.1, Time = new TimeSpan(0, (int)(Distance * 60 / 50), 0) });
                }
                ListLine.Add(line);
                ListLineTrip.Add(new DO.LineTrip { Id = ++DO.Counter.LineTripNum++, LineId = line.Id, StartAt = new TimeSpan(), Freuquency = new TimeSpan(0, 10, 0), StartFinishAt = new TimeSpan() });

            }
        }
    }

}


