using POSDAL;
using POSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for DeletePage.xaml
    /// </summary>
    public partial class DeletePage : Page
    {
        DataAcces da = new DataAcces();
        public DeletePage()
        {
            InitializeComponent();
        }

        private void BtnDeleteProduct(object sender, RoutedEventArgs e)
        {
            Product p = new Product();
            p.ProductID = long.Parse(txtProdId.Text);

            string msg = da.DeleteProduct(p);
            MessageBox.Show(""+msg);
            if(msg.Equals("Product Discontinued"))
            {
                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.MainFrame.Content = new ManageProducts();
            }
           

        }

        private void BtnUnDiscP(object sender, RoutedEventArgs e)
        {
            Product p = new Product();
            p.ProductID = long.Parse(txtProdId.Text);
            string msg = da.UnDiscProducs(p);
            MessageBox.Show(""+msg);
            if(msg.Equals("Product Undiscontinued"))
            {
                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.MainFrame.Content = new ManageProducts();
            }
            
        }

        private void NumberWithNoD(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
