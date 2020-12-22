using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace dotNet5781_03B_7195_2621
{
    /// <summary>
    /// Interaction logic for ShowWindow.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        public Bus ExtraData { get; set; }

        public ShowWindow(Bus _ExtraData)
        {
            InitializeComponent();
            ExtraData = _ExtraData;
            //this.DataContext = ExtraData;
            tbKmofCare.Text = (ExtraData.Kilometrage - ExtraData.KmsLastCare).ToString();
            tbAvailableKm.Text = ExtraData.AvailableKm.ToString();
            tbCareDate.Text = ExtraData.LastCare.Date.ToString();
            tbKm.Text = ExtraData.Kilometrage.ToString();
            tbKmofCare.Text = ExtraData.KmsLastCare.ToString();
            tbNum.Text = ExtraData.VehicleNum;
            tbStart.Text = ExtraData.StartDate.Date.ToString();
            tbStatus.Text = ExtraData.Status.ToString();





        }

        private void btCare_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MainWindow).IsCare = true;
            Close();
        }

        private void btRef_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MainWindow).IsRef = true;
            Close();
        }

       
    }
}
