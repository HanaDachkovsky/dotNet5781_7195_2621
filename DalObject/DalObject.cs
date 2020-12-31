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
            if(DataSource.ListAdjacentStations.FirstOrDefault(adjacent=> adjacent.Station1==adjacentStations.Station1 && adjacent.Station2 == adjacentStations.Station2) !=null)
            {
                //throw new 
            }
            if(DataSource.ListStation.FirstOrDefault(s=>s.Code==adjacentStations.Station1)==null|| DataSource.ListStation.FirstOrDefault(s => s.Code == adjacentStations.Station2) == null)
            {
                //throw new
            }
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }

        public void AddBus(Bus bus)
        {
            if (DataSource.ListBus.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum) != null)
            {
                //throw new 
            }
          
            DataSource.ListBus.Add(bus.Clone());
        }

        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            if (DataSource.ListBus.FirstOrDefault(b => b.LicenseNum == busOnTrip.LicenseNum) == null)
            {
                //throw new 
            }
            if (DataSource.ListLine.FirstOrDefault(b => b.Id == busOnTrip.LineId) == null )
            {
                //throw new
            }
            //להוסיף חריגה עם תחנה קודמת?
            busOnTrip.Id = ++Counter.BusOnTripNum;
            DataSource.ListBusOnTrip.Add(busOnTrip.Clone());
        }

        public void AddLine(Line line)
        {
            //חריגות אם תחנות לא קיימות? 
            line.Id = ++Counter.LineNum;
            DataSource.ListLine.Add(line.Clone());
        }

        public void AddLineStation(LineStation lineStation)
        {
            //חריגה עם תחנה קיימת בקו?
            if (DataSource.ListStation.FirstOrDefault(s => s.Code == lineStation.Station) == null || DataSource.ListLine.FirstOrDefault(l =>l.Id == lineStation.LineId) == null)
            {
                //throw new
            }
            DataSource.ListLineStation.Add(lineStation.Clone());
        }

        public void AddLineTrip(LineTrip lineTrip)
        {
            if (DataSource.ListLine.FirstOrDefault(l => l.Id == lineTrip.LineId) == null)
            {
                //throw new
            }
            lineTrip.Id = ++Counter.LineTripNum;
            DataSource.ListLineTrip.Add(lineTrip.Clone());
        }

        public void AddStation(Station station)
        {
            if(DataSource.ListStation.FirstOrDefault(s => s.Code == station.Code) != null)
            {
                //throw new
            }
            DataSource.ListStation.Add(station.Clone());
        }

        public void AddTrip(Trip trip)
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
            DO.Bus bus = DataSource.ListBus.Find(b => b.LicenseNum == licenseNum) ;
            if(bus==null)
            {
                //throw new
            }
            DataSource.ListBus.Remove(bus);
        }

        public void DeleteBusOnTrip(int id)
        {
            
        }

        public void DeleteLine(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineStation(int lineId, int station)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineTrip(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int code)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrip(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string userName)
        {
            throw new NotImplementedException();
        }

        public AdjacentStations GetAdjacentStations(int station1, int station2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdjacentStations> GetAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBus()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusOnTrip> GetAllBusOnTrip()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLine()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineStation> GetAllLineStation()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineTrip> GetAllLineTrip()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStation()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trip> GetAllTrip()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetBusBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public BusOnTrip GetBusOnTrip(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusOnTrip> GetBusOnTripBy(Predicate<BusOnTrip> predicate)
        {
            throw new NotImplementedException();
        }

        public Line GetLine(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetLine(Predicate<Line> predicate)
        {
            throw new NotImplementedException();
        }

        public LineStation GetLineStation(int lineId, int station)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineStation> GetLineStation(Predicate<LineStation> predicate)
        {
            throw new NotImplementedException();
        }

        public LineTrip GetLineTrip(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineTrip> GetLineTrip(Predicate<LineTrip> predicate)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int code)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStation(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public Trip GetTrip(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trip> GetTrip(Predicate<Trip> predicate)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUser(Predicate<User> predicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdjacentStations(int station1, int station2, Action<AdjacentStations> update)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int licenseNum, Action<Bus> update)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusOnTrip(int id, Action<BusOnTrip> update)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(int id, Action<Line> update)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineStation(int lineId, int station, Action<LineStation> update)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineTrip(LineTrip lineTrip)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineTrip(int id, Action<LineTrip> update)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(int code, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrip(int id, Action<Trip> update)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(string userName, Action<User> update)
        {
            throw new NotImplementedException();
        }
    }
}
