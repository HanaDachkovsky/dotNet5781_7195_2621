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
    /// Interaction logic for UserTimesWindow.xaml
    /// </summary>
    public partial class UserTimesWindow : Window
    {
        IBL bl;

        public UserTimesWindow(IBL bl2, BO.Line line)
        {
            try
            {
                InitializeComponent();
                bl = bl2;
                DataContext = line;
                dgTime.ItemsSource = new ObservableCollection<BO.LineTrip>(bl.getLineTrips(line.Id));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

