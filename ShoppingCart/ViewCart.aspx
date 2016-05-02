<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/MasterPage.master" AutoEventWireup="true" CodeFile="ViewCart.aspx.cs" Inherits="ShoppingCart_ViewCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../scripts/jquery-2.2.3.min.js"></script>
    <script type="text/javascript" src="../scripts/viewCart.js"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="heading" CssClass="Heading" runat="server" text="Shopping Cart"></asp:Label>
    <asp:PlaceHolder ID="Content" runat="server"></asp:PlaceHolder>
    <asp:Table ID="tbl" CssClass="table" GridLines="Both" runat="server"></asp:Table>

</asp:Content>

