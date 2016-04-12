using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GraduationAgeCalculator_AgeAtGraduation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string getDate()
    {
        return DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
    }
}