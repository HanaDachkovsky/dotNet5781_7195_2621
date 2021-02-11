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
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        IBL bl;
        public SignUpWindow(IBL bl2)
        {
            InitializeComponent();
            bl = bl2;
            pbManPassword.Visibility = Visibility.Hidden;
            tbMan.Visibility = Visibility.Hidden;
        }

        private void btSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(pbPassword.Password!=pbConfirmPassword.Password)
                {
                    MessageBox.Show("אימות סיסמא שגוי, ודא שהסיסמאות זהות", "שגיאה");
                    return;
                }
                bl.CreateUser(tbUserName.Text, pbPassword.Password, (bool)cbAdmin.IsChecked, pbManPassword.Password);
                Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "שגיאה");
            }
        }

        private void cbAdmin_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked == true)
            {
                pbManPassword.Visibility = Visibility.Visible;
                tbMan.Visibility = Visibility.Visible;
            }
            else
            {
                pbManPassword.Visibility = Visibility.Hidden;
                tbMan.Visibility = Visibility.Hidden;
            }
        }
    }
}
