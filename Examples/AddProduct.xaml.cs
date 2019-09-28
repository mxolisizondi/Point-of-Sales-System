using POSDAL;
using POSModel;
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
using System.Text.RegularExpressions;

namespace Examples
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Page
    {
        DataAcces da = new DataAcces();
        public AddProduct()
        {
            InitializeComponent();
        }

        private void BtnAddProduct(object sender, RoutedEventArgs e)
        {
            if(!(txtProId.Text.Equals("") || txtCatId.Text.Equals("") || txtProName.Text.Equals("") || txtSuppId.Text.Equals("") || txtQuaPerUn.Text.Equals("") || txtUnitPrice.Text.Equals("") || txtUnitInStock.Text.Equals("") || txtReOder.Text.Equals("")))
            {
                Product p = new Product();

                p.ProductID = long.Parse(txtProId.Text);
                p.ProductName = txtProName.Text;
                p.SupplierID = Int32.Parse(txtSuppId.Text);
                p.CategoryID = Int32.Parse(txtCatId.Text);
                p.QtyPerUnit = txtQuaPerUn.Text;
                p.UnitPrice = Double.Parse(txtUnitPrice.Text);
                p.UnitInStock = Int32.Parse(txtUnitInStock.Text);
                p.ReOderLevel = Int32.Parse(txtReOder.Text);
                int sel = comDisc.SelectedIndex;
                if (sel >= 0)
                {
                    p.Discontinued = sel.ToString();
                }
                string msg = da.addProduct(p);
                MessageBox.Show("" + msg);
                if(msg.Equals("Product Added"))
                {
                    MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    if (mw != null)
                        mw.MainFrame.Content = new ManageProducts();
                }
               
            }
            else
            {
                MessageBox.Show("All texbox are required");
            }
           
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.,]+");
           
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumberWithNoD(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumandLetters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9 a-zA-Z]+");

            e.Handled = regex.IsMatch(e.Text);
        }

        private void LetterVal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
