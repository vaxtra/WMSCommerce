<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="PerbandinganHarga.aspx.cs" Inherits="WITReport_Supplier_PerbandinganHarga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table.table tr th {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Perbandingan Harga Supplier
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
    <asp:UpdatePanel ID="UpdatePanelPerbandinganHarga" runat="server">
        <ContentTemplate>
            <div class="panel panel-success">
                <div class="panel-heading">
                    Perbandingan Harga
                </div>
                <div class="table-responsive">
                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                        <thead>
                            <tr class="active">
                                <th>No</th>
                                <th>Bahan Baku</th>
                                <th>Satuan</th>
                                <asp:Repeater ID="RepeaterSupplier" runat="server">
                                    <ItemTemplate>
                                        <th class="text-center fitSize"><%# Eval("Nama") %></th>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="success" style="font-weight: bold;">
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="DropDownListCariBahanBaku" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="DropDownListCariSatuan" runat="server" Style="width: 100%;" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></td>
                                <asp:Literal ID="LiteralColspan" runat="server"></asp:Literal>
                            </tr>
                            <asp:Repeater ID="RepeaterBahanBaku" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td class="fitSize"><%# Eval("BahanBaku") %></td>
                                        <td class="text-center fitSize"><%# Eval("Satuan") %></td>
                                        <asp:Repeater ID="RepeaterHargaSupplier" runat="server" DataSource='<%# Eval("HargaSupplier") %>'>
                                            <ItemTemplate>
                                                <td class="text-right"><%# Eval("Harga") %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressPerbandinganHarga" runat="server" AssociatedUpdatePanelID="UpdatePanelPerbandinganHarga">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressPerbandinganHarga" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

