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
    /// Interaction logic for UpdateLineStationWindow.xaml
    /// </summary>
    public partial class UpdateLineStationWindow : Window
    {
        IBL bl;
        int lineId;
        public UpdateLineStationWindow(IBL bl2, BO.LineStation lineStation, int lineID)
        {
            InitializeComponent();
            bl = bl2;
            DataContext = lineStation;
            init();
            lineId = lineID;
        }

        private void init()
        {
            BO.LineStation lineStation = DataContext as BO.LineStation;
            tbHours.Text = lineStation.TimeFromPrevStat.Hours.ToString();
            tbMinutes.Text = lineStation.TimeFromPrevStat.Minutes.ToString();
            tbDistance.Text = lineStation.DistanceFromPrevStat.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbHours.Text == string.Empty || tbMinutes.Text == string.Empty)
            {
                MessageBox.Show("יש להכניס זמן", "שגיאה");
                init();
                return;
            }
            if (tbDistance.Text == string.Empty)
            {
                MessageBox.Show("יש להכניס מרחק", "שגיאה");
                init();
                return;
            }
            try
            {
                bl.UpdateTimeOrDistance((DataContext as BO.LineStation).Code, lineId, new TimeSpan(int.Parse(tbHours.Text), int.Parse(tbMinutes.Text), 0), double.Parse(tbDistance.Text));
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }

        private void tb_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void tbDistance_PreviewKeyDown(object sender, KeyEventArgs e)
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
            if (c == '¾')
            {
                bool hasPoint = false;
                int length = (sender as TextBox).Text.Length;
                for (int i = 0; i < length; i++)
                {
                    if ((sender as TextBox).Text[i] == '.')
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
