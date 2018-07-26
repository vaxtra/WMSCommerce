<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Tempat_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Lokasi
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
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="card">
                    <div class="card-header bg-gradient-black">
                        <h5 class="font-weight-light">PROFIL</h5>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Kategori</label>
                            <asp:DropDownList ID="DropDownListKategoriTempat" CssClass="select2" Width="100%" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Kode</label>
                            <asp:TextBox ID="TextBoxKode" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Nama</label>
                            <asp:TextBox ID="TextBoxNama" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Alamat</label>
                            <asp:TextBox ID="TextBoxAlamat" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Email</label>
                            <asp:TextBox ID="TextBoxEmail" CssClass="form-control input-sm" runat="server" TextMode="Email"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Telepon 1</label>
                                    <asp:TextBox ID="TextBoxTelepon1" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Telepon 2</label>
                                    <asp:TextBox ID="TextBoxTelepon2" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Latitude</label>
                                    <asp:TextBox ID="TextBoxLatitude" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Longitude</label>
                                    <asp:TextBox ID="TextBoxLongitude" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Footer Print</label>
                            <asp:TextBox ID="TextBoxFooterPrint" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="card">
                    <div class="card-header bg-gradient-black">
                        <h5 class="font-weight-light">BIAYA TAMBAHAN TRANSAKSI</h5>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Keterangan 1</label>
                                    <asp:TextBox ID="TextBoxKeteranganBiayaTambahan1" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Biaya</label>
                                    <asp:TextBox ID="TextBoxBiayaTambahan1" CssClass="InputDesimal form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Keterangan 2</label>
                                    <asp:TextBox ID="TextBoxKeteranganBiayaTambahan2" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Biaya</label>
                                    <asp:TextBox ID="TextBoxBiayaTambahan2" CssClass="InputDesimal form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Keterangan 3</label>
                                    <asp:TextBox ID="TextBoxKeteranganBiayaTambahan3" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Biaya</label>
                                    <asp:TextBox ID="TextBoxBiayaTambahan3" CssClass="InputDesimal form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Keterangan 4</label>
                                    <asp:TextBox ID="TextBoxKeteranganBiayaTambahan4" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="font-weight-bold text-muted">Biaya</label>
                                    <asp:TextBox ID="TextBoxBiayaTambahan4" CssClass="InputDesimal form-control input-sm" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <asp:Button ID="ButtonSimpan" runat="server" Text="Tambah" OnClick="ButtonSimpan_Click" CssClass="btn btn-success btn-const" />
        <asp:Button ID="ButtonBatal" runat="server" Text="Batal" OnClick="ButtonBatal_Click" CssClass="btn btn-danger btn-const" />
    </div>
</asp:Content>

