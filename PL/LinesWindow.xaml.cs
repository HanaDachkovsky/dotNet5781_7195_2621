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
            new AddWindowLine(bl).ShowDialog();
            lbLines.ItemsSource = new ObservableCollection<BO.Line>(bl.GetAllLines());
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = ((((sender as Button).Parent as Grid).DataContext as BO.Line)).Id;///בעיית מחיקה
            bl.DeleteLine(id);
            refresh();
        }
        private void refresh()
        {
            lbLines.ItemsSource = new ObservableCollection<BO.Line>(bl.GetAllLines());
        }
        private void lbLines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new LineDetails(bl, (sender as ListBox).SelectedItem as BO.Line).ShowDialog();
        }

        private void lbLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
