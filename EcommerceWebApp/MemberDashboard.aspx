<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MemberDashboard.aspx.cs" Inherits="EcommerceWebApp.MemberDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container py-4">
        <h3>My Orders</h3>
        <asp:Literal ID="ltMessage" runat="server" />

        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
            OnRowCommand="gvOrders_RowCommand" DataKeyNames="OrderId">
            <Columns>
                <asp:BoundField DataField="OrderId" HeaderText="Order ID" ReadOnly="True" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" ReadOnly="True" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:C}" ReadOnly="True" />
                <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" ReadOnly="True" />
                <asp:BoundField DataField="ShippingAddress" HeaderText="Shipping Address" ReadOnly="True" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" CommandName="ViewOrder" CommandArgument='<%# Eval("OrderId") %>' Text="View" CssClass="btn btn-primary btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
