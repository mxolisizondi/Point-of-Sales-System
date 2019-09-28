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
    /// Interaction logic for ManageSuppliers.xaml
    /// </summary>
    public partial class ManageSuppliers : Page
    {
        DataAcces da = new DataAcces();
        public ManageSuppliers()
        {
            InitializeComponent();
        }

        private void BtnAddSupplier(object sender, RoutedEventArgs e)
        {
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
                mw.MainFrame.Content = new AddSupplier();

        }

        private void BtnEditESupplier(object sender, RoutedEventArgs e)
        {
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
                mw.MainFrame.Content = new EditSupplier();
        }

        private void BtnDeleteSupplier(object sender, RoutedEventArgs e)
        {
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
                mw.MainFrame.Content = new DeleteSupplier();
        }

        private void BtnveiwSupplier(object sender, RoutedEventArgs e)
        {
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
                mw.MainFrame.Content = new VeiwAllSuppliers();

        }
    }
}
