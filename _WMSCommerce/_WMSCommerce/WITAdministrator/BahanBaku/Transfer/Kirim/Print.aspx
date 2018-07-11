<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="WITAdministrator_BahanBaku_Transfer_Kirim_Print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        window.print();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-6">
            <h1>
                <img src="/images/logo.jpg">
            </h1>
        </div>
        <div class="col-xs-6 text-right">
            <h1>Transfer Bahan Baku</h1>
            <div style="font-size: 17px; font-weight: bold;">#<asp:Label ID="LabelIDTransferBahanBaku" runat="server"></asp:Label></div>
            <div style="font-size: 17px; font-weight: bold;">
                <asp:Label ID="LabelPengirimTempat1" runat="server"></asp:Label>
            </div>
            <div>
                Print :
                <asp:Label ID="LabelPrintTanggal" runat="server"></asp:Label><br />
                <asp:Label ID="LabelPrintPengguna" runat="server"></asp:Label><br />
                <asp:Label ID="LabelPrintTempat" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-5">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Pengirim
                    <asp:Label ID="LabelPengirimTempat" runat="server"></asp:Label>
                    </b>
                </div>
                <div class="panel-body">
                    <asp:Label ID="LabelPengirimanPengguna" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPengirimanTanggal" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="LabelPengirimAlamat" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPengirimTelepon" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPengirimEmail" runat="server"></asp:Label>
                    <br />
                    <br />
                    <b>Keterangan :</b>
                    <br />
                    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-xs-5 col-xs-offset-2 text-left">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Penerima
                    <asp:Label ID="LabelPenerimaTempat" runat="server"></asp:Label>
                    </b>
                </div>
                <div class="panel-body">
                    <asp:Label ID="LabelPenerimaPengguna" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPenerimaTanggal" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="LabelPenerimaAlamat" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPenerimaTelepon" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelPenerimaEmail" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <table class="table table-bordered table-condensed laporan">
        <thead>
            <tr>
                <th class="fitSize">No.</th>
                <th>Bahan Baku</th>
                <th>Satuan</th>
                <th>Kategori</th>
                <th class="fitSize" style="width: 120px;">Kode</th>
                <th style="width: 120px;">Harga</th>
                <th style="width: 60px;">Quantity</th>
                <th style="width: 120px;">Subtotal</th>
            </tr>
        </thead>
        <tfoot>
            <tr>
                <th class="fitSize">No.</th>
                <th>Bahan Baku</th>
                <th>Satuan</th>
                <th>Kategori</th>
                <th class="fitSize" style="width: 120px;">Kode</th>
                <th style="width: 120px;">Harga</th>
                <th style="width: 60px;">Quantity</th>
                <th style="width: 120px;">Subtotal</th>
            </tr>
        </tfoot>
        <tbody>
            <tr style="font-size: 10px; background-color: #e8e8e8; text-align: center;">
                <td colspan="6"></td>
                <td>Total</td>
                <td>Total</td>
            </tr>
            <tr style="font-weight: bold;">
                <td colspan="7"></td>
                <td class="text-right" style="font-size: 17px;">
                    <asp:Label ID="LabelGrandTotal1" runat="server"></asp:Label></td>
            </tr>

            <asp:Repeater ID="RepeaterTransferBahanBaku" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                        <td><%# Eval("BahanBaku") %></td>
                        <td class="fitSize"><%# Eval("SatuanBesar") %></td>
                        <td><%# Eval("Kategori") %></td>
                        <td class="fitSize"><%# Eval("KodeBahanBaku") %></td>
                        <td class="text-right fitSize"><%# Eval("HargaBeli").ToFormatHarga() %></td>
                        <td class="text-right fitSize"><%# Eval("Jumlah").ToFormatHarga() %></td>
                        <td class="text-right fitSize warning bold"><%# Eval("Subtotal").ToFormatHarga() %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

            <tr style="font-weight: bold;">
                <td colspan="7"></td>
                <td class="text-right" style="font-size: 17px;">
                    <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label></td>
            </tr>
            <tr style="font-size: 10px; background-color: #e8e8e8; text-align: center;">
                <td colspan="6"></td>
                <td>Total</td>
                <td>Total</td>
            </tr>
        </tbody>
    </table>

    <div class="row">
        <div class="col-xs-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Pengirim
                        <asp:Label ID="LabelPengirimanPengguna1" runat="server"></asp:Label></b>
                </div>
                <div class="panel-body">
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
        <div class="col-xs-3 col-xs-offset-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Penerima
                        <asp:Label ID="LabelPenerimaPengguna1" runat="server"></asp:Label></b>
                </div>
                <div class="panel-body">
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>

    <table style="width: 100%;">
        <tr>
            <td class="text-center">THANK YOU</td>
        </tr>
        <tr>
            <td class="text-center"><b>Warehouse Management System Powered by WIT.</b></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

