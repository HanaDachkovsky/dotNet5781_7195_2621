using BLAPI;
using System;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for LineDetails.xaml
    /// </summary>
    public partial class LineDetails : Window
    {
        IBL bl;
        public LineDetails(IBL bl2, BO.Line line)
        {
            InitializeComponent();
            bl = bl2;
            DataContext = line;
            lbStations.ItemsSource = new ObservableCollection<BO.LineStation>(line.Stations);////ex
        }

        
        private void btAddStation_Click(object sender, RoutedEventArgs e)
        {
            new AddLineStation(bl, DataContext as BO.Line).ShowDialog() ;
            refreshStations();
            //?
         
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            new UpdateLine(bl, DataContext as BO.Line).ShowDialog();
            refresh();
            
        }
        private void refresh()
        {
            DataContext = bl.GetLine((DataContext as BO.Line).Id);
        }

        private void btTimes_Click(object sender, RoutedEventArgs e)
        {
            new TimesWindow(bl, DataContext as BO.Line).ShowDialog();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            int code = (((sender as Button).Parent as Grid).DataContext as BO.LineStation).Code;
            int lineId = (DataContext as BO.Line).Id;
            bl.DeleteStationInLine(code, lineId);
            //refresh();
            refreshStations();
        }

        private void refreshStations()
        {
            lbStations.ItemsSource = new ObservableCollection<BO.LineStation>((DataContext as BO.Line).Stations);
        }

        private void btUpdateStation_Click(object sender, RoutedEventArgs e)
        {
            //new UpdateLine(bl, DataContext as BO.Line).ShowDialog();
            //refresh();
            var lineStation = ((sender as Button).Parent as Grid).DataContext as BO.LineStation;
            int lineId = (DataContext as BO.Line).Id;
            new UpdateLineStationWindow(bl, lineStation, lineId).ShowDialog();
            refreshStations();
            //!!!!!!!!!!!!!!!!!
        }
    }
}
