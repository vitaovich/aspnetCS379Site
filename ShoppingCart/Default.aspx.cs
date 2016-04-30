using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCart_Default : System.Web.UI.Page
{
    private List<MovieInfo> transactions;
    private Table tbl;

    protected void Page_Load(object sender, EventArgs e)
    {
        OleDbConnection cn;
        OleDbCommand cmd;
        OleDbDataReader dr;

        if (IsPostBack == false)
        {
            try
            {
                this.transactions = new List<MovieInfo>();
                //  Get database objects...
                //  Connect to database and open...
                cn = new OleDbConnection();

                if (Request.UserHostAddress.ToString().Equals("::1"))
                {
                    // Local server...
                    cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Vitaliy\Source\Repos\aspnetCS379Site\App_Data\SalesTransactionDB.accdb;Persist Security Info=False;";
                }
                else
                {
                    // Remote server...  (Note difference for older version of Access.)
                    cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=h:\root\home\valekhnovich-001\www\valekhnovichsite\App_Data\SalesTransactionDB.accdb";
                }

                // Create the SQL command...
                //                cmd = new OleDbCommand("SELECT * " +
                //                                       "FROM ([Customer] c " +
                //                                       "    inner join " +
                //                                       "[Invoice] i " +
                //                                       "    on c.CustNumber = i.CustNumber " +
                //                                       " )   inner join " +
                //                                       "[LineItemID] ld " +
                //                                       "    on i.InvoiceNumber = ld.InvoiceNumber " +
                //                                       "where i.Status = 'Open' "
                //                                       , cn);
                //
                //                cn.Open();

                cmd = new OleDbCommand("SELECT * " +
                                       "FROM " +
                                       "[Customer] c,  [Invoice] i, [LineItemID] ld, [LineItem] li, [Inventory] inv, [Supplier] s " +
                                       "where " +
                                       "c.CustNumber = i.CustNumber " +
                                       "AND i.InvoiceNumber = ld.InvoiceNumber " +
                                       "AND ld.InvoiceNumber = li.LineItemID " +
                                       "AND li.SKU = inv.SKU " +
                                       "AND inv.SupplierID = s.SupplierID " +
                                       "AND i.Status = 'Open' "
                    , cn);

                cn.Open();

                // Execute the SQL statement and get the dataset...
                dr = cmd.ExecuteReader();

                // Iterate over the dataset, create orders and add to collection...
                while (dr.Read())
                {
                    CustomerInfo customerInfo = new CustomerInfo(
                        int.Parse(dr["c.CustNumber"].ToString()),
                        int.Parse(dr["c.Phone"].ToString()),
                        dr["c.Company"].ToString(),
                        dr["c.Contact"].ToString(),
                        dr["Billing"].ToString(),
                        dr["Shipping"].ToString()
                        );
                    Invoice invoice = new Invoice(
                        int.Parse(dr["i.InvoiceNumber"].ToString()),
                        dr["Shipped Date"].ToString(),
                        dr["Order Date"].ToString(),
                        dr["Status"].ToString(),
                        dr["i.CustNumber"].ToString());
                    LineItem lineItem = new LineItem(
                        int.Parse(dr["QuantityOrdered"].ToString()),
                        dr["li.SKU"].ToString(),
                        dr["LineItemID"].ToString());
                    Inventory inventory = new Inventory(
                        int.Parse(dr["QOH"].ToString()),
                        int.Parse(dr["UnitWeight"].ToString()),
                        double.Parse(dr["UnitPrice"].ToString()),
                        dr["inv.SKU"].ToString(),
                        dr["inv.SupplierID"].ToString(),
                        dr["Description"].ToString()
                        );
                    Supplier supplier = new Supplier(
                        dr["s.SupplierID"].ToString(),
                        int.Parse(dr["Phone#"].ToString()),
                        dr["s.Company"].ToString(),
                        dr["s.Contact"].ToString(),
                        dr["Addresses"].ToString()
                        );
                    transactions.Add(new Transaction(customerInfo, invoice, lineItem, inventory, supplier));
                }

                dr.Close();
                cn.Close();
            }
            catch (Exception err)
            {
                lblStatus.Text = err.Message;
                return;
            }

        }  //  End !IsPostBack

        try
        {
            //  Restore the orders array from the viewstate...
            if (transactions == null)
            {
                transactions = (List<Transaction>)ViewState["theOrders"];
            }

            Label lbl;
            int count = 0;
            double total = 0;
            foreach (Transaction transaction in transactions)
            {
                count++;
                lbl = new Label();
                lbl.Text = "ORDER #" + count;
                lbl.Font.Bold = true;
                Page.Controls.Add(lbl);

                tbl = new Table();
                tbl.BorderStyle = BorderStyle.Solid;
                tbl.BorderColor = Color.FromArgb(0xBDFCC9);
                tbl.BackColor = Color.Cornsilk;
                tbl.EnableViewState = true;
                transaction.SalesInfo(tbl);
                Page.Controls.Add(tbl);
                total += transaction.LineItem.QuantityOrdered * transaction.Inventory.UnitPrice;
            }
            lbl = new Label();
            lbl.Text = "INVOICE SUMMARY";
            lbl.Font.Bold = true;
            Page.Controls.Add(lbl);
            tbl = new Table();
            TableRow row = new TableRow();
            row.Cells.Add(addCell("Order count"));
            row.Cells.Add(addCell("Average Order Amount"));
            row.Cells.Add(addCell("Total Sales"));
            tbl.Rows.Add(row);
            row = new TableRow();
            row.Cells.Add(addCell(count.ToString()));
            row.Cells.Add(addCell("$" + (total / count)));
            row.Cells.Add(addCell("$" + total));
            tbl.Rows.Add(row);
            Page.Controls.Add(tbl);
        }

        catch (Exception err)
        {
            lblStatus.Text = err.Message;
        }
    }

    public static TableCell addCell(String pText)
    {
        TableCell cell = new TableCell();
        cell.BorderStyle = BorderStyle.Groove;
        cell.Text = pText;
        cell.HorizontalAlign = HorizontalAlign.Center;
        return cell;
    }

    public class MovieInfo
    {
        public string MovieName { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public int Rating { get; private set; }
        public double Price { get; private set; }

        public MovieInfo(string movieName, string description, string imageUrl, int rating, double price)
        {
            MovieName = movieName;
            Description = description;
            ImageUrl = imageUrl;
            Rating = rating;
            Price = price;
        }
    }
}