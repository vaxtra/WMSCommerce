<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Manager.aspx.cs" Inherits="WITAdministrator_Pengguna_Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Pengaturan Manager
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonSimpan" CssClass="btn btn-success btn-const" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelManager" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-12 col-md-12 col-sm-12 col-lg-12">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 col-sm-6">
                                <div class="card">
                                    <div class="card-body">
                                        <asp:DropDownList ID="DropDownListPengguna" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPengguna_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <br />
                                        <br />
                                        <label class="form-label bold">Daftar Bawahan</label>
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover table-bordered mb-0">
                                                <asp:Repeater ID="RepeaterBawahan" runat="server">
                                                    <ItemTemplate>
                                                        <thead>
                                                            <tr class="table-warning">
                                                                <th colspan="4">Bawahan <%# Eval("LevelJabatan") %></th>
                                                            </tr>
                                                            <tr class="table-active">
                                                                <th style="width: 2%">No</th>
                                                                <th class="text-center">Nama</th>
                                                                <th class="text-center">Grup</th>
                                                                <th class="text-center">Atasan</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("Body") %>'>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                                        <td><%# Eval("NamaLengkap") %></td>
                                                                        <td><%# Eval("GrupPengguna") %></td>
                                                                        <td><strong><%# Eval("PenggunaParent") %></strong></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="table-responsive">
                                            <table class="table table-sm table-hover table-bordered mb-0">
                                                <thead>
                                                    <tr class="thead-light">
                                                        <th class="text-center" style="width: 2%">No</th>
                                                        <th class="text-center" style="width: 3%"></th>
                                                        <th class="text-center">Pegawai</th>
                                                        <th class="text-center">Atasan</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterPengguna" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                                <td style="vertical-align: middle">
                                                                    <asp:CheckBox ID="CheckBoxPilih" runat="server" Visible='<%# Eval("Sendiri") %>' />
                                                                    <asp:HiddenField ID="HiddenFieldIDPengguna" runat="server" Value='<%# Eval("IDPengguna") %>' />
                                                                    <asp:HiddenField ID="HiddenFieldIDPenggunaParent" runat="server" Value='<%# Eval("IDPenggunaParent") %>' />
                                                                </td>
                                                                <td><%# Eval("NamaLengkap") %></td>
                                                                <td><strong><%# Eval("PenggunaParent") %></strong></td>
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

