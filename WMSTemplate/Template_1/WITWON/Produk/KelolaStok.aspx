<%@ Page Title="" Language="C#" MasterPageFile="~/WITWON/MasterPageWebView.master" AutoEventWireup="true" CodeFile="KelolaStok.aspx.cs" Inherits="WITWON_Produk_KelolaStok" %>

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

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleLeft" runat="Server">
    <asp:Label ID="LabelJudul" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonSimpan" runat="server" Text="SIMPAN" CssClass="btn btn-success btn-sm" ClientIDMode="Static" OnClick="ButtonSimpan_Click" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelStokOpname" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="table-responsive">
                        <table class="table table-condensed table-bordered table-hover" style="font-size: 12px;">
                            <thead>
                                <tr class="active">
                                    <th>No.</th>
                                    <th>Produk</th>
                                    <th>Kategori</th>
                                    <th>Varian</th>
                                    <th>Quantity</th>
                                    <th>Stok</th>
                                </tr>
                                <tr class="active">
                                    <th></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxProduk" Style="width: 100%;" CssClass="form-control input-sm" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                    <th>
                                        <asp:DropDownList ID="DropDownListKategori" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                                        </asp:DropDownList></th>
                                    <th>
                                        <asp:DropDownList ID="DropDownListVarian" Style="width: 100%;" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                                        </asp:DropDownList></th>
                                    <th>
                                        <asp:TextBox ID="TextBoxQuantity" Style="width: 100%;" runat="server" CssClass="text-right form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                    <th class="fitSize">
                                        <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-primary btn-sm btn-block" ClientIDMode="Static" OnClick="LoadData_Event" /></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr style="font-weight: bold;" class="success">
                                    <td colspan="4"></td>
                                    <td class="text-right">
                                        <%= Result != null ? Parse.ToFormatHargaBulat(Result["TotalQuantity"]) : "0" %>
                                    </td>
                                    <td></td>
                                </tr>

                                <asp:Repeater ID="RepeaterLaporan" runat="server">
                                    <ItemTemplate>
                                        <tr style="border-top: 3px double #aaa !important;">
                                            <td class="fitSize" rowspan='<%# Eval("JumlahStok") %>'>
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td rowspan='<%# Eval("JumlahStok") %>'><%# Eval("Produk") %></td>
                                            <td rowspan='<%# Eval("JumlahStok") %>'><%# Eval("Kategori") %></td>

                                            <asp:Repeater ID="RepeaterKombinasiProduk" runat="server" DataSource='<%# Eval("Stok") %>'>
                                                <ItemTemplate>
                                                    <td class="fitSize">
                                                        <asp:Label ID="LabelIDStokProduk" Visible="false" runat="server" Text='<%# Eval("IDStokProduk") %>'></asp:Label><%# Eval("Varian") %></td>
                                                    <td class="text-right fitSize">
                                                        <%# Eval("Jumlah").ToFormatHargaBulat() %>
                                                    </td>
                                                    <td style="width: 100px;">
                                                        <asp:TextBox ID="TextBoxStokTerbaru" runat="server" CssClass="form-control text-right input-sm InputInteger" onkeypress="return Func_ButtonSimpan(event)"></asp:TextBox></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <tr style="font-weight: bold; border-top: 3px double #aaa !important;" class="success">
                                    <td colspan="4"></td>
                                    <td class="text-right">
                                        <%= Result != null ? Parse.ToFormatHargaBulat(Result["TotalQuantity"]) : "0" %>
                                    </td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
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
