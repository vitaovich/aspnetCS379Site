using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCart_OrderSuccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl.Text = Request.QueryString["orderID"] +
                   " has been processed\n An email has been sent to confirm your order.";
    }
}