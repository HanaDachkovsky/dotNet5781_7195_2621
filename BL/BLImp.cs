using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLAPI;
using BO;
using DLAPI;
namespace BLs
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
            catch(Exception ex)
            {

                
            }
        }

        public void AddLine(int code, Enums.Areas area, object )
        {
            throw new NotImplementedException();
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                //throw
            }


        }

        public void DeleteLine(int num)
        {//bus on trip,trip

            try
            {
                dl.DeleteLine(num);
                dl.GetAllLineStationBy(ls => ls.LineId == num).ToList().ForEach(ls=>dl.DeleteLineStation(num,ls.Station));
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
            select new BO.Line//בעיה עם תחנה ראשונה
            {
                Id = line.Id,
                Code = line.Code,
                Arae = (Enums.Areas)line.Arae,
                Stations =from station in dl.GetAllLineStationBy(ls=>ls.LineId==line.Id).OrderBy(s=>s.LineStationIndex)
                          let name=dl.GetStation(station.Station).Name
                          //let prev=dl.GetAllLineStationBy(ls=>ls.LineId==line.Id&&ls.LineStationIndex==station.LineStationIndex-1).First().
                          let time=dl.GetAdjacentStations(station.PrevStation, station.Station).Time
                          let dis= dl.GetAdjacentStations(station.PrevStation, station.Station).Distance
                          select new BO.LineStation { Code= station.Station, Name=name, DistanceFromPrevStat=dis, TimeFromPrevStat=time }
                          
            };

        }

        public IEnumerable<Station> GetAllStations()
        {
            return from station in dl.GetAllStation()
                   select new BO.Station
                   { 
                       Code=station.Code,
                       Name=station.Name,
                       Address=station.Address,
                       Longitude=station.Longitude,
                       Latitude=station.Latitude,
                       Lines=from lineSt in dl.GetAllLineStationBy(ls=>ls.Station==station.Code)
                             let line=dl.GetLine(lineSt.LineId)
                             let arrivalTimes=ExceptedArrivalTimes(line.Id, station.Code)
                             select new BO.StationLine
                             {
                                Id=line.Id,
                                Code=line.Code,
                                LastStation=line.LastStation,
                                ArrivalTimes=arrivalTimes
                             }
                   }
        }
        private IEnumerable<DateTime> ExceptedArrivalTimes(int lineId, int code)
        {
            throw new NotImplementedException();
        }

        public bool IsAdminAndExists(string userName, string password)
        {
            try
            {
                DO.User user =dl.GetUser(userName);
                if (user.Password != password)
                    //throw
                    ;
                return user.Admin;
            }
            catch(Exception ex)
            {

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
            catch(Exception ex)
            {
                //throw
            }
        }

        public void UpdateLine(Line line)
        {
           
        }

        public void UpdateStation(Station station)
        {
            //    //public int Code { get; set; }
            //public string Name { get; set; }
            //public string Address { get; set; }
            //public double Latitude { get; set; }
            //public double Longitude { get; set; }
            //public IEnumerable<StationLine> Lines { get; set; }////?
            ///////
        }
        void AddStationToLine(int code, int lineId, TimeSpan time, double distance, int stationBefore)
        { 
            try
            {
                int index;
                int next;
                dl.GetStation(code);
                dl.GetStation(stationBefore);
                if(stationBefore==0)
                {
                    next = dl.GetLine(lineId).FirstStation;
                    dl.UpdateLine(lineId, l => l.FirstStation = code);
                    index = 1; 
                }
                else
                {
                    dl.GetLineStation(lineId, stationBefore);  
                    index = dl.GetLineStation(lineId, stationBefore).LineStationIndex + 1;
                    next = dl.GetLineStation(lineId, stationBefore).NextStation;
                    //dl.GetLineStation(lineId, stationBefore).NextStation = code;
                    dl.UpdateLineStation(lineId, stationBefore, ls => ls.NextStation = code);

                }
                if (stationBefore==dl.GetLine(lineId).LastStation)
                {
                    dl.UpdateLine(lineId, l => l.LastStation = code);
                }
                else
                {
                    //dl.GetLineStation(lineId, next).PrevStation = code;
                    dl.UpdateLineStation(lineId, next, ls => ls.PrevStation = code);
                }
               
                //dl.GetAllLineStationBy(ls => ls.LineId == lineId && ls.LineStationIndex >= index).ToList().ForEach(x => x.LineStationIndex++);
                dl.GetAllLineStationBy(ls => ls.LineId == lineId && ls.LineStationIndex >= index).ToList().ForEach(x => dl.UpdateLineStation(lineId, x.Station, ls=>ls.LineStationIndex++));
                dl.AddLineStation(new DO.LineStation { Station = code, LineId = lineId, LineStationIndex = index, PrevStation = stationBefore, NextStation = next });

                //תחנות עוקבות?


            }
            catch (Exception ex)
            {

                //throw;
            }
        }
        void DeleteStationInLine(int code, int lineId)
        {
            try
            {
                if(dl.GetLineStation(lineId,dl.GetLine(lineId).LastStation).LineStationIndex<=2)
                {
                   // throw new less than 2 stations in the line
                }
                int index;
                dl.GetStation(code);
                int next=dl.GetLineStation(lineId, code).NextStation;
                int prev = dl.GetLineStation(lineId, code).PrevStation;
                if (dl.GetLine(lineId).FirstStation==code)
                {

                    int first = dl.GetLineStation(lineId, next).Station;
                    dl.UpdateLine(lineId, l => l.FirstStation = first);
                }
                else if (dl.GetLine(lineId).LastStation == code)
                {
                    dl.UpdateLine(lineId, l => l.LastStation = prev);
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
        void UpdateLineStation(BO.LineStation lineStation, int lineId)
        {





        }
    }
}
