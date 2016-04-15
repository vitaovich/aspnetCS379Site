<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AjaxLab_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CS379 Ajax Lab</title>
</head>
<body>
    <form id="form1" runat="server">
        <img src="lava_lamp.gif"/>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="background-color: cyan; padding: 20px;">
                    <asp:Label ID="timeLbl" runat="server" Font-Bold="True"></asp:Label>
                    <br/>
                    <br/>
                    <asp:Button ID="RefreshTime" runat="server" OnClick="RefreshTime_OnClick" Text="Refresh Time" />
                    <asp:HyperLink ID="Part2" NavigateUrl="Default2.aspx" runat="server">Part2 Link</asp:HyperLink>                  
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
