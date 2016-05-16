using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Labs_Navigator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] files = null;
            files = GetDirectories(@"C:\Users\Vitaliy\Desktop\cs379website\Labs\");
            RunThroughDirectories(files);
  
        }
    }

    private void RunThroughDirectories(string [] directories)
    {
        foreach (string directory in directories)
        {
            if (Directory.Exists(directory))
            {
                RunThroughDirectories(Directory.GetDirectories(directory));
                string [] files = Directory.GetFiles(directory);
                ListAllPages(files);
            }
        }

    }

    private void ListAllPages(string [] files)
    {
        HyperLink link = null;
        foreach (string file in files)
        {
            if (file.EndsWith(".aspx"))
            {
                link = new HyperLink
                {
                    NavigateUrl = ".\\" + file.Substring(file.IndexOf("Labs/") + 5),
                    Text = file
                };
                ContentArea.Controls.Add(link);
                ContentArea.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }

    private static string[] GetDirectories(string directory)
    {
        string[] directories = null;
        directories = Directory.GetDirectories(directory);
        return directories;
    }

    private static  string [] GetFiles(string directory)
    {
        string [] files = null;
        files = Directory.GetFiles(directory);
        return files;
    }
}