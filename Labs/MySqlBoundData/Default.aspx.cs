using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Labs_MySqlBoundData_Default : System.Web.UI.Page
{
    private string conString = WebConfigurationManager.ConnectionStrings["SmarterASP"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sCommand = "";
            string sWork = "";
            string custNo = "";
            string custFName = "";
            string custLName = "";

            try
            {
                OdbcConnection cn = new OdbcConnection(conString);

                cn.Open();

                //Label1.Text = "   DB: " + cn.Database;
                //Label1.Text += "   DS: " + cn.DataSource;
                //Label1.Text += "   Driver: " + cn.Driver;
                //Label1.Text += "   -: " + cn.ToString();

                OdbcCommand cmd = new OdbcCommand(sCommand, cn);

                sCommand = "SELECT Distinct(Continent) FROM country ORDER BY Continent;";
                cmd.CommandText = sCommand;

                OdbcDataReader dr;
                // OdbcDataReader dr2; 

                dr = cmd.ExecuteReader();
                while (dr.Read() == true)
                {
                    custFName = dr["Continent"].ToString();

                    sWork = custFName;
                    lbCountry.Items.Add(sWork);
                }

                cn.Close();

                cn = null;

            }
            catch (Exception err)
            {
                this.Label1.Text = "Error:  " + err.Message + " " + conString;
            }
        }
    }

    protected void btnGetEm_Click(object sender, EventArgs e)
    {
        string sCommand = "";
        string sWork = "";
        string custNo = "";
        string custFName = "";
        string custLName = "";

        try
        {
            OdbcConnection cn = new OdbcConnection(conString);

            cn.Open();

            //Label1.Text = "   DB: " + cn.Database;
            //Label1.Text += "   DS: " + cn.DataSource;
            //Label1.Text += "   Driver: " + cn.Driver;
            //Label1.Text += "   -: " + cn.ToString();

            OdbcCommand cmd = new OdbcCommand(sCommand, cn);

            sCommand = "SELECT * FROM country" +
                       " Where Continent = '" + lbCountry.SelectedValue + "'" +
                       " ORDER BY Name;";
            cmd.CommandText = sCommand;

            OdbcDataReader dr;
            // OdbcDataReader dr2; 

            dr = cmd.ExecuteReader();
            GridView1.DataSource = dr;
            GridView1.DataBind();

            dr.Close();

            cn.Close();

            cn = null;

        }
        catch (Exception err)
        {
            this.Label1.Text = "Error:  " + err.Message + " " + conString;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        OdbcConnection cn;

        string sqlstr;
        sqlstr = "Update country ";
        sqlstr += " Set Name = '" + txtNewCountryName.Text + "'";
        sqlstr += " Where Name = '" + txtCountryName.Text + "';";

        try
        {
            cn = new OdbcConnection(conString);
            cn.Open();

            OdbcCommand cmd = cn.CreateCommand();
            //cmd.Connection = cn;
            cmd.CommandText = sqlstr;

//            cmd.Parameters.AddWithValue("CountryName", txtCountryName.Text);
//            cmd.Parameters.AddWithValue("NewCountryName", txtNewCountryName.Text);

            cmd.ExecuteNonQuery();

            cn.Close();
            cn = null;

            txtCountryName.Text = "";
            txtNewCountryName.Text = "";

            this.btnGetEm_Click(sender, e);
        }
        catch (Exception err)
        {
            Label1.Text = err.Message;
        }

    }
}