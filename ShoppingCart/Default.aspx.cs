using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

public partial class ShoppingCart_Default : System.Web.UI.Page
{
    private List<MovieInfo> Movies;
    private Table tbl;

    protected void Page_Load(object sender, EventArgs e)
    {
        OleDbConnection cn;
        OleDbCommand cmd;
        OleDbDataReader dr;

        if (IsPostBack == false)
        {
            try
            {
                this.Movies = new List<MovieInfo>();
                //  Get database objects...
                //  Connect to database and open...
                cn = new OleDbConnection();

                if (Request.UserHostAddress.ToString().Equals("::1"))
                {
                    // Local server...
                    cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Vitaliy\Desktop\CS379\Git\aspDotNetWebsite\App_Data\movieDB.accdb;Persist Security Info=False;";
                }
                else
                {
                    // Remote server...  (Note difference for older version of Access.)
                    cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=h:\root\home\valekhnovich-001\www\valekhnovichsite\App_Data\movieDB.accdb";
                }

                cmd = new OleDbCommand(
                    "SELECT * " +
                    "FROM " +
                    "[Movies] "
                    , cn);

                cn.Open();

                // Execute the SQL statement and get the dataset...
                dr = cmd.ExecuteReader();

                // Iterate over the dataset, create orders and add to collection...
                while (dr.Read())
                {
                    MovieInfo movieInfo = new MovieInfo(
                        dr["ID"].ToString(),
                        dr["MovieName"].ToString(),
                        dr["Description"].ToString(),
                        dr["imageUrl"].ToString(),
                        int.Parse(dr["Rating"].ToString()),
                        double.Parse(dr["Price"].ToString())
                        );
                    Movies.Add(movieInfo);
                }

                dr.Close();
                cn.Close();
            }
            catch (Exception err)
            {
//                lblStatus.Text = err.Message;
                return;
            }

        }  //  End !IsPostBack

        try
        {
            //  Restore the orders array from the viewstate...
            if (Movies == null)
            {
                Movies = (List<MovieInfo>)ViewState["theOrders"];
            }
            Label lbl, ratings;
            Image image;
            HyperLink linkButton;
            foreach (MovieInfo movieInfo in Movies)
            {
                image = new Image();
                linkButton = new HyperLink();
                lbl = new Label();
                ratings = new Label();

                image.ImageUrl = movieInfo.ImageUrl;
                image.CssClass = "imageSizing";

                lbl.Text = movieInfo.MovieName + " $" + movieInfo.Price;
                lbl.CssClass = "labels";
                lbl.Width = linkButton.Width;

                ratings.Text = "Rating: " + movieInfo.Rating  + " / 5";
                ratings.CssClass = "ratings";

                linkButton.CssClass = "panel";
                linkButton.ID = movieInfo.ID;
                linkButton.ClientIDMode = ClientIDMode.Static;
                linkButton.Controls.Add(image);
                linkButton.Controls.Add(new LiteralControl("<br />"));
                linkButton.Controls.Add(lbl);
                linkButton.Controls.Add(new LiteralControl("<br />"));
                linkButton.Controls.Add(ratings);
                linkButton.NavigateUrl = "./Details.aspx?ProductID=" + movieInfo.ID;
                

                content.Controls.Add(linkButton);
            }
//            lbl = new Label();
//            lbl.Text = "INVOICE SUMMARY";
//            lbl.Font.Bold = true;
//            Page.Controls.Add(lbl);
//            tbl = new Table();
//            TableRow row = new TableRow();
//            row.Cells.Add(addCell("Order count"));
//            row.Cells.Add(addCell("Average Order Amount"));
//            row.Cells.Add(addCell("Total Sales"));
//            tbl.Rows.Add(row);
//            row = new TableRow();
//            row.Cells.Add(addCell(count.ToString()));
//            row.Cells.Add(addCell("$" + (total / count)));
//            row.Cells.Add(addCell("$" + total));
//            tbl.Rows.Add(row);
//            Page.Controls.Add(tbl);
        }

        catch (Exception err)
        {
//            lblStatus.Text = err.Message;
        }
    }

    protected void Movie_OnClick(object sender, EventArgs e)
    {
        ImageButton image = (ImageButton) sender;
        Session["CurrentMovie"] = image.ID;
        Response.Redirect("Details.aspx");
    }

    public static TableCell addCell(String pText)
    {
        TableCell cell = new TableCell();
        cell.BorderStyle = BorderStyle.Groove;
        cell.Text = pText;
        cell.HorizontalAlign = HorizontalAlign.Center;
        return cell;
    }


}