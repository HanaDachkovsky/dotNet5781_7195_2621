﻿using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using BLAPI;
using BO;
using DLAPI;
namespace BL
{
    sealed class BLImp : IBL
    {

        #region singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        BLImp() { } // default => private
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion
        IDAL dl = DLFactory.GetDL();


        #region User
        public bool IsAdminAndExists(string userName, string password)
        {
            try
            {
                DO.User user = dl.GetUser(userName);
                if (decrypt(user.Password) != password)
                    throw new BO.BadUserUserNameException(userName, "שם המשתמש או הסיסמא שגויים");
                return user.Admin;
            }
            catch (DO.BadUserUserNameException ex)
            {

                throw new BO.BadUserUserNameException("שם המשתמש או הסיסמא שגויים", ex);
            }

        }
        public void CreateUser(string userName, string password, bool isAdmin, string manPassword)
        {
            try
            {
                if (isAdmin == true && manPassword != decrypt(dl.GetManagementPassword()))
                {
                    throw new BO.BadManagementPasswordEception("סיסמת המנהל שגויה");
                }
                dl.AddUser(new DO.User { UserName = userName, Password = encrypt(password), Admin = isAdmin });
            }
            catch (DO.BadUserUserNameException ex)
            {

                throw new BO.BadUserUserNameException("שם המשתמש תפוס", ex);
            }
        }
        #endregion

        #region Line
        public BO.Line GetLine(int id)
        {
            try
            {
                DO.Line line = dl.GetLine(id);
                return new BO.Line//
                {
                    Id = line.Id,
                    Code = line.Code,
                    Arae = (Enums.Areas)line.Arae,
                    LastStationName = dl.GetStation(line.LastStation).Name,
                    Stations = from station in dl.GetAllLineStationBy(ls => ls.LineId == line.Id).OrderBy(s => s.LineStationIndex)
                               let name = dl.GetStation(station.Station).Name
                               let time = dl.GetAdjacentStations(station.PrevStation, station.Station).Time
                               let dis = dl.GetAdjacentStations(station.PrevStation, station.Station).Distance
                               select new BO.LineStation { Code = station.Station, Name = name, DistanceFromPrevStat = dis, TimeFromPrevStat = time }

                };
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException("שגיאה:תחנה לא קיימת", ex);
            }
            catch (DO.BadAdjacentStationsCodesException ex)
            {
                throw new BO.BadAdjacentStationsCodesException("שגיאה: תחנה לא קיימת", ex);
            }
        }
        public IEnumerable<Line> GetAllLines()
        {

            try
            {
                return from line in dl.GetAllLine()
                       select new BO.Line//
                       {
                           Id = line.Id,
                           Code = line.Code,
                           Arae = (Enums.Areas)line.Arae,
                           LastStationName = dl.GetStation(line.LastStation).Name,
                           Stations = from station in dl.GetAllLineStationBy(ls => ls.LineId == line.Id).OrderBy(s => s.LineStationIndex)
                                      let name = dl.GetStation(station.Station).Name
                                      let time = dl.GetAdjacentStations(station.PrevStation, station.Station).Time
                                      let dis = dl.GetAdjacentStations(station.PrevStation, station.Station).Distance
                                      select new BO.LineStation { Code = station.Station, Name = name, DistanceFromPrevStat = dis, TimeFromPrevStat = time }

                       };
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException("שגיאה:תחנה לא קיימת", ex);
            }
            catch (DO.BadAdjacentStationsCodesException ex)
            {
                throw new BO.BadAdjacentStationsCodesException("שגיאה: תחנה לא קיימת", ex);
            }

        }
        public void DeleteLine(int id)
        {

            try
            {
                dl.DeleteLine(id);
                dl.GetAllLineStationBy(ls => ls.LineId == id).ToList().ForEach(ls => dl.DeleteLineStation(id, ls.Station));
                dl.GetAllLineTripBy(t => t.LineId == id).ToList().ForEach(t => dl.DeleteLineTrip(t.Id));

            }

            catch (DO.BadLineIdException ex)
            {

                throw new BO.BadLineIdException("לא ניתן למחוק את הקו כיון שהוא לא קיים", ex);
            }
            catch (DO.BadLineStationIdException ex)
            {

                throw new BO.BadLineIdException("לא ניתן למחוק את תחנות קו שאינן קיימות", ex);//?
            }
            catch (DO.BadLineTripIdException ex)
            {
                throw new BO.BadLineTripIdException("לא ניתן למחוק את לוחות הזמנים של הקו כיון שהם לא קיימים", ex);//?
            }
        }
        public void UpdateLine(int id, int code, Enums.Areas area)
        {
            try
            {
                dl.UpdateLine(id, l => l.Code = code);
                dl.UpdateLine(id, l => l.Arae = (DO.Enums.Areas)area);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException("לא ניתן לעדכן קו שלא קיים", ex);
            }

        }
        public void AddLine(int code, Enums.Areas area, BO.Station station1, BO.Station station2)

