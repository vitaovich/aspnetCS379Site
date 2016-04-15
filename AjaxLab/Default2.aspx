<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="AjaxLab_Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" />
         <asp:UpdatePanel runat="server" id="UpdatePanel" updatemode="Conditional">
         <Triggers>
            <asp:AsyncPostBackTrigger controlid="UpdateButton2" eventname="Click" />
         </Triggers>
             <ContentTemplate>
                 <asp:Label runat="server" id="DateTimeLabel1" />
                 <asp:Button runat="server" id="UpdateButton1" onclick="UpdateButton_Click" text="Update" />
             </ContentTemplate>
         </asp:UpdatePanel>
         <asp:UpdatePanel runat="server" id="UpdatePanel1" updatemode="Conditional">
             <ContentTemplate>
                 <asp:Label runat="server" id="DateTimeLabel2" />
                 <asp:Button runat="server" id="UpdateButton2" onclick="UpdateButton_Click" text="Update" />
             </ContentTemplate>
         </asp:UpdatePanel>
     </form>
</body>
</html>
