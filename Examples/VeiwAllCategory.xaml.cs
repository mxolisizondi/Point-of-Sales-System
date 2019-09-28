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
    /// Interaction logic for VeiwAllCategory.xaml
    /// </summary>
    public partial class VeiwAllCategory : Page
    {
        public VeiwAllCategory()
        {
            InitializeComponent();

            DataAcces da = new DataAcces();

            DataTable dt = da.VewAllCategory();
            datagridView.ItemsSource = dt.DefaultView;
        }
    }
}
