<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="DefaultPrint.aspx.cs" Inherits="WITAdministrator_Produk_Stok_DefaultPrint" %>

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
            <h1>Stok Produk</h1>
            <div style="font-size: 17px; font-weight: bold;">
                <asp:Label ID="LabelTempatStok" runat="server"></asp:Label>
            </div>
            <div>
                Print :
                    <asp:Label ID="LabelTanggalPrint" runat="server"></asp:Label><br />
                <asp:Label ID="LabelNamaPengguna" runat="server"></asp:Label><br />
                <asp:Label ID="LabelTempatPengguna" runat="server"></asp:Label>
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
    </div>

    <table class="table table-bordered table-condensed laporan">
        <thead>
            <tr class="active">
                <th class="fitSize">No.</th>
                <th>Produk</th>
                <th>Warna</th>
                <th>Brand</th>
                <th>Kategori</th>
                <th class="fitSize" style="width: 120px;">Kode</th>
                <th style="width: 60px;">Varian</th>
                <th style="width: 120px;">Harga</th>
                <th style="width: 60px;">Quantity</th>
                <th style="width: 120px;">Subtotal</th>
            </tr>
        </thead>
        <tfoot>
            <tr class="active">
                <th class="fitSize">No.</th>
                <th>Produk</th>
                <th>Warna</th>
                <th>Brand</th>
                <th>Kategori</th>
                <th class="fitSize" style="width: 120px;">Kode</th>
                <th style="width: 60px;">Varian</th>
                <th style="width: 120px;">Harga</th>
                <th style="width: 60px;">Quantity</th>
                <th style="width: 120px;">Subtotal</th>
            </tr>
        </tfoot>
        <tbody>
            <tr style="font-size: 10px; background-color: #e8e8e8; text-align: center;">
                <td colspan="7"></td>
                <td>Rata-Rata</td>
                <td>Total</td>
                <td>Total</td>
            </tr>
            <tr style="font-weight: bold;">
                <td colspan="7"></td>
                <td class="text-right" style="padding-top: 5px;">
                    <asp:Label ID="LabelRataRataHargaJual1" runat="server"></asp:Label></td>
                <td class="text-right" style="font-size: 17px;">
                    <asp:Label ID="LabelTotalQuantity1" runat="server"></asp:Label></td>
                <td class="text-right" style="font-size: 17px;">
                    <asp:Label ID="LabelGrandTotal1" runat="server"></asp:Label></td>
            </tr>

            <asp:Literal ID="LiteralLaporan" runat="server"></asp:Literal>

            <tr style="font-weight: bold;">
                <td colspan="7"></td>
                <td class="text-right" style="padding-top: 5px;">
                    <asp:Label ID="LabelRataRataHargaJual" runat="server"></asp:Label></td>
                <td class="text-right" style="font-size: 17px;">
                    <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label></td>
                <td class="text-right" style="font-size: 17px;">
                    <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label></td>
            </tr>
            <tr style="font-size: 10px; background-color: #e8e8e8; text-align: center;">
                <td colspan="7"></td>
                <td>Rata-Rata</td>
                <td>Total</td>
                <td>Total</td>
            </tr>
        </tbody>
    </table>

    <div class="row">
        <div class="col-xs-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>
                        <asp:Label ID="LabelNamaPengguna1" runat="server"></asp:Label></b>
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

