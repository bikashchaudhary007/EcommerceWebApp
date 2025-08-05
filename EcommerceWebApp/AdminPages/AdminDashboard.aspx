<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPages/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="EcommerceWebApp.AdminPages.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Hello 
          <strong>
              <asp:Literal ID="litUsername" runat="server" Text="AdminUser" /></strong>
    </h1>

    <div class="row">

        <div class="col-md-4 mb-3">
            <div class="card text-white bg-primary">
                <div class="card-body">
                    <h5 class="card-title">Placed New Orders</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="lblPlacedOrders" runat="server" Text="0"></asp:Label>
                    </p>
                </div>
            </div>
        </div>

        <div class="col-md-4 mb-3">
            <div class="card text-white bg-warning">
                <div class="card-body">
                    <h5 class="card-title">Shipped Orders</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="lblShippedOrders" runat="server" Text="0"></asp:Label>
                    </p>
                </div>
            </div>
        </div>

        <div class="col-md-4 mb-3">
            <div class="card text-white bg-info">
                <div class="card-body">
                    <h5 class="card-title">Processing Orders</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="lblProcessingOrders" runat="server" Text="0"></asp:Label>
                    </p>
                </div>
            </div>
        </div>

        <div class="col-md-4 mb-3">
            <div class="card text-white bg-success">
                <div class="card-body">
                    <h5 class="card-title">Delivered Orders</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="lblDeliveredOrders" runat="server" Text="0"></asp:Label>
                    </p>
                </div>
            </div>
        </div>

        <div class="col-md-4 mb-3">
            <div class="card text-white bg-danger">
                <div class="card-body">
                    <h5 class="card-title">Cancelled Orders</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="lblCancelledOrders" runat="server" Text="0"></asp:Label>
                    </p>
                </div>
            </div>
        </div>

        <div class="col-md-4 mb-3">
            <div class="card text-white bg-secondary">
                <div class="card-body">
                    <h5 class="card-title">Total Users</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="lblTotalUsers" runat="server" Text="0"></asp:Label>
                    </p>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
