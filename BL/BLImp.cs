using System;
using System.Collections.Generic;
using System.Text;
using BLAPI;
using BO;
using DLAPI; 
namespace BL
{
    class BLImp : IBL
    {
        IDAL dl =DLFactory.GetDL();
        public void AddBus(int licenseNum, DateTime fromDate, double totalTrip, double fuelRemain, Enums.BusStatus status)
        {
            DO.Bus bus = new DO.Bus();
            bus.LicenseNum = licenseNum;
            bus.FromDate = fromDate;
            bus.TotalTrip = totalTrip;
            bus.FuelRemain = fuelRemain;
            bus.Status = (DO.Enums.BusStatus)status;
            dl.AddBus(bus);
        }

        public void AddLine(int code, Enums.Areas area, object )
        {
            throw new NotImplementedException();
        }

        public void AddStation(int code, string name, string address, double latitude, double longitude, IEnumerable<StationLine> lines)
        {
            throw new NotImplementedException();
        }

        public void CareBus(int num)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int num)
        {
            dl.DeleteBus(num);
        }

        public void DeleteLine(int num)
        {
            dl.DeleteLine(num);
        }

        public void DeleteStation(int num)
        {
            dl.DeleteStation(num);
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public bool IsAdminAndExists(string userName, string password)
        {
            throw new NotImplementedException();
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
