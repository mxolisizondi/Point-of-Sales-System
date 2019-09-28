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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        
        DataAcces da = new DataAcces();
        // MainWindow mn = new MainWindow();
        public Login()
        {
            InitializeComponent();
            txtPass.MaxLength = 8;
        }
       
        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee();
            if (!(txtUserName.Text.Equals("") || txtPass.Password.Equals("")))
            {
                emp.Username = txtUserName.Text;
                emp.Password = txtPass.Password;
                if (da.SignIn(emp) != null)
                {
                    if (da.SignIn(emp).Equals("Cashier"))
                    {
                        MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                        if (mw != null)
                        {                            
                            CashierScreen obj = new CashierScreen();
                            obj.idlog = da.saveLoginEmployee(txtUserName.Text);
                            mw.MainFrame.Content = obj;
                            mw.btnLogout.Visibility = Visibility.Visible;
                        }                          
                    }
                    else if (da.SignIn(emp).Equals("Supervisor"))
                    {
                        MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                        if (mw != null)
                        {
                            Supervisor obj = new Supervisor();
                            obj.idlog = da.saveLoginEmployee(txtUserName.Text);
                            obj.txtUsername = txtUserName.Text;
                            mw.MainFrame.Content = obj;
                            mw.btnLogout.Visibility = Visibility.Visible;
                        }                         
                    }
                    else if(da.SignIn(emp).Equals("You dont have acess you no longer working here"))
                    {
                        MessageBox.Show(""+ (da.SignIn(emp)));
                    }
                    else
                    {
                        MessageBox.Show("Wrong username or password");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong username or password");
                }
            }
            else
            {
                MessageBox.Show("Username/Password cant be empty");
            }

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = "";
            txtPass.Password = "";
        }
    }
}
