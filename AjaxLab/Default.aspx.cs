using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AjaxLab_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void RefreshTime_OnClick(object sender, EventArgs e)
    {
        timeLbl.Text = DateTime.Now.ToLongTimeString();
    }
}