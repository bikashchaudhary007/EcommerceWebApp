<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductView.aspx.cs" Inherits="EcommerceWebApp.ProductView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">

        <h3>Product Details</h3>
        <asp:Literal ID="ltMessage" runat="server" />

        <div class="row">
            <div class="col-md-4">
                <asp:Image ID="imgProduct" runat="server" CssClass="img-fluid border" />
            </div>
            <div class="col-md-8">
                <dl class="row">
                    <dt class="col-sm-4">Name</dt>
                    <dd class="col-sm-8">
                        <asp:Label ID="lblName" runat="server" />
                    </dd>

                    <dt class="col-sm-4">Category</dt>
                    <dd class="col-sm-8">
                        <asp:Label ID="lblCategory" runat="server" />
                    </dd>

                    <dt class="col-sm-4">Price</dt>
                    <dd class="col-sm-8">
                        <asp:Label ID="lblPrice" runat="server" />
                    </dd>

                    <dt class="col-sm-4">Stock</dt>
                    <dd class="col-sm-8">
                        <asp:Label ID="lblStock" runat="server" />
                    </dd>

                    <dt class="col-sm-4">Featured</dt>
                    <dd class="col-sm-8">
                        <asp:Label ID="lblFeatured" runat="server" />
                    </dd>

                    <dt class="col-sm-4">Dimensions</dt>
                    <dd class="col-sm-8">
                        <asp:Label ID="lblDimensions" runat="server" />
                    </dd>

                    <dt class="col-sm-4">Short Description</dt>
                    <dd class="col-sm-8">
                        <asp:Label ID="lblShortDesc" runat="server" />
                    </dd>

                    <dt class="col-sm-4">Long Description</dt>
                    <dd class="col-sm-8">
                        <asp:Label ID="lblLongDesc" runat="server" />
                    </dd>
                </dl>

                <div class="d-flex align-items-center gap-2 mb-3">
                    <div class="me-2">
                        <label for="txtQuantity" class="form-label mb-0">Quantity</label>
                        <asp:TextBox ID="txtQuantity" runat="server" Text="1" CssClass="form-control" Style="width: 80px;" />
                    </div>
                    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-primary" OnClick="btnAddToCart_Click" />
                </div>

                <asp:Button runat="server" Text="Back to Home" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
</asp:Content>
