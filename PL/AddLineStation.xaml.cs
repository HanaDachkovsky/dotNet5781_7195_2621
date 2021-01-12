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
            cbAddedStation.ItemsSource=new ObservableCollection<>
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
