using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AccessdBLab_CheckOutPage : System.Web.UI.Page
{
    private List<InvoiceLineItem> theOrders = null;
    private Table tbl; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (PreviousPage != null)
        {
            theOrders = PreviousPage.getorders();

            tbl = new Table();
            tbl.BorderStyle = BorderStyle.Dotted;
            tbl.EnableViewState = true;
            tbl.Rows.Add(PreviousPage.RowHeader());
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
        else
        {
            Label1.Text = "Didn't have a previous page.";
        }
        
    }

    private TableCell addCell(String pText)
    {
        TableCell cell = new TableCell();
        cell.BorderStyle = BorderStyle.Solid;
        cell.BorderWidth = 1;
        cell.Text = pText;

        return cell;
    }
}