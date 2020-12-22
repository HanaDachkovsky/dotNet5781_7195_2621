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
        public int Km { get; set; } = 0;
        public bool IsRef { get; set; } = false;
        public bool IsCare { get; set; } = false;
        List<BackgroundWorker> driveWorkers = new List<BackgroundWorker>();
        public Random rand = new Random(DateTime.Now.Millisecond);
        ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
        public MainWindow()
        {

            InitializeComponent();

            for (int i = 0; i < 10; i++)
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
            buses.Add(new Bus(rand1.ToString(), new DateTime(2016, 1, 1), DateTime.Now.AddMonths(-15), 200, 200, 1000, STATUS.Ready));
            buses.Add(new Bus(rand2.ToString(), new DateTime(2016, 1, 1), DateTime.Now.AddMonths(-2), 100000, 119000, 1000, STATUS.Ready));
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
            int index = (e.Argument as ThreadBus).Index;
            int length = (e.Argument as ThreadBus).Seconds;
            for (int i = 1; i <= length; i++)
            {
                if (driveWorkers[index].CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    (e.Argument as ThreadBus).Seconds--;
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(1000);
                    driveWorkers[index].ReportProgress(i * 100 / length, e.Argument as ThreadBus);
                }

            }
            driveWorkers[index].ReportProgress(0 , e.Argument as ThreadBus);
            
            e.Result = (e.Argument as ThreadBus).Bus;


        }
        private void DriveWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            (e.UserState as ThreadBus).ProggressTime.Value = progress;
            (e.UserState as ThreadBus).TextTime.Text = (e.UserState as ThreadBus).Seconds.ToString();
            if((e.UserState as ThreadBus).Seconds==0)
            {
                (e.UserState as ThreadBus).TextTime.Text = string.Empty;
            }

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
          
            while(buses.Count>=driveWorkers.Count)
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

        }

        private void busList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void refuelingButton_Click(object sender, RoutedEventArgs e)
        {
            IsRef = true;
            refueling(((sender as Button).Parent as Grid));


        }

        private void refueling(Grid grid)
        {
            Bus bus = (grid.DataContext) as Bus;
            int index = buses.IndexOf(bus);
            if (IsRef ==true )
            {
                IsRef = false;
                if (driveWorkers[index].IsBusy == false)
                {
                    bus.AvailableKm = 1200;
                    bus.Status = STATUS.Refueling;
                    grid.Background = Brushes.Orange;
                    int length = 12;
                    (grid.Children[4] as TextBlock).Text = length.ToString();
                    ThreadBus threadBus = new ThreadBus(bus, grid.Children[4] as TextBlock, grid.Children[3] as ProgressBar, length, index);
                    driveWorkers[index].RunWorkerAsync(threadBus);
                    grid.Background = Brushes.AliceBlue;

                }
            }
        }
       
        private void driveButton_Click(object sender, RoutedEventArgs e)
        {
       
            Bus bus = ((sender as Button).DataContext) as Bus;
            int index= buses.IndexOf(bus);
            DriveWindow driveWindow = new DriveWindow(buses);
            driveWindow.DataContext = this;
            driveWindow.ShowDialog();
            if (bus.CheckBus(Km) == false)
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
                    ((sender as Button).Parent as Grid).Background = Brushes.GreenYellow;
                    int length = Km * 6 / rand.Next(20, 50);
                    (((sender as Button).Parent as Grid).Children[4] as TextBlock).Text = length.ToString();
                    ThreadBus threadBus = new ThreadBus(bus, ((sender as Button).Parent as Grid).Children[4] as TextBlock, ((sender as Button).Parent as Grid).Children[3] as ProgressBar, length,index);
                    driveWorkers[index].RunWorkerAsync(threadBus);
                    ((sender as Button).Parent as Grid).Background = Brushes.AliceBlue;
                  
                }
            }

        }



        private void busList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowWindow showWindow = new ShowWindow((Bus)busList.SelectedItem);
            showWindow.DataContext = this;
            showWindow.ShowDialog();
            DependencyObject obj = (DependencyObject)e.OriginalSource;
            




        }

    }
}



