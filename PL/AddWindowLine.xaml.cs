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
    /// Interaction logic for AddWindowLine.xaml
    /// </summary>
    public partial class AddWindowLine : Window
    {
        IBL bl;
        public AddWindowLine(IBL bl2)
        {
            InitializeComponent();
            bl = bl2; 
            cbArea.ItemsSource = Enum.GetValues(typeof(BO.Enums.Areas));
            ObservableCollection<BO.Station> stations = new ObservableCollection<BO.Station>(bl.GetAllStations());
            cbFirst.ItemsSource = stations;
            cbLast.ItemsSource = stations;
            
        }

        private void tbNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if (e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home || e.Key == Key.End ||
                e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);


            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                
                
            }

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be route
        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if(tbNumber.Text==string.Empty)
            {
                MessageBox.Show("יש להכניס מספר קו","שגיאה");
                return;
            }
            if(cbArea.SelectedItem==null)//?
            {
                MessageBox.Show("יש לבחור איזור", "שגיאה");
                return;
            }
            if (cbFirst.SelectedItem == null)//?
            {
                MessageBox.Show("יש לבחור תחנה ראשונה", "שגיאה");
                return;
            }
            if (cbLast.SelectedItem == null)//?
            {
                MessageBox.Show("יש לבחור תחנה אחרונה", "שגיאה");
                return;
            }
            bl.AddLine(int.Parse(tbNumber.Text), (BO.Enums.Areas)cbArea.SelectedItem, (BO.Station)cbFirst.SelectedItem, (BO.Station)cbLast.SelectedItem);
            Close();
        }
    }
}
