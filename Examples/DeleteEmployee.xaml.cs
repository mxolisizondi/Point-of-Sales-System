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
    /// Interaction logic for DeleteEmployee.xaml
    /// </summary>
    public partial class DeleteEmployee : Page
    {
        DataAcces da = new DataAcces();
        public DeleteEmployee()
        {
            InitializeComponent();
        }

        private void BtnDeleteEmployee(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee();

            emp.EmpID = Int32.Parse(txtEmpId.Text);

            string msg = da.DeleteEmployee(emp);
            MessageBox.Show(""+msg);
            if(msg.Equals("Employee Discontinued"))
            {
                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.MainFrame.Content = new ManageEmployees();
            }
            
        }

        private void BtnUnDiscEmployee(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee();

            emp.EmpID = Int32.Parse(txtEmpId.Text);

            string msg = da.UnDiscEmployee(emp);
            MessageBox.Show(""+msg);
            if(msg.Equals("Employee Undiscontinued"))
            {
                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.MainFrame.Content = new ManageEmployees();
            }
           
            
        }

        private void NumberWithNoD(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
