using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    sealed class DLObject : IDAL    //internal
    {
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            throw new NotImplementedException();
        }

        public void AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            throw new NotImplementedException();
        }

        public void AddLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void AddLineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }

        public void AddLineTrip(LineTrip lineTrip)
        {
            throw new NotImplementedException();
        }

        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void AddTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteAdjacentStations(int station1, int station2)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusOnTrip(int id)
        {
            throw new NotImplementedException();
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
