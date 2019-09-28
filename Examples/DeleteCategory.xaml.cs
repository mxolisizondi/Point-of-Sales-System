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
    /// Interaction logic for DeleteCategory.xaml
    /// </summary>
    public partial class DeleteCategory : Page
    {
        DataAcces da = new DataAcces();
        public DeleteCategory()
        {
            InitializeComponent();
        }

        private void BtnDeleteCategory(object sender, RoutedEventArgs e)
        {
            if(!txtCatId.Text.Equals(""))
            {
                Category c = new Category();
                c.CategoryID = Int32.Parse(txtCatId.Text);
                string msg = da.DeleteCategory(c);
                MessageBox.Show("" + msg);

                if (msg.Equals("Category Discontinued Successful"))
                {
                    MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    if (mw != null)
                        mw.MainFrame.Content = new ManageCategories();
                }
            }
            else
            {
                MessageBox.Show("Category Id can't be empty");
            }
            
            
        }

        private void BtnUnDisc(object sender, RoutedEventArgs e)
        {
            Category c = new Category();
            c.CategoryID = Int32.Parse(txtCatId.Text);
            string msg = da.DeleteCategory(c);
            MessageBox.Show("" + msg);

            if (msg.Equals("Category udiscontinued Successful"))
            {
                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.MainFrame.Content = new ManageCategories();
            }
        }

        private void NumberWithNoD(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
