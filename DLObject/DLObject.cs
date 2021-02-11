using DLAPI;
using DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL
{
    sealed class DLObject : IDAL    
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion
        
        #region AdjacentStations
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(adjacent => adjacent.Station1 == adjacentStations.Station1 && adjacent.Station2 == adjacentStations.Station2) != null)
            {
                throw new DO.BadAdjacentStationsCodesException(adjacentStations.Station1, adjacentStations.Station2, "Duplicate adjacent stations");
            }
            if (DataSource.ListStation.FirstOrDefault(s => s.Code == adjacentStations.Station1) == null)
            {//2 exs??

                throw new DO.BadStationCodeException(adjacentStations.Station1, $" bad station code: {adjacentStations.Station1}");
            }
            if (DataSource.ListStation.FirstOrDefault(s => s.Code == adjacentStations.Station2) == null)
            {
                throw new DO.BadStationCodeException(adjacentStations.Station2, $" bad station code: {adjacentStations.Station2}");
            }
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }
        public void DeleteAdjacentStations(int station1, int station2)
        {
            DO.AdjacentStations adj = DataSource.ListAdjacentStations.Find(a => a.Station1 == station1 && a.Station2 == station2);
            if (adj == null)
            {
                throw new BadAdjacentStationsCodesException(station1, station2, $"bad adjecent stations codes: { station1 } and {station2} ");
            }
            DataSource.ListAdjacentStations.Remove(adj);
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
                throw new BadAdjacentStationsCodesException(station1, station2, $"bad stations codes: {station1} and {station2}");
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
        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            DO.AdjacentStations adj = DataSource.ListAdjacentStations.Find(a => a.Station1 == adjacentStations.Station1 && a.Station2 == adjacentStations.Station2);

            if (adj == null)
            {
                throw new BadAdjacentStationsCodesException(adjacentStations.Station1, adjacentStations.Station2, $"bad stations codes: {adjacentStations.Station1} and {adjacentStations.Station2}");
            }
            DataSource.ListAdjacentStations.Remove(adj);
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());

        }

        public void UpdateAdjacentStations(int station1, int station2, Action<AdjacentStations> update)
        {
            DO.AdjacentStations adj = DataSource.ListAdjacentStations.Find(a => a.Station1 == station1);

            if (adj == null)
            {
                throw new BadAdjacentStationsCodesException(station1, station2, $"bad stations codes: {station1} and {station2}");
            }
            update(adj);

        }

        #endregion
      
        #region Line
        public int AddLine(Line line)
        {
            line.Id = ++Counter.LineNum;
            DataSource.ListLine.Add(line.Clone());
            return line.Id;
        }
        public void DeleteLine(int id)
        {
            DO.Line line = DataSource.ListLine.Find(l => l.Id == id);
            if (line == null)
            {
                throw new BadLineIdException(id, $"bad line id: {line.Id}");
            }
            DataSource.ListLine.Remove(line);
        }
        public IEnumerable<Line> GetAllLine()
        {
            return from line in DataSource.ListLine
                   select line.Clone();
        }
        public Line GetLine(int id)
        {
            DO.Line line = DataSource.ListLine.Find(l => l.Id == id);
            if (line == null)
            {
                throw new BadLineIdException(id, $"bad line id: {id}");
            }
            return line.Clone();
        }

        public IEnumerable<Line> GetAllLineBy(Predicate<Line> predicate)
        {
            return from line in DataSource.ListLine
                   where predicate(line)
                   select line.Clone();
        }
        public void UpdateLine(Line line)
        {
            DO.Line lin = DataSource.ListLine.Find(l => l.Id == line.Id);

            if (lin == null)
            {
                throw new BadLineIdException(line.Id, $"bad line id: {line.Id} ");
            }
            DataSource.ListLine.Remove(lin);
            DataSource.ListLine.Add(line.Clone());
        }

        public void UpdateLine(int id, Action<Line> update)
        {
            DO.Line lin = DataSource.ListLine.Find(l => l.Id == id);

            if (lin == null)
            {
                throw new BadLineIdException(id, $"bad line id: {id} ");
            }
            update(lin);
        }
        #endregion
        #region LineStation
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
                throw new DO.BadStationCodeException(lineStation.Station, $"bad station code: {lineStation.Station}");
            }
            if (DataSource.ListLine.FirstOrDefault(l => l.Id == lineStation.LineId) == null)
            {
                throw new DO.BadLineIdException(lineStation.LineId, $"bad line id: {lineStation.LineId}");
            }
            DataSource.ListLineStation.Add(lineStation.Clone());
        }

        public void DeleteLineStation(int lineId, int station)
        {
            DO.LineStation lineStation = DataSource.ListLineStation.Find(l => l.LineId == lineId && l.Station == station);
            if (lineStation == null)
            {
                throw new BadLineStationIdException(lineId, station, $"bad station code: {station} in line {lineId}");
            }
            DataSource.ListLineStation.Remove(lineStation);
        }
        public IEnumerable<LineStation> GetAllLineStation()
        {
            return from lineStation in DataSource.ListLineStation
                   select lineStation.Clone();
        }

        public LineStation GetLineStation(int lineId, int station)
        {
            DO.LineStation lineStation = DataSource.ListLineStation.Find(ls => ls.LineId == lineId && ls.Station == station);
            if (lineStation == null)
            {
                throw new BadLineStationIdException(lineId, station, $"bad station code: {station} in line {lineId}");
            }
            return lineStation.Clone();
        }

        public IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate)
        {
            return from lineStation in DataSource.ListLineStation
                   where predicate(lineStation)
                   select lineStation.Clone();

        }
        public void UpdateLineStation(LineStation lineStation)
        {
            DO.LineStation lineS = DataSource.ListLineStation.Find(ls => ls.LineId == lineStation.LineId && ls.Station == lineStation.Station);

            if (lineS == null)
            {
                throw new BadLineStationIdException(lineStation.Station, lineStation.LineId, $"bad station code : {lineStation.Station} in line {lineStation.LineId} ");
            }
            DataSource.ListLineStation.Remove(lineS);
            DataSource.ListLineStation.Add(lineStation.Clone());
        }

        public void UpdateLineStation(int lineId, int station, Action<LineStation> update)
        {
            DO.LineStation lineS = DataSource.ListLineStation.Find(ls => ls.LineId == lineId && ls.Station == station);

            if (lineS == null)
            {
                throw new BadLineStationIdException(station, lineId, $"bad station code : {station} in line {lineId} ");
            }
            update(lineS);
        }
        #endregion
        
        #region LineTrip
        public int AddLineTrip(LineTrip lineTrip)
        {
            if (DataSource.ListLine.FirstOrDefault(l => l.Id == lineTrip.LineId) == null)
            {
                throw new DO.BadLineIdException(lineTrip.LineId, $"bad line id: {lineTrip.LineId}");
            }
            lineTrip.Id = ++Counter.LineTripNum;
            DataSource.ListLineTrip.Add(lineTrip.Clone());
            return lineTrip.Id;
        }
        public void DeleteLineTrip(int id)
        {
            DO.LineTrip lineTrip = DataSource.ListLineTrip.Find(l => l.Id == id);
            if (lineTrip == null)
            {
                throw new BadLineTripIdException(id, $"bad line id: {lineTrip.LineId}");
            }
            DataSource.ListLineTrip.Remove(lineTrip);
        }

        public IEnumerable<LineTrip> GetAllLineTrip()
        {
            return from lineTrip in DataSource.ListLineTrip
                   select lineTrip.Clone();
        }
        public LineTrip GetLineTrip(int id)
        {
            DO.LineTrip lineTrip = DataSource.ListLineTrip.Find(lt => lt.Id == id);
            if (lineTrip == null)
            {
                throw new BadLineTripIdException(id, $"bad line trip id: {id}");
            }
            return lineTrip.Clone();
        }

        public IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate)
        {
            return from lineTrip in DataSource.ListLineTrip
                   where predicate(lineTrip)
                   select lineTrip.Clone();
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
                throw new BadLineTripIdException(id, $"bad line trip id:{id}");
            }
            update(lineT);
        }

        #endregion
        #region Station
        public void AddStation(Station station)
        {
            if (DataSource.ListStation.FirstOrDefault(s => s.Code == station.Code) != null)
            {
                throw new DO.BadStationCodeException(station.Code, "Duplicate stations");
            }
            DataSource.ListStation.Add(station.Clone());
        }
        public void DeleteStation(int code)
        {
            DO.Station station = DataSource.ListStation.Find(s => s.Code == code);
            if (station == null)
            {
                throw new BadStationCodeException(code, $"bad station code: {code}");
            }
            DataSource.ListStation.Remove(station);
        }
        public IEnumerable<Station> GetAllStation()
        {
            return from station in DataSource.ListStation
                   select station.Clone();
        }

        public Station GetStation(int code)
        {
            DO.Station station = DataSource.ListStation.Find(s => s.Code == code);
            if (station == null)
            {
                throw new BadStationCodeException(code, $"bad station code: {code}");
            }
            return station.Clone();
        }

        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate)
        {
            return from station in DataSource.ListStation
                   where predicate(station)
                   select station.Clone();
        }
        public void UpdateStation(Station station)
        {
            DO.Station stat = DataSource.ListStation.Find(s => s.Code == station.Code);

            if (stat == null)
            {
                throw new BadStationCodeException(station.Code, $"bad station code:{station.Code}");
            }
            DataSource.ListStation.Remove(stat);
            DataSource.ListStation.Add(station.Clone());
        }

        public void UpdateStation(int code, Action<Station> update)
        {
            DO.Station stat = DataSource.ListStation.Find(s => s.Code == code);

            if (stat == null)
            {
                throw new BadStationCodeException(code, $"bad station code:{code}");
            }
            update(stat);
        }

        #endregion
        
        #region User
        public void AddUser(User user)
        {
            if (DataSource.ListUser.FirstOrDefault(u => u.UserName == user.UserName) != null)
            {
                throw new BadUserUserNameException(user.UserName, "Duplicate users");
            }
            DataSource.ListUser.Add(user.Clone());
        }

        public void DeleteUser(string userName)
        {
            DO.User user = DataSource.ListUser.Find(u => u.UserName == userName);
            if (user == null)
            {
                throw new BadUserUserNameException(userName, $"bad user name: {userName}");
            }
            DataSource.ListUser.Remove(user);
        }

        public IEnumerable<User> GetAllUser()
        {
            return from user in DataSource.ListUser
                   select user.Clone();
        }
        public User GetUser(string userName)
        {
            DO.User user = DataSource.ListUser.Find(u => u.UserName == userName);
            if (user == null)
            {
                throw new BadUserUserNameException(userName, $"bad user name: {userName}");
            }
            return user.Clone();
        }

        public IEnumerable<User> GetAllUserBy(Predicate<User> predicate)
        {
            return from user in DataSource.ListUser
                   where predicate(user)
                   select user.Clone();
        }
        public void UpdateUser(User user)
        {
            DO.User use = DataSource.ListUser.Find(u => u.UserName == user.UserName);

            if (use == null)
            {
                throw new BadUserUserNameException(user.UserName,  $"bad user name:{user.UserName}");
            }
            DataSource.ListUser.Remove(use);
            DataSource.ListUser.Add(user.Clone());
        }

        public void UpdateUser(string userName, Action<User> update)
        {
            DO.User use = DataSource.ListUser.Find(u => u.UserName == userName);

            if (use == null)
            {
                throw new BadUserUserNameException(userName, $"bad user name:{userName}");
            }
            update(use);
        }
        public string GetManagementPassword()
        {
            return "MTI2Nzk0";
        }
    }
    #endregion
}
