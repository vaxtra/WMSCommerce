<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Minimum.aspx.cs" Inherits="WITAdministrator_StokProduk_Bertambah" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_ButtonCariProduk(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCariProduk');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Pengaturan Minimum Stok Produk
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="table-responsive">
                <table class="table table-sm table-hover table-bordered">
                    <thead>
                        <tr class="thead-light">
                            <th>No</th>
                            <th>Kode</th>
                            <th>Brand</th>
                            <th>Produk</th>
                            <th>Varian</th>
                            <th>Warna</th>
                            <th>Kategori</th>
                            <th>Minimum</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterStokProduk" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td class="fitSize">
                                        <asp:Label ID="LabelIDStokProduk" runat="server" CssClass="d-none" Text='<%# Eval("IDStokProduk") %>'></asp:Label><%# Container.ItemIndex + 1 %></td>
                                    <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                    <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                    <td><%# Eval("Produk") %></td>
                                    <td class="fitSize"><%# Eval("Atribut") %></td>
                                    <td class="fitSize"><%# Eval("Warna") %></td>
                                    <td><%# Eval("Kategori") %></td>
                                    <td class="table-warning" style="width: 100px;">
                                        <asp:TextBox ID="TextBoxJumlahMinimum" runat="server" CssClass="form-control angka text-right form-control-sm" onfocus="this.select();" Text='<%# Eval("JumlahMinimum") %>'></asp:TextBox></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="card">
                <div class="card-footer">
                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" OnClick="ButtonSimpan_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="server">
</asp:Content>
