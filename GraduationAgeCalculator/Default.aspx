<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vitaliy's GAC</title>
</head>
<body style="background-color: #66FFFF">
    <form id="form1" runat="server">
        <asp:Image ID="banner" runat="server" ImageUrl="./mybanner.png" ResizeMode="Fit" Width="100%" Height="100%" />
        <div>
        <asp:Label ID="currentDate" runat="server" Text="" style="font-weight: 700"></asp:Label>
        <br />
        <br />
        How old will you be when you graduate?<br />
        <br />
        Enter your dates:<br />
        <br />
        <div class="auto-style1" style="float: left; text-align: right">
            Birthdate: <br />
            Month:<asp:TextBox ID="bMonth" runat="server"></asp:TextBox>
            <br />
            Day:<asp:TextBox ID="bDay" runat="server"></asp:TextBox>
            <br />
            Year:<asp:TextBox ID="bYear" runat="server"></asp:TextBox>
        </div>
        <div style="float: left; text-align: right; left: 150px; position: relative">
            Graduation date:
            <br />Month<asp:TextBox ID="gMonth" runat="server"></asp:TextBox>
            <br />Day:<asp:TextBox ID="gDay" runat="server"></asp:TextBox>
            <br />Year<asp:TextBox ID="gYear" runat="server"></asp:TextBox>
        </div>
            <br />
            <br />
            <br />
            <br />
        <br />

        <asp:Button ID="getAge" runat="server" OnClick="Button1_Click" Text="Get age" style="float: right"/>
            <br />
        <asp:Label ID="ageResult" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
