<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Laporan_Transaksi_Transaksi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function Func_ButtonCari(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCari');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function Func_ButtonSimpan(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonSimpan');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
    <style>
        table.table tr th {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelJudul" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelTitleRight" runat="server">
        <ContentTemplate>
            <asp:Button ID="ButtonCetak" runat="server" Text="Cetak" CssClass="btn btn-dark btn-const" />

            <asp:UpdateProgress ID="updateProgressTitleRight" runat="server" AssociatedUpdatePanelID="UpdatePanelTitleRight">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressTitleLeft" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelStokOpname" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:DropDownList ID="DropDownListTempat" CssClass="select2 mr-1" runat="server" Enabled="false"></asp:DropDownList>
                        <asp:DropDownList ID="DropDownListJenisStok" CssClass="select2 mr-1" runat="server"></asp:DropDownList>
                        <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="LoadData_Event" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="table-responsive">
                    <table class="table table-sm table-bordered table-hover">
                        <thead>
                            <tr class="thead-light">
                                <th>No.</th>
                                <th>Produk</th>
                                <th>Warna</th>
                                <th>Brand</th>
                                <th>Kategori</th>
                                <th>Kode</th>
                                <th>Varian</th>
                                <th>Harga</th>
                                <th>Quantity</th>
                                <th>Stok</th>
                            </tr>
                            <tr class="thead-light">
                                <th></th>
                                <th>
                                    <asp:TextBox ID="TextBoxProduk" CssClass="form-control input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListWarna" runat="server" CssClass="select2">
                                    </asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListBrand" runat="server" CssClass="select2">
                                    </asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListKategori" runat="server" CssClass="select2">
                                    </asp:DropDownList></th>
                                <th>
                                    <asp:TextBox ID="TextBoxKodeProduk" CssClass="form-control input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListVarian" runat="server" CssClass="select2">
                                    </asp:DropDownList></th>
                                <th></th>
                                <th></th>
                                <th class="fitSize">
                                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-block" ClientIDMode="Static" OnClick="ButtonSimpan_Click" /></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize" rowspan='<%# Eval("Count") %>'>
                                            <%# Container.ItemIndex + 1 %>
                                        </td>
                                        <td rowspan='<%# Eval("Count") %>'><%# Eval("Produk") %></td>
                                        <td rowspan='<%# Eval("Count") %>'><%# Eval("Warna") %></td>
                                        <td rowspan='<%# Eval("Count") %>'><%# Eval("Brand") %></td>
                                        <td rowspan='<%# Eval("Count") %>'><%# Eval("Kategori") %></td>

                                        <asp:Repeater ID="RepeaterKombinasiProduk" runat="server" DataSource='<%# Eval("Stok") %>'>
                                            <ItemTemplate>
                                                <td class="align-middle fitSize">
                                                    <%# Eval("Kode") %>
                                                    <asp:Label ID="LabelIDStokProduk" Visible="false" runat="server" Text='<%# Eval("IDStokProduk") %>'></asp:Label>
                                                </td>
                                                <td class="align-middle fitSize"><%# Eval("Varian") %></td>
                                                <td class="align-middle text-righ fitSize"><%# Eval("HargaJual").ToFormatHarga() %></td>
                                                <td class="align-middle text-right fitSize table-warning font-weight-bold">
                                                    <%# Eval("Jumlah").ToFormatHargaBulat() %>
                                                </td>
                                                <td class="table-success" style="width: 100px;">
                                                    <asp:TextBox ID="TextBoxStokTerbaru" runat="server" CssClass="form-control text-right form-control-sm InputInteger" onfocus="this.select();" onkeypress="return Func_ButtonSimpan(event)"></asp:TextBox></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressStokOpname" runat="server" AssociatedUpdatePanelID="UpdatePanelStokOpname">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressStokOpname" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
