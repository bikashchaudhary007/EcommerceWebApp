<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ContactPage.aspx.cs" Inherits="EcommerceWebApp.ContactPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>
        .contact-form-container {
            margin-top: 40px;
            margin-bottom: 60px;
        }

        .form-heading {
            text-align: center;
            margin-bottom: 30px;
        }

        .form-control {
            border-radius: 0.375rem;
        }

        .success-message {
            color: green;
            margin-top: 20px;
            display: block;
            text-align: center;
        }

        .btn-primary {
            width: 100%;
        }

         .faq-answer {
            display: none;
            margin-top: 5px;
            color: #333;
        }

        .faq-item h4 {
            cursor: pointer;
            color: #007bff;
            margin-bottom: 0;
        }

        .faq-item {
            margin-bottom: 15px;
        }
        .faq-container {
             max-width: 800px;
            margin: 40px auto;
            padding: 20px;
            background-color: #f8f9fa;
            border-radius: 0.375rem;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container contact-form-container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <h2 class="form-heading">Contact Us</h2>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtName" Text="Name:" />
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ControlToValidate="txtName" runat="server" ErrorMessage="Name is required." ForeColor="Red" Display="Dynamic" />
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email:" />
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" ErrorMessage="Email is required." ForeColor="Red" Display="Dynamic" />
                    <asp:RegularExpressionValidator ControlToValidate="txtEmail" runat="server"
                        ErrorMessage="Invalid email format."
                        ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$"
                        ForeColor="Red" Display="Dynamic" />
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtSubject" Text="Subject:" />
                    <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ControlToValidate="txtSubject" runat="server" ErrorMessage="Subject is required." ForeColor="Red" Display="Dynamic" />
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtMessage" Text="Message:" />
                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" />
                    <asp:RequiredFieldValidator ControlToValidate="txtMessage" runat="server" ErrorMessage="Message is required." ForeColor="Red" Display="Dynamic" />
                </div>

                <asp:Button ID="btnSubmit" runat="server" Text="Send" CssClass="btn btn-primary mt-2" OnClick="btnSubmit_Click" />

                <asp:Label ID="lblResult" runat="server" CssClass="success-message" Visible="false" />
            </div>
        </div>
    </div>


    <div class="faq-container">
     <h2>Frequently Asked Questions</h2>

    <div class="faq-item">
        <h4 onclick="toggleAnswer(this)">What is your return policy?</h4>
        <p class="faq-answer">We accept returns within 30 days of purchase with original packaging.</p>
    </div>

    <div class="faq-item">
        <h4 onclick="toggleAnswer(this)">Do you ship internationally?</h4>
        <p class="faq-answer">Yes, we ship to most countries worldwide. Shipping fees may vary.</p>
    </div>

    <div class="faq-item">
        <h4 onclick="toggleAnswer(this)">How can I track my order?</h4>
        <p class="faq-answer">Once your order is shipped, you’ll receive an email with tracking info.</p>
    </div>
        </div>

    <script>
        function toggleAnswer(element) {
            const answer = element.nextElementSibling;
            answer.style.display = (answer.style.display === 'block') ? 'none' : 'block';
        }
    </script>
</asp:Content>
