<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPages/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ManageOrders.aspx.cs" Inherits="EcommerceWebApp.AdminPages.ManageOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <h2 class="mb-4">Manage Placed Orders</h2>

   <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
    OnRowEditing="gvOrders_RowEditing"
    OnRowCancelingEdit="gvOrders_RowCancelingEdit"
    OnRowUpdating="gvOrders_RowUpdating"
    DataKeyNames="OrderId">
    <Columns>
        <asp:BoundField DataField="OrderId" HeaderText="Order ID" ReadOnly="true" />
        <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
        <asp:BoundField DataField="OrderDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
       
        <asp:ImageField DataImageUrlField="ImageUrl" HeaderText="Image">
    <ControlStyle Width="60px" Height="60px" />
</asp:ImageField>

        
        <asp:BoundField DataField="TotalAmount" HeaderText="Total" DataFormatString="{0:C}" />
        
        <asp:TemplateField HeaderText="Order Status">
            <EditItemTemplate>
                <asp:DropDownList ID="ddlOrderStatus" runat="server">
                    <asp:ListItem Text="New" Value="New" />
                    <asp:ListItem Text="Processing" Value="Processing" />
                    <asp:ListItem Text="Shipped" Value="Shipped" />
                    <asp:ListItem Text="Delivered" Value="Delivered" />
                    <asp:ListItem Text="Cancelled" Value="Cancelled" />
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <%# Eval("OrderStatus") %>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:BoundField DataField="PaymentStatus" HeaderText="Payment" />
        <asp:CommandField ShowEditButton="True" />
        <asp:HyperLinkField Text="View" DataNavigateUrlFields="OrderId" DataNavigateUrlFormatString="OrderDetailsView.aspx?orderId={0}" HeaderText="Details" />
    </Columns>
</asp:GridView>

</asp:Content>
