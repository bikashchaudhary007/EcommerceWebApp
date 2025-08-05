<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EcommerceWebApp.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
    #carouselExampleCaptions {
        height: 50vh;
        overflow: hidden;
    }

    #carouselExampleCaptions .carousel-item img {
        height: 50vh;
        object-fit: cover;
        width: 100%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Image Carosoule --%>

    <div id="carouselExampleCaptions" class="carousel slide" data-bs-ride="carousel">
        <div class="carousel-indicators">
            <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
            <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="1" aria-label="Slide 2"></button>
            <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="2" aria-label="Slide 3"></button>
        </div>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="assets/images/hand-image-1.jpeg" class="d-block w-100" alt="...">
                <div class="carousel-caption d-none d-md-block">
                    <h5>First slide label</h5>
                    <p>Some representative placeholder content for the first slide.</p>
                </div>
            </div>
            <div class="carousel-item">
                <img src="assets/images/hand-image-2.jpeg" class="d-block w-100" alt="...">
                <div class="carousel-caption d-none d-md-block">
                    <h5>Second slide label</h5>
                    <p>Some representative placeholder content for the second slide.</p>
                </div>
            </div>
            <div class="carousel-item">
                <img src="assets/images/hand-image-3.jpeg" class="d-block w-100" alt="...">
                <div class="carousel-caption d-none d-md-block">
                    <h5>Third slide label</h5>
                    <p>Some representative placeholder content for the third slide.</p>
                </div>
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>


    <%-- Image Carosoule End --%>


    <div class="container py-4">
    <h3 class="mb-4">Our Products</h3>
    <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4">
        <%-- Example of a product card, repeat this dynamically from your data source --%>
        <asp:Repeater ID="rptProducts" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100">
                       <img src='<%# ResolveImageUrl(Eval("ImageUrl")) %>' class="card-img-top" alt='<%# Eval("ProductName") %>' style="height: 200px; object-fit: cover;" />

                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title"><%# Eval("ProductName") %></h5>
                            <p class="card-text text-truncate"><%# Eval("ShortDescription") %></p>
                            <p class="card-text fw-bold mt-auto"><%# String.Format("{0:C}", Eval("Price")) %></p>
                            <a href='ProductView.aspx?id=<%# Eval("ProductId") %>' class="btn btn-primary mt-2">View Details</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

</asp:Content>
