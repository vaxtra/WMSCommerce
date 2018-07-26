<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="PindahLokasi.aspx.cs" Inherits="WITAdministrator_Pengguna_PindahTempat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Pindah Lokasi
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
        <div class="col-xs-12 col-md-12 col-sm-8 col-lg-6">
            <div class="card">
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-sm table-hover table-bordered mb-0">
                            <thead>
                                <tr class="thead-light">
                                    <th>No.</th>
                                    <th>Nama</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterTempat" runat="server" OnItemCommand="RepeaterTempat_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                            <td><%# Eval("Nama") %></td>
                                            <td class="text-right fitSize">
                                                <asp:Button ID="ButtonPindah" runat="server" Text="Pindah" CssClass="btn btn-success btn-xs" CommandName="Pindah" CommandArgument='<%# Eval("IDTempat") %>' /></td>
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
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