        {

            try
            {
                dl.GetStation(station1.Code);
                dl.GetStation(station2.Code);
                if (station1.Code == station2.Code)
                {
                    throw new BO.BadStationCodeException(station1.Code, "לא ניתן להוסיף קו עם תחנות זהות");
                }

                int lineID = dl.AddLine(new DO.Line { Code = code, Arae = (DO.Enums.Areas)area, FirstStation = station1.Code, LastStation = station2.Code });
                dl.AddLineStation(new DO.LineStation { Station = station1.Code, LineStationIndex = 1, PrevStation = 0, NextStation = station2.Code, LineId = lineID });
                dl.AddLineStation(new DO.LineStation { Station = station2.Code, PrevStation = station1.Code, NextStation = 0, LineStationIndex = 2, LineId = lineID });
                if (dl.GetAllAdjacentStationsBy(a => a.Station1 == station1.Code && a.Station2 == station2.Code).Count() < 1)
                {
                    double dis = calDistance(station1.Code, station2.Code);
                    TimeSpan time = calTime(station1.Code, station2.Code);
                    dl.AddAdjacentStations(new DO.AdjacentStations { Station1 = station1.Code, Station2 = station2.Code, Distance = dis, Time = time });
                }

            }

            catch (DO.BadStationCodeException ex)
            {

                throw new BO.BadStationCodeException("לא ניתן להוסיף את הקו כיון שהתחנות לא קיימות", ex);
            }
            catch (DO.BadLineIdException ex)
            {

                throw new BO.BadLineIdException("לא ניתן להוסיף את הקו", ex);//?
            }
            catch (DO.BadLineStationIdException ex)
            {

                throw new BO.BadLineStationIdException("לא ניתן להוסיף קו עם תחנות אלו כיון שהן כבר לא קיימות בקו", ex);
            }

            catch (DO.BadAdjacentStationsCodesException ex)
            {

                throw new BO.BadAdjacentStationsCodesException("לא ניתן להוסיף את הקו כיון שהתחנות לא קיימות", ex);
            }

        }
        public void AddStationToLine(int code, int lineId, int stationBefore)
        {
            try
            {
                int index;
                int next;
                dl.GetStation(code);
                bool first = false;
                bool last = false;
                if (stationBefore == 0)
                {
                    next = dl.GetLine(lineId).FirstStation;
                    dl.UpdateLine(lineId, l => l.FirstStation = code);
                    index = 1;
                    first = true;

                }
                else
                {
                    dl.GetStation(stationBefore);
                    dl.GetLineStation(lineId, stationBefore);
                    index = dl.GetLineStation(lineId, stationBefore).LineStationIndex + 1;
                    next = dl.GetLineStation(lineId, stationBefore).NextStation;
                    dl.UpdateLineStation(lineId, stationBefore, ls => ls.NextStation = code);

                }
                if (stationBefore == dl.GetLine(lineId).LastStation)
                {
                    dl.UpdateLine(lineId, l => l.LastStation = code);
                    last = true;
                }
                else
                {
                    dl.UpdateLineStation(lineId, next, ls => ls.PrevStation = code);
                }

                dl.GetAllLineStationBy(ls => ls.LineId == lineId && ls.LineStationIndex >= index).ToList().ForEach(x => dl.UpdateLineStation(lineId, x.Station, ls => ls.LineStationIndex++));
                dl.AddLineStation(new DO.LineStation { Station = code, LineId = lineId, LineStationIndex = index, PrevStation = stationBefore, NextStation = next });



                if (first == false && dl.GetAllAdjacentStationsBy(a => a.Station1 == stationBefore && a.Station2 == code).Count() < 1)
                {
                    double dis = calDistance(stationBefore, code);
                    TimeSpan time = calTime(stationBefore, code);
                    dl.AddAdjacentStations(new DO.AdjacentStations { Station1 = stationBefore, Station2 = code, Distance = dis, Time = time });
                }
                if (last == false && dl.GetAllAdjacentStationsBy(a => a.Station1 == code && a.Station2 == next).Count() < 1)
                {
                    double dis = calDistance(code, next);
                    TimeSpan time = calTime(code, next);
                    dl.AddAdjacentStations(new DO.AdjacentStations { Station1 = code, Station2 = next, Distance = dis, Time = time });
                }

            }
            catch (DO.BadStationCodeException ex)
            {

                throw new BadStationCodeException("לא ניתן להוסיף תחנה שלא קיימת", ex);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BadLineIdException("לא ניתן להוסיף תחנה לקו שלא קיים", ex);
            }
            catch (DO.BadLineStationIdException ex)
            {
                throw new BadLineStationIdException("לא ניתן להוסיף את התחנה כיון שהיא כבר קיימת בקו או שהתחנה לפניה או אחריה לא קיימות בקו", ex);
            }
            catch (DO.BadAdjacentStationsCodesException ex)
            {
                throw new BadAdjacentStationsCodesException("לא ניתן להוסיף מרחק ומן בין תחנות כיון שהמרחק והזמן ביניהן כבר קיימים ", ex);
            }
        }
        public void DeleteStationInLine(int code, int lineId)
        {
            try
            {
                if (dl.GetLineStation(lineId, dl.GetLine(lineId).LastStation).LineStationIndex <= 2)
                {
                    throw new BO.BadLineIdException(lineId, "לא ניתן למחוק את התחנה כיון שקימות בקו 2 תחנות");
                }
                int index;
                dl.GetStation(code);
                int next = dl.GetLineStation(lineId, code).NextStation;
                int prev = dl.GetLineStation(lineId, code).PrevStation;
                if (dl.GetLine(lineId).FirstStation == code)
                {

                    int first = dl.GetLineStation(lineId, next).Station;
                    dl.UpdateLine(lineId, l => l.FirstStation = first);
                    dl.UpdateLineStation(lineId, next, ls => ls.PrevStation = prev);
                }
                else if (dl.GetLine(lineId).LastStation == code)
                {
                    dl.UpdateLine(lineId, l => l.LastStation = prev);
                    dl.UpdateLineStation(lineId, prev, ls => ls.NextStation = next);
                }
                else
                {
                    dl.UpdateLineStation(lineId, next, ls => ls.PrevStation = prev);
                    dl.UpdateLineStation(lineId, prev, ls => ls.NextStation = next);
                    if (dl.GetAllAdjacentStationsBy(a => a.Station1 == prev && a.Station2 == next).Count() < 1)
                    {
                        double dis = calDistance(prev, next);
                        TimeSpan time = calTime(prev, next);
                        dl.AddAdjacentStations(new DO.AdjacentStations { Station1 = prev, Station2 = next, Distance = dis, Time = time });
                    }
                }
                index = dl.GetLineStation(lineId, code).LineStationIndex;
                dl.GetAllLineStationBy(ls => ls.LineId == lineId && ls.LineStationIndex >= index).ToList().ForEach(x => dl.UpdateLineStation(lineId, x.Station, ls => ls.LineStationIndex--));
                dl.DeleteLineStation(lineId, code);


            }
            catch (DO.BadStationCodeException ex)
            {

                throw new BadStationCodeException("לא ניתן למחוק תחנה שלא קיימת", ex);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BadLineIdException("לא ניתן למחוק תחנה מקו שלא קיים", ex);
            }
            catch (DO.BadLineStationIdException ex)
            {
                throw new BadLineStationIdException("לא ניתן למחוק את התחנה כיון שהיא לא קיימת בקו או שהתחנה לפניה או אחריה לא קיימות בקו", ex);
            }
            catch (DO.BadAdjacentStationsCodesException ex)
            {
                throw new BadAdjacentStationsCodesException("לא ניתן להוסיף מרחק וזמן בין תחנות כיון שהמרחק והזמן ביניהן כבר קיימים ", ex);
            }
        }
        //void UpdateLineStation(BO.LineStation lineStation, int lineId);
        #endregion

