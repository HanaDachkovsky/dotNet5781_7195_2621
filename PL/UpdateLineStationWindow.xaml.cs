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
        public UpdateLineStationWindow(IBL bl2,BO.LineStation lineStation , int lineID)
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
            if(tbHours.Text==string.Empty||tbMinutes.Text==string.Empty)
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
            bl.UpdateTimeOrDistance((DataContext as BO.LineStation).Code, lineId, new TimeSpan(int.Parse(tbHours.Text),int.Parse(tbMinutes.Text),0), double.Parse(tbDistance.Text));
            Close();
        }
    }
}
