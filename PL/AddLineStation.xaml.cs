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
    /// Interaction logic for AddLineStation.xaml
    /// </summary>
    public partial class AddLineStation : Window
    {
        IBL bl;
        public AddLineStation(IBL bl2,BO.Line line)
        {
            InitializeComponent();
            bl = bl2;
            DataContext = line;
            cbAddedStation.ItemsSource = new ObservableCollection<BO.Station>(bl.GetAllStations());
            cbStationBefore.ItemsSource = new ObservableCollection<BO.LineStation>(line.Stations);
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if(cbAddedStation.SelectedItem==null)
            {
                MessageBox.Show("יש להכניס תחנה להוספה", "שגיאה");
                return;
            }
            if (chIsFirst.IsChecked==false&&cbStationBefore.SelectedItem==null)
            {
                MessageBox.Show("יש להכניס תחנה קודמת", "שגיאה");
                return;
            }
            int code = (cbAddedStation.SelectedItem as BO.Station).Code;
            int lineId = (DataContext as BO.Line).Id;
            int statBefore;
            if (chIsFirst.IsChecked == true)
                statBefore = 0;
            else
                statBefore = (cbStationBefore.SelectedItem as BO.LineStation).Code;
            try
            {
                bl.AddStationToLine(code, lineId, statBefore);
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
            
        }

        private void chIsFirst_Click(object sender, RoutedEventArgs e)
        {
            if((sender as CheckBox).IsChecked==true)
            {
                cbStationBefore.Visibility = Visibility.Hidden;
                textStatBef.Visibility = Visibility.Hidden;
            }
            else
            {
                cbStationBefore.Visibility = Visibility.Visible;
                textStatBef.Visibility = Visibility.Visible;
            }
        }
    }
}
