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
    }
}
