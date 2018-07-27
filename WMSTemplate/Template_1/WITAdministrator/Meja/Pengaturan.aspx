<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Meja_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Meja
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
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="font-weight-bold text-muted">Nama</label>
                                <asp:TextBox ID="TextBoxNama" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="font-weight-bold text-muted">Jumlah Kursi</label>
                                <asp:TextBox ID="TextBoxJumlahKursi" runat="server" CssClass="form-control InputInteger"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="font-weight-bold text-muted">VIP</label>
                                <br />
                                <asp:CheckBox ID="CheckBoxVIP" runat="server" />
                            </div>
                            <div class="col-md-6">
                                <label class="font-weight-bold text-muted">Status</label>
                                <br />
                                <asp:CheckBox ID="CheckBoxStatus" runat="server" Checked="true" Enabled="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-footer">
            <asp:Button ID="ButtonSimpan" runat="server" CssClass="btn btn-success btn-const" Text="Tambah" OnClick="ButtonSimpan_Click" />
            <asp:Button ID="ButtonKembali" runat="server" CssClass="btn btn-danger btn-const" Text="Kembali" OnClick="ButtonKembali_Click" />
        </div>
    </div>
</asp:Content>

