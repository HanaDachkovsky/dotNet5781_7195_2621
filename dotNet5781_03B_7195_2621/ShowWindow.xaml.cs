﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace dotNet5781_03B_7195_2621
{
    /// <summary>
    /// Interaction logic for ShowWindow.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        public Bus ExtraData { get; set; }

        public ShowWindow(Bus _ExtraData)
        {
            InitializeComponent();
            ExtraData = _ExtraData;
            this.DataContext = ExtraData;
            tbKmofCare.Text = (ExtraData.Kilometrage - ExtraData.KmsLastCare).ToString();//the Kms of the last care

        }

        private void btCare_Click(object sender, RoutedEventArgs e)
        {
            Bus bus = DataContext as Bus;
            int index=MainWindow.buses.IndexOf(bus);
            if (MainWindow.driveWorkers[index].IsBusy == false)
            {
                bus.KmsLastCare=bus.Kilometrage;
                bus.LastCare = DateTime.Now;
                bus.Status = STATUS.Care;
                bus.Color = Brushes.Aqua;
                int length = 144;
                bus.WatchTime = length.ToString();
                
                ThreadBus threadBus = new ThreadBus(bus , length, index);
                MainWindow.driveWorkers[index].RunWorkerAsync(threadBus);

            }
        }

        private void btRef_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.refueling(DataContext as Bus);
        }

       
    }
}
