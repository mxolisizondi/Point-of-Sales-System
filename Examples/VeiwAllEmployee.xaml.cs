using POSDAL;
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

namespace Examples
{
    /// <summary>
    /// Interaction logic for VeiwAllEmployee.xaml
    /// </summary>
    public partial class VeiwAllEmployee : Page
    {

        public VeiwAllEmployee()
        {
            InitializeComponent();

            DataAcces da = new DataAcces();

            System.Data.DataTable dt = da.VeiwAllEmployees();
            datagridView.ItemsSource = dt.DefaultView;
        }

        private void DatagridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
