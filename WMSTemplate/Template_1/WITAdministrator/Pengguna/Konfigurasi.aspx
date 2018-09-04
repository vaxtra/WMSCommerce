<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Konfigurasi.aspx.cs" Inherits="WITAdministrator_Pengguna_Konfigurasi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Fitur Pengguna
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
    <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-8">
            <div class="card">
                <h4 class="card-header bg-smoke">Konfigurasi</h4>
                <div class="card-body">
                    <div class="form-group">
                        <asp:DropDownList ID="DropDownListGrupPengguna" CssClass="select2 w-100" AutoPostBack="true" OnSelectedIndexChanged="DropDownListGrupPengguna_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                        <br />
                    </div>
                    <div class="form-group">
                        <asp:CheckBoxList ID="CheckBoxListKonfigurasi" runat="server" CssClass="checkboxlist" RepeatDirection="Horizontal" RepeatColumns="4"></asp:CheckBoxList>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" OnClick="ButtonSimpan_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

