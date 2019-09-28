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
    /// Interaction logic for AddSupplier.xaml
    /// </summary>
    public partial class AddSupplier : Page
    {
        DataAcces da = new DataAcces();
        public AddSupplier()
        {
            InitializeComponent();
            txtPhoneNumber.MaxLength = 10;
            txtPostCode.MaxLength = 4;
        }

        private void BtnAddSupplier(object sender, RoutedEventArgs e)
        {

            if(!(txtsupId.Text.Equals("") || txtCompanyName.Text.Equals("") || txtAddress.Text.Equals("") || txtCity.Text.Equals("") || txtRegion.Text.Equals("") || txtPostCode.Text.Equals("") || txtCountry.Text.Equals("") || txtPhoneNumber.Text.Equals("") || txtEmail.Text.Equals("")))
            {
                Supplier s = new Supplier();

                s.SupplierID = Int32.Parse(txtsupId.Text);
                s.CompanyName = txtCompanyName.Text;
                s.Address = txtAddress.Text;
                s.City = txtCity.Text;
                s.Region = txtRegion.Text;
                s.PostalCode = txtPostCode.Text;
                s.Country = txtCountry.Text;
                s.Phone = txtPhoneNumber.Text;
                s.Email = txtEmail.Text;
                if (da.EmailIsValid(txtEmail.Text))
                {

                    string msg = da.AddSupplier(s);

                    MessageBox.Show("" + msg);
                    if(msg.Equals("New Supplier Added"))
                    {
                        MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                        if (mw != null)
                            mw.MainFrame.Content = new ManageSuppliers();
                    }    
                }
                else
                {
                    MessageBox.Show("Wrong email please enter correct email");
                }
            }
            else
            {
                MessageBox.Show("All textbox must be full");
            }
           
            //is knowlegde obtained through observetion  critical tested and expirinmented
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LetterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void notSign(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^^0-9 a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }

       


    }
}
