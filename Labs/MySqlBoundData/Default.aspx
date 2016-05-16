<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Labs_MySqlBoundData_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Select a Continent to view countries.</h2>
        <br/>
        <asp:ListBox ID="lbCountry" runat="server"></asp:ListBox>
        <asp:Button ID="GetData" OnClick="btnGetEm_Click" runat="server" Text="Get Countries" />
        <br/>
        <br/>
        Country Name<asp:TextBox ID="txtCountryName" runat="server"></asp:TextBox>
        New Country Name<asp:TextBox ID="txtNewCountryName" runat="server"></asp:TextBox>
        <asp:Button ID="UpdateData" OnClick="btnAdd_Click" runat="server" Text="Update Data" />
        <br/>
        <br/>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>


