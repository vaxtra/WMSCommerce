<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Ringkasan.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_ButtonCari(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCari');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Summary Revenue
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-secondary btn-const mr-1" OnClick="ButtonExcel_Click" />
    <h6 class="mr-1 mt-2"><a id="LinkDownload" runat="server" visible="false">Download File</a></h6>
    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-secondary btn-const" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="form-group">
        <div class="form-inline">
            <div class="form-group mr-1 mb-1">
                <asp:DropDownList ID="DropDownListJenisLaporan" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                    <asp:ListItem Text="Harian" Value="1" />
                    <asp:ListItem Text="Bulanan" Value="2" Selected="True" />
                    <asp:ListItem Text="Tahunan" Value="3" />
                </asp:DropDownList>
            </div>
            <div id="PanelTahun" runat="server" class="form-group mr-1 mb-1">
                <asp:DropDownList ID="DropDownListTahun" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
            <div class="form-group mb-1">
                <asp:Button CssClass="btn btn-light btn-const" ID="ButtonCari" runat="server" Text="Cari" OnClick="LoadData_Event" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-6">
                <asp:DropDownList ID="DropDownListTempat" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
            <div class="col-6">
                <asp:DropDownList ID="DropDownListJenisTransaksi" CssClass="select2 w-100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
            </div>
        </div>
    </div>
    <h4 class="text-uppercase mb-3">
        <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h4>
    <div class="card">
        <h5 class="card-header bg-gradient-green">REVENUE</h5>
        <div class="table-responsive">
            <table class="table table-sm table-bordered table-hover">
                <thead>
                    <tr class="thead-light">
                        <th rowspan="2" style="width: 2%">No.</th>
                        <th rowspan="2" style="width: 13%"></th>
                        <th rowspan="2">Tamu</th>
                        <th rowspan="2">Quantity</th>

                        <th colspan="6">Transaksi</th>

                        <th rowspan="2">Transaksi</th>
                        <th rowspan="2">Nominal</th>
                    </tr>
                    <tr>
                        <th class="table-warning text-muted">Pelanggan</th>
                        <th class="table-danger text-muted">Bukan Pelanggan</th>

                        <th class="table-warning text-muted">Discount</th>
                        <th class="table-danger text-muted">Tidak Discount</th>

                        <th class="table-warning text-muted">Delivery</th>
                        <th class="table-danger text-muted">Bukan Delivery</th>
                    </tr>
                    <tr class="text-right table-success font-weight-bold">
                        <td colspan="2"></td>
                        <td>
                            <asp:Label ID="LabelTamu" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelQuantity" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelPelanggan" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelNonPelanggan" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelDiscount" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelNonDiscount" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelPengiriman" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelNonPengiriman" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelTransaksi" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelNominal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Index") %></td>
                                <td class="text-right"><%# Eval("Tamu").ToFormatHargaBulat() %></td>
                                <td class="text-right"><%# Eval("Quantity").ToFormatHargaBulat() %></td>
                                <td class="text-right" style="border-left: double;"><%# Eval("Pelanggan").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("NonPelanggan").ToFormatHarga() %></td>
                                <td class="text-right" style="border-left: double;"><%# Eval("Discount").ToFormatHarga() %></td>
                                <td class="text-right"><%# Eval("NonDiscount").ToFormatHarga() %></td>
                                <td class="text-right" style="border-left: double;"><%# Eval("Pengiriman").ToFormatHarga() %></td>
                                <td class="text-right" style="border-right: double;"><%# Eval("NonPengiriman").ToFormatHarga() %></td>
                                <td class="text-right"><strong><%# Eval("Transaksi").ToFormatHargaBulat() %></strong></td>
                                <td class="text-right"><strong><%# Eval("Nominal").ToFormatHarga() %></strong></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr class="text-right table-success font-weight-bold">
                        <td colspan="2"></td>
                        <td>
                            <asp:Label ID="LabelTamu1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelQuantity1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelPelanggan1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelNonPelanggan1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelDiscount1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelNonDiscount1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelPengiriman1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelNonPengiriman1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelTransaksi1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelNominal1" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