        #region LineTrip
        public void AddLineTrip(int lineId, TimeSpan startAt, TimeSpan finishAt, TimeSpan freq)
        {
            try
            {
                if (dl.GetAllLineTripBy(lt => lt.LineId == lineId && ((lt.StartAt == startAt) || (lt.StartAt > startAt && lt.StartAt < finishAt) || (lt.StartAt < startAt && lt.FinishAt > startAt))).Count() > 0)
                {
                    throw new ArgumentOutOfRangeException("לא ניתן להוסיף תדירות שחופפת לתדירות אחרת בקו");

                }
                dl.AddLineTrip(new DO.LineTrip { LineId = lineId, StartAt = startAt, FinishAt = finishAt, Frequency = freq });
            }
            catch (DO.BadLineIdException ex)
            {

                throw new BO.BadLineIdException("לא ניתן להוסיף לוח זמנים לקו שלא קיים", ex);
            }
        }
        public IEnumerable<BO.LineTrip> getLineTrips(int id)
        {

            return (from item in dl.GetAllLineTripBy(lt => lt.LineId == id)
                    select new BO.LineTrip { StartAt = item.StartAt, FinishAt = item.FinishAt, Frequency = item.Frequency, Id = item.Id, LineId = item.LineId }).OrderBy(x => x.StartAt);

        }

