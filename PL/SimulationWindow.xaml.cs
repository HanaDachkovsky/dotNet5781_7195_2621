using BLAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
        IBL bl;
        BackgroundWorker worker;
        bool isWorking;
        Stopwatch stopwatch;
        TimeSpan start;
        public SimulationWindow(IBL bl2,BO.Station station)
        {
            
            InitializeComponent();
            bl = bl2;
            DataContext = station;
            stopwatch = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            start = DateTime.Now.TimeOfDay;
            tbTime.Text = start.ToString().Substring(0, 8);
            tbStation.Text = station.Name; 
            stopwatch.Restart();
            isWorking = true;
            dgTime.ItemsSource = bl.GetArrivalTimes(DataContext as BO.Station, DateTime.Now.TimeOfDay);
            worker.RunWorkerAsync();

        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan now = start + stopwatch.Elapsed;
            tbTime.Text = now.ToString().Substring(0, 8);
            dgTime.ItemsSource = bl.GetArrivalTimes(DataContext as BO.Station, now);

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(isWorking)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            stopwatch.Stop();
            isWorking = false;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
