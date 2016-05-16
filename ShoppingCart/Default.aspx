<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ShoppingCart_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:PlaceHolder ID="content" runat="server"></asp:PlaceHolder>
    <asp:Label ID="lbl" CssClass="details" runat="server" Text=""></asp:Label>
</asp:Content>