        public void DeleteLineTrip(int id)
        {
            try
            {
                dl.GetLine((dl.GetLineTrip(id)).LineId);
                dl.DeleteLineTrip(id);
            }
            catch (DO.BadLineTripIdException ex)
            {

                throw new BO.BadLineTripIdException("לא ניתן למחוק תדירות שלא קיימת", ex);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException("לא ניתן למחוק תדירות מקו שלא קיים", ex);
            }
        }
        public void UpdateLineTrip(LineTrip lineTrip, TimeSpan startAt, TimeSpan finishAt, TimeSpan freq)
        {
            try
            {
                dl.GetLine(lineTrip.LineId);
                if (dl.GetAllLineTripBy(lt => lt.LineId == lineTrip.LineId && lt.Id != lineTrip.Id).FirstOrDefault(lt => (lt.StartAt == startAt) || (lt.StartAt > startAt && lt.StartAt < finishAt) || (lt.StartAt < startAt && lt.FinishAt > startAt)) != null)
                {
                    throw new ArgumentOutOfRangeException("לא ניתן לעדכן תדירות כך שתהיה חופפת לתדירות אחרת בלוח הזמנים של הקו");
                }
                dl.UpdateLineTrip(new DO.LineTrip { LineId = lineTrip.LineId, Id = lineTrip.Id, StartAt = startAt, FinishAt = finishAt, Frequency = freq });
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException("לא ניתן לעדכן תדירות בקו שלא קיים", ex);
            }
            catch (DO.BadLineTripIdException ex)
            {

                throw new BO.BadLineTripIdException("לא ניתן לעדכן תדירות שלא קיימת", ex);
            }
        }
        #endregion

