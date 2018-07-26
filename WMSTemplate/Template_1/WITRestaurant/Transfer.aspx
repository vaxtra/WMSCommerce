<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Transfer.aspx.cs" Inherits="WITRestaurant_SplitBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-header" style="margin-top: -20px;">
                <h3>
                    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label>
                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success pull-right" OnClick="ButtonSimpan_Click" />
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="text-center"><strong>
                                <asp:Label ID="LabelMejaAwal" runat="server"></asp:Label></strong></h4>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="display SplitBill1">
                                    <thead>
                                        <tr>
                                            <th class="hidden"></th>
                                            <th>Product</th>
                                            <th>Price</th>
                                            <th>Discount</th>
                                            <th>Qty</th>
                                            <th>Subtotal</th>
                                            <th style="width: 1%;"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterTransaksiDetail" runat="server" OnItemCommand="RepeaterTransaksiDetail_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="hidden">
                                                        <asp:Label ID="LabelIDDetailTransaksi" runat="server" Text='<%# Eval("IDDetailTransaksi") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <span style="font-weight: bold; font-size: 12px; width: 100%; text-align: left; white-space: pre-wrap; text-transform: uppercase;"><%# Eval("Nama") + (Parse.Decimal(Eval("PersentaseDiscount").ToString()) > 0 ? " - " + Pengaturan.FormatHarga(Eval("PersentaseDiscount")) + "%" : "") %></span>
                                                    </td>
                                                    <td class="text-right">
                                                        <span style="font-weight: bold; font-size: 12px; text-align: right;"><%# Pengaturan.FormatHarga(Eval("HargaJual")) %></span>
                                                    </td>
                                                    <td class="text-right">
                                                        <span style="font-weight: bold; font-size: 12px; color: red; text-align: left;"><%# Pengaturan.FormatHarga(Eval("PotonganHargaJual")) %></span>
                                                    </td>
                                                    <td class="text-right">
                                                        <span style="font-weight: bold; font-size: 12px; text-align: left;">
                                                            <asp:Label runat="server" Text='<%# Pengaturan.FormatHarga(Eval("JumlahProduk")) %>' ForeColor='<%# (Parse.Int(Eval("JumlahProduk").ToString()) < 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black %>'></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td class="text-right">
                                                        <span style="font-weight: bold; font-size: 12px; text-align: left;">
                                                            <asp:Label runat="server" Text='<%# Pengaturan.FormatHarga(Eval("Subtotal")) %>' ForeColor='<%# (Parse.Decimal(Eval("Subtotal").ToString()) < 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black %>'></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td style="width: 1%;">
                                                        <asp:ImageButton ID="ImageButtonPindah" ImageUrl="/WITPointOfSales/img/right.png" Style="width: 15px;" CommandName="Pindah" CommandArgument='<%# Eval("IDDetailTransaksi") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h4 class="text-center"><strong>
                                <asp:Label ID="LabelMejaTujuan" runat="server"></asp:Label></strong></h4>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="display SplitBill2">
                                    <thead>
                                        <tr>
                                            <th class="hidden"></th>
                                            <th style="width: 1%;"></th>
                                            <th>Product</th>
                                            <th>Price</th>
                                            <th>Discount</th>
                                            <th>Qty</th>
                                            <th>Subtotal</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterTransaksiSplitBill" runat="server" OnItemCommand="RepeaterTransaksiSplitBill_ItemCommand">
                                            <ItemTemplate>
                                                <tr class='<%# Parse.Int(Eval("JumlahDetail").ToString()) == (Container.ItemIndex + 1) ? "odd selected" : "odd" %>'>
                                                    <td class="hidden">
                                                        <asp:Label ID="LabelIDDetailTransaksi" runat="server" Text='<%# Eval("IDDetailTransaksi") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 1%;">
                                                        <asp:ImageButton ID="ImageButtonPindah" ImageUrl="/WITPointOfSales/img/left.png" Style="width: 15px;" CommandName="Pindah" CommandArgument='<%# Eval("IDDetailTransaksi") %>' runat="server" />
                                                    </td>
                                                    <td>
                                                        <span style="font-weight: bold; font-size: 12px; width: 100%; text-align: left; white-space: pre-wrap; text-transform: uppercase;"><%# Eval("Nama") + (Parse.Decimal(Eval("PersentaseDiscount").ToString()) > 0 ? " - " + Pengaturan.FormatHarga(Eval("PersentaseDiscount")) + "%" : "") %></span>
                                                    </td>
                                                    <td class="text-right">
                                                        <span style="font-weight: bold; font-size: 12px; text-align: right;"><%# Pengaturan.FormatHarga(Eval("HargaJual")) %></span>
                                                    </td>
                                                    <td class="text-right">
                                                        <span style="font-weight: bold; font-size: 12px; color: red; text-align: left;"><%# Pengaturan.FormatHarga(Eval("PotonganHargaJual")) %></span>
                                                    </td>
                                                    <td class="text-right">
                                                        <span style="font-weight: bold; font-size: 12px; text-align: left;">
                                                            <asp:Label runat="server" Text='<%# Pengaturan.FormatHarga(Eval("JumlahProduk")) %>' ForeColor='<%# (Parse.Int(Eval("JumlahProduk").ToString()) < 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black %>'></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td class="text-right">
                                                        <span style="font-weight: bold; font-size: 12px; text-align: left;">
                                                            <asp:Label runat="server" Text='<%# Pengaturan.FormatHarga(Eval("Subtotal")) %>' ForeColor='<%# (Parse.Decimal(Eval("Subtotal").ToString()) < 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black %>'></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

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

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

