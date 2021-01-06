using BLAPI;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpDateStation.xaml
    /// </summary>
    public partial class UpDateStation : Window
    {
        IBL bl;
        public UpDateStation(IBL bl2, BO.Station station)
        {
            InitializeComponent();
            bl = bl2;
            DataContext = station;
            tbAddress.Text = (DataContext as BO.Station).Address;
            tbName.Text = (DataContext as BO.Station).Name;
        }
        private void btUpDate_Click(object sender, RoutedEventArgs e)
        {
            if(tbAddress.Text==string.Empty)
            {
                MessageBox.Show("שדה הכתובת ריק");
                tbAddress.Text = (DataContext as BO.Station).Address;
                return;
            }
            if (tbName.Text == string.Empty)
            {
                MessageBox.Show("שדה שם התחנה ריק");
                tbName.Text = (DataContext as BO.Station).Name;
                return;
            }
            bl.UpdateStation((DataContext as BO.Station).Code, tbName.Text, tbAddress.Text);
            Close();
        }
    }
}
