<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="EcommerceWebApp.AboutUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .about-section {
            margin-left: 50px;
            margin-top: 30px;
        }

        .about-image {
            width: 100%;
            height: auto;
            border-radius: 10px;
        }

        .about-text {
            margin-top: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container about-section">
        <div class="row">
            <div class="col-md-6">
                <img src="assets/images/team-image-1.jpeg" class="about-image" alt="Our Team" />
            </div>
            <div class="col-md-6 about-text">
                <h2>About Us</h2>
                <p>
                    Welcome to <strong>Our Ecommerce Website</strong>! We are dedicated to offering the best quality products and customer service.
                </p>
                <p>
                    Our mission is to bring convenience and satisfaction to your online shopping experience.
                </p>
            </div>
        </div>

        <div class="row mt-5">
            <div class="col-md-4">
                <img src="assets/images/mission-image-1.jpg" class="about-image" alt="Mission" />
                <h5 class="mt-2">Our Mission</h5>
                <p>To deliver quality products at competitive prices to your doorstep.</p>
            </div>
            <div class="col-md-4">
                <img src="assets/images/vision-image-1.jpg" class="about-image" alt="Vision" />
                <h5 class="mt-2">Our Vision</h5>
                <p>To become the leading online marketplace known for trust and innovation.</p>
            </div>
            <div class="col-md-4">
                <img src="assets/images/values-image-1.jpg" class="about-image" alt="Values" />
                <h5 class="mt-2">Our Values</h5>
                <p>Integrity, customer focus, and continuous improvement guide everything we do.</p>
            </div>
        </div>
    </div>
</asp:Content>
