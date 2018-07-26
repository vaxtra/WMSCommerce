<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="PengaturanPrint.aspx.cs" Inherits="WITAdministrator_BahanBaku_Stok_PengaturanPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <tr class="active">
                        <th class="text-center fitSize">No.</th>
                        <th class="text-center">Kode</th>
                        <th class="text-center">Bahan Baku</th>
                        <th class="text-center">Kategori</th>
                        <th class="text-center">Jumlah Kecil</th>
                        <th class="text-center">Satuan</th>
                        <th class="text-center">Jumlah Besar</th>
                        <th class="text-center">Satuan</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                        <ItemTemplate>
                            <tr style="border-top: 3px double #aaa !important;">
                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Kode") %></td>
                                <td><%# Eval("BahanBaku") %></td>
                                <td><%# Eval("Kategori") %></td>
                                <td style="width: 100px;"></td>
                                <td><%# Eval("SatuanKecil") %></td>
                                <td style="width: 100px;"></td>
                                <td><%# Eval("SatuanBesar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

