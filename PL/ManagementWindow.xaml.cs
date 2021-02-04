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
    /// Interaction logic for ManagementWindow.xaml
    /// </summary>
    public partial class ManagementWindow : Window
    {
        IBL bl;
        public ManagementWindow(IBL bl2)
        {
            InitializeComponent();
            bl = bl2;
        }



        private void btLinesView_Click(object sender, RoutedEventArgs e)
        {
            new LinesWindow(bl).ShowDialog();
        }

        private void btStationsView_Click(object sender, RoutedEventArgs e)
        {
            new StationsWindow(bl).ShowDialog();
        }
    }
}
