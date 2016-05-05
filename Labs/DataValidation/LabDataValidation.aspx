<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabDataValidation.aspx.cs" Inherits="Labs_LabDataValidation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        		<script type="text/javascript">
<!--
function ValidatePie(objSource, objArgs)
{
    // Get value.
    var pie = objArgs.Value;//.substr(0, 3);
     objArgs.IsValid = true;

    // Check value and return result.
    
    if (pie === "pie")
    {
        objArgs.IsValid = true;
        return;
    }
    else
    {
        objArgs.IsValid = false;
        return;
    }
}
// -->
		</script>
        <link rel="stylesheet" href="StyleSheet.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style=" text-align: center; ">
        Date: <asp:TextBox ID="Date" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="vldDate" CssClass="errors" runat="server"  ControlToValidate="Date" ErrorMessage="Date is required."></asp:RequiredFieldValidator>
        <br />
        Email: <asp:TextBox ID="email" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionEmailValidator" CssClass="errors" runat="server" ValidationExpression=".+@.+" ControlToValidate="email" ErrorMessage="Email is not valid"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="errors" runat="server" ControlToValidate="email" ErrorMessage="Email is required."></asp:RequiredFieldValidator>
        <br />
         Type in pie: <asp:TextBox ID="Pie" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="CustomValidator1" runat="server"  CssClass="errors"
            ErrorMessage="Type in the word 'pie'." ValidateEmptyText="False"
					ControlToValidate="Pie" ClientValidationFunction="MyCustomValidation" OnServerValidate="vldCode_ServerValidate"></asp:CustomValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="errors" runat="server" ControlToValidate="Pie" ErrorMessage="Pie is required."></asp:RequiredFieldValidator>

        <br />
        				<asp:Button id="cmdSubmit" 
					runat="server" Width="120px" Text="Submit" OnClick="cmdSubmit_Click"></asp:Button>
        <br />
        <asp:Label ID="lblMessage" CssClass="errors" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
