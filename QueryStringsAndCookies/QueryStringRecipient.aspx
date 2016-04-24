<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryStringRecipient.aspx.cs" Inherits="QueryStringsAndCookies_QueryStringRecipient" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Query String Recipient page
        <br/>
        <br/>
    <div style="border-right: 2px solid; width: 300px; padding-right: 30px; border-top: 2px solid; padding-left: 30px; padding-bottom: 30px; border-left: 2px solid; padding-top: 30px; border-bottom: 2px solid;">
        <asp:Label ID="lblInfo" runat="server" EnableViewState="False" ></asp:Label>
    </div>
        <br/>
        <asp:Button ID="traceButton" OnClick="traceButton_OnClick" runat="server" Text="Get Trace" />
    </form>
</body>
</html>
