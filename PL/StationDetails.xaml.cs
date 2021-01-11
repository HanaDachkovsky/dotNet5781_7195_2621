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
    /// Interaction logic for StationDetails.xaml
    /// </summary>
    public partial class StationDetails : Window
    {
        IBL bl;
        public StationDetails(IBL bl2, BO.Station station )
        {
            InitializeComponent();
            DataContext = station;
            bl = bl2;
            lbLines.ItemsSource = new ObservableCollection<BO.StationLine>(station.Lines);//ex     
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            new UpDateStation(bl, DataContext as BO.Station).ShowDialog();
            DataContext = bl.getStation((DataContext as BO.Station).Code);
        }

        private void lbLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
