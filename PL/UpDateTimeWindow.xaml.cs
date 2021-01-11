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
    /// Interaction logic for UpDateTimeWindow.xaml
    /// </summary>
    public partial class UpDateTimeWindow : Window
    {
        IBL bl;
        public UpDateTimeWindow(IBL bl2,BO.LineTrip linetrip)
        {
            InitializeComponent();
            bl = bl2;
            DataContext = linetrip;

        }

    

        private void btUpDate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbStartH_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbStartM_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbFinishH_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbFinishM_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbfreuquency_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
