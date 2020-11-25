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
        private double latitude;
        private double longitude;
        private string adress;
        static protected Random rand = new Random(DateTime.Now.Millisecond);

        public int BusStationKey { get => busStationKey; set => busStationKey = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
        public string Adress { get => adress; set => adress = value; }
        //ctors
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
            latitude = rand.NextDouble() * (33.3 - 31) + 31;//random number from 31 to 33.3
            longitude = rand.NextDouble() * (35.5 - 34.3) + 34.3;//random number from 34.3 to 35.5
        }
        public BusStation ()
        {
            adress = "";
            latitude = rand.NextDouble() * (33.3 - 31) + 31;//random number from 31 to 33.3
            longitude = rand.NextDouble() * (35.5 - 34.3) + 34.3;//random number from 34.3 to 35.5
            busStationKey = rand.Next(1000000);// random number until 1000000
        }
            
            

        public override string ToString()
        {
            return ("Bus Station Code: " + busStationKey + ", " + latitude + "°N " + longitude + "°E");
        }
    }
}
