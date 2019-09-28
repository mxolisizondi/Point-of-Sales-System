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
    /// Interaction logic for DeleteSupplier.xaml
    /// </summary>
    public partial class DeleteSupplier : Page
    {
        public DeleteSupplier()
        {
            InitializeComponent();
        }

        private void BtnDeleteSupplier(object sender, RoutedEventArgs e)
        {
            DataAcces da = new DataAcces();
            Supplier s = new Supplier();

            s.SupplierID = Int32.Parse(txtSupId.Text);

            string msg = da.DeleteSupplier(s);

            MessageBox.Show(""+msg);

            if(msg.Equals("Supplier Discontinued"))
            {
                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.MainFrame.Content = new ManageSuppliers();
            }
           

        }

        private void BtnUnDiscSupplier(object sender, RoutedEventArgs e)
        {
            DataAcces da = new DataAcces();
            Supplier s = new Supplier();

            s.SupplierID = Int32.Parse(txtSupId.Text);

            string msg = da.UnDiscSupplier(s);

            MessageBox.Show("" + msg);
           
            if (msg.Equals("Supplier Undiscontinued"))
            {
                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.MainFrame.Content = new ManageSuppliers();
            }
        }
    }
}
