using POSDAL;
using POSModel;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;
using System.Text.RegularExpressions;


namespace Examples
{
    public partial class CashierScreen : Page
    {
        public int idlog { get; set; }

        DataAcces da = new DataAcces();
        public CashierScreen()
        {
            InitializeComponent();
           
        }

        DataRowView row_selected = null;
        int currentRowIndex;
        bool BuyFinished = false;
        private void BtnRemoveItem(object sender, RoutedEventArgs e)
        {
            // Window.GetWindow(this);
            MainWindow.storeCashier = this;
            
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mw.MainFrame.Content = new Void();
            
            if (row_selected != null)
                row_selected.Delete();
            else
                MessageBox.Show("Click Item you want to remove");
        }

        private void BtnPurchase(object sender, RoutedEventArgs e)
        {
            if(!txtTotVatInc.Text.Equals(""))
            {
                if(!txtAmout.Text.Equals(""))
                {
                    if (Double.Parse(txtTotVatInc.Text) < Double.Parse(txtAmout.Text))
                    {
                        txtChange.Text = da.Buy(Double.Parse(txtTotVatInc.Text), Double.Parse(txtAmout.Text)).ToString();
                        txtChange.Text = (Math.Round(Double.Parse(txtChange.Text),2)).ToString();
                        BuyFinished = true;
                        da.DecreaseUnitInStock();
                        da.SaveInvoice(idlog);
                    }
                    else
                    {
                        MessageBox.Show("Insuficient amount please add");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter amount rendered");
                }
                
            }
            else
            {
                MessageBox.Show("Please scan items");
            }

        }

        private void BtnScanProductID(object sender, RoutedEventArgs e)
        {
            if (!txtProdID.Text.Equals(""))
            {
                DataTable dt = da.SelectProduct(long.Parse(txtProdID.Text));
                try
                {
                    datagridVeiw.ItemsSource = dt.DefaultView;
                    txtTotExclVac.Text = da.totExcl().ToString();
                    txtVatAmoutn.Text = da.VatAmount().ToString();
                    txtTotVatInc.Text = (Double.Parse(txtTotExclVac.Text) + Double.Parse(txtVatAmoutn.Text)).ToString();
                }
                catch(NullReferenceException)
                {
                    MessageBox.Show(""+da.Message);
                }
                
            }
            else
            {
                MessageBox.Show("Scan item barcode can't be empty");
            }
           
        }

        private void Selected_Change(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            row_selected = gd.SelectedItem as DataRowView;
             currentRowIndex = datagridVeiw.Items.IndexOf(datagridVeiw.CurrentItem);
            // da.DescreaseItem(row_selected,currentRowIndex);
            // MessageBox.Show("" + currentRowIndex);
        }
       
        private void BtnDescrease(object sender, RoutedEventArgs e)
        {
            DataTable dt = da.table();
            try
            {
                if(Int32.Parse(dt.Rows[currentRowIndex]["Qty"].ToString()) > 1)
                {
                    dt.Rows[currentRowIndex]["Qty"] = (int)(row_selected["Qty"]) - 1;
                }
                else
                {
                    MessageBox.Show("You cant decrease item instead click remove item");
                }
               
            }
            catch(IndexOutOfRangeException)
            {
                MessageBox.Show("Select the Item you want to decrease");
            }
               
           
        }

        private void BtnPrintSlip(object sender, RoutedEventArgs e)
        { 
            if(BuyFinished)
            {
                da.CreateDocument(txtTotVatInc.Text, txtAmout.Text, txtChange.Text);
                MessageBox.Show("Slip Printed");
            }
            else
            {
                MessageBox.Show("First finish buying process");
            }
            

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumVal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
