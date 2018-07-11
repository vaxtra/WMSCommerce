<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Produk.aspx.cs" Inherits="WITAdministrator_Import_Produk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Import Excel Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
            <div class="card">
                <div class="card-header bg-gradient-black">
                    <h5 class="font-weight-light">IMPORT PRODUK</h5>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <a href="/file_excel/template/(Format) Produk.xls">download template</a>
                    </div>
                    <div class="form-group">
                        <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
                    </div>
                    <div class="form-group">
                        <label class="form-label label-warning">Jika ada produk duplikat dengan database</label>
                        <div style="margin-left: 20px;">
                            <asp:RadioButtonList ID="RadioButtonListJenisImport" runat="server" CssClass="radio">
                                <asp:ListItem Text="Tambah stok lama dengan stok baru" Value="2" Selected="True" />
                                <asp:ListItem Text="Ganti jumlah stok dengan yang baru" Value="1" />
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:FileUpload ID="FileUploadProduk" runat="server" CssClass="form-control" Width="100%" />
                    </div>
                    <div class="form-group">
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonUpload" CssClass="btn btn-success btn-const" runat="server" Text="Upload" OnClick="ButtonUpload_Click" />
                    <asp:Button ID="ButtonKeluar" CssClass="btn btn-danger btn-const" runat="server" Text="Keluar" OnClick="ButtonKeluar_Click" />
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
            <div class="card h-100">
                <div class="card-header bg-gradient-black">
                    <h5 class="font-weight-light">IMPORT CHECK EXCEL PRODUCT DUPLICATE</h5>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <asp:FileUpload ID="FileUploadChecker" runat="server" CssClass="form-control" Width="100%" />
                    </div>
                    <div class="form-group">
                        <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                            <asp:Literal ID="LiteralChecker" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonUploadChecker" CssClass="btn btn-primary btn-const" runat="server" Text="Upload" OnClick="ButtonUploadChecker_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

