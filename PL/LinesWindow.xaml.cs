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

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new AddWindowLine(bl).ShowDialog();
                lbLines.ItemsSource = new ObservableCollection<BO.Line>(bl.GetAllLines());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = ((((sender as Button).Parent as Grid).DataContext as BO.Line)).Id;
            try
            {
                bl.DeleteLine(id);
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }
        private void refresh()
        {
            try
            {
                lbLines.ItemsSource = new ObservableCollection<BO.Line>(bl.GetAllLines());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }
        private void lbLines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new LineDetails(bl, (sender as ListBox).SelectedItem as BO.Line).ShowDialog();
            refresh();
        }

        private void lbLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
