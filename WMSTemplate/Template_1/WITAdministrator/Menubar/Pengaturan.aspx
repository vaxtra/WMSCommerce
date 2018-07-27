<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Menubar_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
    Menubar
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Modul</label>
                        <asp:DropDownList ID="DropDownListEnumMenubarModul" CssClass="select2" runat="server" Style="width: 100%;">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <label class="font-weight-bold text-muted">Parent</label>
                                <asp:DropDownList ID="DropDownListMenuLevel1" CssClass="select2" runat="server" Style="width: 100%;" OnSelectedIndexChanged="DropDownListMenubarParent_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" runat="server" id="PanelIcon">
                                <label class="font-weight-bold text-muted">Icon</label>
                                <asp:TextBox ID="TextBoxIcon" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="form-group" runat="server" id="PanelLevel2" visible="false">
                        <label class="font-weight-bold text-muted">Sub Parent</label>
                        <asp:DropDownList ID="DropDownListMenuLevel2" CssClass="select2" runat="server" Style="width: 100%;">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Urutan</label>
                        <asp:TextBox ID="TextBoxUrutan" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Kode</label>
                        <asp:TextBox ID="TextBoxKode" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Nama</label>
                        <asp:TextBox ID="TextBoxNama" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Url</label>
                        <asp:TextBox ID="TextBoxUrl" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-footer">
            <asp:Button ID="ButtonSimpan" runat="server" CssClass="btn btn-success btn-const" Text="Simpan" OnClick="ButtonSimpan_Click" />
            <asp:Button ID="ButtonKembali" runat="server" CssClass="btn btn-danger btn-const" Text="Kembali" OnClick="ButtonKembali_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

