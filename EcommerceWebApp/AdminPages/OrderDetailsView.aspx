<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPages/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="OrderDetailsView.aspx.cs" Inherits="EcommerceWebApp.AdminPages.OrderDetailsView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container py-4">
        <h3>Order Details</h3>
        <asp:Literal ID="ltMessage" runat="server" />
        <div class="mb-3">
            <asp:Label ID="lblOrderStatus" runat="server" CssClass="h5" />
        </div>
        <div class="mb-3">
            <asp:Label ID="lblPaymentStatus" runat="server" CssClass="h5" />
        </div>
        <div class="mb-3">
            <asp:Label ID="lblOrderDate" runat="server" CssClass="h6 text-muted" />
        </div>

        <asp:GridView ID="gvOrderDetails" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:TemplateField HeaderText="Product Image">
                    <ItemTemplate>
                        <asp:Image ID="imgProduct" runat="server" Width="80px" Height="80px" ImageUrl='<%# Eval("ImageUrl") ?? "~/images/placeholder.png" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="PriceAtOrder" HeaderText="Unit Price" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Line Total">
                    <ItemTemplate>
                        <%# String.Format("{0:C}", Convert.ToInt32(Eval("Quantity")) * Convert.ToDecimal(Eval("PriceAtOrder"))) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>
