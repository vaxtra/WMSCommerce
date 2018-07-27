<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="PackingSlipToko46.aspx.cs" Inherits="WITPointOfSales_PackingSlipToko46" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        window.print();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <div class="row">
        <div class="col-xs-6">
            <h1>
                <img src="/images/logo.jpg" style="margin-bottom:25px;">
            </h1>
        </div>
         
        <div class="col-xs-6 text-right">
            <h1>Packing Slip</h1>
            <div style="font-size: 17px; font-weight: bold;">Order #<asp:Label ID="LabelIDTransaksi" runat="server"></asp:Label></div>
            <div>
                <asp:Label ID="LabelTanggalTransaksi" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-5">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="LabelNamaStore" runat="server" Style="font-weight: bold;"></asp:Label>
                </div>
                <div class="panel-body">
                    <asp:Label ID="LabelAlamatStore" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelTeleponStore" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelWebsite" runat="server"></asp:Label>
                    <br />
                    <asp:HyperLink ID="HyperLinkEmail" runat="server"></asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="col-xs-5 col-xs-offset-2 text-left">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="LabelNamaPelanggan" Style="font-weight: bold;" runat="server"></asp:Label>
                </div>
                <div class="panel-body">

                    <asp:Label ID="LabelAlamatPelanggan" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelTeleponPelanggan" runat="server"></asp:Label>
                    <br />
                    <br />
                    <b>Pembayaran : </b>
                    <asp:Label ID="LabelJenisPembayaran" runat="server"></asp:Label>
                    <br />
                    <b>Status : </b>
                    <asp:Label ID="LabelStatusTransaksi" runat="server"></asp:Label>
                    <br />
                    <b>Keterangan :</b>
                    <br />
                    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <table class="table table-bordered laporan">
        <thead>
            <tr>
                <th class="fitSize">No.</th>
                <th class="fitSize">Design No.
                </th>
                <th>Color
                </th>
                <th>Piece Length</th>
                <th>Quantity
                </th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="RepeaterDetailTransaksi" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex + 1 %></td>
                        <td style="word-wrap: break-word;"><%# Eval("Kode") %></td>
                        <td style="word-wrap: break-word;"><%# Eval("Nama") %></td>
                        <td style="word-wrap: break-word;"><%# Eval("Kategori") %></td>
                        <td class="text-right">
                            <%# Eval("JumlahProduk") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            
        </tbody>
    </table>

    <div class="row text-right">
        <div class="col-xs-3 col-xs-offset-7">
            <p>
                <strong>
                    <span style="font-size: 17px;">Total Quantity</span>
                </strong>
            </p>
        </div>
        <div class="col-xs-2">
            <strong>
                <span style="font-size: 17px;">
                    <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label>
                </span>
            </strong>
        </div>
    </div>

    <table class="table table-bordered laporan" style="text-align:center">
        <tr>
            <td style="width:275px;">Disetujui oleh <br />Kepala Marketing</td>
            <td>Diketahui oleh <br />Kepala Gudang</td>
            <td>Barang telah diperiksa oleh<br />Foreman Gudang</td>
            <td>Dibuat oleh</td>
        </tr>
         <tr>
            <td style="height:100px;"></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>

    <table style="width: 100%;">
        <tr>
            <asp:Label ID="LabelFooterPrint" runat="server"></asp:Label>
        </tr>
        <tr>
            <td class="text-center">THANK YOU</td>
        </tr>
        <tr>
            <td class="text-center"><b>Warehouse Management System Powered by WIT.</b></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" Runat="Server">
</asp:Content>

