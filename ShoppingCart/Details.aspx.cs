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

public partial class ShoppingCart_Details : System.Web.UI.Page
{
    SqlConnection cn;
    SqlCommand cmd;
    SqlDataReader dr;
    private MovieInfo movieInfo;
    public List<MovieInfo> AddedMovies;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                AddedMovies = (List<MovieInfo>)Session["cart"];
                if (AddedMovies == null)
                {
                    Session["cart"] = new List<MovieInfo>();
                }

                //  Get database objects...
                //  Connect to database and open...
                cn = new SqlConnection();

                if (Request.UserHostAddress.ToString().Equals("::1"))
                {
                    // Local server...
                    cn.ConnectionString = @"Data Source=VITALIY-PC;Initial Catalog=CS379Website;Integrated Security=True";
                }
                else
                {
                    // Remote server...  (Note difference for older version of Access.)
                    cn.ConnectionString = @"Data Source=SQL5019.Smarterasp.net;Initial Catalog=DB_9FA50D_valekhnovich;User Id=DB_9FA50D_valekhnovich_admin;Password=esm001fh;"
;
                }

                string movieID = Request.QueryString["ProductID"];

                cmd = new SqlCommand(
                    "SELECT * " +
                    "FROM " +
                    "[Movies] " +
                    "WHERE ID = " + movieID
                    , cn);

                cn.Open();

                // Execute the SQL statement and get the dataset...
                dr = cmd.ExecuteReader();

                // Iterate over the dataset, create orders and add to collection...
                while (dr.Read())
                {
                    movieInfo = new MovieInfo(
                        dr["ID"].ToString(),
                        dr["MovieName"].ToString(),
                        dr["Description"].ToString(),
                        dr["imageUrl"].ToString(),
                        int.Parse(dr["Rating"].ToString()),
                        double.Parse(dr["Price"].ToString())
                        );
                }
                Session["currentMovie"] = movieInfo;
                Image1.ImageUrl = movieInfo.ImageUrl;

                MovieName.Text = movieInfo.MovieName;
                Ratings.Text = " Movie Rating: "+ movieInfo.Rating + "/ 5";
                Description.Text = movieInfo.Description;

                dr.Close();
                cn.Close();
            }
            catch (Exception err)
            {
                //                lblStatus.Text = err.Message;
                return;
            }

        }  //  End !IsPostBack
    }

    protected void AddToCart_OnClick(object sender, EventArgs e)
    {
        movieInfo = (MovieInfo) Session["currentMovie"];
        bool containsMovie = false;
        if (movieInfo != null)
        {
            List<MovieInfo> movies = (List<MovieInfo>)Session["cart"];

            foreach (MovieInfo info in movies)
            {
                if (info.MovieName.Equals(movieInfo.MovieName))
                {
                    containsMovie = true;
                }
            }
            if (!containsMovie)
            {
                movies.Add(movieInfo);
                Session["cart"] = movies;
            }
        }
        Response.Redirect("ViewCart.aspx");
    }

    protected void ViewCart_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ViewCart.aspx");
    }
}