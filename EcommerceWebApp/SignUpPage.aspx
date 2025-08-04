<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SignUpPage.aspx.cs" Inherits="EcommerceWebApp.SignUpPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center mb-4" style="min-height: calc(100vh - 100px);">
        <div class="card shadow-sm" style="max-width: 500px; width: 100%;">
            <div class="card-body">
                <h3 class="card-title text-center mb-3">Sign Up</h3>

                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                    CssClass="alert alert-danger" HeaderText="Please fix the following:"
                    DisplayMode="BulletList" />

                <div class="mb-3">
                    <label for="txtFullName" class="form-label">Full Name</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter full name" />
                    <asp:RequiredFieldValidator ID="rfvFullName" runat="server"
                        ControlToValidate="txtFullName" ErrorMessage="Full name required."
                        Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
                </div>

                <div class="mb-3">
                    <label for="txtAddress" class="form-label">Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter your address" />
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server"
                        ControlToValidate="txtAddress" ErrorMessage="Address required."
                        Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
                </div>

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="you@example.com" />
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="Email required."
                        Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Invalid email format."
                        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                        Display="Dynamic" ForeColor="Red" />
                </div>

                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"
                        TextMode="Password" placeholder="Enter password" />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ControlToValidate="txtPassword" ErrorMessage="Password required."
                        Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
                </div>

                <div class="mb-3">
                    <label for="txtConfirmPassword" class="form-label">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control"
                        TextMode="Password" placeholder="Repeat password" />
                    <asp:CompareValidator ID="cvPassword" runat="server"
                        ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"
                        ErrorMessage="Passwords do not match."
                        Display="Dynamic" ForeColor="Red" />
                </div>

                <asp:Button ID="btnRegister" runat="server" Text="Sign Up"
                    CssClass="btn btn-success w-100" OnClick="btnRegister_Click" />

                <div class="text-center mt-3">
                    <small>Already have an account? <a href="LoginPage.aspx">Login</a></small>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
