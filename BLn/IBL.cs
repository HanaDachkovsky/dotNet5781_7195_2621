using BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLAPI
{
    public interface IBL
    {
        #region User
        bool IsAdminAndExists(string userName, string password);
        #endregion

        #region Bus
        IEnumerable<BO.Bus> GetAllBuses();
        void DeleteBus(int num);
        void RefuelBus(int num);
        void CareBus(int num);
        void UpdateBus(BO.Bus bus);
        void AddBus(int licenseNum, DateTime fromDate, double totalTrip, double fuelRemain, Enums.BusStatus status);
        #endregion

        #region Line
        BO.Line GetLine(int id);
        IEnumerable<BO.Line> GetAllLines();
        void DeleteLine(int num);
        void UpdateLine(int id, int code, Enums.Areas area);
        void AddLine(int code, Enums.Areas area, BO.Station station1, BO.Station station2);
        void AddStationToLine(int code, int lineId, TimeSpan time, double distance, int stationBefore);
        void DeleteStationInLine(int code, int lineId);
        void UpdateLineStation(BO.LineStation lineStation, int lineId);
        #endregion

        #region LineTrip
        void AddLineTrip(int lineId, TimeSpan startAt, TimeSpan finishAt, TimeSpan freq);
        IEnumerable<BO.LineTrip> getLineTrips(int id);
        void DeleteLineTrip(int id);
        void UpdateLineTrip(LineTrip lineTrip, TimeSpan startAt, TimeSpan finishAt, TimeSpan freq);
        #endregion

        #region Station
        BO.Station getStation(int code);
        IEnumerable<BO.Station> GetAllStations();
        void DeleteStation(int num);
        void UpdateStation(int code, string name, string address);
        void AddStation(int code, string name, string address, double latitude, double longitude);
        #endregion

    }
}

