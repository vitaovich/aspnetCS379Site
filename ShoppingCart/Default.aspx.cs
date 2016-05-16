using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
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
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        Label ratings;
        if (IsPostBack == false)
        {
            try
            {
                Movies = (List<MovieInfo>)ViewState["content"];

                if (Movies == null)
                {
                    //  Get database objects...
                    //  Connect to database and open...
                    cn = new SqlConnection();
                    Movies = new List<MovieInfo>();
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

                    cmd = new SqlCommand(
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
                            double.Parse(dr["Price"].ToString()),
                            double.Parse(dr["weight"].ToString())
                            );
                        Movies.Add(movieInfo);
                    }

                    dr.Close();
                    cn.Close();
                }

            }
            catch (Exception err)
            {
                lbl.Text = err.Message;
                return;
            }

        }  //  End !IsPostBack

        if (Movies == null)
        {
            Movies = (List<MovieInfo>)ViewState["theOrders"];
        }

        if (Movies != null)
        { }
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

                ratings.Text = "Rating: " + movieInfo.Rating + " / 5";
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
        ViewState["content"] = Movies;
    }

    protected void Movie_OnClick(object sender, EventArgs e)
    {
        ImageButton image = (ImageButton)sender;
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