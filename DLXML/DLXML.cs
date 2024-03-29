﻿using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace DL
{
    sealed class DLXML : IDAL //internal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

        string AdjacentStationsPath = @"AdjacentStationsXml.xml"; //XElement
        string LinePath = @"LineXml.xml"; //XMLSerializer
        string LineStationPath = @"LineStationXml.xml"; //XMLSerializer
        string LineTripPath = @"LineTripXml.xml"; //XElement
        string UserPath = @"UserXml.xml"; //XMLSerializer
        string StationPath = @"StationXml.xml"; //XElement
        string counterXML = @"CounterXml.xml";
        string passwordXML = @"Password.xml";
        #endregion
        #region AdjacentStations
        public void  AddAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(StationPath);

            XElement stat2 = (from s in stationsRootElem.Elements()
                              where int.Parse(s.Element("Code").Value) == adjacentStations.Station1
                              select s).FirstOrDefault();
            if (stat2 == null)
                throw new BadStationCodeException(adjacentStations.Station1, $"bad station code: {adjacentStations.Station1}");

            XElement stat3 = (from s in stationsRootElem.Elements()
                              where int.Parse(s.Element("Code").Value) == adjacentStations.Station2
                              select s).FirstOrDefault();
            if (stat3 == null)
                throw new BadStationCodeException(adjacentStations.Station2, $"bad station code: {adjacentStations.Station2}");

            XElement stat1 = (from s in adjacentStationsRootElem.Elements()
                              where int.Parse(s.Element("Station1").Value) == adjacentStations.Station1
                              && int.Parse(s.Element("Station2").Value) == adjacentStations.Station2
                              select s).FirstOrDefault();

            if (stat1 != null)
                throw new DO.BadAdjacentStationsCodesException(adjacentStations.Station1, adjacentStations.Station2, "Duplicate adjacent stations");

            XElement stationElem = new XElement("AdjacentStations",
                                   new XElement("Station1", adjacentStations.Station1.ToString()),
                                   new XElement("Station2", adjacentStations.Station2.ToString()),
                                   new XElement("Distance", adjacentStations.Distance.ToString()),
                                   new XElement("Time", XmlConvert.ToString(adjacentStations.Time)));
            adjacentStationsRootElem.Add(stationElem);

            XMLTools.SaveListToXMLElement(adjacentStationsRootElem, AdjacentStationsPath);
        }
        public void DeleteAdjacentStations(int station1, int station2)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);


            XElement stat1 = (from s in stationsRootElem.Elements()
                              where int.Parse(s.Element("Station1").Value) == station1
                              && int.Parse(s.Element("Station2").Value) == station2
                              select s).FirstOrDefault();
            if (stat1 != null)
            {
                stat1.Remove();
                XMLTools.SaveListToXMLElement(stationsRootElem, AdjacentStationsPath);
            }
            else
                throw new BadAdjacentStationsCodesException(station1, station2, $"bad adjecent stations codes: { station1 } and {station2} ");
        }

        public AdjacentStations GetAdjacentStations(int station1, int station2)
        {
            if (station1 == 0)
            {
                return new DO.AdjacentStations { Station1 = 0, Station2 = station2, Distance = 0, Time = new TimeSpan(0, 0, 0) };
            }
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);

            AdjacentStations stat = (from s in adjacentStationsRootElem.Elements()
                            where int.Parse(s.Element("Station1").Value) == station1
                               && int.Parse(s.Element("Station2").Value) == station2
                            select new AdjacentStations()
                            {

                                Station1 = Int32.Parse(s.Element("Station1").Value),
                                Station2 = Int32.Parse(s.Element("Station2").Value),
                                Distance = double.Parse(s.Element("Distance").Value),
                                Time = XmlConvert.ToTimeSpan(s.Element("Time").Value)
                            }
                        ).FirstOrDefault();

            if (stat == null)
                throw new BadAdjacentStationsCodesException(station1, station2, $"bad stations codes: {station1} and {station2}");
            return stat;


        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate)//
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);


            return (from s in adjacentStationsRootElem.Elements()
                    let item= new AdjacentStations()
                    {

                        Station1 = Int32.Parse(s.Element("Station1").Value),
                        Station2 = Int32.Parse(s.Element("Station2").Value),
                        Distance = double.Parse(s.Element("Distance").Value),
                        Time = XmlConvert.ToTimeSpan(s.Element("Time").Value)

                    }
                    where predicate(item)
                    select item
                       );

        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {

            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);


        return (from s in adjacentStationsRootElem.Elements()
                    select new AdjacentStations()
                    {

                        Station1 = Int32.Parse(s.Element("Station1").Value),
                        Station2 = Int32.Parse(s.Element("Station2").Value),
                        Distance = double.Parse(s.Element("Distance").Value),
                        Time = XmlConvert.ToTimeSpan(s.Element("Time").Value)

                    }
                   );
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations)//////
        {

        XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);

        XElement stat1 = (from s in adjacentStationsRootElem.Elements()
                          where int.Parse(s.Element("Station1").Value) == adjacentStations.Station1
                               && int.Parse(s.Element("Station2").Value) == adjacentStations.Station2
                          select s).FirstOrDefault();

            if (stat1 != null)
            {
                stat1.Element("Time").Value = XmlConvert.ToString(adjacentStations.Time);
                stat1.Element("Distance").Value = adjacentStations.Distance.ToString();
                XMLTools.SaveListToXMLElement(adjacentStationsRootElem, AdjacentStationsPath);
            }
            else
                throw new BadAdjacentStationsCodesException(adjacentStations.Station1, adjacentStations.Station2, $"bad stations codes: {adjacentStations.Station1} and {adjacentStations.Station2}");
            

        }

        public void UpdateAdjacentStations(int station1, int station2, Action<AdjacentStations> update)
        {

            DO.AdjacentStations stat = GetAdjacentStations(station1, station2);
            update(stat);
            UpdateAdjacentStations(stat);
        }

        #endregion
        #region Line
        public int AddLine(Line line)
        {
            List<Line> ListLine = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            XElement countersRootElem = XMLTools.LoadListFromXMLElement(counterXML);
            int id=int.Parse(countersRootElem.Elements().First().Element("lineIdCounter").Value);
            countersRootElem.Elements().First().Element("lineIdCounter").Value = (id + 1).ToString();
            XMLTools.SaveListToXMLElement(countersRootElem, counterXML);
            line.Id = id;
           ListLine.Add(line);
            XMLTools.SaveListToXMLSerializer<Line>(ListLine, LinePath);
            return id;
        }
        public void DeleteLine(int id)
        {
            List<Line> ListLine = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            DO.Line line = ListLine.Find(l => l.Id == id);
            if (line == null)
            {
                throw new BadLineIdException(id, $"bad line id: {line.Id}");
            }
            ListLine.Remove(line);
            XMLTools.SaveListToXMLSerializer<Line>(ListLine, LinePath);
        }
        public IEnumerable<Line> GetAllLine()
        {
            List<Line> ListLine = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            return from line in ListLine
                   select line;
        }
        public Line GetLine(int id)
        {
            List<Line> ListLine = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            DO.Line line = ListLine.Find(l => l.Id == id);
            if (line == null)
            {
                throw new BadLineIdException(id, $"bad line id: {id}");
            }
            return line;
        }

        public IEnumerable<Line> GetAllLineBy(Predicate<Line> predicate)
        {
            List<Line> ListLine = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            return from line in ListLine
                   where predicate(line)
                   select line;
        }
        public void UpdateLine(Line line)
        {
            List<Line> ListLine = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            DO.Line lin = ListLine.Find(l => l.Id == line.Id);

            if (lin == null)
            {
                throw new BadLineIdException(line.Id, $"bad line id: {line.Id} ");
            }
            ListLine.Remove(lin);
            ListLine.Add(line);
            XMLTools.SaveListToXMLSerializer<Line>(ListLine, LinePath);
        }

        public void UpdateLine(int id, Action<Line> update)
        {
            List<Line> ListLine = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            DO.Line lin = ListLine.Find(l => l.Id == id);

            if (lin == null)
            {
                throw new BadLineIdException(id, $"bad line id: {id} ");
            }
            update(lin);
            XMLTools.SaveListToXMLSerializer<Line>(ListLine, LinePath);
        }
        #endregion
        #region LineStation
        public void AddLineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            List<Line> ListLine = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            if (ListLineStation.FirstOrDefault(ls => ls.Station == lineStation.Station && ls.LineId == lineStation.LineId) != null)
            {
                throw new DO.BadLineStationIdException(lineStation.LineId, lineStation.Station, "Duplicate line stations");
            }
            GetStation(lineStation.Station);
            if (ListLine.FirstOrDefault(l => l.Id == lineStation.LineId) == null)
            {
                throw new DO.BadLineIdException(lineStation.LineId, $"bad line id: {lineStation.LineId}");
            }
            ListLineStation.Add(lineStation);
            XMLTools.SaveListToXMLSerializer<LineStation>(ListLineStation, LineStationPath);
        }

        public void DeleteLineStation(int lineId, int station)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            DO.LineStation lineStation = ListLineStation.Find(l => l.LineId == lineId && l.Station == station);
            if (lineStation == null)
            {
                throw new BadLineStationIdException(lineId, station, $"bad station code: {station} in line {lineId}");
            }
            ListLineStation.Remove(lineStation);
            XMLTools.SaveListToXMLSerializer<LineStation>(ListLineStation, LineStationPath);
        }
        public IEnumerable<LineStation> GetAllLineStation()
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            return from lineStation in ListLineStation
                   select lineStation;
        }

        public LineStation GetLineStation(int lineId, int station)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            DO.LineStation lineStation = ListLineStation.Find(ls => ls.LineId == lineId && ls.Station == station);
            if (lineStation == null)
            {
                throw new BadLineStationIdException(lineId, station, $"bad station code: {station} in line {lineId}");
            }
            return lineStation;
        }

        public IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            return from lineStation in ListLineStation
                   where predicate(lineStation)
                   select lineStation;

        }
        public void UpdateLineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            DO.LineStation lineS =ListLineStation.Find(ls => ls.LineId == lineStation.LineId && ls.Station == lineStation.Station);

            if (lineS == null)
            {
                throw new BadLineStationIdException(lineStation.Station, lineStation.LineId, $"bad station code : {lineStation.Station} in line {lineStation.LineId} ");
            }
            ListLineStation.Remove(lineS);
            ListLineStation.Add(lineStation);
            XMLTools.SaveListToXMLSerializer<LineStation>(ListLineStation, LineStationPath);
        }

        public void UpdateLineStation(int lineId, int station, Action<LineStation> update)
        {
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            DO.LineStation lineS =ListLineStation.Find(ls => ls.LineId == lineId && ls.Station == station);

            if (lineS == null)
            {
                throw new BadLineStationIdException(station, lineId, $"bad station code : {station} in line {lineId} ");
            }
            update(lineS);
            XMLTools.SaveListToXMLSerializer<LineStation>(ListLineStation, LineStationPath);
        }
        #endregion
        #region LineTrip
        public int AddLineTrip(LineTrip lineTrip)
        {

            XElement countersRootElem = XMLTools.LoadListFromXMLElement(counterXML);
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(LineTripPath);
            int id = int.Parse(countersRootElem.Elements().First().Element("lineTripIdCounter").Value);
            countersRootElem.Elements().First().Element("lineTripIdCounter").Value = (id + 1).ToString();
            XMLTools.SaveListToXMLElement(countersRootElem, counterXML);
            XElement lineTripElem = new XElement("LineTrip", new XElement("Id", id.ToString()),
                                  new XElement("LineId", lineTrip.LineId.ToString()),
                                  new XElement("StartAt", XmlConvert.ToString(lineTrip.StartAt)),
                                  new XElement("Frequency", XmlConvert.ToString(lineTrip.Frequency)),
                                  new XElement("FinishAt", XmlConvert.ToString(lineTrip.FinishAt)));

            lineTripRootElem.Add(lineTripElem);

            XMLTools.SaveListToXMLElement(lineTripRootElem, LineTripPath);
            return id;

        }
        public void DeleteLineTrip(int id)
        {

            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(LineTripPath);

            XElement lt = (from l in lineTripRootElem.Elements()
                            where int.Parse(l.Element("Id").Value) == id
                            select l).FirstOrDefault();

            if (lt != null)
            {
                lt.Remove(); 

                XMLTools.SaveListToXMLElement(lineTripRootElem, LineTripPath);
            }
            else
                throw new BadLineTripIdException(id, $"bad line id: {id}");
        }

        public IEnumerable<LineTrip> GetAllLineTrip()
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(LineTripPath);

            return (from lt in lineTripRootElem.Elements()
                    select new DO.LineTrip()
                    {
                        Id = Int32.Parse(lt.Element("Id").Value),
                        StartAt=XmlConvert.ToTimeSpan(lt.Element("StartAt").Value),
                        Frequency=XmlConvert.ToTimeSpan(lt.Element("Frequency").Value),
                        FinishAt= XmlConvert.ToTimeSpan(lt.Element("FinishAt").Value),
                        LineId= Int32.Parse(lt.Element("LineId").Value)
                    }
                   );
        }
        public LineTrip GetLineTrip(int id)
        { 
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(LineTripPath);

            DO.LineTrip l = (from lt in lineTripRootElem.Elements()
                        where int.Parse(lt.Element("Id").Value) == id
                        select new DO.LineTrip()
                        {
                            Id = Int32.Parse(lt.Element("Id").Value),
                            StartAt = XmlConvert.ToTimeSpan(lt.Element("StartAt").Value),
                            Frequency = XmlConvert.ToTimeSpan(lt.Element("Frequency").Value),
                            FinishAt = XmlConvert.ToTimeSpan(lt.Element("FinishAt").Value),
                            LineId = Int32.Parse(lt.Element("LineId").Value)
                        }
                        ).FirstOrDefault();

            if (l == null)
                throw new BadLineTripIdException(id, $"bad line trip id: {id}");

            return l ;
        }

        public IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate)
        {
           
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(LineTripPath);

            return from lt in lineTripRootElem.Elements()
                   let l = new DO.LineTrip()
                   {
                       Id = Int32.Parse(lt.Element("Id").Value),
                       StartAt = XmlConvert.ToTimeSpan(lt.Element("StartAt").Value),
                       Frequency = XmlConvert.ToTimeSpan(lt.Element("Frequency").Value),
                       FinishAt = XmlConvert.ToTimeSpan(lt.Element("FinishAt").Value),
                       LineId = Int32.Parse(lt.Element("LineId").Value)
                   }
                   where predicate(l)
                   select l;
        }
        public void UpdateLineTrip(LineTrip lineTrip)
        {
            
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(LineTripPath);

            XElement lt = (from l in lineTripRootElem.Elements()
                            where int.Parse(l.Element("Id").Value) == lineTrip.Id
                            select l).FirstOrDefault();

            if (lt != null)
            {
                lt.Element("Id").Value = lineTrip.Id.ToString();
                lt.Element("LineId").Value = lineTrip.LineId.ToString();
                lt.Element("StartAt").Value = XmlConvert.ToString(lineTrip.StartAt);
                lt.Element("Frequency").Value = XmlConvert.ToString(lineTrip.Frequency);
                lt.Element("FinishAt").Value = XmlConvert.ToString(lineTrip.FinishAt);
                XMLTools.SaveListToXMLElement(lineTripRootElem, LineTripPath);
            }
            else
                throw new DO.BadLineTripIdException(lineTrip.Id, $"bad line trip id: {lineTrip.Id}");
        }

        public void UpdateLineTrip(int id, Action<LineTrip> update)
        {
           

            DO.LineTrip lineTrip = GetLineTrip(id);
            update(lineTrip);
            UpdateLineTrip(lineTrip);
        }

        #endregion
        #region Station
        public void AddStation(Station station)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(StationPath);

            XElement stat1 = (from s in stationsRootElem.Elements()
                             where int.Parse(s.Element("Code").Value) == station.Code
                             select s).FirstOrDefault();

            if (stat1 != null)
                throw new DO.BadStationCodeException(station.Code, "Duplicate station Code");

            XElement stationElem = new XElement("Station",
                                   new XElement("Code", station.Code.ToString()),
                                   new XElement("Name", station.Name),
                                   new XElement("Address", station.Address),
                                   new XElement("Longitude", station.Longitude.ToString()),
                                   new XElement("Latitude", station.Latitude.ToString()));

            stationsRootElem.Add(stationElem);

            XMLTools.SaveListToXMLElement(stationsRootElem, StationPath);

        }
        public void DeleteStation(int code)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(StationPath);


            XElement stat1 = (from s in stationsRootElem.Elements()
                              where int.Parse(s.Element("Code").Value) == code
                              select s).FirstOrDefault();
            if (stat1 != null)
            {
                stat1.Remove();
                XMLTools.SaveListToXMLElement(stationsRootElem, StationPath);
            }
            else
                throw new BadStationCodeException(code, $"bad station code: {code}");

        }
        public IEnumerable<Station> GetAllStation()
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(StationPath);


            return (from s in stationsRootElem.Elements()
                    select new Station()
                    {
                        
                        Code = Int32.Parse(s.Element("Code").Value),
                        Name = s.Element("Name").Value,
                        Address = s.Element("Address").Value,
                        Longitude = double.Parse(s.Element("Longitude").Value),
                        Latitude = double.Parse(s.Element("Latitude").Value)
                        
                    }
                   );
        }

        public Station GetStation(int code)
        {

            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(StationPath);
           
            Station stat = (from s in stationsRootElem.Elements()
                        where int.Parse(s.Element("Code").Value) == code
                        select new Station()
                        {
                            Code = Int32.Parse(s.Element("Code").Value),
                            Name = s.Element("Name").Value,
                            Address = s.Element("Address").Value,
                            Longitude = double.Parse(s.Element("Longitude").Value),
                            Latitude = double.Parse(s.Element("Latitude").Value)
                        }
                        ).FirstOrDefault();

            if (stat == null)
                throw new BadStationCodeException(code, $"bad station code: {code}");
            return stat;

        }

        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(StationPath);

            return from s in stationsRootElem.Elements()
                   let s1 = new Station()
                   {
                       Code = Int32.Parse(s.Element("Code").Value),
                       Name = s.Element("Name").Value,
                       Address = s.Element("Address").Value,
                       Longitude = double.Parse(s.Element("Longitude").Value),
                       Latitude = double.Parse(s.Element("Latitude").Value)
                   }
                   where predicate(s1)
                   select s1;

        }
        public void UpdateStation(Station station)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(StationPath);

            XElement stat1 = (from s in stationsRootElem.Elements()
                              where int.Parse(s.Element("Code").Value) == station.Code
                              select s).FirstOrDefault();

            if (stat1 != null)
            {
                //stat1.Element("Code").Value = station.Code.ToString();
                stat1.Element("Name").Value = station.Name;
               stat1.Element("Address").Value = station.Address;
                stat1.Element("Longitude").Value = station.Longitude.ToString();
                stat1.Element("Latitude").Value = station.Latitude.ToString();
                

                XMLTools.SaveListToXMLElement(stationsRootElem, StationPath);
            }
            else
                throw new BadStationCodeException(station.Code, $"bad station code:{station.Code}");

        }

        public void UpdateStation(int code, Action<Station> update)
        {
            DO.Station stat = GetStation(code);
            update(stat);
            UpdateStation(stat);
        }

        #endregion

        #region User
        public void AddUser(User user)
        {
            List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            if (ListUser.FirstOrDefault(u => u.UserName == user.UserName) != null)
            {
                throw new BadUserUserNameException(user.UserName, "Duplicate users");
            }
            ListUser.Add(user);
            XMLTools.SaveListToXMLSerializer<User>(ListUser, UserPath);
        }

        public void DeleteUser(string userName)
        {
            List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            DO.User user = ListUser.Find(u => u.UserName == userName);
            if (user == null)
            {
                throw new BadUserUserNameException(userName, $"bad user name: {userName}");
            }
            ListUser.Remove(user);
            XMLTools.SaveListToXMLSerializer<User>(ListUser, UserPath);

        }

        public IEnumerable<User> GetAllUser()
        {
            List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            return from user in ListUser
                   select user;
        }
        public User GetUser(string userName)
        {
            List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            DO.User user = ListUser.Find(u => u.UserName == userName);
            if (user == null)
            {
                throw new BadUserUserNameException(userName, $"bad user name: {userName}");
            }
            return user;
        }

        public IEnumerable<User> GetAllUserBy(Predicate<User> predicate)
        {
            List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            return from user in ListUser
                   where predicate(user)
                   select user;
        }
        public void UpdateUser(User user)
        {
            List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            DO.User use = ListUser.Find(u => u.UserName == user.UserName);

            if (use == null)
            {
                throw new BadUserUserNameException(user.UserName, $"bad user name:{user.UserName}");
            }
           ListUser.Remove(use);
            ListUser.Add(user);
            XMLTools.SaveListToXMLSerializer<User>(ListUser, UserPath);
        }

        public void UpdateUser(string userName, Action<User> update)
        {
            List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            DO.User use =ListUser.Find(u => u.UserName == userName);

            if (use == null)
            {
                throw new BadUserUserNameException(userName, $"bad user name:{userName}");
            }
            update(use);
            XMLTools.SaveListToXMLSerializer<User>(ListUser, UserPath);
        }

        public string GetManagementPassword()
        {
            XElement passRootElem = XMLTools.LoadListFromXMLElement(passwordXML);
            return passRootElem.Elements().First().Element("ManagementPassword").Value;
        }
    }
    #endregion
}
