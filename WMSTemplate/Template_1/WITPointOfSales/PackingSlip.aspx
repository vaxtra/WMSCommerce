<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPageLaporan.master" AutoEventWireup="true" CodeFile="PackingSlip.aspx.cs" Inherits="WITPointOfSales_NewInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        window.print();
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <asp:Panel ID="PanelLogo" runat="server">
            <div class="col-xs-6">
                <h1>
                    <img src="/images/logo.jpg">
                </h1>
            </div>
        </asp:Panel>

        <div class="col-xs-6 text-right">
            <h1>Packing Slip</h1>
            <div style="font-size: 17px; font-weight: bold;">Order #<asp:Label ID="LabelIDTransaksi" runat="server"></asp:Label></div>
            <div>
                <asp:Label ID="LabelTanggalTransaksi" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-7">
            <table class="table table-bordered laporan">
                <thead>
                    <tr>
                        <th class="fitSize">No.</th>
                        <th class="fitSize">Kode
                        </th>
                        <th>Produk
                        </th>
                        <th>Kategori</th>
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
                                    <%# Eval("Quantity") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="col-xs-5 text-left">
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
                    <b>FROM :</b>
                    <br />
                    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <%--    <div class="col-xs-5" style="visibility: hidden;">
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
    </div>--%>

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

