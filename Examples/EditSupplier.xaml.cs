using POSDAL;
using POSModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditSupplier.xaml
    /// </summary>
    public partial class EditSupplier : Page
    {
        DataAcces da = new DataAcces();
        public EditSupplier()
        {
            InitializeComponent();
            txtCompanyName.Visibility = Visibility.Collapsed;
            txtAddress.Visibility = Visibility.Collapsed;
            txtCity.Visibility = Visibility.Collapsed;
            txtRegion.Visibility = Visibility.Collapsed;
            txtPostalCode.Visibility = Visibility.Collapsed;
            txtCountry.Visibility = Visibility.Collapsed;
            txtPhoneNum.Visibility = Visibility.Collapsed;
            txtEmailAddress.Visibility = Visibility.Collapsed;
            btnSaveChanges.Visibility = Visibility.Collapsed;
            txtPhoneNum.MaxLength = 10;
            txtPostalCode.MaxLength = 4;
        }

        private void BtnSaveSupplierDetails(object sender, RoutedEventArgs e)
        {
            Supplier s = new Supplier();
            s.SupplierID = Int32.Parse(txtSuppId.Text);

            s.CompanyName =  txtCompanyName.Text;
            s.Address = txtAddress.Text;
            s.City =  txtCity.Text;
            s.Region =  txtRegion.Text;
            s.PostalCode =  txtPostalCode.Text;
            s.Country =   txtCountry.Text;
            s.Phone = txtPhoneNum.Text;
            s.Email = txtEmailAddress.Text;
            da.EditSuppliers(s);
            MessageBox.Show("Supplier Details Updated");
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
                mw.MainFrame.Content = new ManageSuppliers();
        }

        private void BtnSearchSupplier(object sender, RoutedEventArgs e)
        {
            if(!txtSuppId.Text.Equals(""))
            {
                SqlDataReader data = da.SearchSuppliers(Int32.Parse(txtSuppId.Text));
                try
                {
                    if (data.HasRows)
                    {
                        txtCompanyName.Visibility = Visibility.Visible;
                        txtAddress.Visibility = Visibility.Visible;
                        txtCity.Visibility = Visibility.Visible;
                        txtRegion.Visibility = Visibility.Visible;
                        txtPostalCode.Visibility = Visibility.Visible;
                        txtCountry.Visibility = Visibility.Visible;
                        txtPhoneNum.Visibility = Visibility.Visible;
                        txtEmailAddress.Visibility = Visibility.Visible;
                        btnSaveChanges.Visibility = Visibility.Visible;

                        txtSuppId.Visibility = Visibility.Collapsed;
                        btnSearchSupplier.Visibility = Visibility.Collapsed;

                        data.Read();

                        txtSuppId.Text = data["SupplierID"].ToString();
                        txtCompanyName.Text = data["CompanyName"].ToString();
                        txtAddress.Text = data["Address"].ToString();
                        txtCity.Text = data["City"].ToString();
                        txtRegion.Text = data["Region"].ToString();
                        txtPostalCode.Text = data["PostalCode"].ToString();
                        txtCountry.Text = data["Country"].ToString();
                        txtPhoneNum.Text = data["Phone"].ToString();
                        txtEmailAddress.Text = data["Email"].ToString();
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Supplier not found double check supplier id on veiw all suppliers");
                }
            }
            else
            {
                MessageBox.Show("Please enter supplier id you can get it in veiw all suppliers");
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
