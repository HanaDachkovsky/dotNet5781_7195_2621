using BLAPI;
using BO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL();
        public MainWindow()
        {
            InitializeComponent();
            bl.todelete();
            
        }

        private void tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            TextBox text = sender as TextBox;
            if (sender as PasswordBox == null && text == null) return ;
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
            if(Char.IsLetter(c))
            {
                return;
            }

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                try
                {
                    if (bl.IsAdminAndExists(tbUserName.Text, pbPassword.Password))
                    {
                        ManagementWindow management = new ManagementWindow(bl);
                        management.Show();
                        Close();
                    }
                    else
                    {
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה");
                }

            }
            e.Handled = true;
        }

        private void tbUserName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btSignIn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (bl.IsAdminAndExists(tbUserName.Text, pbPassword.Password))
                {
                    ManagementWindow management = new ManagementWindow(bl);
                    management.Show();
                    Close();
                }
                else
                {

                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void btSignUp_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //    //List<BO.StationLine> Lines1 = new List<StationLine>() { new BO.StationLine { Code = 1, Id = 9, NameLastStation = "last", LastStation = 23 } };
        //    //new SimulationWindow(bl, new BO.Station { Code = 1, Address = "Hello", Latitude = 33, Longitude = 31, Name = "Hi", Lines = Lines1 }).Show();
        //    new SimulationWindow(bl, bl.getStation(86)).Show();
        //}
    }
}