        #region Station
        public BO.Station getStation(int code)
        {
            try
            {
                DO.Station station = dl.GetStation(code);
                return new BO.Station
                {
                    Code = station.Code,
                    Name = station.Name,
                    Address = station.Address,
                    Longitude = station.Longitude,
                    Latitude = station.Latitude,
                    Lines = from lineSt in dl.GetAllLineStationBy(ls => ls.Station == station.Code)
                            let line = dl.GetLine(lineSt.LineId)
                            select new BO.StationLine
                            {
                                Id = line.Id,
                                Code = line.Code,
                                LastStation = line.LastStation,
                                NameLastStation = dl.GetStation(line.LastStation).Name
                            }
                };
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException("שגיאה:תחנה לא קיימת", ex);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException("שגיאה:הקו לא קיים", ex);
            }
        }
        public IEnumerable<Station> GetAllStations()
        {
            try
            {
                return from station in dl.GetAllStation()
                       select new BO.Station
                       {
                           Code = station.Code,
                           Name = station.Name,
                           Address = station.Address,
                           Longitude = station.Longitude,
                           Latitude = station.Latitude,
                           Lines = from lineSt in dl.GetAllLineStationBy(ls => ls.Station == station.Code)
                                   let line = dl.GetLine(lineSt.LineId)
                                   select new BO.StationLine
                                   {
                                       Id = line.Id,
                                       Code = line.Code,
                                       LastStation = line.LastStation,
                                       NameLastStation = dl.GetStation(line.LastStation).Name
                                   }
                       };
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException("שגיאה:תחנה לא קיימת", ex);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException("שגיאה:הקו לא קיים", ex);
            }

        }
        public void DeleteStation(int code)
        {
            try
            {
                dl.GetAllLineStationBy(ls => ls.Station == code).ToList().ForEach(ls => DeleteStationInLine(code, ls.LineId));
                dl.GetAllAdjacentStationsBy(a => a.Station1 == code || a.Station2 == code).ToList().ForEach(a => dl.DeleteAdjacentStations(a.Station1, a.Station2));
                dl.DeleteStation(code);
            }
            catch (DO.BadLineStationIdException ex)
            {

                throw new BO.BadLineIdException("לא ניתן למחוק את תחנות קו שאינן קיימות", ex);//?
            }
            catch (DO.BadAdjacentStationsCodesException ex)
            {
                throw new BO.BadAdjacentStationsCodesException("לא ניתן למחוק תחנות שלא קיימות", ex);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException("לא ניתן למחוק תחנה שלא קיימת", ex);
            }
        }
        public void UpdateStation(int code, string name, string address)
        {
            try
            {
                dl.UpdateStation(code, s => s.Name = name);
                dl.UpdateStation(code, s => s.Address = address);

            }
            catch (DO.BadStationCodeException ex)
            {

                throw new BO.BadStationCodeException("לא ניתן לעדכן תחנה שלא קיימת", ex);
            }
        }
        public void AddStation(int code, string name, string address, double latitude, double longitude)
        {
            try
            {
                if (longitude < 34.3 || longitude > 35.5 || latitude < 31 || latitude > 33.3)
                    throw new ArgumentOutOfRangeException("לא ניתן להוסיף תחנה שקווי האורך או הרוחב שלה מחוץ לישראל");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            try
            {
                dl.AddStation(new DO.Station { Code = code, Address = address, Latitude = latitude, Longitude = longitude, Name = name });
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException("לא ניתן להוסיף תחנה שכבר קיימת", ex);
            }
        }
        #endregion

        #region LineStation
        public void UpdateTimeOrDistance(int code, int lineId, TimeSpan time, double distance)
        {
            try
            {
                dl.GetStation(code);
                var prev = dl.GetAllLineStationBy(ls => ls.LineId == lineId && ls.NextStation == code).FirstOrDefault();
                if (prev == null)
                {
                    throw new BO.BadLineStationIdException(lineId, code, "לא ניתן לעדכן זמן ומחרחק מתחנה קודמת כיון שהתחנה הקודמת לא קיימת");

                }
                dl.UpdateAdjacentStations(new DO.AdjacentStations { Station1 = prev.Station, Station2 = code, Distance = distance, Time = time });

            }
            catch (DO.BadStationCodeException ex)
            {

                throw new BO.BadStationCodeException("לא ניתן לעדכן זמן ומרחק כיון שהתחנה לא קיימת", ex);
            }
            catch (DO.BadAdjacentStationsCodesException ex)
            {
                throw new BO.BadAdjacentStationsCodesException("לא ניתן לעדכן זמן ומרחק מהתחנה הקודמת כיון שהתחנה או התחנה הקודמת לא קיימות", ex);
            }
        }

        #endregion

        public IEnumerable<BO.LineArrivalTime> GetArrivalTimes(BO.Station station, TimeSpan time)
        {
            return (from line in station.Lines
                    let duration = duration(line.Id, station.Code)
                    let start = startTime(line.Id, duration, time)
                    where start != null
                    select new BO.LineArrivalTime { StartTime = (TimeSpan)start, LineCode = line.Code, LastStation = line.NameLastStation, Arrive = arrive(duration, start, time), ArriveTime = (TimeSpan)start + duration - time }).OrderBy(t => t.ArriveTime);


        }

        /// <summary>
        /// calculates the time between 2 stations
        /// </summary>
        /// <param name="code1">code of first station</param>
        /// <param name="code2">code of second station</param>
        /// <returns>time</returns>
        private TimeSpan calTime(int code1, int code2)
        {
            TimeSpan time = new TimeSpan(0, (int)(60 * calDistance(code1, code2) / 30), 0);
            if (time.TotalSeconds == 0)
            {
                return new TimeSpan(0, 1, 0);
            }
            return time;
        }

        /// <summary>
        /// calculates the distance between 2 stations
        /// </summary>
        /// <param name="code1">code of first station</param>
        /// <param name="code2">code of second station</param>
        /// <returns>distance</returns>
        private double calDistance(int code1, int code2)
        {
            try
            {
                DO.Station station1 = dl.GetStation(code1);
                DO.Station station2 = dl.GetStation(code2);
                GeoCoordinate coor1 = new GeoCoordinate(station1.Latitude, station1.Longitude);
                GeoCoordinate coor2 = new GeoCoordinate(station2.Latitude, station2.Longitude);
                return Math.Round((coor1.GetDistanceTo(coor2) * 1.5 / 1000), 2);

            }
            catch (DO.BadStationCodeException ex)
            {

                throw new BO.BadStationCodeException("לא ניתן לחשב מרחק בין התחנות כיון שהתחנות לא קיימות", ex);
            }
        }
  
        
       
        /// <summary>
        /// retrn status of bus arrives at station
        /// </summary>
        /// <param name="duration">the time between the first station to current station</param>
        /// <param name="start">start time of bus</param>
        /// <param name="time">the time now</param>
        /// <returns>string of status or time</returns>
        private string arrive(TimeSpan duration, TimeSpan? start, TimeSpan time)
        {
            int sec = (int)((TimeSpan)start + duration - time).TotalSeconds;
            if (sec < -5)
                return "";
            if (sec >= -5 && sec <= 0)
                return "בתחנה";
            if (sec >= 1 && sec <= 5)
                return "נכנס לתחנה";
            TimeSpan time1 = ((TimeSpan)start + duration - time);
            return time1.ToString().Substring(0, 8);

        }
        /// <summary>
        /// returns the time between the first station to current station
        /// </summary>
        /// <param name="id">line id</param>
        /// <param name="code">current station code</param>
        /// <returns>duration in timespan</returns>
        private TimeSpan duration(int id, int code)
        {
            int index = dl.GetLineStation(id, code).LineStationIndex;
            List<DO.LineStation> stationsInLine = dl.GetAllLineStationBy(ls => ls.LineId == id && ls.LineStationIndex <= index).OrderBy(ls => ls.LineStationIndex).ToList();
            TimeSpan sum = new TimeSpan(0, 0, 0);
            stationsInLine.Where(ls => ls.LineStationIndex != index).ToList().ForEach(ls => sum += dl.GetAdjacentStations(ls.Station, stationsInLine[ls.LineStationIndex].Station).Time);
            return sum;
        }
        /// <summary>
        /// return the closest start time of line 
        /// </summary>
        /// <param name="lineId"> line id</param>
        /// <param name="duration">the time between the first station to current station </param>
        /// <param name="time">the time now</param>
        /// <returns>time of arriveal or null</returns>
        private TimeSpan? startTime(int lineId, TimeSpan duration, TimeSpan time)
        {
            foreach (var lineTrip in dl.GetAllLineTripBy(lt => lt.LineId == lineId).OrderBy(lt => lt.StartAt).ToList())
            {
                if (lineTrip.Frequency.TotalSeconds == 0)
                {
                    if ((lineTrip.StartAt + duration - time).TotalSeconds > -6)
                        return lineTrip.StartAt;
                }
                else
                {
                    for (TimeSpan t = lineTrip.StartAt; t <= lineTrip.FinishAt; t += lineTrip.Frequency)
                    {
                        if ((t + duration - time).TotalSeconds > -6)
                            return t;
                    }
                }
            }
            return null;

        }
        /// <summary>
        /// Returns an encrypted password
        /// </summary>
        /// <param name="value">original password</param>
        /// <returns>encrypted password</returns>
        private string encrypt(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }
        /// <summary>
        /// returns the original password from an encrypted password
        /// </summary>
        /// <param name="value">encrypted password</param>
        /// <returns>original password</returns>
        private string decrypt(string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
        

    }


}

