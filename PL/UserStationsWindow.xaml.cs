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
    /// Interaction logic for UsetStationsWindow.xaml
    /// </summary>
    public partial class UserStationsWindow : Window
    {
       
        
            IBL bl;
            public UserStationsWindow(IBL bl2)
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
           
            private void lbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new SimulationWindow(bl,((sender as Button).Parent as Grid).DataContext as BO.Station).ShowDialog();
        }
    }
    
}
