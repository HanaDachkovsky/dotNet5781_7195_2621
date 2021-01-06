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
    /// Interaction logic for LinesWindow.xaml
    /// </summary>
    public partial class LinesWindow : Window
    {
        IBL bl;
        public LinesWindow(IBL bl2)
        {
            InitializeComponent();
            bl = bl2;
            lbLines.ItemsSource = new ObservableCollection<BO.Line>(bl.GetAllLines());
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
