using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using POSModel;
using System.Text.RegularExpressions;

namespace POSDAL
{
    public class DataAcces
    {
        string Conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
        //string Conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;

        public string SignIn(Employee emp)
        {
            string query = string.Format("Select Password,Discontinued From Employees where Username ='" + emp.Username + "'");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Read();
                if (emp.Password.Equals(dataReader["Password"].ToString()))
                {
                    if(!bool.Parse(dataReader["Discontinued"].ToString()))
                    {
                        return position(emp.Username);
                    }
                    else
                    {
                        return "You dont have acess you no longer working here";
                    }
                    
                }
            }
            else
            {
                return null;
            }
            cmd.Dispose();
            connect.Close();
            connect.Dispose();
            return null;
        }

        public string position(string username)
        {
            string query = string.Format("Select PositionName From EmployeePositions where EmployeePositions.EmployeePositionID = (Select Employees.PositionID from Employees where username = '" + username + "')");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            SqlDataReader dataReader = cmd.ExecuteReader();

            dataReader.Read();

            string Position = dataReader["PositionName"].ToString();
            cmd.Dispose();
            connect.Close();
            connect.Dispose();
            return Position;

        }

        public string Message { get; set; }
        DataTable dt = new DataTable();
        bool first = true;
        int RowPosition = 0;

        public static object ValidEmailRegex { get; private set; }

        public DataTable SelectProduct(long proId)
        {          
            if (first)
            {
                dt.Columns.Add("Product ID", typeof(long));
                dt.Columns.Add("Product Name", typeof(string));
                dt.Columns.Add("Unit Price", typeof(double));
                dt.Columns.Add("Qty", typeof(int));
                dt.Columns.Add("Total", typeof(double));

                first = false;
            }
            string query = string.Format("Select ProductId,ProductName,UnitPrice,Discontinued,UnitsInStock From Products where ProductID =" + proId + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            //cmd.ExecuteNonQuery();

            SqlDataReader dataReader = cmd.ExecuteReader();
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);

            if(dataReader.HasRows)
            {
                dataReader.Read();
                String productName = dataReader["ProductName"].ToString();
                double price = Double.Parse(dataReader["UnitPrice"].ToString());
                DataRow dr = dt.NewRow();
                if(!Boolean.Parse(dataReader["Discontinued"].ToString()))
                {
                    if(Int32.Parse(dataReader["UnitsInStock"].ToString()) > 0)
                    {
                        if (!ProductExists(proId))
                        {
                            dr["Product ID"] = dataReader["ProductId"];
                            dr["Product Name"] = dataReader["ProductName"];
                            dr["Unit Price"] = dataReader["UnitPrice"];
                            dr["Qty"] = 1;
                            double p = Double.Parse((dataReader["UnitPrice"]).ToString());
                            dr["Total"] = p;
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            dt.Rows[RowPosition]["Qty"] = (int)dt.Rows[RowPosition]["Qty"] + 1;
                            double p = Double.Parse((dataReader["UnitPrice"]).ToString());
                            dt.Rows[RowPosition]["Total"] = (p * (int)dt.Rows[RowPosition]["Qty"]);
                        }
                    }
                    else
                    {
                        Message = "We dont have stock";
                        return null;
                    }
                    
                }
                else
                {
                    Message = "Product is Discontinued";
                    return null;
                }
                

                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return dt;
            }
            else
            {
                Message = "Wrong barcode";
                return dt;
            }

        }
        

