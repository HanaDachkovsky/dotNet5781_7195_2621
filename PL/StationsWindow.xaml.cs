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
    /// Interaction logic for StationsWindow.xaml
    /// </summary>
    public partial class StationsWindow : Window
    {
        IBL bl;
        public StationsWindow()
        {
            InitializeComponent();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            bl.DeleteStation((((sender as Button).Parent as Grid).DataContext as BO.Station).Code);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddStationWindow addStationWindow = new AddStationWindow(bl);
        }
    }
}
