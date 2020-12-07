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

namespace dotNet5781_03B_7195_2621
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Random rand = new Random(DateTime.Now.Millisecond);
        public MainWindow()
        {

            InitializeComponent();

            List<Bus> buses = new List<Bus>();
            for(int i=0;i<10;i++)
            {
                Bus addedBus = new Bus();
                bool found = true;
                foreach (Bus item in buses)
                {
                    if(item.VehicleNum==addedBus.VehicleNum)
                    {
                        found = false;
                    }
                }
                if (found == false)
                    i--;
                else
                    buses.Add(addedBus);
            }
            int rand1,rand2;
            bool found = false;
            foreach (Bus item in buses)
            {
                if (item.VehicleNum == rand1.ToString())
                {
                    found = false;
                }
            }
            if (found == false)
                

            buses.Add(new Bus(rand.Next(1000000, 10000000).ToString(), new DateTime(2016, 1, 1), DateTime.Now.AddMonths(-15),200, 200, 1000, STATUS.Care));

        }
    }
}
