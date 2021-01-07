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
    /// Interaction logic for LineDetails.xaml
    /// </summary>
    public partial class LineDetails : Window
    {
        IBL bl;
        public LineDetails(IBL bl2, BO.Line line)
        {
            InitializeComponent();
            bl = bl2;
            DataContext = line;
            lbStations.ItemsSource = new ObservableCollection<BO.LineStation>(line.Stations);
        }

        
        private void btAddStation_Click(object sender, RoutedEventArgs e)
        {
            new AddLineStation(bl, line);
         
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            new UpdateLine(bl, line).Show();
            refresh();
        }
        private void refresh()
        {
            DataContext = bl.GetLine((DataContext as BO.Line).Id);
        }
    }
}
