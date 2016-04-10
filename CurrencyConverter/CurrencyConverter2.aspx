<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="CurrencyConverter2.aspx.cs" Inherits="CurrencyConverter2" %>
<!DOCTYPE html >
<html>
  <head>
    <title>Currency Converter</title>
      <link href="StyleSheet.css" rel="stylesheet">
  </head>
  <body>
    <form ID="Form1" method="post" runat="server">
      <div id="main">
        Convert:
        <input type="text" ID="US" runat="server"/>&nbsp; U.S. dollars to &nbsp;
        <select ID="Currency" runat="server" />
        <br /><br />
        <input type="submit" value="OK" ID="Convert" runat="server" OnServerClick="Convert_ServerClick" />
        <input type="submit" value="Show Graph" ID="ShowGraph" runat="server" OnServerClick="ShowGraph_ServerClick" />
        <input type="submit" value="Redirect" ID="Redirectbutton" runat="server" OnServerClick="Redirectbutton_Click" />
          <br /><br />
          <img ID="Graph" alt="Currency Graph" src="" runat="server" />
        <br /><br />
        <p ID="Result" src="" runat="server"></p>
      </div>
    </form>
  </body>
</html>
