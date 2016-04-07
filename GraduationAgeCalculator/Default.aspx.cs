using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        currentDate.Text = "The current date is " + DateTime.Now.ToString("dddd, MMMM dd, yyyy");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int bm = 0, bd = 0, by = 0, gm = 0, gd = 0, gy =0, age= 0;
        Int32.TryParse(bMonth.Text, out bm);
        Int32.TryParse(bDay.Text, out bd);
        Int32.TryParse(bYear.Text, out by);
        Int32.TryParse(gMonth.Text, out gm);
        Int32.TryParse(gDay.Text, out gd);
        Int32.TryParse(gYear.Text, out gy);

        age = gy - by;
        if (bm - gm >= 0 && bd - gd > 0)
        {
            age--;
        }
        ageResult.Text = "You will be " + age + " years old when you graduate. Congratz!!";
    }
}