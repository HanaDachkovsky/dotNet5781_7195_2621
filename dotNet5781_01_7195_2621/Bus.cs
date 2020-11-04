using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_7195_2621
{
    class Bus
    {
        private string vehicleNum;
        private DateTime startDate;
        private DateTime lastCare;
        private double kmsLastCare;//kilometrage of the last care
        private double kilometrage;//the total kms
        private double availableKm;//the kms we can travel until the oil ends
        public Bus(string _vehicleNum, DateTime _startDate, DateTime _lastCare, double _kmsLastCare, double _kilometrage, double _availableKm )
        {//ctor
            vehicleNum = _vehicleNum;
            startDate = _startDate;
            lastCare = _lastCare;
            kmsLastCare = _kmsLastCare;
            kilometrage = _kilometrage;
            availableKm = _availableKm;
        }
        public bool CheckBus(string vehNum,int num)//check if the bus suitable to the drive
        {
            TimeSpan timeFromLastCare = new TimeSpan();
            timeFromLastCare = DateTime.Now - LastCare;
            if (VehicleNum == vehNum)// if this is the bus
            {
                if (AvailableKm >= num && Kilometrage-KmsLastCare < 20000 && timeFromLastCare.TotalDays < 365)
                {//check if the bus is suitable to drive
                    //update the drive:
                    Kilometrage += num;//the kilometrage grows
                    //KmsLastCare = Kilometrage;//the ilometrage of the last care is the current
                    AvailableKm -= num;//we can drive less kms because of the fuel
                    Console.WriteLine("the drive succeeded");
                    return true;
                }
                //if the bus exit but not suitable to drive
                Console.WriteLine("the bus is not suitable to drive");
                return true;//return true because we found the bus and print messege
            }
            return false;//if it is not the bus return false because true means we found(good or even not)

        }
        public string GetStringVehNum()
        {
            string returnNum;
            if (StartDate.Year < 2018)
            {
                returnNum = VehicleNum.Insert(2, "-");//put "-" in the string in index 2
                returnNum = returnNum.Insert(6, "-");
                return returnNum;
            }
          //2017 and before
            returnNum = VehicleNum.Insert(3, "-");
            returnNum = returnNum.Insert(6, "-");
            return returnNum;
        }

        public string VehicleNum { get => vehicleNum;}
        public double AvailableKm { get => availableKm; set => availableKm = value; }
        public double KmsLastCare { get => kmsLastCare; set => kmsLastCare = value; }
        public DateTime LastCare { get => lastCare; set => lastCare = value; }
        public double Kilometrage { get => kilometrage; set => kilometrage = value; }
        public DateTime StartDate { get => startDate; }
    }

    internal class sring
    {
    }
}
