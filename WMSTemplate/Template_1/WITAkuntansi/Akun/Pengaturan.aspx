<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAkuntansi_Akun_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Konfigurasi Akun & Akun Grup
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
        <asp:Panel runat="server" ID="PanelAkunGrup">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-4">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Akun Grup
                    <asp:Label ID="LabelTitleAkunGrup" runat="server"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="form-label bold">Header</label>
                            <br />
                            <asp:DropDownList ID="DropDownListAkuntansiGrupAkunTambah" CssClass="select2" Width="100%" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="form-label bold">Posisi Akun</label>
                                    <br />
                                    <asp:DropDownList ID="DropDownListPosisiAkun" CssClass="select2" Width="100%" runat="server">
                                        <asp:ListItem Value="1" Text="Aktiva"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Pasiva"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label bold">Posisi Saldo</label>
                                    <br />
                                    <asp:DropDownList ID="DropDownListPosisiDebitKredit" CssClass="select2" Width="100%" runat="server">
                                        <asp:ListItem Value="1" Text="Debit"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Kredit"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="form-label bold">Nama</label>
                            <asp:TextBox ID="TextBoxNamaAkunGrup" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                        <asp:Button ID="ButtonOkAkunGrup" CssClass="btn btn-success btn-sm" runat="server" Text="Tambah" OnClick="ButtonOkAkunGrup_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-danger btn-sm" Text="Keluar" OnClick="ButtonKeluar_Click" />
                    </div>
                </div>
            </div>

        </asp:Panel>
        <asp:Panel runat="server" ID="PanelAkun">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-4">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Akun
                        <asp:Label ID="LabelTitleAkun" runat="server"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="form-label bold">Header</label>
                            <asp:DropDownList ID="DropDownListAkuntansiGrupAkun" Width="100%" CssClass="select2" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label class="form-label bold">Kode</label>
                            <asp:TextBox ID="TextBoxKode" runat="server" Width="100%" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label bold">Nama</label>
                            <asp:TextBox ID="TextBoxNama" runat="server" Width="100%" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                        <asp:Button ID="ButtonOk" CssClass="btn btn-success btn-sm" runat="server" Text="Tambah" OnClick="ButtonOk_Click" />
                        <asp:Button ID="ButtonKeluar" CssClass="btn btn-danger btn-sm" runat="server" Text="Keluar" OnClick="ButtonKeluar_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

