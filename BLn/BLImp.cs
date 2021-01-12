﻿using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
//using System.Device.Location;
using BLAPI;
using BO;
using DLAPI;
namespace BL
{
    class BLImp : IBL
    {
        IDAL dl = DLFactory.GetDL();
        public void AddBus(int licenseNum, DateTime fromDate, double totalTrip, double fuelRemain, Enums.BusStatus status)
        {
            try
            {
                DO.Bus bus = new DO.Bus();
                bus.LicenseNum = licenseNum;
                bus.FromDate = fromDate;
                bus.TotalTrip = totalTrip;
                bus.FuelRemain = fuelRemain;
                bus.Status = (DO.Enums.BusStatus)status;
                dl.AddBus(bus);
            }
            catch (Exception ex)
            {


            }
        }

        public void AddLine(int code, Enums.Areas area, BO.Station station1, BO.Station station2)

        {
            //line trip??
            try
            {
                dl.GetStation(station1.Code);
                dl.GetStation(station2.Code);
                if (station1.Code == station2.Code)
                {
                    //thow 2 תחנות שוות
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
            catch (Exception ex)
            {

                //throw;
            }

        }

        private TimeSpan calTime(int code1, int code2)
        {
            return new TimeSpan(0, (int)(60 * calDistance(code1, code2) / 30), 0);
        }

        private double calDistance(int code1, int code2)
        {
            try
            {
                DO.Station station1 = dl.GetStation(code1);
                DO.Station station2 = dl.GetStation(code2);
                GeoCoordinate coor1 = new GeoCoordinate(station1.Latitude, station1.Longitude);
                GeoCoordinate coor2 = new GeoCoordinate(station2.Latitude, station2.Longitude);
                return coor1.GetDistanceTo(coor2) * 1.5;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void AddStation(int code, string name, string address, double latitude, double longitude)
        {
            try
            {
                //Longitude = rand.NextDouble() * (35.5 - 34.3) + 34.3, Lattitude = rand.NextDouble() * (33.3 - 31) + 31
                if (longitude < 34.3 || longitude > 35.5 || latitude < 31 || latitude > 33.3)
                    //throw new 
                    ;
            }
            catch (Exception ex)
            {

                //throw
            }
            try
            {
                dl.AddStation(new DO.Station { Code = code, Address = address, Latitude = latitude, Longitude = longitude, Name = name });
            }
            catch (Exception ex)
            {
                //throw
            }
        }

        public void CareBus(int num)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int num)
        {//bus on trip

            try
            {
                dl.DeleteBus(num);
            }
            catch (Exception ex)
            {
                //throw
            }


        }

        public void DeleteLine(int num)
        {//bus on trip,trip

            try
            {
                dl.DeleteLine(num);
                dl.GetAllLineStationBy(ls => ls.LineId == num).ToList().ForEach(ls => dl.DeleteLineStation(num, ls.Station));
                dl.GetAllLineTripBy(t => t.LineId == num).ToList().ForEach(t => dl.DeleteLineTrip(t.Id));

            }

            catch (Exception ex)
            {

                //throw;
            }
        }

        public void DeleteStation(int num)
        {
            try
            {
                dl.DeleteStation(num);
                dl.GetAllLineStationBy(ls => ls.Station == num).ToList().ForEach(ls => DeleteStationInLine(num, ls.LineId));
                //תחנות עוקבות?
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            return from item in dl.GetAllBus()
                   select new BO.Bus { LicenseNum = item.LicenseNum, FromDate = item.FromDate, FuelRemain = item.FuelRemain, TotalTrip = item.TotalTrip, Status = (Enums.BusStatus)item.Status };
        }

        public IEnumerable<Line> GetAllLines()
        {
            return from line in dl.GetAllLine()
                   select new BO.Line//
                   {
                       Id = line.Id,
                       Code = line.Code,
                       Arae = (Enums.Areas)line.Arae,
                       Stations = from station in dl.GetAllLineStationBy(ls => ls.LineId == line.Id).OrderBy(s => s.LineStationIndex)
                                  let name = dl.GetStation(station.Station).Name
                                  //let prev=dl.GetAllLineStationBy(ls=>ls.LineId==line.Id&&ls.LineStationIndex==station.LineStationIndex-1).First().
                                  where (station.LineStationIndex != 1)
                                  let time = dl.GetAdjacentStations(station.PrevStation, station.Station).Time
                                  let dis = dl.GetAdjacentStations(station.PrevStation, station.Station).Distance
                                  select new BO.LineStation { Code = station.Station, Name = name, DistanceFromPrevStat = dis, TimeFromPrevStat = time }

                   };

        }

        public IEnumerable<Station> GetAllStations()
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
                               let arrivalTimes = ExceptedArrivalTimes(line.Id, station.Code)
                               select new BO.StationLine
                               {
                                   Id = line.Id,
                                   Code = line.Code,
                                   LastStation = line.LastStation,
                                   // ArrivalTimes = arrivalTimes
                               }
                   };
        }
        private IEnumerable<DateTime> ExceptedArrivalTimes(int lineId, int code)
        {
            throw new NotImplementedException();
        }

        public bool IsAdminAndExists(string userName, string password)
        {
            try
            {
                DO.User user = dl.GetUser(userName);
                if (user.Password != password)
                    throw new Exception();
                return user.Admin;
            }
            catch (Exception ex)
            {

                throw new Exception();
            }

        }

        public void RefuelBus(int num)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(Bus bus)
        {//
            try
            {
                DO.Bus busToUpdate = new DO.Bus();
                busToUpdate.LicenseNum = bus.LicenseNum;
                busToUpdate.FromDate = bus.FromDate;
                busToUpdate.TotalTrip = bus.TotalTrip;
                busToUpdate.FuelRemain = bus.FuelRemain;
                busToUpdate.Status = (DO.Enums.BusStatus)bus.Status;
                dl.UpdateBus(busToUpdate);
            }
            catch (Exception ex)
            {
                //throw
            }
        }

        public void UpdateLine(int id, int code, Enums.Areas area)
        {
            dl.UpdateLine(id, l => l.Code = code);
            dl.UpdateLine(id, l => l.Arae = (DO.Enums.Areas)area);
        }
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
                    Stations = from station in dl.GetAllLineStationBy(ls => ls.LineId == line.Id).OrderBy(s => s.LineStationIndex)
                               let name = dl.GetStation(station.Station).Name
                               //let prev=dl.GetAllLineStationBy(ls=>ls.LineId==line.Id&&ls.LineStationIndex==station.LineStationIndex-1).First().
                               where (station.LineStationIndex != 1)
                               let time = dl.GetAdjacentStations(station.PrevStation, station.Station).Time
                               let dis = dl.GetAdjacentStations(station.PrevStation, station.Station).Distance
                               select new BO.LineStation { Code = station.Station, Name = name, DistanceFromPrevStat = dis, TimeFromPrevStat = time }

                };
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public void UpdateStation(int code, string name, string address)
        {
            try
            {
                dl.UpdateStation(code, s => s.Name = name);
                dl.UpdateStation(code, s => s.Address = address);

            }
            catch (Exception ex)
            {

                //throw;
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
                    //dl.GetLineStation(lineId, stationBefore).NextStation = code;
                    dl.UpdateLineStation(lineId, stationBefore, ls => ls.NextStation = code);

                }
                if (stationBefore == dl.GetLine(lineId).LastStation)
                {
                    dl.UpdateLine(lineId, l => l.LastStation = code);
                    last = true;
                }
                else
                {
                    //dl.GetLineStation(lineId, next).PrevStation = code;
                    dl.UpdateLineStation(lineId, next, ls => ls.PrevStation = code);
                }

                //dl.GetAllLineStationBy(ls => ls.LineId == lineId && ls.LineStationIndex >= index).ToList().ForEach(x => x.LineStationIndex++);
                dl.GetAllLineStationBy(ls => ls.LineId == lineId && ls.LineStationIndex >= index).ToList().ForEach(x => dl.UpdateLineStation(lineId, x.Station, ls => ls.LineStationIndex++));
                dl.AddLineStation(new DO.LineStation { Station = code, LineId = lineId, LineStationIndex = index, PrevStation = stationBefore, NextStation = next });

                //תחנות עוקבות

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
                    dl.AddAdjacentStations(new DO.AdjacentStations { Station1 = code, Station2 = code, Distance = dis, Time = time });
                }

            }
            catch (Exception ex)
            {

                //throw;
            }
        }
        public void DeleteStationInLine(int code, int lineId)
        {
            try
            {
                if (dl.GetLineStation(lineId, dl.GetLine(lineId).LastStation).LineStationIndex <= 2)
                {
                    // throw new less than 2 stations in the line
                }
                int index;
                dl.GetStation(code);
                int next = dl.GetLineStation(lineId, code).NextStation;
                int prev = dl.GetLineStation(lineId, code).PrevStation;
                if (dl.GetLine(lineId).FirstStation == code)
                {

                    int first = dl.GetLineStation(lineId, next).Station;////?
                    dl.UpdateLine(lineId, l => l.FirstStation = first);
                }
                else if (dl.GetLine(lineId).LastStation == code)
                {
                    dl.UpdateLine(lineId, l => l.LastStation = prev);
                }
                else
                {
                    if (dl.GetAllAdjacentStationsBy(a => a.Station1 == prev && a.Station2 == next).Count() < 1)
                    {
                        double dis = calDistance(prev, next);
                        TimeSpan time = calTime(prev, next);
                        dl.AddAdjacentStations(new DO.AdjacentStations { Station1 = prev, Station2 = next, Distance = dis, Time = time });
                    }
                }
                index = dl.GetLineStation(lineId, code).LineStationIndex;
                dl.GetAllLineStationBy(ls => ls.LineId == lineId && ls.LineStationIndex >= index).ToList().ForEach(x => dl.UpdateLineStation(lineId, x.Station, ls => ls.LineStationIndex++));
                dl.DeleteLineStation(lineId, code);
                //תחנות עוקבות?

            }
            catch (Exception ex)
            {

                //throw;
            }
        }
        public void UpdateLineStation(BO.LineStation lineStation, int lineId)
        {





        }
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
                            let arrivalTimes = ExceptedArrivalTimes(line.Id, station.Code)
                            select new BO.StationLine
                            {
                                Id = line.Id,
                                Code = line.Code,
                                LastStation = line.LastStation,
                                // ArrivalTimes = arrivalTimes

                            }
                };
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public void AddLineTrip(int lineId, TimeSpan startAt, TimeSpan finishAt, TimeSpan freq)
        {
            try
            {
                //if (dl.GetAllLineTripBy(lt => lt.LineId == lineId).Select(lt => (lt.StartAt == startAt) || (lt.StartAt > startAt && lt.StartAt < finishAt) || (lt.StartAt < startAt && lt.FinishAt > startAt)).Count() > 0)
                if (dl.GetAllLineTripBy(lt => lt.LineId == lineId && ((lt.StartAt == startAt) || (lt.StartAt > startAt && lt.StartAt < finishAt) || (lt.StartAt < startAt && lt.FinishAt > startAt))).Count() > 0)
                {
                    //trow
                    //delete else
                }
                else
                    dl.AddLineTrip(new DO.LineTrip { LineId = lineId, StartAt = startAt, FinishAt = finishAt, Frequency = freq });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public IEnumerable<BO.LineTrip> getLineTrips(int id)
        {
            try
            {
                return (from item in dl.GetAllLineTripBy(lt => lt.LineId == id)
                        select new BO.LineTrip { StartAt = item.StartAt, FinishAt = item.FinishAt, Frequency = item.Frequency, Id = item.Id, LineId = item.LineId }).OrderBy(x => x.StartAt);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void DeleteLineTrip(int id)
        {
            try
            {
                dl.DeleteLineTrip(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void UpdateLineTrip(LineTrip lineTrip, TimeSpan startAt, TimeSpan finishAt, TimeSpan freq)
        {
            try
            {
                if (dl.GetAllLineTripBy(lt => lt.LineId == lineTrip.LineId && lt.Id != lineTrip.Id).Select(lt => (lt.StartAt == startAt) || (lt.StartAt > startAt && lt.StartAt < finishAt) || (lt.StartAt < startAt && lt.FinishAt > startAt)).Count() > 0)
                {
                    //trow
                }
                dl.UpdateLineTrip(new DO.LineTrip { LineId = lineTrip.LineId, Id = lineTrip.Id, StartAt = startAt, FinishAt = finishAt, Frequency = freq });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void UpdateTimeOrDistance(int code, int lineId, TimeSpan time, double distance)
        {
            try
            {
                dl.GetStation(code);
                var prev = dl.GetAllLineStationBy(ls => ls.LineId == lineId && ls.NextStation == code).FirstOrDefault();
                if(prev==null)
                {
                    //throw;
                    //delete else!
                }
                else
                {
                    dl.UpdateAdjacentStations(new DO.AdjacentStations { Station1 = prev.Station, Station2 = code, Distance = distance, Time = time });
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}

