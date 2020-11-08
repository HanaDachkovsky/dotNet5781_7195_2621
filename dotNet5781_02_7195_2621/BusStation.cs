using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7195_2621
{
    class BusStation
    {
        private int busStationKey;//number of station
        private double latitude;//קו רוחב
        private double longitude;//קו אורך  
        private string adress;
        static private Random rand = new Random(DateTime.Now.Millisecond);

        public int BusStationKey { get => busStationKey;}

        public BusStation(int code, double _latitude, double _longitude, string _adress = "")
        {
            busStationKey = code;
            latitude = _latitude;
            longitude = _longitude;
            adress = _adress;
        }
        public BusStation(int code, string _adress = "")
        {
            busStationKey = code;
            adress = _adress;
            latitude = rand.NextDouble() * (33.3 - 31) + 31;
            longitude = rand.Next() * (35.5 - 34.3) + 34.3;
        }
        public BusStation ()
        {
            adress = "";
            latitude = rand.NextDouble() * (33.3 - 31) + 31;
            longitude = rand.Next() * (35.5 - 34.3) + 34.3;
            busStationKey = rand.Next(1000000);
        }
            
            

        public override string ToString()
        {
            return ("Bus Station Code: " + busStationKey + ", " + latitude + "°N " + longitude + "°E");
        }
    }
}
