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
    /// Interaction logic for StationsWindow.xaml
    /// </summary>
    public partial class StationsWindow : Window
    {
        IBL bl;
        public StationsWindow(IBL bl2)
        {
            try
            {
                InitializeComponent();
                bl = bl2;
                lbStations.ItemsSource = new ObservableCollection<BO.Station>(bl.GetAllStations());
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
                lbStations.ItemsSource = new ObservableCollection<BO.Station>(bl.GetAllStations());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
                bl.DeleteStation((((sender as Button).Parent as Grid).DataContext as BO.Station).Code);
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
            

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                new AddStationWindow(bl).ShowDialog();
                lbStations.ItemsSource = new ObservableCollection<BO.Station>(bl.GetAllStations());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }

        private void lbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var x = (sender as ListBox).SelectedItem;
            new StationDetails(bl, (sender as ListBox).SelectedItem as BO.Station).ShowDialog();
        }
    }
}
