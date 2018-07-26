<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Bulanan.aspx.cs" Inherits="WITReport_StokProduk_DeadStok" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Stok Produk
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
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="DropDownListBulan" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="DropDownListTahun" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="panel panel-success">
        <div class="panel-heading">
            Stok Produk
        </div>
        <div class="table-responsive">
            <table class="table table-condensed table-hover" style="font-size: 12px;">
                <thead>
                    <tr class="active">
                        <th>No.</th>
                        <th>Produk</th>
                        <th>Warna</th>
                        <th>Brand</th>
                        <th>Kategori</th>
                        <th>Varian</th>
                        <th class="text-right">Quantity</th>
                    </tr>
                    <tr>
                        <th></th>
                        <th>
                            <asp:TextBox ID="TextBoxProduk" ClientIDMode="Static" Style="width: 120px;" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                        <th>
                            <asp:DropDownList ID="DropDownListWarna" CssClass="select2" Style="width: 120px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                        <th>
                            <asp:DropDownList ID="DropDownListBrand" CssClass="select2" Style="width: 120px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                        <th>
                            <asp:DropDownList ID="DropDownListKategori" CssClass="select2" Style="width: 120px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                        <th>
                            <asp:DropDownList ID="DropDownListVarian" CssClass="select2" Style="width: 120px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                        <th style="text-align: right;">
                            <asp:TextBox ID="TextBoxQuantity" ClientIDMode="Static" Style="width: 120px;" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                    </tr>
                    <tr class="active">
                        <th colspan="7" class="text-right">
                            <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-xs btn-primary" ClientIDMode="Static" OnClick="LoadData_Event" />
                            <asp:Button ID="ButtonReset" runat="server" Text="Reset" CssClass="btn btn-xs btn-danger" ClientIDMode="Static" OnClick="ButtonReset_Click" />
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="info">
                        <td colspan="6"></td>
                        <td class="text-right" style="font-weight: bold;">
                            <asp:Label ID="LabelTotalStokProduk" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater ID="RepeaterStokProduk" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Key.Produk") %></td>
                                <td><%# Eval("Key.Warna") %></td>
                                <td><%# Eval("Key.Brand") %></td>
                                <td><%# Eval("Key.Kategori") %></td>
                                <td><%# Eval("Key.Varian") %></td>
                                <td class="text-right"><%# Eval("Total") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="info">
                        <td colspan="6"></td>
                        <td class="text-right" style="font-weight: bold;">
                            <asp:Label ID="LabelTotalStokProduk1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <%--   
                            <asp:Literal ID="LiteralLaporan" runat="server"></asp:Literal>
                    --%>
                </tbody>
            </table>
        </div>
    </div>

    <%--            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

