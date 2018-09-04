<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITAdministrator_BahanBaku_Komposisi_Default" %>

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
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Komposisi Bahan Baku
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="/WITAdministrator/Import/KomposisiBahanBaku.aspx" class="btn btn-secondary btn-const mr-1">Import</a>
    <asp:Button ID="ButtonExport" runat="server" class="btn btn-secondary btn-const mr-1" Text="Export" OnClick="ButtonExport_Click" />
    <h6 class="mr-1 mt-2"><a id="LinkDownload" runat="server" visible="false">Download File</a></h6>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="form-group">
        <div class="card">
            <div class="card-header bg-smoke">
                <ul class="nav nav-tabs card-header-tabs">
                    <li class="nav-item"><a href="#tabKomposisi" id="Komposisi-tab" class="nav-link active" data-toggle="tab">Komposisi</a></li>
                    <li class="nav-item"><a href="#tabPemakaian" role="tab" id="Pemakaian-tab" class="nav-link" data-toggle="tab">Cari Pemakaian</a></li>
                </ul>
            </div>
            <div class="card-body">
                <div id="myTabContent" class="tab-content">
                    <div class="tab-pane active" id="tabKomposisi">
                        <asp:UpdatePanel ID="UpdatePanelKomposisi" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="DropDownListJumlahData" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="20" Value="20" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group float-right mb-0">
                                                <asp:Button ID="ButtonPrevious" runat="server" CssClass="btn btn-outline-light" Text="<<" OnClick="ButtonPrevious_Click" />
                                                <asp:DropDownList ID="DropDownListHalaman" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
                                                </asp:DropDownList>
                                                <asp:Button ID="ButtonNext" runat="server" CssClass="btn btn-outline-light" Text=">>" OnClick="ButtonNext_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <table class="table table-sm table-hover table-bordered TableSorter">
                                            <thead>
                                                <tr class="thead-light">
                                                    <th>No</th>
                                                    <th>Bahan Baku</th>
                                                    <th>Satuan</th>
                                                    <td class="fitSize">
                                                        <asp:ImageButton ID="ImageButtonUpdate" runat="server" ImageUrl="/assets/images/refresh.png" Style="vertical-align: middle" BorderStyle="None" OnClick="ImageButtonUpdate_Click" /></td>
                                                    <th>HPP Saat Ini</th>
                                                    <th>HPP Seharusnya</th>
                                                    <th>Komposisi</th>
                                                    <th></th>
                                                </tr>
                                                <tr class="thead-light">
                                                    <td colspan="8">
                                                        <asp:TextBox ID="TextBoxCari" runat="server" CssClass="form-control" onkeypress="return Func_ButtonCari(event)" placeholder="Cari Pegawai"></asp:TextBox></td>
                                                    <td class="d-none">
                                                        <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn btn-outline-light btn-block d-none" OnClick="EventData" ClientIDMode="Static" /></td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterBahanBaku" runat="server" OnItemCommand="RepeaterBahanBaku_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center fitSize"><%# Container.ItemIndex + 1 + ((DropDownListJumlahData.SelectedValue.ToInt() * (DropDownListHalaman.SelectedValue.ToInt()))) %></td>
                                                            <td><%# Eval("Nama") %></td>
                                                            <td><%# Eval("Satuan") %></td>
                                                            <td class="fitSize">
                                                                <asp:ImageButton ID="ImageButtonUpdate" runat="server" ImageUrl="/assets/images/refresh.png" Style="vertical-align: middle" BorderStyle="None" CommandName="UbahHargaPokokProduksi" CommandArgument='<%# Eval("IDBahanBaku") %>' Visible='<%# Eval("PunyaKomposisi").ToInt() > 0 ? (Eval("HargaPokokProduksi").ToFormatHarga() != Eval("HargaBeli").ToFormatHarga() ? true : false) : false %>' /></td>
                                                            <td class='<%# Eval("HargaBeli").ToFormatHarga() == Eval("HargaPokokProduksi").ToFormatHarga() ? "text-right text-success" : "text-right text-danger" %>'><%# Eval("HargaBeli").ToFormatHarga() %></td>
                                                            <td class="text-right"><%# Eval("HargaPokokProduksi").ToFormatHarga() %></td>
                                                            <td class="text-center"><%# Eval("PunyaKomposisi").ToInt() > 0 ? "<span class='badge badge-pill badge-success'>Punya</span>" : "<span class='badge badge-pill badge-danger'>Tidak</span>" %></td>
                                                            <td class="fitSize text-center"><a href='<%# "Pengaturan.aspx?id=" + Eval("IDBahanBaku") %>' class="btn btn-outline-info btn-xs">Ubah</a></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <asp:UpdateProgress ID="updateProgressKomposisi" runat="server" AssociatedUpdatePanelID="UpdatePanelKomposisi">
                                    <ProgressTemplate>
                                        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                            <asp:Image ID="imgUpdateProgressKomposisi" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane in" id="tabPemakaian">
                        <asp:UpdatePanel ID="UpdatePanelPemakaian" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <table class="w-100">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DropDownListBahanBaku" CssClass="select2 w-100" runat="server"></asp:DropDownList></td>
                                            <td class="fitSize">
                                                <asp:Button ID="ButtonCariPemakaian" runat="server" Text="Cari" CssClass="btn btn-outline-light btn-const" ClientIDMode="Static" OnClick="ButtonCariPemakaian_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-12 col-md-6">
                                            <div class="card">
                                                <h4 class="card-header bg-smoke">Bahan Baku</h4>
                                                <div class="table-responsive">
                                                    <table class="table table-sm table-hover table-bordered">
                                                        <thead>
                                                            <tr class="thead-light">
                                                                <th>No</th>
                                                                <th>Bahan Baku</th>
                                                                <th>Jumlah</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="RepeaterKomposisiBahanBaku" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                        <td><%# Eval("Nama") %></td>
                                                                        <td class="text-right"><strong><%# Eval("Jumlah").ToFormatHarga() %> <%# Eval("Satuan") %></strong></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-md-6">
                                            <div class="card">
                                                <h4 class="card-header bg-smoke">Produk</h4>
                                                <div class="table-responsive">
                                                    <table class="table table-sm table-hover table-bordered">
                                                        <thead>
                                                            <tr class="thead-light">
                                                                <th>No</th>
                                                                <th>Produk</th>
                                                                <th>Varian</th>
                                                                <th>Jumlah</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="RepeaterKomposisiProduk" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                        <td><%# Eval("Nama") %></td>
                                                                        <td><%# Eval("Varian") %></td>
                                                                        <td class="text-right"><strong><%# Eval("Jumlah").ToFormatHarga() %> <%# Eval("Satuan") %></strong></td>
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

                                <asp:UpdateProgress ID="updateProgressPemakaian" runat="server" AssociatedUpdatePanelID="UpdatePanelPemakaian">
                                    <ProgressTemplate>
                                        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                                            <asp:Image ID="imgUpdateProgressPemakaian" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
