<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/MasterPage.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="ShoppingCart_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
          <div class="left_part">
              <asp:Image ID="Image1" Resize="Fit" Width="100%" Height="100%" runat="server" />
          </div> 
          <div class="right_part">
            <asp:Label ID="MovieName" CssClass="MovieHeading" runat="server"></asp:Label>
            <br/>
            <br/>
            <asp:Label ID="Description" CssClass="details" runat="server"></asp:Label>
            <br/>
              <br/>
              <asp:Label ID="Ratings" CssClass="details" runat="server"></asp:Label>
            <br/>
              <br/>
            <asp:Button ID="AddToCart" CssClass="pageButtons" OnClick="AddToCart_OnClick" runat="server" Text="Add to cart" />
            <asp:Button ID="ViewCart" CssClass="pageButtons" runat="server" OnClick="ViewCart_OnClick" Text="View cart" />                     
          </div> 
</asp:Content>

