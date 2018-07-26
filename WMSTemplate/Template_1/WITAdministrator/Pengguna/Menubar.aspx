<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Menubar.aspx.cs" Inherits="WITAdministrator_Menu_Pengguna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Pengaturan Menu
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
        <div class="col-xs-6 col-md-6 col-sm-6 col-lg-6">
            <asp:DropDownList ID="DropDownListGrupPengguna" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListGrupPengguna_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12 col-md-12 col-sm-12 col-lg-12">
            <table class="table table-condensed table-hover table-bordered">
                <thead>
                    <tr class="active">
                        <th></th>
                        <th>Kode</th>
                        <th>Nama</th>
                        <th>Url</th>
                        <th></th>
                    </tr>
                    <asp:Repeater ID="RepeaterMenu" runat="server" OnItemCommand="RepeaterMenu_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td class="fitSize">
                                    <asp:CheckBox ID="CheckBoxPilihParent" runat="server" Visible='<%# !(Eval("PunyaSubMenubar").ToBool()) %>' />
                                    <asp:HiddenField ID="HiddenFieldIDMenuParent" runat="server" Value='<%# Eval("IDMenubar") %>' />

                                    <asp:Button ID="ButtonPilihSemua" runat="server" Text="Pilih Semua" CommandName="PilihSemua" CommandArgument='<%# Eval("IDMenubar") %>' Visible='<%# Eval("PunyaSubMenubar") %>' CssClass="btn btn-primary btn-xs" />
                                    <asp:Button ID="ButtonHapusPilihan" runat="server" Text="Hapus Pilihan" CommandName="HapusPilihan" CommandArgument='<%# Eval("IDMenubar") %>' Visible='<%# Eval("PunyaSubMenubar") %>' CssClass="btn btn-danger btn-xs" />
                                </td>

                                <td class="text-middle"><%# Eval("Kode") %></td>
                                <td class="text-middle"><i class='<%# Eval("Icon") %>'></i>&nbsp;<%# Eval("Nama") %></td>
                                <td class="text-middle"><a href='<%# Eval("Url") %>' target="_blank"><%# Eval("Url") %></a></td>

                                <td class="warning">
                                    <table class="table table-condensed table-hover table-bordered" style="margin: 0px;">
                                        <asp:Repeater ID="RepeaterSubMenubar" runat="server" DataSource='<%# Eval("SubMenu") %>'>
                                            <ItemTemplate>
                                                <tr class="warning">
                                                    <td style="width: 10%;">
                                                        <asp:HiddenField ID="HiddenFieldIDMenu" runat="server" Value='<%# Eval("IDMenubar") %>' />
                                                        <asp:CheckBox ID="CheckBoxPilih" runat="server" />
                                                    </td>
                                                    <td style="width: 40%;"><%# Eval("Nama") %></td>
                                                    <td style="width: 50%;"><a href='<%# Eval("Url") %>' target="_blank"><%# Eval("Url") %></a></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </thead>
            </table>

            <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-sm" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
            <asp:Button ID="ButtonKeluar" CssClass="btn btn-danger btn-sm" runat="server" Text="Keluar" OnClick="ButtonKeluar_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

