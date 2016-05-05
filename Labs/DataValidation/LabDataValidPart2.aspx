<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabDataValidPart2.aspx.cs" Inherits="Labs_DataValidation_LabDataValidPart2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="StyleSheet.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
            Account number that checksums to 0:
	    <asp:TextBox id="accntNum" runat="server"></asp:TextBox>&nbsp;
	    <asp:RangeValidator id="IntValidator" runat="server" ErrorMessage="This Number Is Not An Int" CssClass="errors" MaximumValue="2147483647" MinimumValue="0" ControlToValidate="accntNum" Type="Integer" EnableClientScript="False"></asp:RangeValidator><br />
        <br />
		<br />
		<asp:Button id="submit" runat="server" Text="submit" OnClick="cmdOK_Click"></asp:Button><br />
		<br />
		<asp:Label id="lblMessage" runat="server" EnableViewState="False"></asp:Label>
    </div>
    </form>
</body>
</html>
