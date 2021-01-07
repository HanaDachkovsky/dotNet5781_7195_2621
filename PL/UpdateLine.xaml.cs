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
    /// Interaction logic for UpdateLine.xaml
    /// </summary>
    public partial class UpdateLine : Window
    {
        IBL bl;
        public UpdateLine(IBL bl2, BO.Line line)
        {
            InitializeComponent();
            bl = bl2;
            DataContext = line;
            tbCode.Text = (DataContext as BO.Line).Code.ToString();
            cbArea.SelectedItem = (DataContext as BO.Line).Arae;
            cbArea.ItemsSource = Enum.GetValues(typeof(BO.Enums.Areas));
        }

        private void btUpDate_Click(object sender, RoutedEventArgs e)
        {
            
            if (tbCode.Text == string.Empty)
            {
                MessageBox.Show("שדה מספר הקו ריק");
                tbCode.Text = (DataContext as BO.Line).Code.ToString();
                return;
            }
            if (cbArea.SelectedItem==null)
            {
                MessageBox.Show("שדה שם האיזור ריק");
                cbArea.SelectedItem = (DataContext as BO.Line).Arae;
                return;
            }
            bl.UpdateLine((DataContext as BO.Line).Id, int.Parse(tbCode.Text), (BO.Enums.Areas)cbArea.SelectedItem);
            Close();

        }

        private void tbCode_PreviewKeyDown(object sender, KeyEventArgs e)
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
