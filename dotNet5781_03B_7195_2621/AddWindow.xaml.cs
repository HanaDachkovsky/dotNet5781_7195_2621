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
            dpCare.DisplayDateEnd = DateTime.Now;
            dpStart.DisplayDateEnd = DateTime.Now;

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
            if (tbRef.Text == "")
            {
                tbRef.Text = "1200";
            }
            if ((int.Parse(tbRef.Text) > 1200))
            {
                MessageBox.Show("The Km after refuilling have to be between 0-1200 ", "ERROR");
                return;
            }
            if ((tbNumber.Text.Length == 7 && ((DateTime)dpStart.SelectedDate).Year >= 2018) || (tbNumber.Text.Length == 8 && ((DateTime)dpStart.SelectedDate).Year < 2018))
            {

                MessageBox.Show("The bus number is not suitable to start date ", "ERROR");
                return;
            }
            for (int i = 0; i < ExtraData.Count; i++)
            {
                if (ExtraData[i].VehicleNum == tbNumber.Text)
                {
                    MessageBox.Show("This number allredy exists", "ERROR");
                    return;
                }
            }
            if (dpStart.SelectedDate != null)
            {

                ExtraData.Add(new Bus(tbNumber.Text, (DateTime)dpStart.SelectedDate, (DateTime)dpCare.SelectedDate, double.Parse(tbKmCare.Text), double.Parse(tbKm.Text), 1200 - double.Parse(tbRef.Text), STATUS.Ready));//check status
                MessageBox.Show("The bus was added successfully");
                this.Close();
            }

        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

           

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Return || e.Key == Key.Tab||e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home || e.Key == Key.End ||
                e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right )
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbRef_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbKmCare_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbKm_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
