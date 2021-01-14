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
    /// Interaction logic for AddTimeWindow.xaml
    /// </summary>
    public partial class AddTimeWindow : Window
    {
        IBL bl;
        public AddTimeWindow(IBL bl2, BO.Line line)
        {
            InitializeComponent();
            bl = bl2;
            DataContext = line;
        }

        private void tbFinishM_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbStartM_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbStartH_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbFinishH_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbfreuquency_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if(tbStartH.Text==string.Empty)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה");
                return;
            }
            if (tbStartM.Text == string.Empty)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה");
                return;
            }
            if (tbFinishH.Text == string.Empty)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה");
                return;
            }
            if (tbFinishM.Text == string.Empty)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה");
                return;
            }
            if (tbfreuquency.Text == string.Empty)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה");
                return;
            }
            TimeSpan startAt = new TimeSpan(int.Parse(tbStartH.Text), int.Parse(tbStartM.Text), 0);
            TimeSpan finishAt = new TimeSpan(int.Parse(tbFinishH.Text), int.Parse(tbFinishM.Text), 0);
            TimeSpan freq = new TimeSpan(0, int.Parse(tbfreuquency.Text), 0);
            try
            {
                bl.AddLineTrip((DataContext as BO.Line).Id, startAt, finishAt, freq);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }

        }
    }
}
