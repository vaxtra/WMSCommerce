<%@ Page Title="" Language="C#" MasterPageFile="~/WITWON/MasterPageWebView.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITWON_Produk_PenerimaanTransfer_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitleLeft" runat="Server">
    <asp:Label ID="LabelIDTransferProduk" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Default.aspx" class="btn btn-danger btn-sm">Keluar</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelTransfer" runat="server">
        <ContentTemplate>
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>

            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Pengirim</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownListTempatPengirim" Style="width: 100%" CssClass="select2" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownListPenggunaPengirim" Style="width: 100%" CssClass="select2" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="TextBoxTanggalKirim" CssClass="form-control text-center" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Penerima</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownListTempatPenerima" Style="width: 100%" CssClass="select2" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownListPenggunaPenerima" Style="width: 100%" CssClass="select2" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="TextBoxTanggalTerima" CssClass="form-control text-center" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Produk</h3>
                        </div>
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
                                        <td colspan="5" class="text-right">TOTAL</td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelTotalJumlah1" runat="server"></asp:Label></td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelTotalNominal1" runat="server"></asp:Label></td>
                                    </tr>

                                    <asp:Literal ID="LiteralLaporan" runat="server"></asp:Literal>

                                    <tr class="success" style="font-weight: bold; font-size: 13px;">
                                        <td colspan="5" class="text-right">TOTAL</td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelTotalJumlah" runat="server"></asp:Label></td>
                                        <td class="text-right">
                                            <asp:Label ID="LabelTotalNominal" runat="server"></asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                    </div>
                    <asp:Button ID="ButtonTerima" runat="server" Text="Terima" OnClick="ButtonTerima_Click" CssClass="btn btn-success btn-sm" />
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressTransfer" runat="server" AssociatedUpdatePanelID="UpdatePanelTransfer">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressTransfer" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

