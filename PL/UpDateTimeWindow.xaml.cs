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
        public UpDateTimeWindow(IBL bl2,BO.LineTrip lineTrip)
        {
            InitializeComponent();
            bl = bl2;
            DataContext = lineTrip;
            init();
        }

        private void init()
        {
            BO.LineTrip lineTrip= DataContext as BO.LineTrip;
            tbStartH.Text = lineTrip.StartAt.Hours.ToString();
            tbStartM.Text = lineTrip.StartAt.Minutes.ToString();
            tbFinishH.Text = lineTrip.FinishAt.Hours.ToString();
            tbFinishM.Text = lineTrip.FinishAt.Minutes.ToString();
            tbfreuquency.Text = lineTrip.Frequency.TotalMinutes.ToString();
        }
        private void btUpDate_Click(object sender, RoutedEventArgs e)
        {
            if (tbStartH.Text == string.Empty)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה");
                init();
                return;
            }
            if (tbStartM.Text == string.Empty)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה");
                init();
                return;
            }
            if (tbFinishH.Text == string.Empty)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה");
                init();
                return;
            }
            if (tbFinishM.Text == string.Empty)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה");
                init();
                return;
            }
            if (tbfreuquency.Text == string.Empty)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה");
                init();
                return;
            }
            TimeSpan startAt = new TimeSpan(int.Parse(tbStartH.Text), int.Parse(tbStartM.Text), 0);
            TimeSpan finishAt = new TimeSpan(int.Parse(tbFinishH.Text), int.Parse(tbFinishM.Text), 0);
            TimeSpan freq = new TimeSpan(0, int.Parse(tbfreuquency.Text), 0);
            try
            {
                bl.UpdateLineTrip(DataContext as BO.LineTrip, startAt, finishAt, freq);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
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
    }
}
