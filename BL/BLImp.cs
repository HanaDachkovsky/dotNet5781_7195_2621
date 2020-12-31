using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            catch(Exception ex)
            {
                //throw
            }
        }

        public void AddLine(int code, Enums.Areas area, object )
        {
            throw new NotImplementedException();
        }

        public void AddStation(int code, string name, string address, double latitude, double longitude, IEnumerable<StationLine> lines)
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
        {//bus on trip

            //try
            //{
            //    dl.DeleteLine(num);
            //    var x=dl.GetAllLineStationBy(ls => ls.LineId == num);
            //    IEnumerable<int> xx = new int[] { 1, 2 };
            //    from item in dl.GetAllLineStationBy(ls => ls.LineId == num)
            //    select dl.de
            //}
            
            //catch (Exception ex)
            //{

            //    //throw;
            //}
        }

        public void DeleteStation(int num)
        {
            //try
            //{
            //    dl.DeleteStation(num);

            //}
            //catch (Exception ex)
            //{

            //    //throw;
            //}
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

        }

        public void RefuelBus(int num)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(Bus bus)
        {//
            DO.Bus busToUpdate = new DO.Bus();
            busToUpdate.LicenseNum = bus.LicenseNum;
            busToUpdate.FromDate = bus.FromDate;
            busToUpdate.TotalTrip = bus.TotalTrip;
            busToUpdate.FuelRemain = bus.FuelRemain;
            busToUpdate.Status = (DO.Enums.BusStatus)bus.Status;
            dl.UpdateBus(busToUpdate);
        }

        public void UpdateLine(Line line)
        {
            throw new NotImplementedException();
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
    }
}
