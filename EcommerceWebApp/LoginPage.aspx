<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="EcommerceWebApp.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center mb-4" style="min-height: calc(100vh - 100px);">
        <div class="card shadow-sm" style="max-width: 400px; width: 100%;">
            <div class="card-body">
                <h3 class="card-title text-center mb-3">Login</h3>

                <div>
                    <div class="mb-3">
                        <label for="TextBox1" class="form-label">Email</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Please enter your email." SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username" />
                    </div>
                    <div class="mb-3">
                        <label for="TextBox2" class="form-label">Password </label>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Please enter your password." SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        &nbsp;<asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password" />
                    </div>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
                </div>

                <div class="text-center mt-2">
                    <small>Don't have an account? <a href="SignUpPage.aspx">Sign up</a></small>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
