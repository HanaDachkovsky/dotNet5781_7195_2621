﻿using System;
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
        public Bus()
        {

        }
        public bool CheckBus(string vehNum,int num)
        {
            TimeSpan timeFromLastCare = new TimeSpan();
            timeFromLastCare = DateTime.Now - LastCare;
            if (VehicleNum == vehNum)
            {
                if (AvailableKm >= num && Kilometrage-KmsLastCare < 20000 && timeFromLastCare.TotalDays < 365)
                {
                    Kilometrage += num;
                    KmsLastCare = Kilometrage;
                    AvailableKm -= num;
                    Console.WriteLine("the drive succeeded");
                    return true;
                }
                //if the bus exit but not suitable to drive
                Console.WriteLine("the bus is not suitable to drive");
                return true;//return true because we found the bus and print messege
            }
            return false;

        }

        public string VehicleNum { get => vehicleNum;}
        public double AvailableKm { get => availableKm; set => availableKm = value; }
        public double KmsLastCare { get => kmsLastCare; set => kmsLastCare = value; }
        public DateTime LastCare { get => lastCare; set => lastCare = value; }
        public double Kilometrage { get => kilometrage; set => kilometrage = value; }
    }
}
