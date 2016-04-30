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
                    cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Vitaliy\Source\Repos\aspnetCS379Site\App_Data\movieDB.accdb;Persist Security Info=False;";
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
            Image image;
            Label lbl;
            Panel pnl;
            foreach (MovieInfo movieInfo in Movies)
            {
                pnl = new Panel();
                lbl = new Label();
                image = new Image {ImageUrl = movieInfo.ImageUrl};
                image.CssClass = "imageSizing";

                pnl.CssClass = "panel";

                lbl.Text = movieInfo.MovieName + " $" + movieInfo.Price;
                lbl.CssClass = "labels";
                lbl.Width = pnl.Width;

                //                tbl = new Table();
                //                tbl.BorderStyle = BorderStyle.Solid;
                //                tbl.BorderColor = Color.FromArgb(0xBDFCC9);
                //                tbl.BackColor = Color.Cornsilk;
                //                tbl.EnableViewState = true;
                //                movieInfo.SalesInfo(tbl);
                //                Page.Controls.Add(tbl);
                //                total += movieInfo.LineItem.QuantityOrdered * movieInfo.Inventory.UnitPrice;
                pnl.Controls.Add(image);
                pnl.Controls.Add(new LiteralControl("<br />"));
                pnl.Controls.Add(lbl);
                PlaceHolder1.Controls.Add(pnl);
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

    public static TableCell addCell(String pText)
    {
        TableCell cell = new TableCell();
        cell.BorderStyle = BorderStyle.Groove;
        cell.Text = pText;
        cell.HorizontalAlign = HorizontalAlign.Center;
        return cell;
    }

    public class MovieInfo
    {
        public string MovieName { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public int Rating { get; private set; }
        public double Price { get; private set; }

        public MovieInfo(string movieName, string description, string imageUrl, int rating, double price)
        {
            MovieName = movieName;
            Description = description;
            ImageUrl = imageUrl;
            Rating = rating;
            Price = price;
        }
    }
}