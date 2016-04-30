<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SalesReport.SalesReportDefault" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblStatus" runat="server"  Text=""></asp:Label>
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" font-size="30px" Text="Sales Report" F BackColor="pink"></asp:Label>
        <br/>
        <asp:PlaceHolder ID="Customer_Info" runat="server"></asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
