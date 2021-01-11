//using System;
//using System.Collections.Generic;
//using System.Text;
//using DO;

//namespace DLAPI
//{
//    public interface IDAL
//    {
//        #region AdjacentStations
//        void AddAdjacentStations(DO.AdjacentStations adjacentStations);
//        DO.AdjacentStations GetAdjacentStations(int station1, int station2);
//        IEnumerable<DO.AdjacentStations> GetAllAdjacentStations();
//        IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate);

//        void UpdateAdjacentStations(DO.AdjacentStations adjacentStations);//?
//        void UpdateAdjacentStations(int station1, int station2, Action<DO.AdjacentStations> update);
//        void DeleteAdjacentStations(int station1, int station2);

//        #endregion

//        #region Bus
//        void AddBus(DO.Bus bus);
//        DO.Bus GetBus(int licenseNum);
//        IEnumerable<DO.Bus> GetAllBus();
//        IEnumerable<DO.Bus> GetAllBusBy(Predicate<DO.Bus> predicate);
//        void UpdateBus(DO.Bus bus);//?
//        void UpdateBus(int licenseNum, Action<DO.Bus> update);
//        void DeleteBus(int licenseNum);
//        #endregion

//        #region BusOnTrip
//        int AddBusOnTrip(DO.BusOnTrip busOnTrip);
//        DO.BusOnTrip GetBusOnTrip(int id);
//        IEnumerable<DO.BusOnTrip> GetAllBusOnTrip();
//        IEnumerable<DO.BusOnTrip> GetAllBusOnTripBy(Predicate<DO.BusOnTrip> predicate);
//        void UpdateBusOnTrip(DO.BusOnTrip busOnTrip);//?
//        void UpdateBusOnTrip(int id, Action<DO.BusOnTrip> update);
//        void DeleteBusOnTrip(int id);
//        #endregion

//        #region Line
//        int AddLine(DO.Line line);
//        DO.Line GetLine(int id);
//        IEnumerable<DO.Line> GetAllLine();
//        IEnumerable<DO.Line> GetAllLineBy(Predicate<DO.Line> predicate);
//        void UpdateLine(DO.Line line);//?
//        void UpdateLine(int id, Action<DO.Line> update);
//        void DeleteLine(int id);
//        #endregion

//        #region LineStation
//        void AddLineStation(DO.LineStation lineStation);
//        DO.LineStation GetLineStation(int lineId, int station);
//        IEnumerable<DO.LineStation> GetAllLineStation();
//        IEnumerable<DO.LineStation> GetAllLineStationBy(Predicate<DO.LineStation> predicate);
//        void UpdateLineStation(DO.LineStation lineStation);//?
//        void UpdateLineStation(int lineId, int station, Action<DO.LineStation> update);
//        void DeleteLineStation(int lineId, int station);
//        #endregion

//        #region LineTrip
//        int AddLineTrip(DO.LineTrip lineTrip);
//        DO.LineTrip GetLineTrip(int id);
//        IEnumerable<DO.LineTrip> GetAllLineTrip();
//        IEnumerable<DO.LineTrip> GetAllLineTripBy(Predicate<DO.LineTrip> predicate);
//        void UpdateLineTrip(DO.LineTrip lineTrip);//?
//        void UpdateLineTrip(int id, Action<DO.LineTrip> update);
//        void DeleteLineTrip(int id);
//        #endregion

//        #region Station
//        void AddStation(DO.Station station);
//        DO.Station GetStation(int code);
//        IEnumerable<DO.Station> GetAllStation();
//        IEnumerable<DO.Station> GetAllStationBy(Predicate<DO.Station> predicate);
//        void UpdateStation(DO.Station station);//?
//        void UpdateStation(int code, Action<DO.Station> update);
//        void DeleteStation(int code);
//        #endregion

//        #region Trip
//        int AddTrip(DO.Trip trip);
//        DO.Trip GetTrip(int id);
//        IEnumerable<DO.Trip> GetAllTrip();
//        IEnumerable<DO.Trip> GetAllTripBy(Predicate<DO.Trip> predicate);
//        void UpdateTrip(DO.Trip trip);//?
//        void UpdateTrip(int id, Action<DO.Trip> update);
//        void DeleteTrip(int id);
//        #endregion

//        #region User
//        void AddUser(DO.User user);
//        DO.User GetUser(string userName);
//        IEnumerable<DO.User> GetAllUser();
//        IEnumerable<DO.User> GetAllUserBy(Predicate<DO.User> predicate);
//        void UpdateUser(DO.User user);//?
//        void UpdateUser(string userName, Action<DO.User> update);
//        void DeleteUser(string userName);
//        #endregion
//    }
//}