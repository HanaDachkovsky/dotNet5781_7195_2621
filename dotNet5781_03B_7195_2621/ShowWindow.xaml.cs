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
            this.DataContext = ExtraData;
            //tbNum.Text = ExtraData.VehicleNum;
            tbCareDate.Text = ExtraData.LastCare.ToString();
            tbAvailableKm.Text = ExtraData.AvailableKm.ToString();
            tbKmofCare.Text = ExtraData.KmsLastCare.ToString();
            tbStart.Text = ExtraData.StartDate.ToString();
            tbStatus.Text = ExtraData.Status.ToString();
            tbKm.Text = ExtraData.Kilometrage.ToString();
            //use binding


        }

        private void btCare_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btRef_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
