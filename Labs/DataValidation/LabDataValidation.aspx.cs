using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Labs_LabDataValidation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMessage.Text = "This is a valid form.";
            Response.Redirect("LabDataValidPart2.aspx");
        }
    }

    protected void vldCode_ServerValidate(object source, ServerValidateEventArgs e)
    {
        try
        {
            // Check if the first three digits are divisible by seven.
            string val = e.Value;
            if (val.Equals("pie"))
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }
        catch
        {
            // An error occured in the conversion.
            // The value is not valid.
            e.IsValid = false;
        }
    }
}