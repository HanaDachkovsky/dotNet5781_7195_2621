﻿using dotNet5781_02_7195_2621;
using System;
using System.Collections.Generic;
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

namespace dotNet5781_03A_7195_2621
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BusList busLines = new BusList();
        private BusLine currentDisplayBusLine;
        public MainWindow()
        {
            InitializeComponent();
            for (int i=0;i<10;i++)
            {
                busLines.Add(new BusLine());//Because the buses are random, with ctor the have default area, the area of all the buses is:General
            }
            cbBusLines.ItemsSource = busLines;
            cbBusLines.DisplayMemberPath = "BusLineKey";
            cbBusLines.SelectedIndex =0;
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusLineKey);
        }

        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = busLines[index].First();
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusLineKey);
            tbArea.Text= (cbBusLines.SelectedValue as BusLine).Area.ToString();//change the area to be the area of the selected line
           
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
