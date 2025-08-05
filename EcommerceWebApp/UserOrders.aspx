<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserOrders.aspx.cs" Inherits="EcommerceWebApp.UserOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container py-4">
        <h3>My Cart</h3>
        <asp:Literal ID="ltMessage" runat="server" />

        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False"
            OnRowEditing="gvCart_RowEditing" OnRowCancelingEdit="gvCart_RowCancelingEdit"
            OnRowUpdating="gvCart_RowUpdating" OnRowDeleting="gvCart_RowDeleting"
            CssClass="table table-bordered" DataKeyNames="CartItemId">
            <Columns>
                <asp:BoundField DataField="ProductName" HeaderText="Product" ReadOnly="True" />
                <asp:BoundField DataField="PriceAtAdd" HeaderText="Unit Price" DataFormatString="{0:C}" ReadOnly="True" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:TemplateField HeaderText="Added">
                    <ItemTemplate>
                        <%# Eval("AddedDate", "{0:yyyy-MM-dd HH:mm}") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Line Total">
                    <ItemTemplate>
                        <%# String.Format("{0:C}", Convert.ToInt32(Eval("Quantity")) * Convert.ToDecimal(Eval("PriceAtAdd"))) %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

        <div class="mt-3">
            <h5>Grand Total:
                <asp:Literal ID="ltGrandTotal" runat="server" /></h5>
            <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn btn-success" OnClick="btnPlaceOrder_Click" />
        </div>

        <asp:Panel ID="pnlCheckout" runat="server" Visible="false" CssClass="mt-4">
            <h4>Checkout</h4>
            <div class="mb-3">
                <label for="txtShippingAddress" class="form-label">Shipping Address</label>
                <asp:TextBox ID="txtShippingAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
            </div>
            <div class="mb-3">
                <label for="txtPaymentAmount" class="form-label">Amount to Pay</label>
                <asp:TextBox ID="txtPaymentAmount" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <asp:Button ID="btnConfirmOrder" runat="server" Text="Confirm Order & Pay" CssClass="btn btn-primary" OnClick="btnConfirmOrder_Click" />
        </asp:Panel>



    </div>
</asp:Content>
