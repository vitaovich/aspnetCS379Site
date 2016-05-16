<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Labs_BootstrapWebForms_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="bootstrap-3.3.6/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery-2.2.3.min.js"></script>
    <script type="text/javascript" src="bootstrap-3.3.6/js/bootstrap.min.js"></script>
    <title>Bootstrap Lab</title>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-inverse">
            <div class="container" >
                <div class="navbar-header">
                    <a href="#" class="navbar-brand">CSCD379 BootStrap Lab</a>
                </div>
                
                <ul class="nav navbar-nav">
                    <li class="active"><a href="#">Home</a></li>
                    <li><a class="dropdown-toggle" 
                            data-toggle="dropdown" role="button" 
                            aria-haspopup="true" 
                            aria-expanded="false">DropDown 
                        <span class="caret"></span>
                        </a>
                        <ul class="dropdown-menu" role="menu">
                            <li><a href="#">Example1</a></li>
                            <li><a href="#">Example2</a></li>
                            <li><a href="#">Example3</a></li>
                            <li><a href="#">Example4</a></li>
                       </ul>
                    </li>

                    <li ><a href="#">DropDown2</a></li>
                </ul>
                
                <div class="navbar-form navbar-right form-group">
                    <input type="text" class="form-control" placeholder="This doesn't do anything"/>
                    <button type="submit" class="btn btn-default">Search</button>
                </div>
            </div>
        </nav>
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <h1 class="">Rocky Plot</h1>
                    <p>Set in modern day 1970s, Rocky Balboa is a hard-living but failing prize fighter from an 
                        Italian neighborhood of Philadelphia, Pennsylvania. Between fights, he works as an enforcer for loan 
                        shark Tony Gazzo. The World Heavyweight Champion, Apollo Creed, announces plans to hold a match in 
                        Philadelphia during the upcoming United States Bicentennial.</p>
                </div>
                <div class="col-md-4">
                    <h1 class="">Rocky Plot</h1>
                    <p>Set in modern day 1970s, Rocky Balboa is a hard-living but failing prize fighter from an 
                        Italian neighborhood of Philadelphia, Pennsylvania. Between fights, he works as an enforcer for loan 
                        shark Tony Gazzo. The World Heavyweight Champion, Apollo Creed, announces plans to hold a match in 
                        Philadelphia during the upcoming United States Bicentennial.</p>
                </div>
                                <div class="col-md-4">
                    <h1 class="">Rocky Plot</h1>
                    <p>Set in modern day 1970s, Rocky Balboa is a hard-living but failing prize fighter from an 
                        Italian neighborhood of Philadelphia, Pennsylvania. Between fights, he works as an enforcer for loan 
                        shark Tony Gazzo. The World Heavyweight Champion, Apollo Creed, announces plans to hold a match in 
                        Philadelphia during the upcoming United States Bicentennial.</p>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
