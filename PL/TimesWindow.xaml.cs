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
    /// Interaction logic for TimesWindow.xaml
    /// </summary>
    public partial class TimesWindow : Window
    {
        IBL bl;
        
        public TimesWindow(IBL bl2, BO.Line line)
        {
            InitializeComponent();
            bl = bl2;
            DataContext = line;
            lbTrips.ItemsSource = new ObservableCollection<BO.LineTrip>(bl.getLineTrips(line.Id));
        }

      
        private void btAddTime_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
