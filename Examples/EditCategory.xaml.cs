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
    /// Interaction logic for EditCategory.xaml
    /// </summary>
    public partial class EditCategory : Page
    {
        DataAcces da = new DataAcces();
        public EditCategory()
        {
            InitializeComponent();
            txtCatName.Visibility = Visibility.Collapsed;
            txtDescri.Visibility = Visibility.Collapsed;
            btnSaveCategory.Visibility = Visibility.Collapsed;
        }

        private void BtnSearchcategory(object sender, RoutedEventArgs e)
        {
            if(!txtCatId.Text.Equals(""))
            {
                Category c = new Category();
                c.CategoryID = Int32.Parse(txtCatId.Text.ToString());

                SqlDataReader rd = da.selectCategory(c);

                try
                {
                    if (rd.HasRows)
                    {
                        rd.Read();

                        txtCatName.Text = rd["CategoryName"].ToString();
                        txtDescri.Text = rd["Description"].ToString();
                    }
                    txtCatId.Visibility = Visibility.Collapsed;
                    btnSearchCategory.Visibility = Visibility.Collapsed;

                    txtCatName.Visibility = Visibility.Visible;
                    txtDescri.Visibility = Visibility.Visible;
                    btnSaveCategory.Visibility = Visibility.Visible;
                }
                catch(NullReferenceException)
                {
                    MessageBox.Show("Category not found make sure category id is correct");
                }
                
            }
           
        }

        private void BtnSavecategory(object sender, RoutedEventArgs e)
        {
            if (!txtCatId.Text.Equals(""))
            {
                Category c = new Category();
                c.CategoryID = Int32.Parse(txtCatId.Text);
                c.CategoryName = txtCatName.Text;
                c.Description = txtDescri.Text;
                da.EditCategory(c);
                MessageBox.Show("Category Updated");
                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                    mw.MainFrame.Content = new ManageCategories();
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
            Regex regex = new Regex("[^a-zA-Z,]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
