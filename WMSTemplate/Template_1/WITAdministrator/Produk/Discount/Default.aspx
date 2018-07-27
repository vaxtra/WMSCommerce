<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_Produk_Discount_Event_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Discount
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonSimpan" Style="font-weight: bold;" class="btn btn-sm btn-primary hidden-print" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="table-responsive">
                <table class="table table-bordered table-condensed table-hover TableSorter">
                    <thead>
                        <tr class="active">
                            <th class="text-center" rowspan="2">No.</th>
                            <th class="text-center" rowspan="2">Kode</th>
                            <th class="text-center" rowspan="2">Produk</th>
                            <th class="text-center" rowspan="2">Warna</th>
                            <th class="text-center" rowspan="2">Varian</th>
                            <th class="text-center" rowspan="2">Kategori</th>
                            <th class="text-center" rowspan="2">Pemilik</th>
                            <th class="text-center" rowspan="2">Harga</th>
                            <th class="text-center" rowspan="2">Stok</th>
                            <th class="text-center" colspan="2">Disc. Store</th>
                            <th class="text-center" colspan="2">Disc. Consignment</th>
                        </tr>
                        <tr class="active">
                            <th class="text-center">Persentase</th>
                            <th class="text-center">Nominal</th>
                            <th class="text-center">Persentase</th>
                            <th class="text-center">Nominal</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterStokProduk" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex + 1 %></td>
                                    <td class="hidden">
                                        <asp:Label ID="LabelIDStokProduk" runat="server" Text='<%# Eval("IDStokProduk") %>'></asp:Label>
                                    </td>
                                    <td><%# Eval("KodeKombinasiProduk") %></td>
                                    <td><%# Eval("Nama") %></td>
                                    <td><%# Eval("Warna") %></td>
                                    <td><%# Eval("Varian") %></td>
                                    <td><%# Eval("Kategori") %></td>
                                    <td><%# Eval("Brand") %></td>
                                    <td class="text-right"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                    <td class="text-right"><%# Eval("Jumlah").ToFormatHargaBulat() %> </td>
                                    <td class="text-right">
                                        <asp:TextBox ID="TextBoxDiscountStorePersentase" Text='<%# Eval("DiscountStorePersentase") %>' Style="width: 60px;" CssClass="angka" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="text-right">
                                        <asp:TextBox ID="TextBoxDiscountStoreNominal" Text='<%# Eval("DiscountStoreNominal") %>' Style="width: 60px;" CssClass="angka" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="text-right">
                                        <asp:TextBox ID="TextBoxDiscountConsignmentPersentase" Text='<%# Eval("DiscountKonsinyasiPersentase") %>' Style="width: 60px;" CssClass="angka" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="text-right">
                                        <asp:TextBox ID="TextBoxDiscountConsignmentNominal" Text='<%# Eval("DiscountKonsinyasiNominal") %>' Style="width: 60px;" CssClass="angka" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

