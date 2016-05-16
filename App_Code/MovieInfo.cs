using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MovieInfo
/// </summary>
[Serializable]
public class MovieInfo
{
    public string ID { get; private set; }
    public string MovieName { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    public int Rating { get; private set; }
    public double Price { get; private set; }
    public double Weight { get; private set; }
    public int Quantity { get; set; }

    public MovieInfo(string id, string movieName, string description, string imageUrl, int rating, double price, double weight)
    {
        ID = id;
        MovieName = movieName;
        Description = description;
        ImageUrl = imageUrl;
        Rating = rating;
        Price = price;
        Weight = weight;
        Quantity = 1;
    }
}