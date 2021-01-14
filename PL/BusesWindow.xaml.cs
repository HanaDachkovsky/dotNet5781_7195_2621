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
    /// Interaction logic for BusesWindow.xaml
    /// </summary>
    public partial class BusesWindow : Window
    {
        IBL bl;
        public BusesWindow(IBL bl2)
        {
            try
            {
                InitializeComponent();
                bl = bl2;
                lbBuses.ItemsSource = new ObservableCollection<BO.Bus>(bl.GetAllBuses());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }


        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btRefuiling_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btCare_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
