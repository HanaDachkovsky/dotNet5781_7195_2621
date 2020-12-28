using System;
using System.Collections.Generic;
using System.Text;

namespace BLAPI
{
    public interface IBL
    {
        bool IsAdminAndExists(string userName, string password);
        IEnumerable<BO.Bus> GetAllBuses();
        IEnumerable<BO.Line> GetAllLines();
        IEnumerable<BO.Station> GetAllStations();


    }
}
