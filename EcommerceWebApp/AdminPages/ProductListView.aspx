<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPages/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="EcommerceWebApp.AdminPages.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <h3 class="mb-4">Product List</h3>
        <asp:Literal ID="ltMessage" runat="server" />

        <div class="mb-3">
            <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-success" Text="Add New Product" OnClick="btnAddNew_Click" />
        </div>

        <asp:GridView ID="gvProducts" runat="server" CssClass="table table-bordered table-striped"
            AutoGenerateColumns="False" OnRowCommand="gvProducts_RowCommand" DataKeyNames="ProductId">
            <Columns>
                <asp:BoundField DataField="ProductId" HeaderText="#">
                    <ItemStyle Width="50px" />
                </asp:BoundField>

                <asp:BoundField DataField="ProductName" HeaderText="Name" />
                <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                <asp:BoundField DataField="StockQuantity" HeaderText="Stock" />
                <asp:TemplateField HeaderText="Featured" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("IsFeatured")) ? "<span class='badge bg-success'>Yes</span>" : "<span class='badge bg-secondary'>No</span>" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:Image ID="imgThumb" runat="server" Width="50px" Height="50px"
                            ImageUrl='<%# ResolveUrl(Eval("ImageUrl") != null ? Eval("ImageUrl").ToString() : "~/images/placeholder.png") %>' CssClass="img-thumbnail" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <div class="btn-group" role="group">
                            <asp:Button runat="server" CommandName="View" CommandArgument='<%# Eval("ProductId") %>' CssClass="btn btn-sm btn-info" Text="View" />
                            <asp:Button runat="server" CommandName="Edit" CommandArgument='<%# Eval("ProductId") %>' CssClass="btn btn-sm btn-warning" Text="Edit" />
                           <asp:Button runat="server" CommandName="DoDelete" CommandArgument='<%# Eval("ProductId") %>' CssClass="btn btn-sm btn-danger" Text="Delete" OnClientClick="return confirm('Are you sure?');" />

                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