        public bool ProductExists(long ProductId)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((long)dt.Rows[i]["Product ID"] == ProductId)
                {
                    RowPosition = i;
                    return true;
                }
                    
            }

            return false;
        }

        public void DecreaseUnitInStock()
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string query = string.Format("Update Products Set UnitsInStock = UnitsInStock -"+(int)(dt.Rows[i]["Qty"]) +" where ProductID ="+ (long)dt.Rows[i]["Product ID"]+"");

                SqlConnection connect = new SqlConnection(Conn);
                connect.Open();

                SqlCommand cmd = new SqlCommand(query, connect);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                CheckReOrderLevel((long)dt.Rows[i]["Product ID"]);
            }
           
        }

        public void CheckReOrderLevel(long prodId)
        {
            string query = string.Format("Select ReorderLevel,UnitsInStock From Products where ProductID = " + prodId + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            SqlDataReader dataReader = cmd.ExecuteReader();

            dataReader.Read();
            if (Int32.Parse(dataReader["UnitsInStock"].ToString()) < Int32.Parse(dataReader["ReorderLevel"].ToString()))
            {
                //string send = ConfigurationManager.AppSettings["SendEmail"];
                dataReader.Close();
                query = string.Format("Select Suppliers.Email,Products.ProductName From Suppliers,Products where Suppliers.SupplierID = Products.SupplierID and Products.ProductID =" + prodId + "");
                cmd = new SqlCommand(query, connect);
                dataReader = cmd.ExecuteReader();
                if(dataReader.HasRows)
                {
                    dataReader.Read();
                    MailMessage mail = new MailMessage("mxolisizondi@gmail.com", dataReader["Email"].ToString());
                    mail.Subject = "Decrease in Stock";
                    mail.Body = "Hey\nIt the manager of MUT Tuck Shop\nWe running out of stock of " + dataReader["ProductName"].ToString() + "\nPlease supply us with stock As soon as possible\n\nThanks in advance";
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = "mxolisizondi20@gmail.com",
                        Password = "mxolisizondi20"
                    };
                    client.EnableSsl = true;
                    {
                        client.Send(mail);
                    }
                }
                
            }
            cmd.Dispose();
            connect.Close();
            connect.Dispose();
        }

        public double totExcl()
        {
            double totExcl = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totExcl = totExcl + Double.Parse(dt.Rows[i]["Total"].ToString()); 
            }
            return totExcl;
        }

        public double VatAmount()
        {
            double totvat = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totvat = totvat + ((Double.Parse(dt.Rows[i]["Total"].ToString()))*0.15);
            }
            return totvat;
        }

        public double Buy(double total,double amount)
        {
            return amount - total;
        }

        public void DeleteRow(DataRow selected)
        {
            
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["Product ID"] == selected)
                    dr.Delete();
            }
            dt.AcceptChanges();

        }

        public DataTable table()
        {
            return dt;
        }

        public void DescreaseItem(DataRowView row,int currRow)
        {
            //DataTable t = new DataTable();
            //t = dt;
          // return datagridVeiw.Items.IndexOf(datagridVeiw.CurrentItem);
        }

        public void CreateDocument(string txtVinc,string am,string txtChng)
        {
            Document doc = new Document(iTextSharp.text.PageSize.A3,10,10,42,35);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            PdfWriter write =  PdfWriter.GetInstance(doc, new FileStream(path+"\\Slip.pdf", FileMode.Create));
            var time = DateTime.Now;
            doc.Open();
            string msg = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tP.O.Box 123\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tUmlazi\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tMangosuthu High Way\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t134\n\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tMuT Tuckshop\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tTax Invoice\n\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tEntry Sale" +
                "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" + time.ToString("yyyy/MM/dd hh'h':mm")+ "\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tUmlazi st 123\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t....................................................................................................\n\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tItem/Description\t\t\t\t\t\t\t\t\tQty\t\t\t\t\t\t\t\t\t\t\tUnit Price\t\t\t\t\t\t\t\t\tTotal";
            string items = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";

            //MessageBox.Show("" + dt);
            
            for(int i = 0;i < dt.Rows.Count;i++)
            {
                items = items+dt.Rows[i]["Product ID"].ToString()+""+dt.Rows[i]["Product Name"].ToString() +
                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" + dt.Rows[i]["Qty"].ToString() + "\t\t\t\t\t\t\t\t\t\t\tR" + dt.Rows[i]["Unit Price"].ToString() 
                    + "\t\t\t\t\t\t\t\t\t\t\tR " + dt.Rows[i]["Total"].ToString() + "\t\t\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";
            }
            Paragraph p = new Paragraph(msg+"\n\n"+items+ "\n\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t....................................................................................................\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tTotal excl tax.............................................. R" + totExcl()+ "\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tTax at 15% ..............................................R" + VatAmount()+ "\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" +
                "Total inc tax.............................................. R" + txtVinc+ "\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tAmount Paid.............................................. R" + am+ "\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tChange..............................................R" + txtChng);
       
            doc.Add(p);    

            doc.Close();
        }

        public string addProduct(Product pro)
        {
            string query = string.Format("Insert Into Products values("+pro.ProductID+",'"+pro.ProductName+"',"+pro.SupplierID+","+pro.CategoryID+",'"+pro.QtyPerUnit+"',"+pro.UnitPrice+","+pro.UnitInStock+","+pro.ReOderLevel+","+pro.Discontinued+")");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            if(CheckCategory(pro.CategoryID))
            {
                if(CheckSupplierID(pro.SupplierID))
                {
                    if(!CheckProduct(pro.ProductID))
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        connect.Close();
                        connect.Dispose();
                        return "Product Added";
                    }
                    else
                    {
                        cmd.Dispose();
                        connect.Close();
                        connect.Dispose();
                        return "Product Already exits";
                    }  
                }
                else
                {
                    cmd.Dispose();
                    connect.Close();
                    connect.Dispose();
                    return "Supplier does not exits";
                }
            }
            else
            {
                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return "Category does not exits";
            }  
        }

        public bool CheckProduct(long pID)
        {
            string query = string.Format("Select * From Products where ProductID = " + pID);

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable("Products");

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            try
            {
                dt.Rows[0]["ProductName"].ToString();
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public bool CheckCategory(int catId)
        {
            string query = string.Format("Select * From Categories where categoryID = "+catId);

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable("Categories");

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            try
            {
                dt.Rows[0]["CategoryName"].ToString();
                return true;
            }
            catch(IndexOutOfRangeException)
            {
                return false;
            }
           
        }

        public bool CheckSupplierID(int supId)
        {
            string query = string.Format("Select * From Suppliers where SupplierID = "+supId);

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable("Suppliers");

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            try
            {
                dt.Rows[0]["CompanyName"].ToString();
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

        }

        public bool Exits(string tb,int id)
        {
            string query = string.Format("Select * from "+tb+" where ");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if(dataReader.HasRows)
            {
                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return true;
            }
            else
            {
                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return false;
            }
            
        }

        public SqlDataReader SearchProduct(long id)
        {
            string query = string.Format("Select * From Products where ProductID =" + id + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {

                return dataReader;
            }
            else
            {
                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return null;
            }
        }

        public string UpdateProduct(Product p)
        {
            string query = string.Format("Update Products set ProductName ='"+p.ProductName+"',SupplierID ="+p.SupplierID+",CategoryID="+p.CategoryID+",QuantintyPerUnit='"+p.QtyPerUnit+"',Unitprice="+p.UnitPrice+",UnitsInStock="+p.UnitInStock+",ReorderLevel="+p.ReOderLevel+",Discontinued='"+p.Discontinued+"' where ProductID =" + p.ProductID + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            if(CheckSupplierID(p.SupplierID))
            {
                if(CheckCategory(p.CategoryID))
                {
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    connect.Close();
                    connect.Dispose();
                    return "Product updated";
                }
                else
                {
                    return "Category Not Found make sure you put correct category id";
                }
                
            }
            else
            {
                return "Supplier ID Not Found make sure you put correct supplier id";
            }
            
        }

        public string DeleteProduct(Product p)
        {
            string query = string.Format("Update Products set Discontinued = 1 where ProductID =" + p.ProductID + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);
            if(CheckProduct(p.ProductID))
            {
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                connect.Close();
                connect.Dispose();

                return "Product Discontinued";
            }
            else
            {
                return "Wrong Product ID make sure you enter valid product id";
            }
            
        }

        public string UnDiscProducs(Product p)
        {
            string query = string.Format("Update Products set Discontinued = 0 where ProductID =" + p.ProductID + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            if (CheckProduct(p.ProductID))
            {
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                connect.Close();
                connect.Dispose();

                return "Product Undiscontinued";
            }
            else
            {
                return "Wrong Product ID make sure you enter valid product id";
            }
        }

        public DataTable SelectAllProducts()
        {

            string query = string.Format("Select * From Products ");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable("Products");

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            cmd.Dispose();
            connect.Close();
            connect.Dispose();
            return dt;
        }

        public string AddCategory(Category c)
        {
            
            string query = string.Format("Insert Into Categories values(" + c.CategoryID + ",'" + c.CategoryName + "','" + c.Description + "',"+0+")");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

          if(!CheckCategory(c.CategoryID))
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return "Category Added";
            }
            else
            {
                return "Category ID already exist";
            }
            
        }

        public SqlDataReader selectCategory(Category c)
        {

            string query = string.Format("Select CategoryName,Description From Categories where CategoryID = "+c.CategoryID+"");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            SqlDataReader rd = cmd.ExecuteReader();
            if(rd.HasRows)
            return rd;
            else
            {
                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return null;
            }
        }

        public void EditCategory(Category c)
        {
            string query = string.Format("Update Categories set CategoryName ='" + c.CategoryName + "',Description='" + c.Description + "' where CategoryId ="+c.CategoryID+"");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            connect.Close();
            connect.Dispose();
        }

        public DataTable VewAllCategory()
        {
            string query = string.Format("Select * From Categories ");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable("Categories");

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            cmd.Dispose();
            connect.Close();
            connect.Dispose();
            return dt;
        }

        public string DeleteCategory(Category c)
        {
            string query = string.Format("Update Categories set Discontinued = 1 where CategoryID =" + c.CategoryID + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            if(CheckCategory(c.CategoryID))
            {
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return "Category Discontinued Successful";
            }
            else
            {
                return "Category not found double check your category id on veiw all";
            }
           
        }

        public string UnDiscCategory(Category c)
        {
            string query = string.Format("Update Categories set Discontinued = 0 where CategoryID =" + c.CategoryID + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            if (CheckCategory(c.CategoryID))
            {
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return "Category udiscontinued Successful";
            }
            else
            {
                return "Category not found double check your category id on veiw all";
            }
        }

        public string AddSupplier(Supplier s)
        {
            if(s.Phone.Length == 10)
            {
                string query = string.Format("Insert Into Suppliers values(" + s.SupplierID + ",'" + s.CompanyName + "','" + s.Address + "','" + s.City + "','" + s.Region + "','" + s.PostalCode + "','" + s.Country + "','" + s.Phone + "','" + s.Email + "'," + 0 + ")");

                SqlConnection connect = new SqlConnection(Conn);
                connect.Open();

                SqlCommand cmd = new SqlCommand(query, connect);

                if (!CheckSupplierID(s.SupplierID))
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connect.Close();
                    connect.Dispose();

                    return "New Supplier Added";
                }
                else
                {
                    return "This Supplier exists";
                }
            }
            else
            {
                return "Phone number must contain 10 digits";
            }
           
            
        }

        public DataTable VeiwAllSuppliers()
        {
            string query = string.Format("Select * From Suppliers ");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable("Suppliers");

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            cmd.Dispose();
            connect.Close();
            connect.Dispose();
            return dt;
        }

        public string DeleteSupplier(Supplier s)
        {
            string query = string.Format("Update Suppliers set Discontinued = 1 where SupplierID =" +s.SupplierID+ "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            if(CheckSupplierID(s.SupplierID))
            {
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                connect.Close();
                connect.Dispose();

                return "Supplier Discontinued";
            }
            else
            {
                return "Supplier does not exits confirm supplier id on veiw all suppliers";
            }
            
        }

        public string UnDiscSupplier(Supplier s)
        {
            string query = string.Format("Update Suppliers set Discontinued = 0 where SupplierID =" + s.SupplierID + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            if (CheckSupplierID(s.SupplierID))
            {
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                connect.Close();
                connect.Dispose();

                return "Supplier Undiscontinued";
            }
            else
            {
                return "Supplier does not exits confirm supplier id on veiw all suppliers";
            }

        }

        public SqlDataReader SearchSuppliers(int suppid)
        {
            string query = string.Format("Select * From Suppliers where SupplierID =" + suppid + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {

                return dataReader;
            }
            else
            {
                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return null;
            }
        }

        public void EditSuppliers(Supplier s)
        {
            string query = string.Format("Update Suppliers set CompanyName ='" +s.CompanyName + "'" +
                ",Address='" + s.Address + "',City = '"+s.City+"',Region = '"+s.Region+"',PostalCode = '"+s.PostalCode+"'," +
                "Country = '"+s.Country+"',Phone = '"+s.Phone+"',Email = '"+s.Email+ "' where SupplierID =" + s.SupplierID + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            connect.Close();
            connect.Dispose();
        }

        public DataTable VeiwAllEmployees()
        {
            string query = string.Format("Select * From Employees ");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable("Employees");

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            cmd.Dispose();
            connect.Close();
            connect.Dispose();
            return dt;
        }

        public string DeleteEmployee(Employee emp)
        {
            string query = string.Format("Update Employees set Discontinued = 1 where EmployeeID =" +emp.EmpID + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            if (CheckEmployees(emp.EmpID))
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                connect.Close();
                connect.Dispose();

                return "Employee Discontinued";
            }
            else
            {
                return "Employee does not exist";
            }
        }

        public string UnDiscEmployee(Employee emp)
        {
            string query = string.Format("Update Employees set Discontinued = 0 where EmployeeID =" + emp.EmpID + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            if(CheckEmployees(emp.EmpID))
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                connect.Close();
                connect.Dispose();

                return "Employee Undiscontinued";
            }
            else
            {
                return "Employee does not exist";
            }
            
        }

        public string AddEmployee(Employee emp)
        {
            string query = string.Format("Insert Into Employees values(" + emp.EmpID + ",'" + emp.LastName + "','" + emp.FirstName + "','" + emp.Title + "','" + emp.IDNo + "','" + emp.Address + "'," + emp.ReportTo + ",'" + emp.Email + "','" + emp.Username + "','" + emp.Password + "'," + emp.EmployeePositionID + ","+0+")");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            if(!CheckEmployees(emp.EmpID))
            {
                if(CheckReportToID(emp.ReportTo))
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connect.Close();
                    connect.Dispose();
                    return "Employee Added";
                }
                else
                {
                    return "Wrong Report ID or the Employee is not Supervisor or the employee is discontinued";
                }
            }
            else
            {
                return "Employee already exist";
            }
            
        }

        public bool CheckReportToID(int rID)
        {
            string query = string.Format("Select EmployeeID,Discontinued From Employees where EmployeeID = " + rID +"and PositionID = 2");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable("Employees");

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            try
            {
                dt.Rows[0]["EmployeeID"].ToString();
                if(!bool.Parse(dt.Rows[0]["Discontinued"].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public bool CheckEmployees(int empId)
        {

            string query = string.Format("Select * From Employees where EmployeeId = " + empId);

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable("Employees");

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            try
            {
                dt.Rows[0]["FirstName"].ToString();
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public SqlDataReader SearchEmployees(int empid)
        {
            string query = string.Format("Select * From Employees where EmployeeID =" + empid + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {

                return dataReader;
            }
            else
            {
                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return null;
            }
        }

        public void EditEmployees(Employee emp)
        {
            string query = string.Format("Update Employees set LastName ='" +emp.LastName + "',FirstName='" + emp.FirstName + "',Title = '" + emp.Title + "',IDNO = '" + emp.IDNo + "',Address = '" + emp.Address + "',ReportTo = " + emp.ReportTo + ",Email = '" + emp.Email + "',Username = '" + emp.Username + "',Password = '"+emp.Password+"' where EmployeeID =" + emp.EmpID + "");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            connect.Close();
            connect.Dispose();
        }

        public void SaveInvoice(int idlog)
        {
            DateTime date = DateTime.Now;
            string dyt = date.ToShortDateString();
           
            string query = string.Format("Insert Into Invoice(InvoiceDate,EmployeeId) values('" +dyt+ "'," +idlog+ ")");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            connect.Close();
            connect.Dispose();
            BoughtItems();
        }

        public void BoughtItems()
        {
            DataTable data = table();

            string query = string.Format("Select Top 1 InvoiceNumber from Invoice order by InvoiceNumber desc");
            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            string invoiceNum="";
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Read();
                invoiceNum = dataReader["InvoiceNumber"].ToString();

            }
            cmd.Dispose();
            dataReader.Close();
            foreach(DataRow dr in data.Rows)
            {
                long prodId = long.Parse(dr["Product ID"].ToString());
                int qty = Int32.Parse(dr["Qty"].ToString());
                double tot = Int32.Parse(dr["Total"].ToString());
                query = string.Format("Insert Into InvoiceItems(InvoiceNumber,ProductID,Qty,TotalPrice) values(" + Convert.ToInt32(invoiceNum) + "," +prodId+","+qty+","+tot+")");
                cmd = new SqlCommand(query, connect);
                cmd.ExecuteNonQuery();
            }
            cmd.Dispose();
            connect.Close();
            connect.Dispose();



        }
      
        public int saveLoginEmployee(string username)
        {
            string query = string.Format("Select EmployeeID From Employees where Username ='" + username + "'");

            int idlog = 0;
            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Read();
                idlog = Int32.Parse((dataReader["EmployeeID"].ToString()));
            }
            cmd.Dispose();
            connect.Close();
            connect.Dispose();
            return idlog;
        }
         
        public Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        public bool EmailIsValid(string emailAddress)
        {
            Regex ValidEmailRegex = CreateValidEmailRegex();
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }

        public bool Void(Employee emp,string pass)
        {
            string query = string.Format("Select Username,Password From Employees where EmployeeID = '"+emp.Username+"'");

            SqlConnection connect = new SqlConnection(Conn);
            connect.Open();

            SqlCommand cmd = new SqlCommand(query, connect);
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Read();

                if(position(dataReader["Username"].ToString()).Equals("Supervisor"))
                {
                    if ((dataReader["password"].ToString()).Equals(pass))
                    {
                        cmd.Dispose();
                        connect.Close();
                        connect.Dispose();
                        return true;
                    }
                    else
                    {
                        return false;
                    }                    
                }
                else
                {
                    cmd.Dispose();
                    connect.Close();
                    connect.Dispose();
                    return false;
                }
               
            }
            else
            {
                cmd.Dispose();
                connect.Close();
                connect.Dispose();
                return false;
            }
            
        }


    }

}
