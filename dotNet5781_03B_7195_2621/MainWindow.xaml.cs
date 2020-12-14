using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public int Km { get; set; }
        public BackgroundWorker driveWorker;
        public Random rand = new Random(DateTime.Now.Millisecond);
        ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
        public MainWindow()
        {

            InitializeComponent();

            for(int i=0;i<10;i++)
            {
                Bus addedBus = new Bus();
                bool found = false;
                foreach (Bus item in buses)
                {
                    if(item.VehicleNum==addedBus.VehicleNum)
                    {
                        found = true;
                    }
                }
                if (found == true)
                    i--;
                else
                    buses.Add(addedBus);
            }
            int rand1,rand2;
            bool found1 = false;
            do
            {
                found1 = false;
                rand2 = rand.Next(1000000, 10000000);
                rand1 = rand.Next(1000000, 10000000);
                foreach (Bus item in buses)
                {
                    if (item.VehicleNum == rand1.ToString()|| item.VehicleNum == rand2.ToString())
                    {
                        found1 = true;
                        break;
                    }

                }
            }
            while (found1 == true);
            buses.Add(new Bus(rand1.ToString(), new DateTime(2016, 1, 1), DateTime.Now.AddMonths(-15),200, 200, 1000, STATUS.Ready));
            buses.Add(new Bus(rand2.ToString(), new DateTime(2016, 1, 1), DateTime.Now.AddMonths(-2), 100000, 119000, 1000, STATUS.Ready));
            buses[0].AvailableKm = 10;
            busList.ItemsSource = buses;
            driveWorker = new BackgroundWorker();
            driveWorker.DoWork += DriveWorker_DoWork;
            driveWorker.ProgressChanged += DriveWorker_ProgressChanged;
            driveWorker.WorkerReportsProgress = true;

    
        }

        private void DriveWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DriveWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Bus bus = e.Argument as Bus;
            int km = Km;
            Km = 0;

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(buses);
            addWindow.Show();

        }

        private void busList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void refuelingButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
       

        private void driveButton_Click(object sender, RoutedEventArgs e)
        {
            
           Bus bus = ((sender as Button).DataContext)as Bus;
           DriveWindow driveWindow = new DriveWindow(buses);
           driveWindow.DataContext = this;
           driveWindow.ShowDialog();
            if (bus.CheckBus(Km) == false)
            {
                Km = 0;
                MessageBox.Show("the bus is not suitable for driving", "ERROR",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if(Km>0)
            {
                //var x = busList.SelectedItem;
                driveWorker.RunWorkerAsync(bus);
                //var x = busList.FindResource(bus);
                
            }

        }

        private void busList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowWindow showWindow = new ShowWindow((Bus)busList.SelectedItem);
            showWindow.Show();

        }


    }
}
