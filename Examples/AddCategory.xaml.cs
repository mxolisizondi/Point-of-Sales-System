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
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Page
    {
        DataAcces da = new DataAcces();
        public AddCategory()
        {
            InitializeComponent();
        }

        private void BtnAddcategory(object sender, RoutedEventArgs e)
        {
            
            if(!(txtCId.Text.Equals("") || txtCName.Text.Equals("") || txtDescr.Text.Equals("")))
            {
                Category c = new Category();

                c.CategoryID = Int32.Parse(txtCId.Text);
                c.CategoryName = txtCName.Text;
                c.Description = txtDescr.Text;

                string msg = da.AddCategory(c);

                MessageBox.Show("" + msg);

                if (msg.Equals("Category Added"))
                {
                    MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    if (mw != null)
                        mw.MainFrame.Content = new ManageCategories();
                }
            }
            else
            {
                MessageBox.Show("Make sure all textbox are full");
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
