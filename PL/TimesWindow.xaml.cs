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


        private void btAddTime_Click(object sender, RoutedEventArgs e)
        {
            new AddTimeWindow(bl, DataContext as BO.Line).ShowDialog();
            refresh();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = ((sender as Button).DataContext as BO.LineTrip).Id;
                bl.DeleteLineTrip(id);
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }

        private void refresh()
        {
            try
            {
                dgTime.ItemsSource = new ObservableCollection<BO.LineTrip>(bl.getLineTrips((DataContext as BO.Line).Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }

        private void btUpDate_Click(object sender, RoutedEventArgs e)
        {
            new UpDateTimeWindow(bl, (sender as Button).DataContext as BO.LineTrip).ShowDialog();
            refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
