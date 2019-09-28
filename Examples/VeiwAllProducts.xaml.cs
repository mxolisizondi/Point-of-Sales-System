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
    /// Interaction logic for VeiwAllProducts.xaml
    /// </summary>
    public partial class VeiwAllProducts : Page
    {
        DataAcces da = new DataAcces();
        public VeiwAllProducts()
        {
            InitializeComponent();

            DataTable dt = da.SelectAllProducts();

            datagridView.ItemsSource = dt.DefaultView;
        }
    }
}
