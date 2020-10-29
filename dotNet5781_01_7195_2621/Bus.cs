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
        private double lastKm;//km from the last care
        private double kilometrage;//the total kms
        private double availableKm;//the kms we can travel until the oil ends
        public Bus()
        {

        }

        public string VehicleNum { get => vehicleNum;}
        public double AvailableKm { get => availableKm; set => availableKm = value; }
        public double LastKm { get => lastKm; set => lastKm = value; }
        public DateTime LastCare { get => lastCare; set => lastCare = value; }
        public double Kilometrage { get => kilometrage; set => kilometrage = value; }
    }
}
