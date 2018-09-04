<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="WITAdministrator_Pengguna_Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Hak Akses
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
        <div class="col-xs-12 col-md-12 col-sm-6 col-lg-4">
            <div class="form-group">
                <asp:DropDownList ID="DropDownListGrupPengguna" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListGrupPengguna_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="form-group">

                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered mb-0">
                        <asp:Repeater ID="RepeaterMenu" runat="server">
                            <ItemTemplate>
                                <tr class="table-info">
                                    <td class="text-middle fitSize">
                                        <asp:CheckBox ID="CheckBoxPilih" runat="server" Visible='<%# !(Eval("HaveChild").ToBool()) %>' />
                                        <asp:HiddenField ID="HiddenFieldIDMenu" runat="server" Value='<%# Eval("IDMenubar") %>' />
                                    </td>
                                    <td class="text-middle bold" colspan="3"><i class='<%# Eval("Icon") %>'></i>&nbsp;<%# Eval("Nama") %></td>
                                </tr>
                                <asp:Repeater ID="RepeaterMenuLevel2" runat="server" DataSource='<%# Eval("MenuLevel2") %>'>
                                    <ItemTemplate>
                                        <tr class="table-warning">
                                            <td></td>
                                            <td class="text-middle fitSize">
                                                <asp:CheckBox ID="CheckBoxPilih" runat="server" Visible='<%# !(Eval("HaveChild").ToBool()) %>' />
                                                <asp:HiddenField ID="HiddenFieldIDMenu" runat="server" Value='<%# Eval("IDMenubar") %>' />
                                            </td>
                                            <td class="text-middle" colspan="3"><i class='<%# Eval("Icon") %>'></i>&nbsp;<%# Eval("Nama") %></td>
                                        </tr>
                                        <asp:Repeater ID="RepeaterMenuLevel3" runat="server" DataSource='<%# Eval("MenuLevel3") %>'>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="2"></td>
                                                    <td class="text-middle fitSize">
                                                        <asp:CheckBox ID="CheckBoxPilih" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldIDMenu" runat="server" Value='<%# Eval("IDMenubar") %>' />
                                                    </td>
                                                    <td class="text-middle" colspan="3"><i class='<%# Eval("Icon") %>'></i>&nbsp;<%# Eval("Nama") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div class="card">
                    <div class="card-footer">
                        <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-const" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
                        <a href="Default.aspx" class="btn btn-danger btn-const">Batal</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

