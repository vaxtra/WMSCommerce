<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_BahanBaku_Stok_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_ButtonCariBahanBaku(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCariBahanBaku');
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
    Stok Bahan Baku
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelTitleRight" runat="server">
        <ContentTemplate>
            <div class="form-inline">
                <div class="form-group">
                    <asp:Button ID="ButtonExcel" CssClass="btn btn-dark btn-const mr-1" runat="server" Text="Export" OnClick="ButtonExcel_Click" />
                    <h5><a id="LinkDownload" runat="server" class="mr-1" visible="false">Download File</a></h5>
                    <asp:Button ID="ButtonCetakBahanBaku" CssClass="btn btn-dark btn-const" runat="server" Text="Cetak" />
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
    <asp:UpdatePanel ID="UpdatePanelStokBahanBaku" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownListTempat" CssClass="select2 mr-1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                                <asp:DropDownList ID="DropDownListKondisiStokBahanBaku" CssClass="select2 mr-1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadData_Event"></asp:DropDownList>
                                <asp:Button ID="ButtonCariBahanBaku" runat="server" Text="Cari" CssClass="btn btn-primary d-none" ClientIDMode="Static" OnClick="LoadData_Event" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-inline float-right">
                            <div class="form-group">
                                <h4 class="text-success">SUBTOTAL : 
                        <asp:Label ID="LabelSubtotal" runat="server" Text="0"></asp:Label></h4>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="table-responsive">
                    <table class="table table-sm table-hover table-bordered">
                        <thead>
                            <tr class="thead-light">
                                <th>No</th>
                                <th>Kode</th>
                                <th>Bahan Baku</th>
                                <th>Kategori</th>
                                <th class="fitSize">Harga Beli</th>
                                <th class="fitSize">Batas Stok</th>
                                <th>Stok</th>
                                <th>Status</th>
                            </tr>
                            <tr class="thead-light">
                                <th></th>
                                <th>
                                    <asp:TextBox ID="TextBoxCariKodeBahanBaku" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariBahanBaku(event)"></asp:TextBox></th>
                                <th>
                                    <asp:TextBox ID="TextBoxCariBahanBaku" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCariBahanBaku(event)"></asp:TextBox></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariKategoriBahanBaku" runat="server" Width="100%" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCariKategoriBahanBaku_SelectedIndexChanged">
                                    </asp:DropDownList></th>
                                <th colspan="3">
                                    <asp:DropDownList ID="DropDownListCariSatuanBahanBaku" runat="server" Width="100%" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCariSatuanBahanBaku_SelectedIndexChanged">
                                        <asp:ListItem Text="Satuan Besar" Value="Besar" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Satuan Kecil" Value="Kecil"></asp:ListItem>
                                    </asp:DropDownList></th>
                                <th>
                                    <asp:DropDownList ID="DropDownListCariStatusBahanBaku" runat="server" Width="100%" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCariStatusBahanBaku_SelectedIndexChanged">
                                        <asp:ListItem Text="Semua" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Cukup" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Butuh Restok" Value="2"></asp:ListItem>
                                    </asp:DropDownList></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterStokBahanBaku" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                        <td class="fitSize"><%# Eval("KodeBahanBaku") %></td>
                                        <td><%# Eval("BahanBaku") %></td>
                                        <td><%# Eval("KategoriBahanBaku") %></td>
                                        <td class="text-right fitSize"><%# Eval("HargaBeli").ToFormatHarga() %> /<%# Eval("Satuan") %></td>
                                        <td class="text-right fitSize"><%# Eval("JumlahMinimum").ToFormatHarga() %> <%# Eval("Satuan") %></td>
                                        <td class="text-right fitSize warning"><strong><%# Eval("Jumlah").ToFormatHarga() %> <%# Eval("Satuan") %></strong></td>
                                        <td class="text-center fitSize"><%# StokBahanBaku_Class.StatusStokBahanBaku(Eval("Status").ToString()) %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressStokBahanBaku" runat="server" AssociatedUpdatePanelID="UpdatePanelStokBahanBaku">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressStokBahanBaku" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
