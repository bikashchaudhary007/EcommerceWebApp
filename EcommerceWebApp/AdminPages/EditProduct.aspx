<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPages/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="EcommerceWebApp.AdminPages.EditProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <h3 class="mb-4">Edit Product</h3>
        <asp:Literal ID="ltMessage" runat="server" />
        <div class="card shadow-sm">
            <div class="card-body">
                <div runat="server" class="row g-3" enctype="multipart/form-data">
                    <!-- replicate the same fields as in create: -->
                    <div class="col-md-6">
                        <label class="form-label">Product Name</label>
                        <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Category</label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select" />
                    </div>
                    <div class="col-12">
                        <label class="form-label">Short Description</label>
                        <asp:TextBox ID="txtShortDescription" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                    </div>
                    <div class="col-12">
                        <label class="form-label">Long Description</label>
                        <asp:TextBox ID="txtLongDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" />
                    </div>
                    <div class="col-md-3">
                        <label class="form-label">Price</label>
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-3">
                        <label class="form-label">Stock Quantity</label>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-3 d-flex align-items-end">
                        <div class="form-check">
                            <asp:CheckBox ID="chkIsFeatured" runat="server" CssClass="form-check-input" />
                            <label class="form-check-label">Is Featured</label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Dimensions</label>
                        <asp:TextBox ID="txtDimensions" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Image URL</label>
                        <asp:TextBox ID="txtImageUrl" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Or Upload Image</label>
                        <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-12">
                        <asp:HiddenField ID="hfProductId" runat="server" />
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update Product" OnClick="btnUpdate_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
