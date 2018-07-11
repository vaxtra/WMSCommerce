<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="Cetak.aspx.cs" Inherits="WITAkuntansi_Cetak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        window.print();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="md-12" style="margin-left: 0% ;font-size: 12pt; line-height: 20px;"">
        <asp:Label ID="LabelJenisPrint" runat="server" Text=""></asp:Label>
        <table class="table-bordered">
            <tr>
                <td>Nama Vendor</td>
                <td style="width: 20px;">:</td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td>Nama Pencetak</td>
                <td style="width: 20px;">:</td>
                <td>
                    <asp:Label ID="LabelNamaLengkapPrint" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tanggal Cetak</td>
                <td style="width: 20px;">:</td>
                <td>
                    <asp:Label ID="LabelTanggalCetakPrint" runat="server" Text="01/01/2001"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tanggal Jurnal</td>
                <td style="width: 20px;">:</td>
                <td>
                    <asp:Label ID="LabelTanggalJurnalPrint" runat="server" Text="01/01/2001"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Referensi</td>
                <td style="width: 20px;">:</td>
                <td>
                        <asp:Label ID="LabelReferensi" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Keterangan</td>
                <td style="width: 20px;">:</td>
                <td>uang sejumlah
                        <asp:Label ID="LabelNominalPrint" runat="server" Text="IDR 100.000.000,-"></asp:Label>
                    untuk
                        <asp:Label ID="LabelKeteranganPrint" runat="server" Text="keterangan pembayaran bila diperlukan"></asp:Label>
                </td>
            </tr>
            
        </table>
        <br />
        <table class="table-bordered">
            <tr class="text-center">
                <td style="width: 30%">
                    <b>Menyetujui</b>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td style="width: 30%">
                    <b>Keuangan</b>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td style="width: 30%">
                    <b>Penerima</b>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

