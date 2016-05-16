<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/MasterPage.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="ShoppingCart_Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" CssClass="Heading" Text="Checkout"></asp:Label>
    <br/>
    <div class="summary">
        <asp:Label ID="Label2" runat="server" CssClass="titles" Text="Order Summary"></asp:Label>
        <br/>
        <br/>
        <asp:Label ID="items" runat="server" CssClass="dets" Text="Items:  "></asp:Label>
        <br/>
        <asp:Label ID="Quantity" runat="server" CssClass="dets" Text="Quantity:  "></asp:Label>
        <br/>
        <asp:Label ID="weight" runat="server" CssClass="dets" Text="Weight:  "></asp:Label>
        <br />
        <br />
        <asp:Label ID="orderTotal" runat="server" CssClass="dets" Text="Order Total:  "></asp:Label>
    </div>
    <div style="display: inline-block; float: right; width:40%">
            <asp:Label ID="lblOrderNum" style=" color: white;font-family: sans-serif; font-size: 12px;" runat="server" Text="Order number: "></asp:Label>
        <br/>
        <br/>
            <asp:Label ID="lblitemCost" style=" color: white;font-family: sans-serif;font-size: 12px;" runat="server" Text="Item Total Cost: "></asp:Label>
                <br/>
            <asp:Label ID="lblShippingCost" style=" color: white;font-family: sans-serif;font-size: 12px;" runat="server" Text="Shipping: "></asp:Label>
                        <br/>
        <br/>
            <asp:Label ID="lblTotal" style=" color: white; font-family: sans-serif;font-size: 12px;" runat="server" Text="Total: "></asp:Label>
    </div>


    <div class="summary">
        <asp:Label ID="Label3" runat="server" CssClass="titles" Text="Shipping Information"></asp:Label>  
        <br/>
        <br/>
        <asp:Label ID="Name" runat="server" CssClass="dets" Text="Name: "></asp:Label>
        <asp:TextBox ID="TName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="error" ControlToValidate="TName" ErrorMessage="Name is required."></asp:RequiredFieldValidator>

                <br/>
        <asp:Label ID="Street" runat="server" CssClass="dets" Text="Street: "></asp:Label>
        <asp:TextBox ID="TStreet" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="error" ControlToValidate="TStreet" ErrorMessage="Street is required."></asp:RequiredFieldValidator>

                <br/>
        <asp:Label ID="City" runat="server" CssClass="dets" Text="City: "></asp:Label>
        <asp:TextBox ID="TCity" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="error" ControlToValidate="TCity" ErrorMessage="City is required."></asp:RequiredFieldValidator>

                <br/>
        <asp:Label ID="State" runat="server" CssClass="dets" Text="State: "></asp:Label>
        <asp:TextBox ID="TState" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="error" ControlToValidate="TState" ErrorMessage="State is required."></asp:RequiredFieldValidator>

                <br/>
        <asp:Label ID="Zip" runat="server" CssClass="dets" Text="Zip: "></asp:Label>
        <asp:TextBox ID="TZip" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredZip" runat="server" CssClass="error" ControlToValidate="TZip" ErrorMessage="Zip code is required."></asp:RequiredFieldValidator>
                <br/>
        <asp:Label ID="Email" runat="server" CssClass="dets" Text="Email: "></asp:Label>
        <asp:TextBox ID="TEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="error" ControlToValidate="TEmail" ErrorMessage="Email is required."></asp:RequiredFieldValidator>
    </div>
    <br/>
    <asp:HyperLink ID="ContinueShop" CssClass="removeButton" NavigateUrl="Default.aspx" runat="server">Continue Shopping</asp:HyperLink>
    <asp:Button ID="SubmitOrder" CssClass="removeButton" OnClick="SubmitOrder_OnClick" runat="server" Text="Submit Order" />
</asp:Content>

