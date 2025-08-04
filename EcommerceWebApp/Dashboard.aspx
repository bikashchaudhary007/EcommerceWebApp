<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EcommerceWebApp.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            min-height: 100vh;
        }

        .sidebar {
            min-width: 220px;
            max-width: 220px;
            background: #f8f9fa;
            height: 100vh;
            position: fixed;
            padding-top: 1rem;
        }

        .content {
            margin-left: 220px;
            padding: 1.5rem;
        }

        .nav-link.active {
            background-color: #e9f5ff;
            border-radius: .375rem;
        }

        .badge-complete {
            background-color: #d1e7dd;
            color: #0f5132;
        }

        .badge-cod {
            background-color: #f8d7da;
            color: #842029;
        }

        .table thead th {
            border-bottom: 2px solid #dee2e6;
        }

        .small-input {
            max-width: 220px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Sidebar -->
    <div class="sidebar d-flex flex-column">
        <div class="px-3 mb-4">
            <h5 class="mb-0">DeliCom</h5>
        </div>
        <ul class="nav flex-column px-2">
            <li class="nav-item mb-1">
                <a class="nav-link d-flex align-items-center active" href="#">
                    <span class="flex-grow-1">Dashboard</span>
                </a>
            </li>
            <li class="nav-item mb-1">
                <a class="nav-link d-flex align-items-center" href="#">Orders
                </a>
            </li>
            <li class="nav-item mb-1">
                <a class="nav-link" href="#">Products</a>
            </li>
            <li class="nav-item mb-1">
                <a class="nav-link" href="#">Category</a>
            </li>
            <li class="nav-item mb-1">
                <a class="nav-link" href="#">Customers</a>
            </li>
            <li class="nav-item mb-1">
                <a class="nav-link" href="#">Coupons</a>
            </li>
            <li class="nav-item mb-1">
                <a class="nav-link" href="#">Payment</a>
            </li>
            <li class="nav-item mb-1">
                <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />

            </li>

            <li class="nav-item mt-auto px-2">
                <small class="text-muted">Help & Support</small><br />
                <small class="text-muted">Settings</small>
            </li>
        </ul>
    </div>

    <!-- Main content -->
    <div class="content">
        <!-- Top bar -->
        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h4 class="mb-0">Orders</h4>
            </div>
            <div class="d-flex gap-3 flex-wrap">
                <div class="btn-group" role="group">
                    <button class="btn btn-outline-primary btn-sm active" type="button">All</button>
                    <button class="btn btn-outline-primary btn-sm" type="button">Shipping</button>
                    <button class="btn btn-outline-primary btn-sm" type="button">Completed</button>
                    <button class="btn btn-outline-primary btn-sm" type="button">Cancelled</button>
                </div>
                <div class="input-group small-input">
                    <span class="input-group-text"><i class="bi bi-search"></i></span>
                    <input type="text" class="form-control form-control-sm" placeholder="Search by ID, Name">
                </div>
                <select class="form-select form-select-sm small-input">
                    <option selected>Sort by</option>
                    <option value="1">Date</option>
                    <option value="2">Price</option>
                </select>
                <button class="btn btn-sm btn-outline-secondary">Filter</button>
                <div class="d-flex align-items-center gap-2">
                    <div class="me-2">
                        <input type="month" class="form-control form-control-sm" value="">
                    </div>
                    <button class="btn btn-sm btn-primary">Export</button>
                </div>
            </div>
        </div>

        <!-- Table -->
        <div class="card shadow-sm">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <table class="table mb-0 align-middle">
                        <thead class="bg-white">
                            <tr>
                                <th scope="col">
                                    <input type="checkbox" /></th>
                                <th scope="col">Product Name</th>
                                <th scope="col">Order ID</th>
                                <th scope="col">Customer Name</th>
                                <th scope="col">Date</th>
                                <th scope="col">QTY</th>
                                <th scope="col">Price</th>
                                <th scope="col">Payment Status</th>
                            </tr>
                        </thead>
                        <tbody>
                            <!-- example row -->
                            <tr>
                                <td>
                                    <input type="checkbox" /></td>
                                <td class="d-flex align-items-center gap-2">
                                    <img src="https://via.placeholder.com/40" alt="Denim Jacket" class="rounded" width="40" height="40">
                                    <div>
                                        <div class="fw-semibold">Denim Jacket</div>
                                        <div class="text-muted small">#ODD009</div>
                                    </div>
                                </td>
                                <td>#ODD009</td>
                                <td>Helen Smith</td>
                                <td>30 Nov 2024</td>
                                <td>2</td>
                                <td>$244</td>
                                <td>
                                    <span class="badge badge-complete rounded-pill py-2 px-3">Complete</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="checkbox" /></td>
                                <td class="d-flex align-items-center gap-2">
                                    <img src="https://via.placeholder.com/40" alt="Full Sleeve Shirt" class="rounded" width="40" height="40">
                                    <div>
                                        <div class="fw-semibold">Full Sleeve Shirt</div>
                                        <div class="text-muted small">#ODD008</div>
                                    </div>
                                </td>
                                <td>#ODD008</td>
                                <td>John William</td>
                                <td>29 Nov 2024</td>
                                <td>3</td>
                                <td>$124</td>
                                <td>
                                    <span class="badge badge-cod rounded-pill py-2 px-3">Cash On Delivery</span>
                                </td>
                            </tr>
                            <!-- more rows... -->
                        </tbody>
                    </table>
                </div>
            </div>
            <!-- pagination & summary -->
            <div class="card-footer d-flex justify-content-between align-items-center">
                <div class="small text-muted">Showing 1–9 of 220 entries</div>
                <nav aria-label="Page navigation">
                    <ul class="pagination pagination-sm mb-0">
                        <li class="page-item disabled"><a class="page-link" href="#">«</a></li>
                        <li class="page-item active"><a class="page-link" href="#">1</a></li>
                        <li class="page-item"><a class="page-link" href="#">2</a></li>
                        <li class="page-item"><a class="page-link" href="#">…</a></li>
                        <li class="page-item"><a class="page-link" href="#">26</a></li>
                        <li class="page-item"><a class="page-link" href="#">»</a></li>
                    </ul>
                </nav>
            </div>
        </div>
    </div>


</asp:Content>
