<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Printer.aspx.cs" Inherits="WITAdministrator_Store_Konfigurasi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Konfigurasi Printer
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
                    <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
                    <div class="table-responsive">
                        <table class="table table-sm table-hover table-bordered">
                            <thead>
                                <tr class="thead-light">
                                    <th>No.</th>
                                    <th>Kategori</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="RepeaterKonfigurasi" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="align-middle fitSize"><%# Container.ItemIndex + 1 %>
                                            <asp:Label ID="LabelIDKonfigurasiPrinter" runat="server" Visible="false" Text='<%# Eval("IDKonfigurasiPrinter") %>'></asp:Label></td>
                                        <td class="align-middle"><%# Eval("Kategori") %></td>
                                        <td>
                                            <asp:TextBox ID="TextBoxPengaturan" runat="server" Width="100%" CssClass="form-control input-sm" Text='<%# Eval("NamaPrinter") %>'></asp:TextBox></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-footer">
            <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-const" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

