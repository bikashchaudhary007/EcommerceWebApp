<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPages/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="EcommerceWebApp.AdminPages.Payments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Payments Details</h2>
     <div class="mb-3">
        <strong>Total Payment Amount: </strong>
        <asp:Label ID="lblTotalAmount" runat="server" Text="$0.00" CssClass="h4 text-success"></asp:Label>
    </div>
    <asp:GridView ID="gvPayments" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" 
        GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPayments_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="PaymentId" HeaderText="Payment ID" />
            <asp:BoundField DataField="OrderId" HeaderText="Order ID" />
            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}" />
            <asp:BoundField DataField="PaymentMethod" HeaderText="Payment Method" />
            <asp:BoundField DataField="TransactionId" HeaderText="Transaction ID" />
            <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" />
        </Columns>
    </asp:GridView>
</asp:Content>
