﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Web;
using System.Web.Services;
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
            int count = 0;
            foreach (MovieInfo movieInfo in cart)
            {
                if (movieInfo != null)
                {
                    row = new TableRow();
                    row.CssClass = "row";
                    row.Cells.Add(addCell(movieInfo.MovieName));
                    row.Cells.Add(addCell("$" + movieInfo.Price));
                    TableCell cell = new TableCell();
                    Button button = new Button();
                    button.Text = "Remove this Item.";
                    button.ID = movieInfo.MovieName + "_" + count;
                    button.Click += Button1_OnClick;
                    button.CssClass = "removeButton";
                    cell.Controls.Add(button);
                    row.Cells.Add(cell);
                    count++;
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

    private static TableCell addCell(String pText)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "cell";
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
}