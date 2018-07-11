<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="DeadStock.aspx.cs" Inherits="WITReport_StokProduk_DeadStok" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Laporan Dead Stock
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="form-inline">
                    <div class="btn-group" style="margin: 5px 5px 0 0">
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHari" runat="server" Text="Hari Ini" OnClick="ButtonHari_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMinggu" runat="server" Text="Minggu Ini" OnClick="ButtonMinggu_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulan" runat="server" Text="Bulan Ini" OnClick="ButtonBulan_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahun" runat="server" Text="Tahun Ini" OnClick="ButtonTahun_Click" />
                    </div>
                    <div class="btn-group" style="margin: 5px 5px 0 0">
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariSebelumnya" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnya_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguSebelumnya" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguSebelumnya_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanSebelumnya" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanSebelumnya_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunSebelumnya" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunSebelumnya_Click" />
                    </div>
                    <div style="margin: 5px 5px 0 0" class="form-group">
                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                        <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCariTanggal" runat="server" Text="Cari" OnClick="ButtonCariTanggal_Click" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:DropDownList ID="DropDownListJenisPengurutan" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                    <asp:ListItem Text="Total Quantity Besar ke Kecil" Value="1" />
                    <asp:ListItem Text="Tanggal Pembuatan Lama ke Baru" Value="2" />
                    <asp:ListItem Text="Semua" Value="0" />
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <div class="progress">
                    <asp:Literal ID="LiteralProgressBar" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                    Dead Stock
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
                                <th>Create Date</th>
                                <th>Total Qty</th>
                                <th>Tempat</th>
                                <th>Varian</th>
                                <th class="text-right">Qty</th>
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
                                <th></th>
                                <th></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListTempat" CssClass="select2" Style="width: 120px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListVarian" CssClass="select2" Style="width: 120px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                <th colspan="2"></th>
                            </tr>
                            <tr class="active">
                                <th colspan="10" class="text-right">
                                    <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-xs btn-primary" ClientIDMode="Static" OnClick="LoadData_Event" />
                                    <asp:Button ID="ButtonReset" runat="server" Text="Reset" CssClass="btn btn-xs btn-danger" ClientIDMode="Static" OnClick="ButtonReset_Click" />
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="success">
                                <td colspan="6"></td>
                                <td class="text-right" style="font-weight: bold;">
                                    <asp:Label ID="LabelTotalStokProduk" runat="server"></asp:Label>
                                </td>
                                <td colspan="3"></td>
                            </tr>
                            <asp:Literal ID="LiteralLaporan" runat="server"></asp:Literal>
                            <tr class="success">
                                <td colspan="6"></td>
                                <td class="text-right" style="font-weight: bold;">
                                    <asp:Label ID="LabelTotalStokProduk1" runat="server"></asp:Label>
                                </td>
                                <td colspan="3"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

