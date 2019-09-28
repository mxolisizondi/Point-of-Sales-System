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
    /// Interaction logic for EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Page
    {
        DataAcces da = new DataAcces();
        public EditEmployee()
        {
            InitializeComponent();
            txtLast.Visibility = Visibility.Collapsed;
            txtFName.Visibility = Visibility.Collapsed;
            txtTitle.Visibility = Visibility.Collapsed;
            txtIDNo.Visibility = Visibility.Collapsed;
            txtAddress.Visibility = Visibility.Collapsed;
            txtReportTo.Visibility = Visibility.Collapsed;
            txtEmail.Visibility = Visibility.Collapsed;
            txtUserName.Visibility = Visibility.Collapsed;
            txtPass.Visibility = Visibility.Collapsed;
            txtEmPositionId.Visibility = Visibility.Collapsed;
            txtIDNo.IsEnabled = false;
            btnSaveChanges.Visibility = Visibility.Collapsed;
            txtPass.MaxLength = 8;
        }

        private void BtnSaveUpdateEmp(object sender, RoutedEventArgs e)
        {
            if(!(txtEmpId.Text.Equals("") || txtLast.Text.Equals("") || txtFName.Text.Equals("") || txtTitle.Text.Equals("") || txtIDNo.Text.Equals("") || txtAddress.Text.Equals("") || txtReportTo.Text.Equals("") || txtEmail.Text.Equals("") || txtUserName.Text.Equals("") || txtPass.Password.Equals("") || txtEmPositionId.Text.Equals("")))
            {
                Employee emp = new Employee();
                emp.EmpID = Int32.Parse(txtEmpId.Text);
                emp.LastName = txtLast.Text;
                emp.FirstName = txtFName.Text;
                emp.Title = txtTitle.Text;
                emp.IDNo = txtIDNo.Text;
                emp.Address = txtAddress.Text;
                emp.ReportTo = Int32.Parse(txtReportTo.Text);
                emp.Email = txtEmail.Text;
                emp.Username = txtUserName.Text;
                emp.Password = txtPass.Password;
                emp.EmployeePositionID = Int32.Parse(txtEmPositionId.Text);

                da.EditEmployees(emp);

                MessageBox.Show("Employee Details Updated");

                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.MainFrame.Content = new ManageEmployees();
            }
           
        }

        private void BtnSearchEmployee(object sender, RoutedEventArgs e)
        {

            if (!(txtEmpId.Text.Equals("")))
            {
                SqlDataReader data = da.SearchEmployees(Int32.Parse(txtEmpId.Text));

                try
                {
                    if (data.HasRows)
                    {

                        txtLast.Visibility = Visibility.Visible;
                        txtFName.Visibility = Visibility.Visible;
                        txtTitle.Visibility = Visibility.Visible;
                        txtIDNo.Visibility = Visibility.Visible;
                        txtAddress.Visibility = Visibility.Visible;
                        txtReportTo.Visibility = Visibility.Visible;
                        txtEmail.Visibility = Visibility.Visible;
                        txtUserName.Visibility = Visibility.Visible;
                        txtPass.Visibility = Visibility.Visible;
                        txtEmPositionId.Visibility = Visibility.Visible;
                        btnSaveChanges.Visibility = Visibility.Visible;

                        txtEmpId.Visibility = Visibility.Collapsed;
                        btnSearchEmp.Visibility = Visibility.Collapsed;

                        data.Read();

                        txtLast.Text = data["LastName"].ToString();
                        txtFName.Text = data["FirstName"].ToString();
                        txtTitle.Text = data["Title"].ToString();
                        txtIDNo.Text = data["IDNO"].ToString();
                        txtAddress.Text = data["Address"].ToString();
                        txtReportTo.Text = data["ReportTo"].ToString();
                        txtEmail.Text = data["Email"].ToString();
                        txtUserName.Text = data["Username"].ToString();
                        txtPass.Password = data["Password"].ToString();
                        txtEmPositionId.Text = data["PositionID"].ToString();
                    }
                }
                catch(NullReferenceException)
                {
                    MessageBox.Show("Employee not fount make sure employee id is correct verify it in view all employees");
                }
                
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
