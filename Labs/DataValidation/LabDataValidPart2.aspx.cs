using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Labs_DataValidation_LabDataValidPart2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cmdOK_Click(object sender, EventArgs e)
    {
        string errorMessage = "<b>Mistakes found:</b><br />";

        bool pageIsValid = true;
        // Search through the validation controls.
        int acctnum = Int32.Parse(accntNum.Text);
        int total = 0;
        while (acctnum != 0)
        {
            total += acctnum%10;
            acctnum = acctnum/10;
        }
        int result = total%10;
        if (result != 0)
        {
            pageIsValid = false;
            errorMessage = "Account number didnt checksum to zero: [CHECKSUM]=" + total;
        }

        foreach (BaseValidator ctrl in this.Validators)
        {
            if (!ctrl.IsValid)
            {
                pageIsValid = false;
                errorMessage += ctrl.ErrorMessage + "<br />";

                // Find the corresponding input control, and change the
                // generic Control variable into a TextBox variable.
                // This allows access to the Text property.
                errorMessage += " * Input was not an integer: " + accntNum.Text;
            }
        }
        if (!pageIsValid) lblMessage.Text = errorMessage;
        if (pageIsValid) lblMessage.Text = "This is a valid account number";
    }
}