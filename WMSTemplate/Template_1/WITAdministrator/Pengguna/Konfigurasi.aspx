<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Konfigurasi.aspx.cs" Inherits="WITAdministrator_Pengguna_Konfigurasi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Konfigurasi Pengguna
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
        <div class="col-md-12 col-sm-12 col-xs-12">
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
            <div class="card">
                <div class="card-body">
                    <div class="form-group">
                        <asp:DropDownList ID="DropDownListGrupPengguna" Width="40%" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="DropDownListGrupPengguna_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                        <br />
                    </div>
                    <div class="form-group">
                        <div class="checkbox-inline">
                            <asp:CheckBoxList ID="CheckBoxListKonfigurasi" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"></asp:CheckBoxList>
                        </div>
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

