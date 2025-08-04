<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPages/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="EcommerceWebApp.AdminPages.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <h3 class="mb-4">Create Product</h3>
        <asp:Literal ID="ltMessage" runat="server" />
        <div class="card shadow-sm">
            <div class="card-body">
                <div runat="server" class="row g-3" id="frmProduct">
                    <div class="col-md-6">
                        <label class="form-label" for="txtProductName">Product Name <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label" for="ddlCategory">Category <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select" />
                    </div>

                    <div class="col-12">
                        <label class="form-label" for="txtShortDescription">Short Description</label>
                        <asp:TextBox ID="txtShortDescription" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                    </div>

                    <div class="col-12">
                        <label class="form-label" for="txtLongDescription">Long Description</label>
                        <asp:TextBox ID="txtLongDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" />
                    </div>

                    <div class="col-md-3">
                        <label class="form-label" for="txtPrice">Price</label>
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" TextMode="SingleLine" />
                    </div>

                    <div class="col-md-3">
                        <label class="form-label" for="txtStock">Stock Quantity</label>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="SingleLine" />
                    </div>

                    <div class="col-md-3 d-flex align-items-end">
                        <div class="form-check">
                            <asp:CheckBox ID="chkIsFeatured" runat="server" CssClass="form-check-input" />
                            <label class="form-check-label" for="chkIsFeatured">Is Featured</label>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <label class="form-label" for="txtDimensions">Dimensions</label>
                        <asp:TextBox ID="txtDimensions" runat="server" CssClass="form-control" />
                    </div>


                    <div class="col-md-6">
                        <label class="form-label">Or Upload Image</label>
                        <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" />
                        <div class="form-text">Allowed: .jpg .jpeg .png; max 2MB.</div>
                    </div>


                    <div class="col-12">
                        <asp:Button ID="btnSave" runat="server" Text="Create Product" CssClass="btn btn-primary" OnClick="btnSave_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
