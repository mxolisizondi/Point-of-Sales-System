using POSDAL;
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
    /// Interaction logic for Supervisor.xaml
    /// </summary>
    public partial class Supervisor : Page
    {
        DataAcces da = new DataAcces();
        public Supervisor()
        {
            InitializeComponent();
        }
        
        public int idlog { get; set; }
        public string txtUsername { get; set;}

        private void BtnAcessAdmin(object sender, RoutedEventArgs e)
        {
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
                mw.MainFrame.Content = new AdminScreen();
        }

        private void BtnAcessSales(object sender, RoutedEventArgs e)
        {
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
                MessageBox.Show("Initial" + idlog);
                CashierScreen obj = new CashierScreen();
                obj.idlog = da.saveLoginEmployee(txtUsername);
                mw.MainFrame.Content = obj;
            }
              
        }
    }
}
