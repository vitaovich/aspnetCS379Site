<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryStringSender.aspx.cs" Inherits="QueryStringsAndCookies_QueryStringSender" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListBox ID="lstItems" runat="server" Height="155px" Width="165px"></asp:ListBox><br />
        <br />
        <asp:CheckBox ID="chkDetails" runat="server" Text="Show full details" /><br />
        <br />
        <asp:Button ID="cmdGo" runat="server" OnClick="cmdGo_Click" Text="View Information"
            Width="165px" /><br />
        <br />
        <asp:Label ID="lblError" runat="server" EnableViewState="False"></asp:Label>
    </div>
    </form>
</body>
</html>
