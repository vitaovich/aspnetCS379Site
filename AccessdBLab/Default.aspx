<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AccessdBLab_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        <asp:Button ID="Redirect" PostBackUrl="CheckOutPage.aspx" Text="Redirect Crossback Page" runat="server" />
    </div>
    </form>
</body>
</html>
