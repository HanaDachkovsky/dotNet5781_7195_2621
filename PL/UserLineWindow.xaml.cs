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
    /// Interaction logic for UserLineWindow.xaml
    /// </summary>
    public partial class UserLineWindow : Window
    {
        IBL bl;
        public UserLineWindow(IBL bl2)
        {
            try
            {
                InitializeComponent();
                bl = bl2;
                lbLines.ItemsSource = new ObservableCollection<BO.Line>(bl.GetAllLines());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }
        private void btTime_Click(object sender, RoutedEventArgs e)
        {
            new UserTimesWindow(bl, ((sender as Button).Parent as Grid).DataContext as BO.Line).ShowDialog();
        }
       
        private void lbLines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new UserLineDetails(bl, (sender as ListBox).SelectedItem as BO.Line).ShowDialog();
            
        }

        private void lbLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

