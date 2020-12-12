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
    /// Interaction logic for DriveWindow.xaml
    /// </summary>
    public partial class DriveWindow : Window
    {
        public ObservableCollection<Bus> ExtraData { get; set; }
        //BackgroundWorker driveWorker;
        public DriveWindow(ObservableCollection<Bus> _ExtraData,BackgroundWorker driveWorker)
        {
            InitializeComponent();
            //ExtraData = _ExtraData;
            //driveWorker = new BackgroundWorker();
        //    driveWorker.DoWork += DriveWorker_DoWork;
        //    driveWorker.ProgressChanged += DriveWorker_ProgressChanged;
        //    driveWorker.WorkerReportsProgress = true;
        //
        }

        //private void DriveWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void DriveWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if (e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home || e.Key == Key.End ||
                e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
            if(e.Key ==Key.Enter)
            {
                
            }

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be route

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
