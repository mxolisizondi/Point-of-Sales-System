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

namespace Examples
{
    /// <summary>
    /// Interaction logic for Void.xaml
    /// </summary>
    public partial class Void : Page
    {
        DataAcces da = new DataAcces();
        public Void()
        {
            InitializeComponent();
        }
        public ContentControl mw { get; set; }

        private void BtnVoid(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee();
            emp.Username = txtVoidEmpId.Text;
            if(da.Void(emp,txtPassVoid.Password))
            {
               Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().MainFrame.Content = MainWindow.storeCashier;
            }
        }
    }
}
