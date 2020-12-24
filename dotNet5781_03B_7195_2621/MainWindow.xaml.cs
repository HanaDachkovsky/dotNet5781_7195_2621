using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace dotNet5781_03B_7195_2621
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public int Km { get; set; } = 0;
        //static public bool IsRef { get; set; } = false;
        //static public bool IsCare { get; set; } = false;
        static internal List<BackgroundWorker> driveWorkers = new List<BackgroundWorker>();//list of backgroundworkers for each bus
        public Random rand = new Random(DateTime.Now.Millisecond);
        static internal ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
        public MainWindow()
        {

            InitializeComponent();

            for (int i = 0; i < 10; i++)//intialize 10 random buses
            {
                Bus addedBus = new Bus();
                bool found = false;
                foreach (Bus item in buses)
                {
                    if (item.VehicleNum == addedBus.VehicleNum)
                    {
                        found = true;
                    }
                }
                if (found == true)
                    i--;
                else
                    buses.Add(addedBus);
            }
            int rand1, rand2;
            bool found1 = false;
            do
            {
                found1 = false;
                rand2 = rand.Next(1000000, 10000000);
                rand1 = rand.Next(1000000, 10000000);
                foreach (Bus item in buses)
                {
                    if (item.VehicleNum == rand1.ToString() || item.VehicleNum == rand2.ToString())
                    {
                        found1 = true;
                        break;
                    }

                }
            }
            while (found1 == true);
            buses.Add(new Bus(rand1.ToString(), new DateTime(2016, 1, 1), DateTime.Now.AddMonths(-15), 200, 200, 1000, STATUS.Ready));//add bus that need care
            buses.Add(new Bus(rand2.ToString(), new DateTime(2016, 1, 1), DateTime.Now.AddMonths(-2), 100000, 119000, 1000, STATUS.Ready));//add bus with kilometrage that close to care
            buses[0].AvailableKm = 10;
            busList.ItemsSource = buses;
            for(int i=0;i<buses.Count;i++)
            {
                BackgroundWorker driveWorker = new BackgroundWorker();
                driveWorker = new BackgroundWorker();
                driveWorker.DoWork += DriveWorker_DoWork;
                driveWorker.ProgressChanged += DriveWorker_ProgressChanged;
                driveWorker.RunWorkerCompleted += DriveWorker_RunWorkerCompleted;
                driveWorker.WorkerReportsProgress = true;
                driveWorkers.Add(driveWorker);
            }
            


           
        }



        private void DriveWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            Km = 0;
            int index = (e.Argument as ThreadBus).Index;//the index of the bus 
            int length = (e.Argument as ThreadBus).Seconds;//the length of the thread
            for (int i = 1; i <= length; i++)//until the drive end
            {
                if (driveWorkers[index].CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(1000);
                    (e.Argument as ThreadBus).Seconds--;
                    driveWorkers[index].ReportProgress(i * 100 / length, e.Argument as ThreadBus);
                }

            }
            System.Threading.Thread.Sleep(1000);
            driveWorkers[index].ReportProgress(0 , e.Argument as ThreadBus);//zeroing of the progress bar
            
            e.Result = (e.Argument as ThreadBus).Bus;


        }
        private void DriveWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            (e.UserState as ThreadBus).Bus.ValueProBar = progress.ToString();//update the proggress bar
            (e.UserState as ThreadBus).Bus.WatchTime = (e.UserState as ThreadBus).Seconds.ToString();//update the watch
            if ((e.UserState as ThreadBus).Seconds==0)
            {
                (e.UserState as ThreadBus).Bus.WatchTime = "";
            }

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
          
            while(buses.Count>=driveWorkers.Count)//add bacgroundworker to the new bus
            {
                BackgroundWorker driveWorker = new BackgroundWorker();
                driveWorker = new BackgroundWorker();
                driveWorker.DoWork += DriveWorker_DoWork;
                driveWorker.ProgressChanged += DriveWorker_ProgressChanged;
                driveWorker.RunWorkerCompleted += DriveWorker_RunWorkerCompleted;
                driveWorker.WorkerReportsProgress = true;
                driveWorkers.Add(driveWorker);
            }
            AddWindow addWindow = new AddWindow(buses);
            addWindow.Show();
        }

        private void DriveWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            (e.Result as Bus).Status = STATUS.Ready;
            (e.Result as Bus).Color = Brushes.AliceBlue;
        }

        private void busList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void refuelingButton_Click(object sender, RoutedEventArgs e)
        {
            Bus bus=((sender as Button).Parent as Grid).DataContext as Bus;
            refueling(bus);

        }

        internal static void refueling(Bus bus)
        {
            int index = buses.IndexOf(bus);
                if (driveWorkers[index].IsBusy == false)
                {
                    bus.AvailableKm = 1200;
                    bus.Status = STATUS.Refueling;
                    bus.Color = Brushes.Orange;
                    int length = 12;
                    bus.WatchTime = length.ToString();
                    ThreadBus threadBus = new ThreadBus(bus, length, index);
                    driveWorkers[index].RunWorkerAsync(threadBus);

                }
        }
       
        private void driveButton_Click(object sender, RoutedEventArgs e)
        {
       
            Bus bus = ((sender as Button).DataContext) as Bus;
            int index= buses.IndexOf(bus);
            DriveWindow driveWindow = new DriveWindow(buses);
            driveWindow.DataContext = this;
            driveWindow.ShowDialog();
            if (bus.CheckBus(Km) == false)//if the bus suitable to the drive
            {
                Km = 0;
                MessageBox.Show("the bus is not suitable for driving", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (Km > 0)
            {
                if (driveWorkers[index].IsBusy == false)
                {
                    bus.Kilometrage += Km;
                    bus.AvailableKm -= Km;
                    bus.Status = STATUS.Traveling;
                    bus.Color = Brushes.GreenYellow;
                    int length = Km * 6 / rand.Next(20, 50);//the length of the drive
                    bus.WatchTime = length.ToString();
                    ThreadBus threadBus = new ThreadBus(bus, length,index);
                    driveWorkers[index].RunWorkerAsync(threadBus);
                  
                }
            }

        }



        private void busList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowWindow showWindow = new ShowWindow((Bus)busList.SelectedItem);
            showWindow.ShowDialog();

        }

        
    }
}



