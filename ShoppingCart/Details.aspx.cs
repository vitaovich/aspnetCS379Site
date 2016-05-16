using System;
using System.Collections.Generic;
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
    public List<MovieInfo> AddedMovies, content;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            content = (List<MovieInfo>)ViewState["content"];
            string movieID = Request.QueryString["ProductID"];
            try
            {
                if (content == null)
                {
                    content = GrabMovies();
                }
                MovieInfo currentMovie = content.First(eb => eb.ID.Equals(movieID));
                Session["currentMovie"] = currentMovie;
                Image1.ImageUrl = currentMovie.ImageUrl;

                MovieName.Text = currentMovie.MovieName;
                Ratings.Text = " Movie Rating: " + currentMovie.Rating + "/ 5";
                Description.Text = currentMovie.Description;
                ListItem quant;
                for (int i = 1; i < 26; i++)
                {
                    quant = new ListItem(i + "");
                    Quantity.Items.Add(quant);
                }
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
        movieInfo = (MovieInfo)Session["currentMovie"];

        if (movieInfo != null)
        {
            List<MovieInfo> movies = (List<MovieInfo>)Session["cart"];
            int quantity;

            if (movies == null)
            {
                movies = new List<MovieInfo>();
            }

            foreach (MovieInfo info in movies.ToList())
            {
                if (info.MovieName.Equals(movieInfo.MovieName))
                {
                    movies.Remove(info);
                }
            }
            quantity = Int32.Parse(Quantity.Text);
            movieInfo.Quantity = quantity;
            movies.Add(movieInfo);
            Session["cart"] = movies;
        }
        Response.Redirect("ViewCart.aspx");
    }

    private List<MovieInfo> GrabMovies()
    {
            List<MovieInfo> cont = new List<MovieInfo>();
            //  Get database objects...
            //  Connect to database and open...
            cn = new SqlConnection();

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
                movieInfo = new MovieInfo(
                    dr["ID"].ToString(),
                    dr["MovieName"].ToString(),
                    dr["Description"].ToString(),
                    dr["imageUrl"].ToString(),
                    int.Parse(dr["Rating"].ToString()),
                    double.Parse(dr["Price"].ToString()),
                    double.Parse(dr["weight"].ToString())
                    );
                cont.Add(movieInfo);
            }
            dr.Close();
            cn.Close();
            return cont;
    } 

    protected void ViewCart_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ViewCart.aspx");
    }
}