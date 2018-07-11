<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Multistore.aspx.cs" Inherits="WITAdministrator_Produk_Stok_MultiStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
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

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Stock Multistore
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelTitleRight" runat="server">
        <ContentTemplate>
            <div class="form-inline">
                <div class="form-group">
                    <asp:Button ID="ButtonExcel" runat="server" Text="Export" CssClass="btn btn-dark btn-const mr-1" OnClick="ButtonExcel_Click" />
                    <h5><a id="LinkDownload" runat="server" visible="false" class="mr-1">Download File</a></h5>
                    <asp:Button ID="ButtonPrint" runat="server" Text="Cetak" CssClass="btn btn-dark btn-const" />
                </div>
            </div>

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
    <asp:UpdatePanel ID="UpdatePanelMultiStore" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:DropDownList ID="DropDownListKategoriTempat" CssClass="select2 mr-1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownListJenisStokProduk" CssClass="select2 mr-1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event">
                            <asp:ListItem Text="Semua" Value="0" />
                            <asp:ListItem Text="Ada Stok" Value="1" Selected="True" />
                            <asp:ListItem Text="Tidak Ada Stok" Value="2" />
                            <asp:ListItem Text="Minus" Value="3" />
                        </asp:DropDownList>
                        <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-primary btn-sm d-none" ClientIDMode="Static" OnClick="LoadData_Event" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <div class="table-responsive">
                            <table class="table table-sm table-bordered table-hover">
                                <thead>
                                    <tr class="thead-light">
                                        <th>No.</th>
                                        <th>Kode</th>
                                        <th>Produk</th>
                                        <th>Varian</th>
                                        <th>Brand</th>
                                        <th>Kategori</th>
                                        <th>Total</th>

                                        <asp:Repeater ID="RepeaterTempat" runat="server">
                                            <ItemTemplate>
                                                <th class="fitSize"><%# Eval("Nama") %></th>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <tr class="thead-light">
                                        <th></th>
                                        <th>
                                            <asp:TextBox ID="TextBoxKode" CssClass="form-control input-sm" ClientIDMode="Static" runat="server" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariProduk" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariAtributProduk" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariPemilikProduk" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                        <th>
                                            <asp:DropDownList ID="DropDownListCariKategoriProduk" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList></th>
                                        <th class="text-right fitSize" style="vertical-align: middle;"><%= Result != null ? Parse.ToFormatHargaBulat(Result["TempatTotal"]) : "" %></th>
                                        <asp:Repeater ID="RepeaterTotalTempat1" runat="server">
                                            <ItemTemplate>
                                                <th class="text-right fitSize" style="vertical-align: middle;"><%# Pengaturan.ReplaceNol(Eval("Total").ToFormatHargaBulat()) %></th>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterLaporan" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td class="fitSize"><%# Eval("KodeKombinasiProduk") %></td>
                                                <td><%# Eval("Produk") %></td>
                                                <td class="fitSize"><%# Eval("AtributProduk") %></td>
                                                <td class="fitSize"><%# Eval("PemilikProduk") %></td>
                                                <td><%# Eval("KategoriProduk") %></td>
                                                <td class="text-right fitSize table-info"><strong><%# Pengaturan.ReplaceNol(Eval("Total").ToFormatHargaBulat()) %></strong></td>

                                                <asp:Repeater ID="RepeaterStokProduk" runat="server" DataSource='<%# Eval("StokProduk") %>'>
                                                    <ItemTemplate>
                                                        <td <%# Eval("Stok") != null ? "class='text-right fitSize table-warning'" : "class='text-right fitSize'"  %>><%# Eval("Stok") != null ? Eval("Stok.Jumlah").ToFormatHargaBulat() : ""  %></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <tr class="table-success" style="font-weight: bold;">
                                        <td colspan="6"></td>
                                        <td class="text-right fitSize"><%= Result != null ? Parse.ToFormatHargaBulat(Result["TempatTotal"]) : "" %></td>
                                        <asp:Repeater ID="RepeaterTotalTempat2" runat="server">
                                            <ItemTemplate>
                                                <td class="text-right fitSize"><%# Pengaturan.ReplaceNol(Eval("Total").ToFormatHargaBulat()) %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="updateProgressMultiStore" runat="server" AssociatedUpdatePanelID="UpdatePanelMultiStore">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressMultiStore" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

