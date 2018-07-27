<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Produk_Atribut_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Keterangan Order
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
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="form-group">
                        <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
                    </div>
                    <div class="form-group">
                        <div class="table-responsive">
                            <asp:HiddenField ID="HiddenFieldIDTemplateKeterangan" runat="server" />
                            <table class="table table-sm table-hover table-bordered">
                                <thead>
                                    <tr class="thead-light">
                                        <th style="width: 2%">No.</th>
                                        <th>Keterangan</th>
                                        <th></th>
                                    </tr>
                                    <tr class="thead-light">
                                        <th colspan="2">
                                            <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" runat="server"></asp:TextBox></th>
                                        <th>
                                            <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-block" runat="server" Text="Tambah" OnClick="ButtonSimpan_Click" /></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterTemplateKeterangan" runat="server" OnItemCommand="RepeaterTemplateKeterangan_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td><%# Eval("Isi") %></td>
                                                <td class="text-right fitSize">
                                                    <asp:Button ID="ButtonUbah" CssClass="btn btn-info btn-xs" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDTemplateKeterangan") %>' />
                                                    <asp:Button ID="ButtonHapus" CssClass="btn btn-danger btn-xs" runat="server" Text="Hapus" CommandName="Hapus" CommandArgument='<%# Eval("IDTemplateKeterangan") %>' OnClientClick='<%# "return confirm(\"Apakah Anda yakin menghapus data\")" %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

