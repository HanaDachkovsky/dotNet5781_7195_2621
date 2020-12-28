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
        void UpdateBus(BO.Bus);
        void AddBus(int licenseNum, DateTime fromDate, double totalTrip, double fuelRemain, Enums.BusStatus status);
        #endregion

        #region Line
        IEnumerable<BO.Line> GetAllLines();
        void DeleteLine(int num);
        void UpdateLine(BO.Line);
        void AddLine(......);
        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
        void DeleteStation(int num);
        void UpdateStation(BO.Line);
        void AddStation(......);
        #endregion

    }
}
