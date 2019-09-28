using POSDAL;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for VeiwAllSuppliers.xaml
    /// </summary>
    public partial class VeiwAllSuppliers : Page
    {
        DataAcces da = new DataAcces();

        public VeiwAllSuppliers()
        {
            InitializeComponent();

            DataTable dt = da.VeiwAllSuppliers();

            datagridView.ItemsSource = dt.DefaultView;
        }
    }
}
