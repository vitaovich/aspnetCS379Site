using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCart_Checkout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int count, quantity;
            double totalWeight, total;
            string ordNum = (string)Session["ord#"];

            try
            {
                count = Int32.Parse(Session["quantity"].ToString());
                quantity = Int32.Parse(Session["items"].ToString());
                totalWeight = Double.Parse(Session["weightTotal"].ToString());
                total = Double.Parse(Session["orderTotal"].ToString());
                items.Text += count;
                Quantity.Text += quantity;
                weight.Text += totalWeight;
                orderTotal.Text += "$" + total;
                lblOrderNum.Text += ordNum;
                lblitemCost.Text += "$" + total;
                lblShippingCost.Text += totalWeight*0.46;
                lblTotal.Text += "$" + (total + (totalWeight*0.46));
            }
            catch (Exception ex)
            {
                lblTotal.Text = ex.ToString();
            }
        }
    }

    protected void SubmitOrder_OnClick(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            double total = 0.0;

            string sWork = "CSCD379 - ASP.Net Programming with C# \n\n";
            sWork += "Your order has been processed.\n\n";
            sWork += "Order number: " + lblOrderNum.Text + "\n";

            List<MovieInfo> movies = (List<MovieInfo>)Session["cart"];
            if (movies != null)
            {
                foreach (MovieInfo movie in movies)
                {
                    total = movie.Price * movie.Quantity;
                    sWork += movie.MovieName;
                    for (int i = movie.MovieName.Length; i < 30; i++)
                    {
                        sWork += " ";
                    }
                    sWork += "\t" + movie.Quantity + "\t" + movie.Price + "\t" + total.ToString() + "\n";
                }

                sWork += "\n\nTotal quantity: " + Quantity.Text;
                sWork += "\nShipping cost: " + lblShippingCost.Text;
                sWork += "\nTotal order cost: " + lblTotal.Text;

                sWork += "\n\n" + this.TName.Text;
                sWork += "\n" + this.TStreet.Text;
                sWork += "\n" + this.TCity.Text;
                sWork += " " + this.TState.Text;
                sWork += ", " + this.TZip.Text;

                sWork += "\n\nIf you did not place an order with Movie Hub, please contact mailto:support@EvergreenInteractive.com";

                //  Parms are 'from address', 'to address'...
                MailMessage msg = new MailMessage("OrderReportMH@vitapita.ru", this.TEmail.Text);

                msg.Subject = "Order number " + lblOrderNum.Text + " has been processed";
                msg.Body = sWork;

                SmtpClient client = new SmtpClient();

                client.Credentials = new System.Net.NetworkCredential("OrderReportMH@vitapita.ru", "Esm))1al");
                client.Host = "mail.vitapita.ru";

                try
                {
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    string s = ex.ToString();
                }

                client = null;
                Session.Clear();
                Response.Redirect("OrderSuccess.aspx?" + "orderID=" + this.lblOrderNum.Text);
            }
        }
    }
}