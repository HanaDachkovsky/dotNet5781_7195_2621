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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        IBL bl;
        public AddStationWindow(IBL bl2)
        {
            bl = bl2;
            InitializeComponent();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if(tbAddress.Text==string.Empty)
            {
                MessageBox.Show("יש להכניס כתובת", "שגיאה");
                return;
            }
            if (tbName.Text == string.Empty)
            {
                MessageBox.Show("יש להכניס שם תחנה", "שגיאה");
                return;
            }
            if (tbCode.Text == string.Empty)
            {
                MessageBox.Show("יש להכניס קוד תחנה", "שגיאה");
                return;
            }
            if (tbLong.Text == string.Empty)
            {
                MessageBox.Show("יש להכניס קו אורך ", "שגיאה");
                return;
            }
            if (tbLat.Text == string.Empty)
            {
                MessageBox.Show("יש להכניס קו רוחב", "שגיאה");
                return;
            }
            try
            {
                bl.AddStation(int.Parse(tbCode.Text), tbName.Text, tbAddress.Text, double.Parse(tbLat.Text), double.Parse(tbLong.Text));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }

        private void tbCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;



            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Return || e.Key == Key.Tab || e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home || e.Key == Key.End ||
                e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
            if (e.Key == Key.Enter || e.Key == Key.Return)
                return;
            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be route
        }       

        private void tbLongLat_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;



            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Return || e.Key == Key.Tab || e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home || e.Key == Key.End ||
                e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //if(c=='.')
            if(c== '¾')
            {
                bool hasPoint = false;
                int length = (sender as TextBox).Text.Length;
                for (int i = 0; i < length; i++)
                {
                    if((sender as TextBox).Text[i]=='.')
                    {
                        hasPoint = true;
                    }
                }
                if (hasPoint == false)
                    return;
            }
            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
            if (e.Key == Key.Enter || e.Key == Key.Return)
                return;
            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be route
        }
    }
}
