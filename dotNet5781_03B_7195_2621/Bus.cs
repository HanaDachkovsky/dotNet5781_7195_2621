using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;//.Math;//.dll.Math;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media;

namespace dotNet5781_03B_7195_2621
{
    public enum STATUS { Ready, Traveling, Refueling, Care };
    public class Bus : INotifyPropertyChanged
    {
        private string vehicleNum;
        private DateTime startDate;
        private DateTime lastCare;
        private double kmsLastCare;//kilometrage of the last care
        private double kilometrage;//the total kms
        private double availableKm;//the kms we can travel until the oil ends
        private STATUS status;
        private string valueProBar;
        private SolidColorBrush color=Brushes.AliceBlue;

        public Random rand = new Random(DateTime.Now.Millisecond);

        public event PropertyChangedEventHandler PropertyChanged;

        private string watchTime;
        public string WatchTime { get { return watchTime; } set { watchTime = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("WatchTime")); } } }
        public string ValueProBar { get { return valueProBar; } set { valueProBar = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("ValueProBar")); } } }
        public STATUS Status { get { return status; } set { status = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Status")); } } }
        public SolidColorBrush Color { get { return color; } set { color = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Color")); } } }
       


        public Bus(string _vehicleNum, DateTime _startDate, DateTime _lastCare, double _kmsLastCare, double _kilometrage, double _availableKm, STATUS _status)
        {//ctor
            vehicleNum = _vehicleNum;
            startDate = _startDate;
            lastCare = _lastCare;
            kmsLastCare = _kmsLastCare;
            kilometrage = _kilometrage;
            availableKm = _availableKm;
            Status = _status;
        }
        public Bus()
        {
            startDate = new DateTime(rand.Next(1980, 2021), rand.Next(1, 13), rand.Next(1, 29));
            if (StartDate.Year < 2018)
                vehicleNum = rand.Next(1000000, 10000000).ToString();
            else
                vehicleNum = rand.Next(10000000, 100000000).ToString();
            if (StartDate < (DateTime.Now.AddYears(-1)))
                LastCare = DateTime.Now.AddMonths(rand.Next(-12, 0));
            else
                LastCare = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day);
            KmsLastCare = rand.Next(20000);
            Kilometrage = rand.Next((int)KmsLastCare, 100000);
            AvailableKm = rand.Next(1, 1200);
            Status = STATUS.Ready;
        }
        public bool CheckBus(int num)//check if the bus suitable to the drive
        {
            TimeSpan timeFromLastCare = new TimeSpan();
            timeFromLastCare = DateTime.Now - LastCare;
            if (AvailableKm >= num && Kilometrage - KmsLastCare < 20000 && timeFromLastCare.TotalDays < 365&&Status==STATUS.Ready)
            {//check if the bus is suitable to drive
                return true;
            }

            return false;

        }
        public string GetStringVehNum()
        {
            string returnNum;
            if (StartDate.Year < 2018)
            {
                returnNum = vehicleNum.Insert(2, "-");//put "-" in the string in index 2
                returnNum = returnNum.Insert(6, "-");
                return returnNum;
            }
            //2017 and before
            returnNum = vehicleNum.Insert(3, "-");
            returnNum = returnNum.Insert(6, "-");
            return returnNum;
        }

        public string VehicleNum { get => GetStringVehNum(); }
        public double AvailableKm { get => availableKm; set => availableKm = value; }
        public double KmsLastCare { get => kmsLastCare; set => kmsLastCare = value; }
        public DateTime LastCare { get => lastCare; set => lastCare = value; }
        public double Kilometrage { get => kilometrage; set => kilometrage = value; }
        public DateTime StartDate { get => startDate; }
        //public STATUS Status { get => status;  set => status = value; }
    }

    
}

