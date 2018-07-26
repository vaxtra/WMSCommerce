<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Maintenance.aspx.cs" Inherits="WITAdministrator_Produk_Maintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Maintenance Kombinasi Produk
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
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="row">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:Literal ID="LiteralResult" runat="server"></asp:Literal>
                    </div>

                    <div class="form-group">
                        <label>Kode Lama</label>
                        <asp:TextBox ID="TextBoxKombinasiProdukLama" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
                        <asp:Label ID="LabelKombinasiProdukLama" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>Kode Baru</label>
                        <asp:TextBox ID="TextBoxKombinasiProdukBaru" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
                        <asp:Label ID="LabelKombinasiProdukBaru" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="ButtonValidasi" runat="server" CssClass="btn btn-primary" Text="Validasi" OnClick="ButtonValidasi_Click" />

                        <asp:Button ID="ButtonProses" Visible="false" runat="server" CssClass="btn btn-primary" Text="Proses" OnClick="ButtonProses_Click" />
                        <asp:Button ID="ButtonCancel" Visible="false" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="ButtonCancel_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="form-group">
                <a href="/file_excel/template/(Format) Migrasi Kode Produk.xls">download Migrasi Kode Produk template</a>
                <br />
            </div>
            <div class="form-group">
                <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-8">
                        <asp:FileUpload ID="FileUploadMaintenance" runat="server" Width="100%" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="ButtonView" runat="server" CssClass="btn btn-success btn-cons proses" Text="View Data" OnClick="ButtonView_Click" />
                        <asp:Button ID="ButtonUpload" data-loading-text="Loading..." CssClass="btn btn-primary btn-cons proses" runat="server" Text="Upload" OnClick="ButtonUpload_Click" Enabled="false" />
                        <a href="/WITAdministrator/Produk/Default.aspx" class="btn btn-danger btn-cons">Kembali</a>
                    </div>
                </div>
            </div>
            <asp:Panel ID="PanelViewData" runat="server" Visible="false">
                <div class="form-group">
                    <div class="table-responsive">
                        <table class="table table-condensed table-hover table-bordered">
                            <thead>
                                <tr>
                                    <th class="hidden"></th>
                                    <th class="text-center" style="width: 2%">No</th>
                                    <th class="text-center">Produk Lama</th>
                                    <th class="text-center">Produk Baru</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="LiteralViewData" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

