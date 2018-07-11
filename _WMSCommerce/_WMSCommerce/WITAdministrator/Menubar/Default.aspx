<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Menu_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Menu
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Pengaturan.aspx" class="btn btn-success btn-const">Tambah</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
            <div class="card">
                <div class="card-header bg-gradient-black">
                    <h5 class="font-weight-light">Role Default</h5>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-sm table-hover table-bordered mb-0">
                            <asp:Repeater ID="RepeaterMenuDefault" runat="server">
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
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-const" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
            <div class="card">
                <div class="card-header bg-gradient-black">
                    <h5 class="font-weight-light">DAFTAR MENU</h5>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-sm table-hover table-bordered mb-0">
                            <tbody>
                                <asp:Repeater ID="RepeaterMenu" runat="server" OnItemCommand="RepeaterMenu_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="table-info">
                                            <td class="text-middle fitSize">
                                                <asp:TextBox ID="TextBoxUrutan" runat="server" CssClass="text-center" Text='<%# Eval("Urutan") %>' Style="width: 30px;"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButtonUpdate" BorderStyle="None" ImageUrl="/assets/images/refresh.png" CommandName="Urutkan" CommandArgument='<%# Eval("IDMenubar") %>' runat="server" Style="vertical-align: middle !important; height: 20px;" />
                                            </td>

                                            <td class="text-middle bold" colspan="3"><i class='<%# Eval("Icon") %>'></i>&nbsp;<%# Eval("Nama") %></td>
                                            <td class="text-middle" style="width: 20%;">
                                                <a href='<%# Eval("Url") %>' class='<%# Eval("HaveChild").ToBool() == true ? "hidden" : string.Empty %>'><%# Eval("Url") %></a>
                                            </td>

                                            <td class="fitSize text-middle text-center">
                                                <asp:Button ID="ButtonUbah" CssClass="btn btn-info btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDMenubar") %>' />
                                                <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDMenubar") %>' />
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="RepeaterMenuLevel2" runat="server" OnItemCommand="RepeaterMenu_ItemCommand" DataSource='<%# Eval("MenuLevel2") %>'>
                                            <ItemTemplate>
                                                <tr class="table-warning">
                                                    <td></td>
                                                    <td class="text-middle fitSize">
                                                        <asp:TextBox ID="TextBoxUrutan" runat="server" CssClass="text-center" Text='<%# Eval("Urutan") %>' Style="width: 30px;"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButtonUpdate" BorderStyle="None" ImageUrl="/assets/images/refresh.png" CommandName="Urutkan" CommandArgument='<%# Eval("IDMenubar") %>' runat="server" Style="vertical-align: middle !important; height: 20px;" />
                                                    </td>

                                                    <td class="text-middle" colspan="2"><%# Eval("Nama") %></td>
                                                    <td class="text-middle" style="width: 20%;">
                                                        <a href='<%# Eval("Url") %>' class='<%# Eval("HaveChild").ToBool() == true ? "hidden" : string.Empty %>'><%# Eval("Url") %></a>
                                                    </td>

                                                    <td class="fitSize text-middle text-center">
                                                        <asp:Button ID="ButtonUbah" CssClass="btn btn-info btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDMenubar") %>' />
                                                        <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDMenubar") %>' />
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="RepeaterMenuLevel3" runat="server" OnItemCommand="RepeaterMenu_ItemCommand" DataSource='<%# Eval("MenuLevel3") %>'>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="2"></td>
                                                            <td class="text-middle fitSize">
                                                                <asp:TextBox ID="TextBoxUrutan" runat="server" CssClass="text-center" Text='<%# Eval("Urutan") %>' Style="width: 30px;"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtonUpdate" BorderStyle="None" ImageUrl="/assets/images/refresh.png" CommandName="Urutkan" CommandArgument='<%# Eval("IDMenubar") %>' runat="server" Style="vertical-align: middle !important; height: 20px;" />
                                                            </td>

                                                            <td class="text-middle"><%# Eval("Nama") %></td>
                                                            <td class="text-middle" style="width: 20%;"><a href='<%# Eval("Url") %>'><%# Eval("Url") %></a></td>

                                                            <td class="fitSize text-middle text-center">
                                                                <asp:Button ID="ButtonUbah" CssClass="btn btn-info btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDMenubar") %>' />
                                                                <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDMenubar") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

