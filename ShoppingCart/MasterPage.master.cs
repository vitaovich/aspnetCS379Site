﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCart_MasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ViewCart_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ViewCart.aspx");
    }

    protected void Shopping_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Default.aspx");
    }
}
