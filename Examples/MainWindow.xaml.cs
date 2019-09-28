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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;


//Imports Syncfusion.Pdf
//Imports Syncfusion.Pdf.Graphics
//Imports System
//Imports System.ComponentModel
//Imports System.Drawing
//Imports System.Windows

namespace Examples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnLogout.Visibility = Visibility.Collapsed;
            MainFrame.Content = new Login();          
          
        }
        public static CashierScreen storeCashier { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
                mw.MainFrame.Content = new Login();
                btnLogout.Visibility = Visibility.Collapsed;
                MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            }
        }
    }
}
