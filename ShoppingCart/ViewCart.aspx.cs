using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCart_ViewCart : System.Web.UI.Page
{
    private List<MovieInfo> cart; 
    protected void Page_Load(object sender, EventArgs e)
    {
        cart = (List<MovieInfo>)Session["cart"];
        if (cart != null)
        {
            TableRow row = null;
            int count = 0, items=0;
            double total = 0, extendedPrice, extendedWeight, totalWeight = 0;
            TableCell cell;

            row = new TableRow();
            row.Cells.Add(addCell("Order summary", "headCell"));
            row.Cells.Add(addCell("Unit Price", "headCell"));
            row.Cells.Add(addCell("Quantity", "headCell"));
            row.Cells.Add(addCell("Extension", "headCell"));
            row.Cells.Add(addCell("Weight", "headCell"));
            tbl.Rows.Add(row);
            HyperLink link;
            foreach (MovieInfo movieInfo in cart)
            {
                if (movieInfo != null)
                {
                    row = new TableRow();
                    row.CssClass = "row";
                    row.Cells.Add(addCell(movieInfo.MovieName, "cell"));
                    row.Cells.Add(addCell("$" + movieInfo.Price, "cell"));
                    row.Cells.Add(addCell(movieInfo.Quantity.ToString() , "cell"));
                    extendedPrice = movieInfo.Quantity*movieInfo.Price;
                    extendedWeight = movieInfo.Quantity*movieInfo.Weight;
                    row.Cells.Add(addCell("$" + extendedPrice , "cell"));
                    row.Cells.Add(addCell(extendedWeight.ToString() , "cell"));

                    cell = new TableCell();
                    Button button = new Button();
                    button.Text = "Remove this Item.";
                    button.ID = movieInfo.MovieName + "_" + count;
                    button.Click += Button1_OnClick;
                    button.CssClass = "removeButton";
                    cell.Controls.Add(button);
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    link = new HyperLink();
                    link.Text = "Change";
                    link.ID = movieInfo.MovieName + "_" + count;
                    link.NavigateUrl = "./Details.aspx?ProductID=" + movieInfo.ID;
                    link.CssClass = "removeButton";
                    cell.Controls.Add(link);
                    row.Cells.Add(cell);
                    items += movieInfo.Quantity;
                    count++;
                    total += extendedPrice;
                    totalWeight += extendedWeight;
                }
                else
                {
                    cart.Remove(movieInfo);
                }
                if (row != null)
                {
                    tbl.Rows.Add(row);
                }
            }
            row = new TableRow();
            row.Cells.Add(addCell("", ""));
            row.Cells.Add(addCell("", ""));
            row.Cells.Add(addCell("Count:" + count, "headCell"));
            row.Cells.Add(addCell("$" + total, "headCell"));
            row.Cells.Add(addCell(totalWeight.ToString(), "headCell"));
            tbl.Rows.Add(row);

            Session["quantity"] = count;
            Session["items"] = items;
            Session["weightTotal"] = totalWeight;
            Session["orderTotal"] = total;
        }
        else
        {
            Label lbl = new Label();
            lbl.CssClass = "labels";
            lbl.Text = "Your shopping cart is empty.";
            Content.Controls.Add(new LiteralControl("<br />"));
            Content.Controls.Add(lbl);
        }
        
    }

    private static TableCell addCell(string pText, string CssClass)
    {
        TableCell cell = new TableCell();
        cell.CssClass = CssClass;
        cell.Text = pText;
        
        cell.HorizontalAlign = HorizontalAlign.Center;
        return cell;
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Button send = (Button) sender;
        string movieID = send.ID;

        movieID = movieID.Split('_')[0];
        List<MovieInfo> list = (List<MovieInfo>)Session["cart"];
        MovieInfo target = null;
        if (list != null)
        {
            foreach (MovieInfo movieInfo in list)
            {
                if (movieInfo.MovieName.Equals(movieID))
                {
                    target = movieInfo;
                }
            }
            list.Remove(target);
            Session["cart"] = list;
        }
        Response.Redirect("ViewCart.aspx");

    }

    protected void ContinueShop_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void CheckOut_OnClick(object sender, EventArgs e)
    {
        string ord = (string) Session["ord#"];
        if (ord == null)
        {
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd;
            SqlDataReader dr;
            if (Request.UserHostAddress.ToString().Equals("::1"))
            {
                // Local server...
                cn.ConnectionString = @"Data Source=SQL5019.Smarterasp.net;Initial Catalog=DB_9FA50D_valekhnovich;User Id=DB_9FA50D_valekhnovich_admin;Password=esm001fh;";
            }
            else
            {
                // Remote server...  (Note difference for older version of Access.)
                cn.ConnectionString = @"Data Source=SQL5019.Smarterasp.net;Initial Catalog=DB_9FA50D_valekhnovich;User Id=DB_9FA50D_valekhnovich_admin;Password=esm001fh;";
            }

            string companyName, orderNum, command;

            companyName = "MoviesRUs";

           command = "INSERT INTO NextOrderNumber (CompanyName) " +
                             " Values ('" + companyName +"'); " +
                                          " Select Top 1 *" +
                                          " From NextOrderNumber" +
                                          " Order By NextOrder desc";

            cn.Open();
            cmd = new SqlCommand(command, cn);
            cmd.Parameters.Add("companyName", SqlDbType.VarChar).Value = companyName;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                orderNum = dr["NextOrder"].ToString();
                companyName = dr["companyName"].ToString();
                Session["ord#"] = companyName + "-" + orderNum;
            }

            dr.Close();
            cn.Close();
        }
        Response.Redirect("Checkout.aspx");
    }
}