<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPages/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ProductCategory.aspx.cs" Inherits="EcommerceWebApp.AdminPages.ProductCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Create / Add -->
    <div class="col-md-6 mb-2">
        <div class="card shadow-sm" style="max-width: 700px;">
            <div class="card-body">
                <h4 class="card-title mb-3">New Product Category</h4>

                <div class="mb-3">
                    <label for="txtName" class="form-label">
                        Category Name
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategoryName" Display="Dynamic" ErrorMessage="Please enter category name." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </label>
                    &nbsp;<asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label for="txtDescription" class="form-label">
                        Description
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription" Display="Dynamic" ErrorMessage="Please enter short description." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </label>
                    &nbsp;<asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>

                <div class="form-check mb-3">
                    &nbsp;
                </div>

                <asp:Button ID="btnSave" runat="server" Text="Create Category" CssClass="btn btn-primary" OnClick="btnSave_Click" />

            </div>
        </div>

    </div>
    <!-- Create / Add END-->

    <!-- List / Manage -->
    <!-- List / Manage -->
    <div class="col-md-12">
        <div class="card shadow-sm">
            <div class="card-body">
                <h4 class="card-title mb-3">All Categories</h4>
                <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    DataKeyNames="CategoryId" OnRowEditing="gvCategories_RowEditing"
                    OnRowCancelingEdit="gvCategories_RowCancelingEdit" OnRowUpdating="gvCategories_RowUpdating"
                    OnRowDeleting="gvCategories_RowDeleting" EmptyDataText="No categories available.">
                    <Columns>
                        <asp:BoundField DataField="CategoryId" HeaderText="ID" ReadOnly="True" />

                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <%# Eval("CategoryName") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditName" runat="server" Text='<%# Bind("CategoryName") %>' CssClass="form-control" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <%# Eval("Description") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="form-control" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit"
                                    CssClass="btn btn-sm btn-outline-secondary me-1" CausesValidation="False">
                    Edit
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete"
                                    CssClass="btn btn-sm btn-outline-danger" CausesValidation="False"
                                    OnClientClick="return confirm('Are you sure you want to delete this category?');">
                    Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update"
                                    CssClass="btn btn-sm btn-success me-1" CausesValidation="False">
        Save
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel"
                                    CssClass="btn btn-sm btn-secondary" CausesValidation="False">
        Cancel
                                </asp:LinkButton>
                            </EditItemTemplate>

                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


                <asp:Literal ID="litListMessage" runat="server" />
            </div>
        </div>
    </div>

    <!-- List / Manage END-->


</asp:Content>
