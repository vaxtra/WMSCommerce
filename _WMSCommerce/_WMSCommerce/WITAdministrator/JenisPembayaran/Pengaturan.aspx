<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_JenisPembayaran_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Jenis Pembayaran
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-12 col-lg-6">
                    <div class="form-group">
                        <label class="form-label font-weight-bold text-muted">Nama</label>
                        <asp:TextBox ID="TextBoxNama" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-4">
                                <label class="form-label font-weight-bold text-muted">Jenis Beban Biaya</label>
                                <br />
                                <asp:DropDownList CssClass="select2" ID="DropDownListJenisBebanBiaya" Width="100%" runat="server" OnSelectedIndexChanged="DropDownListJenisBebanBiaya_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <br />
                            </div>
                            <div class="col-md-4">
                                <label class="form-label font-weight-bold text-muted">POS Akun</label>
                                <br />
                                <asp:DropDownList CssClass="select2" ID="DropDownListAkun" runat="server" Width="100%">
                                </asp:DropDownList>
                                <br />
                            </div>
                            <div class="col-md-4">
                                <label class="form-label font-weight-bold text-muted">Biaya (%)</label>
                                <asp:TextBox ID="TextBoxPersentaseBiaya" CssClass="form-control input-sm InputDesimal" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-footer">
            <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-const" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
            <asp:Button ID="ButtonKembali" CssClass="btn btn-danger btn-const" runat="server" Text="Kembali" OnClick="ButtonKembali_Click" />
        </div>
    </div>
</asp:Content>

