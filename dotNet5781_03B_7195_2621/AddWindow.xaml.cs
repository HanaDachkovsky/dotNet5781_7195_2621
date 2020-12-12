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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace dotNet5781_03B_7195_2621
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public ObservableCollection<Bus> ExtraData { get; set; }
        public AddWindow(ObservableCollection<Bus> _ExtraData)
        {
            InitializeComponent();
            ExtraData = _ExtraData;

        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {

            if (tbNumber.Text.Length < 7 || tbNumber.Text.Length > 8)
            {
                MessageBox.Show("Bus number is less than 7 digits or more than 8 digits", "ERROR");
                return;
            }
             if (dpStart.SelectedDate == null || dpCare.SelectedDate == null)
            {
                MessageBox.Show("You didn't select the start date or the last care date ", "ERROR");
                return;
            }
             if (dpStart.SelectedDate > dpCare.SelectedDate)
            {
                MessageBox.Show("The date of the last care is earlier than the start date", "ERROR");
                return;
            }
            if (tbKm.Text == "" || tbKmCare.Text == "")
            {
                MessageBox.Show("You didn't enter Kilometrage ", "ERROR");
                return;
            }
           if (int.Parse(tbKm.Text) < int.Parse(tbKmCare.Text))
            {
                MessageBox.Show("The Kilometrage of the last care is smaller than the Kilometrage ", "ERROR");
                return;
            }
           if((int.Parse(tbRef.Text)>1200))
            {
                MessageBox.Show("The Km after refuilling have to be between 0-1200 ", "ERROR");
                return;
            }
            for(int i=0;i<ExtraData.Count;i++)
            {
                if(ExtraData[i].VehicleNum==tbNumber.Text)
                {
                    MessageBox.Show("This number allredy exists", "ERROR");
                    return;
                }
            }
            if(tbRef.Text=="")
            {
                tbRef.Text = "1200";
            }
            //ExtraData.Add(new Bus(tbNumber.Text, dpStart.SelectedDate, dpCare.SelectedDate, double.Parse(tbKmCare.Text), double.Parse(tbKm.Text), 1200 - double.Parse(tbRef.Text),STATUS.Ready));//check status
            this.Close();

         
        }
    }

}
