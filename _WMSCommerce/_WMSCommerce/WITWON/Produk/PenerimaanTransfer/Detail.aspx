<%@ Page Title="" Language="C#" MasterPageFile="~/WITWON/MasterPageWebView.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITWON_Produk_PenerimaanTransfer_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleLeft" runat="Server">
    Detail Transfer Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a runat="server" id="linkKeluar" class="btn btn-danger btn-sm">Keluar</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <h3>
                <asp:Label ID="LabelStatusTransfer" runat="server"></asp:Label></h3>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Pengirim</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Tanggal</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">
                                    <asp:Label ID="LabelPengirimTanggal" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Lokasi</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">
                                    <asp:Label ID="LabelPengirimLokasi" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Pengirim</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">
                                    <asp:Label ID="LabelPengirimPengguna" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Penerima</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Tanggal</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">
                                    <asp:Label ID="LabelPenerimaTanggal" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Lokasi</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">
                                    <asp:Label ID="LabelPenerimaLokasi" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Penerima</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">
                                    <asp:Label ID="LabelPenerimaPengguna" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="table-responsive">
                <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                    <thead>
                        <tr class="active">
                            <th>No.</th>
                            <th>Produk</th>
                            <th>Kategori</th>
                            <th>Varian</th>
                            <th>Harga</th>
                            <th>Quantity</th>
                            <th>Subtotal</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="success" style="font-weight: bold; font-size: 13px;">
                            <td colspan="5" class="text-center">TOTAL</td>
                            <td class="text-right">
                                <asp:Label ID="LabelTotalJumlah1" runat="server"></asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="LabelTotalNominal1" runat="server"></asp:Label></td>
                        </tr>

                        <asp:Literal ID="LiteralLaporan" runat="server"></asp:Literal>

                        <tr class="success" style="font-weight: bold; font-size: 13px;">
                            <td colspan="5" class="text-center">TOTAL</td>
                            <td class="text-right">
                                <asp:Label ID="LabelTotalJumlah" runat="server"></asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="LabelTotalNominal" runat="server"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

