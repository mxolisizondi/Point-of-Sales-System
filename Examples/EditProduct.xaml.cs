using POSDAL;
using POSModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Page
    {
        DataAcces da = new DataAcces();
        public EditProduct()
        {
           
            InitializeComponent();

            txtReorder.Visibility = Visibility.Collapsed;
            //txtDisc.Visibility = Visibility.Collapsed;
            txtProd.Visibility = Visibility.Collapsed;
            txtSupId.Visibility = Visibility.Collapsed;
            txtCategId.Visibility = Visibility.Collapsed;
            txtQtyPerUnit.Visibility = Visibility.Collapsed;
            txtUnitPrice.Visibility = Visibility.Collapsed;
            txtUnitInStock.Visibility = Visibility.Collapsed;
            txtReorder.Visibility = Visibility.Collapsed;
            btnSaveChanges.Visibility = Visibility.Collapsed;
        }

        private void BtnSaveChanges(object sender, RoutedEventArgs e)
        {
            Product p = new Product();

            if (!(txtProId.Text.Equals("") || txtCategId.Text.Equals("") || txtProd.Text.Equals("") || txtSupId.Text.Equals("") || txtQtyPerUnit.Text.Equals("") || txtUnitPrice.Text.Equals("") || txtUnitInStock.Text.Equals("") || txtReorder.Text.Equals("")))
            {
                p.ProductID = long.Parse(txtProId.Text);
                p.ProductName = txtProd.Text;
                p.SupplierID = Int32.Parse(txtSupId.Text);
                p.CategoryID = Int32.Parse(txtCategId.Text);
                p.QtyPerUnit = txtQtyPerUnit.Text;
                p.UnitPrice = Double.Parse(txtUnitPrice.Text);
                p.UnitInStock = Int32.Parse(txtUnitInStock.Text);
                p.ReOderLevel = Int32.Parse(txtReorder.Text);
                //p.Discontinued = txtDisc.Text;

                string msg = da.UpdateProduct(p);

                MessageBox.Show(""+msg);

                if(msg.Equals("Product updated"))
                {
                    MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    if (mw != null)
                        mw.MainFrame.Content = new ManageProducts();
                }
               

            }
        }

        private void BtnSearchProduct(object sender, RoutedEventArgs e)
        {
            if(!txtProId.Text.Equals(""))
            {
                SqlDataReader data = da.SearchProduct(long.Parse(txtProId.Text));

                try
                {
                    if (data.HasRows)
                    {
                        txtReorder.Visibility = Visibility.Visible;
                        //txtDisc.Visibility = Visibility.Visible;
                        txtProd.Visibility = Visibility.Visible;
                        txtSupId.Visibility = Visibility.Visible;
                        txtCategId.Visibility = Visibility.Visible;
                        txtQtyPerUnit.Visibility = Visibility.Visible;
                        txtUnitPrice.Visibility = Visibility.Visible;
                        txtUnitInStock.Visibility = Visibility.Visible;
                        btnSaveChanges.Visibility = Visibility.Visible;
                        txtReorder.Visibility = Visibility.Visible;
                        data.Read();
                        //txtDisc.Text = data["Discontinued"].ToString();
                        txtProd.Text = data["ProductName"].ToString();
                        txtSupId.Text = data["SupplierID"].ToString();
                        txtCategId.Text = data["CategoryID"].ToString();
                        txtQtyPerUnit.Text = data["QuantintyPerUnit"].ToString();
                        txtUnitPrice.Text = data["Unitprice"].ToString();
                        txtUnitInStock.Text = data["UnitsInStock"].ToString();
                        txtReorder.Text = data["ReorderLevel"].ToString();
                        txtProId.Visibility = Visibility.Collapsed;
                        btnSearchProduct.Visibility = Visibility.Collapsed;
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Wrong Product ID");
                }
            }
            else
            {
                MessageBox.Show("Please enter product id");
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
