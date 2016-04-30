using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AccessdBLab_Default : System.Web.UI.Page
{
    private List<InvoiceLineItem> theOrders;
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
                this.theOrders = new List<InvoiceLineItem>();
                //  Get database objects...
                //  Connect to database and open...
                cn = new OleDbConnection();

                if (Request.UserHostAddress.ToString().Equals("::1"))
                {
                    // Local server...
                    cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Vitaliy\Desktop\CS379\Git\ASPNETWebsite\App_Data\Orders.accdb;Persist Security Info=False;";
                }
                else
                {
                    // Remote server...  (Note difference for older version of Access.)
                    cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=h:\root\home\valekhnovich-001\www\valekhnovichsite\App_Data\Orders.accdb";
                }

                // Create the SQL command...
                cmd = new OleDbCommand("SELECT * FROM [InvoiceLineItem Table] ORDER BY [Customer number]", cn);

                cn.Open();

                // Execute the SQL statement and get the dataset...
                dr = cmd.ExecuteReader();

                // Iterate over the dataset, create orders and add to collection...
                while (dr.Read())
                {
                    InvoiceLineItem ord = new InvoiceLineItem(
                                   int.Parse(dr["Customer number"].ToString()),
                                   int.Parse(dr["Order number"].ToString()),
                                   int.Parse(dr["Invoice sequence number"].ToString()),
                                   dr["Item SKU"].ToString(),
                                   dr["Item description"].ToString(),
                                   int.Parse(dr["Quantity ordered"].ToString()),
                                   double.Parse(dr["Unit price"].ToString()),
                                   double.Parse(dr["Unit weight"].ToString()));
                    theOrders.Add(ord);
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
            if (theOrders == null)
            {
                theOrders = (List<InvoiceLineItem>)ViewState["theOrders"];
            }

            tbl = new Table();
            tbl.BorderStyle = BorderStyle.Solid;
            tbl.BorderColor = Color.FromArgb(0xBDFCC9);
            tbl.BackColor = Color.Cornsilk;
            tbl.EnableViewState = true;

            tbl.Rows.Add(RowHeader());
            foreach (InvoiceLineItem ord in theOrders)
            {
                TableRow row = new TableRow();
                row.Cells.Add(addCell(ord.CustomerNumber.ToString()));
                row.Cells.Add(addCell(ord.OrderNumber.ToString()));
                row.Cells.Add(addCell(ord.InvoiceSequenceNum.ToString()));
                row.Cells.Add(addCell(ord.SKU));
                row.Cells.Add(addCell(ord.Description));
                row.Cells.Add(addCell(ord.Quantity.ToString()));
                row.Cells.Add(addCell("$" + ord.UnitPrice));
                row.Cells.Add(addCell(ord.Weight + ""));
                row.Cells.Add(addCell(ord.ShippingCost + ""));
                
                tbl.Rows.Add(row);

            }
            Page.Controls.Add(tbl);
        }

        catch (Exception err)
        {
            lblStatus.Text = err.Message;
        }
    }

    public TableRow RowHeader()
    {
        TableRow row = new TableRow();
        row.Cells.Add(addCell("Customer Number"));
        row.Cells.Add(addCell("Order Number"));
        row.Cells.Add(addCell("Invoice Sequence Number"));
        row.Cells.Add(addCell("SKU"));
        row.Cells.Add(addCell("Description"));
        row.Cells.Add(addCell("Quantity"));
        row.Cells.Add(addCell("Price"));
        row.Cells.Add(addCell("Weight"));
        row.Cells.Add(addCell("Shipping Cost"));
        return row;
    }

    private TableCell addCell(String pText)
    {
        TableCell cell = new TableCell();
        cell.BorderStyle = BorderStyle.Groove;
        cell.Text = pText;
        cell.HorizontalAlign = HorizontalAlign.Center;
        return cell;
    }

    public List<InvoiceLineItem> getorders()
    {
        return theOrders;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        try
        {
            // Serialize the orders array to the viewstate...
            ViewState["theOrders"] = theOrders;
        }
        catch (Exception err)
        {
            lblStatus.Text = err.Message;
        }
    }

    protected void Redirect_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("CheckOutPage.aspx");
    }
}