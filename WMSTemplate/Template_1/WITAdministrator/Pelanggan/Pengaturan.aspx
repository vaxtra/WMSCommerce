<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Pelanggan_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Pelanggan
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
            <div class="card">
                <div class="card-header bg-smoke">
                    <ul class="nav nav-tabs card-header-tabs">
                        <li class="nav-item"><a href="#tabProfil" id="Profil-tab" class="nav-link active" data-toggle="tab">Profil</a></li>
                        <li class="nav-item"><a href="#tabLengkap" id="Lengkap-tab" class="nav-link" data-toggle="tab">Lengkap</a></li>
                    </ul>
                </div>
                <div class="card-body">
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane active" id="tabProfil">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="form-label font-weight-bold text-muted">Grup</label>
                                        <asp:DropDownList ID="DropDownListGrupPelanggan" CssClass="select2" Width="100%" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label font-weight-bold text-muted">Nama</label>
                                        <asp:TextBox ID="TextBoxNamaLengkap" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <label class="form-label font-weight-bold text-muted">Alamat</label>
                                <asp:TextBox ID="TextBoxAlamat" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label class="form-label font-weight-bold text-muted">Email</label>
                                        <asp:TextBox ID="TextBoxEmail" TextMode="Email" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label font-weight-bold text-muted">Telepon</label>
                                        <asp:TextBox ID="TextBoxTeleponLain" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label font-weight-bold text-muted">Tanggal Lahir</label>
                                        <asp:TextBox ID="TextBoxTanggalLahir" CssClass="form-control Tanggal input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="form-label font-weight-bold text-muted">Status</label>
                                <br>
                                <asp:CheckBox ID="CheckBoxStatus" runat="server" Checked="true" />
                            </div>
                        </div>
                        <div class="tab-pane" id="tabLengkap">
                            <div class="form-group">
                                <label class="form-label font-weight-bold text-muted">Pegawai PIC</label>
                                <asp:DropDownList ID="DropDownListPenggunaPIC" CssClass="select2" Width="100%" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="form-label font-weight-bold text-muted">Username</label>
                                        <asp:TextBox ID="TextBoxUsername" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label font-weight-bold text-muted">Password</label>
                                        <asp:TextBox ID="TextBoxPassword" CssClass="form-control input-sm" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="form-label font-weight-bold text-muted">Handphone</label>
                                        <asp:TextBox ID="TextBoxHandphone" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label font-weight-bold text-muted">Deposit</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <asp:Label ID="LabelRupiahTambahanGaji" runat="server" CssClass="form-label" Font-Size="XX-Small" Font-Bold="true" Text="Rp."></asp:Label>
                                                </div>
                                            </div>
                                            <asp:TextBox ID="TextBoxDeposit" onfocus="this.select();" CssClass="form-control InputDesimal text-right input-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="form-label font-weight-bold text-muted">Catatan</label>
                                <asp:TextBox ID="TextBoxCatatan" TextMode="MultiLine" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonSimpan" runat="server" Text="Tambah" OnClick="ButtonSimpan_Click" CssClass="btn btn-success btn-const" ValidationGroup="groupPelanggan" />
                    <a href="Default.aspx" class="btn btn-danger btn-const">Batal</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

