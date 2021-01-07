using DLAPI;
using DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL
{
    sealed class DLObject : IDAL    //internal
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion
        //לברר בקשר לבונוס של המחיקה!
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(adjacent => adjacent.Station1 == adjacentStations.Station1 && adjacent.Station2 == adjacentStations.Station2) != null)
            {
                throw new DO.BadAdjacentStationsCodesException(adjacentStations.Station1, adjacentStations.Station2, "Duplicate adjacent stations");
            }
            if (DataSource.ListStation.FirstOrDefault(s => s.Code == adjacentStations.Station1) == null)
            {//2 exs??

                throw new DO.BadStationCodeException(adjacentStations.Station1, $", bad station code: {adjacentStations.Station1}");
            }
            if (DataSource.ListStation.FirstOrDefault(s => s.Code == adjacentStations.Station2) == null)
            {
                throw new DO.BadStationCodeException(adjacentStations.Station2, $", bad station code: {adjacentStations.Station2}");
            }
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }

        public void AddBus(Bus bus)
        {
            if (DataSource.ListBus.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum) != null)
            {
                throw new DO.BadBusLicenseNumException(bus.LicenseNum, "Duplicate bus license number");
            }

            DataSource.ListBus.Add(bus.Clone());
        }

        public int AddBusOnTrip(BusOnTrip busOnTrip)
        {
            if (DataSource.ListBus.FirstOrDefault(b => b.LicenseNum == busOnTrip.LicenseNum) == null)
            {
                //throw new 
            }
            if (DataSource.ListLine.FirstOrDefault(b => b.Id == busOnTrip.LineId) == null)
            {
                //throw new
            }
            //להוסיף חריגה עם תחנה קודמת?
            busOnTrip.Id = ++Counter.BusOnTripNum;
            DataSource.ListBusOnTrip.Add(busOnTrip.Clone());
            return busOnTrip.Id;
        }

        public int AddLine(Line line)
        {
            //חריגות אם תחנות לא קיימות? 
            line.Id = ++Counter.LineNum;
            DataSource.ListLine.Add(line.Clone());
            return line.Id;
        }

        public void AddLineStation(LineStation lineStation)
        {
            //חריגה עם תחנה קיימת בקו?
            //שרהלה: אני מוסיפה
            if (DataSource.ListLineStation.FirstOrDefault(ls => ls.Station == lineStation.Station && ls.LineId == lineStation.LineId) != null)
            {
                throw new DO.BadLineStationIdException(lineStation.LineId, lineStation.Station, "Duplicate line stations");
            }
            if (DataSource.ListStation.FirstOrDefault(s => s.Code == lineStation.Station) == null)
            {
                throw new DO.BadStationCodeException(lineStation.Station, $", bad station code: {lineStation.Station}");
            }
            if (DataSource.ListLine.FirstOrDefault(l => l.Id == lineStation.LineId) == null)
            {
                throw new DO.BadLineIdException(lineStation.LineId, ", bad line id: {lineStation.LineId}");
            }
            DataSource.ListLineStation.Add(lineStation.Clone());
        }
        //up to here
        public int AddLineTrip(LineTrip lineTrip)
        {
            if (DataSource.ListLine.FirstOrDefault(l => l.Id == lineTrip.LineId) == null)
            {
                throw new DO.BadLineIdException(lineTrip.LineId, ", bad line id: {lineTrip.LineId}");
            }
            lineTrip.Id = ++Counter.LineTripNum;
            DataSource.ListLineTrip.Add(lineTrip.Clone());
            return lineTrip.Id;
        }

        public void AddStation(Station station)
        {
            if (DataSource.ListStation.FirstOrDefault(s => s.Code == station.Code) != null)
            {
                //throw new
            }
            DataSource.ListStation.Add(station.Clone());
        }

        public int AddTrip(Trip trip)
        {
            if (DataSource.ListLine.FirstOrDefault(l => l.Id == trip.LineId) == null)
            {
                //throw new
            }
            if (DataSource.ListUser.FirstOrDefault(u => u.UserName == trip.UserName) == null)
            {
                //throw new
            }
            //חריגות של תחנה?
            trip.Id = ++Counter.TripNum;
            DataSource.ListTrip.Add(trip.Clone());
            return trip.Id;
        }

        public void AddUser(User user)
        {
            if (DataSource.ListUser.FirstOrDefault(u => u.UserName == user.UserName) != null)
            {
                //throw new
            }
            DataSource.ListUser.Add(user.Clone());
        }

        public void DeleteAdjacentStations(int station1, int station2)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int licenseNum)
        {
            DO.Bus bus = DataSource.ListBus.Find(b => b.LicenseNum == licenseNum);
            if (bus == null)
            {
                //throw new
            }
            DataSource.ListBus.Remove(bus);
        }

        public void DeleteBusOnTrip(int id)
        {
            DO.BusOnTrip busOnTrip = DataSource.ListBusOnTrip.Find(b => b.Id == id);
            if (busOnTrip == null)
            {
                //throw new
            }
            DataSource.ListBusOnTrip.Remove(busOnTrip);
        }

        public void DeleteLine(int id)
        {
            DO.Line line = DataSource.ListLine.Find(l => l.Id == id);
            if (line == null)
            {
                //throw new
            }
            DataSource.ListLine.Remove(line);
        }

        public void DeleteLineStation(int lineId, int station)
        {
            DO.LineStation lineStation = DataSource.ListLineStation.Find(l => l.LineId == lineId && l.Station == station);
            if (lineStation == null)
            {
                //throw new
            }
            DataSource.ListLineStation.Remove(lineStation);
        }

        public void DeleteLineTrip(int id)
        {
            DO.LineTrip lineTrip = DataSource.ListLineTrip.Find(l => l.Id == id);
            if (lineTrip == null)
            {
                //throw new
            }
            DataSource.ListLineTrip.Remove(lineTrip);
        }

        public void DeleteStation(int code)
        {
            DO.Station station = DataSource.ListStation.Find(s => s.Code == code);
            if (station == null)
            {
                //throw new
            }
            DataSource.ListStation.Remove(station);
        }

        public void DeleteTrip(int id)
        {
            DO.Trip trip = DataSource.ListTrip.Find(t => t.Id == id);
            if (trip == null)
            {
                //throw new
            }
            DataSource.ListTrip.Remove(trip);
        }

        public void DeleteUser(string userName)
        {
            DO.User user = DataSource.ListUser.Find(u => u.UserName == userName);
            if (user == null)
            {
                //throw new
            }
            DataSource.ListUser.Remove(user);
        }

        public AdjacentStations GetAdjacentStations(int station1, int station2)
        {/////?
            if (station1 == 0)
            {
                return new DO.AdjacentStations { Station1 = 0, Station2 = station2, Distance = 0, Time = new TimeSpan(0, 0, 0) };
            }
            DO.AdjacentStations adjacentStations = DataSource.ListAdjacentStations.Find(a => a.Station1 == station1 && a.Station2 == station2);
            if (adjacentStations == null)
            {
                //throw new
            }
            return adjacentStations.Clone();
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            return from adjacentStations in DataSource.ListAdjacentStations
                   where predicate(adjacentStations)
                   select adjacentStations.Clone();
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return from adjacentStations in DataSource.ListAdjacentStations
                   select adjacentStations.Clone();
        }

        public IEnumerable<Bus> GetAllBus()
        {
            return from bus in DataSource.ListBus
                   select bus.Clone();
        }

        public IEnumerable<BusOnTrip> GetAllBusOnTrip()
        {
            return from busOnTrip in DataSource.ListBusOnTrip
                   select busOnTrip.Clone();
        }

        public IEnumerable<Line> GetAllLine()
        {
            return from line in DataSource.ListLine
                   select line.Clone();
        }

        public IEnumerable<LineStation> GetAllLineStation()
        {
            return from lineStation in DataSource.ListLineStation
                   select lineStation.Clone();
        }

        public IEnumerable<LineTrip> GetAllLineTrip()
        {
            return from lineTrip in DataSource.ListLineTrip
                   select lineTrip.Clone();
        }

        public IEnumerable<Station> GetAllStation()
        {
            return from station in DataSource.ListStation
                   select station.Clone();
        }

        public IEnumerable<Trip> GetAllTrip()
        {
            return from trip in DataSource.ListTrip
                   select trip.Clone();
        }

        public IEnumerable<User> GetAllUser()
        {
            return from user in DataSource.ListUser
                   select user.Clone();
        }

        public Bus GetBus(int licenseNum)
        {
            DO.Bus bus = DataSource.ListBus.Find(b => b.LicenseNum == licenseNum);
            if (bus == null)
            {
                //throw new
            }
            return bus.Clone();
        }

        public IEnumerable<Bus> GetAllBusBy(Predicate<Bus> predicate)
        {
            return from bus in DataSource.ListBus
                   where predicate(bus)
                   select bus.Clone();
        }

        public BusOnTrip GetBusOnTrip(int id)
        {
            DO.BusOnTrip busOnTrip = DataSource.ListBusOnTrip.Find(b => b.Id == id);
            if (busOnTrip == null)
            {
                //throw new
            }
            return busOnTrip.Clone();
        }

        public IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate)
        {
            return from busOnTrip in DataSource.ListBusOnTrip
                   where predicate(busOnTrip)
                   select busOnTrip.Clone();
        }

        public Line GetLine(int id)
        {
            DO.Line line = DataSource.ListLine.Find(l => l.Id == id);
            if (line == null)
            {
                //throw new
            }
            return line.Clone();
        }

        public IEnumerable<Line> GetAllLineBy(Predicate<Line> predicate)
        {
            return from line in DataSource.ListLine
                   where predicate(line)
                   select line.Clone();
        }

        public LineStation GetLineStation(int lineId, int station)
        {
            DO.LineStation lineStation = DataSource.ListLineStation.Find(ls => ls.LineId == lineId && ls.Station == station);
            if (lineStation == null)
            {
                //throw new
            }
            return lineStation.Clone();
        }

        public IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate)
        {
            return from lineStation in DataSource.ListLineStation
                   where predicate(lineStation)
                   select lineStation.Clone();
        }

        public LineTrip GetLineTrip(int id)
        {
            DO.LineTrip lineTrip = DataSource.ListLineTrip.Find(lt => lt.Id == id);
            if (lineTrip == null)
            {
                //throw new
            }
            return lineTrip.Clone();
        }

        public IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate)
        {
            return from lineTrip in DataSource.ListLineTrip
                   where predicate(lineTrip)
                   select lineTrip.Clone();
        }

        public Station GetStation(int code)
        {
            DO.Station station = DataSource.ListStation.Find(s => s.Code == code);
            if (station == null)
            {
                //throw
            }
            return station.Clone();
        }

        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate)
        {
            return from station in DataSource.ListStation
                   where predicate(station)
                   select station.Clone();
        }

        public Trip GetTrip(int id)
        {
            DO.Trip trip = DataSource.ListTrip.Find(t => t.Id == id);
            if (trip == null)
            {
                //throw new
            }
            return trip.Clone();
        }

        public IEnumerable<Trip> GetAllTripBy(Predicate<Trip> predicate)
        {
            return from trip in DataSource.ListTrip
                   where predicate(trip)
                   select trip.Clone();
        }

        public User GetUser(string userName)
        {
            DO.User user = DataSource.ListUser.Find(u => u.UserName == userName);
            if (user == null)
            {
                //throw new
            }
            return user.Clone();
        }

        public IEnumerable<User> GetAllUserBy(Predicate<User> predicate)
        {
            return from user in DataSource.ListUser
                   where predicate(user)
                   select user.Clone();
        }

        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            DO.AdjacentStations adj = DataSource.ListAdjacentStations.Find(a => a.Station1 == adjacentStations.Station1 && a.Station2 == adjacentStations.Station2);

            if (adj == null)
            {
                //throw new
            }
            DataSource.ListAdjacentStations.Remove(adj);
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());

        }

        public void UpdateAdjacentStations(int station1, int station2, Action<AdjacentStations> update)
        {
            DO.AdjacentStations adj = DataSource.ListAdjacentStations.Find(a => a.Station1 == station1);

            if (adj == null)
            {
                //throw new
            }
            update(adj);

        }

        public void UpdateBus(Bus bus)
        {
            DO.Bus bu = DataSource.ListBus.Find(b => b.LicenseNum == bus.LicenseNum);

            if (bu == null)
            {
                //throw new
            }
            DataSource.ListBus.Remove(bu);
            DataSource.ListBus.Add(bus.Clone());
        }

        public void UpdateBus(int licenseNum, Action<Bus> update)
        {
            DO.Bus bu = DataSource.ListBus.Find(b => b.LicenseNum == licenseNum);

            if (bu == null)
            {
                //throw new
            }
            update(bu);
        }

        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            DO.BusOnTrip busOn = DataSource.ListBusOnTrip.Find(b => b.Id == busOnTrip.Id);

            if (busOn == null)
            {
                //throw new
            }
            DataSource.ListBusOnTrip.Remove(busOn);
            DataSource.ListBusOnTrip.Add(busOnTrip.Clone());
        }

        public void UpdateBusOnTrip(int id, Action<BusOnTrip> update)
        {
            DO.BusOnTrip busOn = DataSource.ListBusOnTrip.Find(b => b.Id == id);

            if (busOn == null)
            {
                //throw new
            }
            update(busOn);
        }

        public void UpdateLine(Line line)
        {
            DO.Line lin = DataSource.ListLine.Find(l => l.Id == line.Id);

            if (lin == null)
            {
                //throw new
            }
            DataSource.ListLine.Remove(lin);
            DataSource.ListLine.Add(line.Clone());
        }

        public void UpdateLine(int id, Action<Line> update)
        {
            DO.Line lin = DataSource.ListLine.Find(l => l.Id == id);

            if (lin == null)
            {
                //throw new
            }
            update(lin);
        }

        public void UpdateLineStation(LineStation lineStation)
        {
            DO.LineStation lineS = DataSource.ListLineStation.Find(ls => ls.LineId == lineStation.LineId && ls.Station == lineStation.Station);

            if (lineS == null)
            {
                //throw new
            }
            DataSource.ListLineStation.Remove(lineS);
            DataSource.ListLineStation.Add(lineStation.Clone());
        }

        public void UpdateLineStation(int lineId, int station, Action<LineStation> update)
        {
            DO.LineStation lineS = DataSource.ListLineStation.Find(ls => ls.LineId == lineId && ls.Station == station);

            if (lineS == null)
            {
                //throw new
            }
            update(lineS);
        }

        public void UpdateLineTrip(LineTrip lineTrip)
        {
            DO.LineTrip lineT = DataSource.ListLineTrip.Find(lt => lt.Id == lineTrip.Id);

            if (lineT == null)
            {
                //throw new
            }
            DataSource.ListLineTrip.Remove(lineT);
            DataSource.ListLineTrip.Add(lineTrip.Clone());
        }

        public void UpdateLineTrip(int id, Action<LineTrip> update)
        {
            DO.LineTrip lineT = DataSource.ListLineTrip.Find(lt => lt.Id == id);

            if (lineT == null)
            {
                //throw new
            }
            update(lineT);
        }

        public void UpdateStation(Station station)
        {
            DO.Station stat = DataSource.ListStation.Find(s => s.Code == station.Code);

            if (stat == null)
            {
                //throw new
            }
            DataSource.ListStation.Remove(stat);
            DataSource.ListStation.Add(station.Clone());
        }

        public void UpdateStation(int code, Action<Station> update)
        {
            DO.Station stat = DataSource.ListStation.Find(s => s.Code == code);

            if (stat == null)
            {
                //throw new
            }
            update(stat);
        }

        public void UpdateTrip(Trip trip)
        {
            DO.Trip tri = DataSource.ListTrip.Find(t => t.Id == trip.Id);

            if (tri == null)
            {
                //throw new
            }
            DataSource.ListTrip.Remove(tri);
            DataSource.ListTrip.Add(trip.Clone());
        }

        public void UpdateTrip(int id, Action<Trip> update)
        {
            DO.Trip tri = DataSource.ListTrip.Find(t => t.Id == id);

            if (tri == null)
            {
                //throw new
            }
            update(tri);
        }

        public void UpdateUser(User user)
        {
            DO.User use = DataSource.ListUser.Find(u => u.UserName == user.UserName);

            if (use == null)
            {
                //throw new
            }
            DataSource.ListUser.Remove(use);
            DataSource.ListUser.Add(user.Clone());
        }

        public void UpdateUser(string userName, Action<User> update)
        {
            DO.User use = DataSource.ListUser.Find(u => u.UserName == userName);

            if (use == null)
            {
                //throw new
            }
            update(use);
        }
    }
}
