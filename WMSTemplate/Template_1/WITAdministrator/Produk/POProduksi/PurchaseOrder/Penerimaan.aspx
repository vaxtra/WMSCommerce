<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Penerimaan.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_PurchaseOrder_Penerimaan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Penerimaan Produk
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
    <asp:UpdatePanel ID="UpdatePanelData" runat="server">
        <ContentTemplate>
            <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                <b>PERINGATAN :</b>
                <asp:Label ID="LabelPeringatan" runat="server" Text="-"></asp:Label>
            </div>
            <div class="card">
                <div class="card-body">
                    <h3 class="border-bottom text-info">VENDOR</h3>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                    <label class="font-weight-bold text-muted">Pegawai</label>
                                    <asp:TextBox ID="TextBoxPegawai" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                    <label class="font-weight-bold text-muted">Tanggal Penerimaan</label>
                                    <asp:TextBox ID="TextBoxTanggalPenerimaan" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                    <label class="font-weight-bold text-muted">Vendor</label>
                                    <asp:DropDownList ID="DropDownListCariVendor" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCariVendor_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">
                                    <label class="font-weight-bold text-muted">ID Produksi</label>
                                    <asp:DropDownList ID="DropDownListIDPOProduksi" runat="server" CssClass="select2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListIDPOProduksi_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h3 class="border-bottom text-info">DETAIL</h3>
                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Produk</label>
                        <div class="table-responsive">
                            <table class="table table-sm table-hover table-bordered" id="daftarPenerimaan">
                                <thead>
                                    <tr class="thead-light">
                                        <th>No</th>
                                        <th class="hidden"></th>
                                        <th>Produk</th>
                                        <th>Varian</th>
                                        <th>Sisa Pesanan</th>
                                        <th>Datang</th>
                                        <th>Terima</th>
                                        <th>Tolak Ke Vendor</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterDetail" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                                                <td class="hidden">
                                                    <asp:Label ID="LabelIDKombinasiProduk" runat="server" Text='<%# Eval("IDKombinasiProduk") %>'></asp:Label></td>
                                                <td><%# Eval("Produk") %></td>
                                                <td class="text-center"><%# Eval("Atribut") %></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxSisaPesanan" Width="100%" CssClass="form-control InputInteger text-right input-sm" runat="server" Text='<%# Eval("Sisa") %>' Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxJumlahDatang" onfocus="this.select();" Width="100%" CssClass="form-control InputInteger text-right JumlahTolak input-sm" runat="server" Text='<%# Eval("Sisa") %>'></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxJumlahTerima" onfocus="this.select();" Width="100%" CssClass="form-control InputInteger text-right JumlahTolak input-sm" runat="server" Text='<%# Eval("Sisa") %>'></asp:TextBox>
                                                </td>
                                                <td class="danger">
                                                    <asp:TextBox ID="TextBoxJumlahTolakKeVendor" onfocus="this.select();" Width="100%" CssClass="form-control InputInteger text-right JumlahTolak input-sm" runat="server" Text="0"></asp:TextBox></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="font-weight-bold text-muted">Keterangan</label>
                        <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:Button ID="ButtonTerima" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" OnClick="ButtonTerima_Click" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                    <a href="Default.aspx" class="btn btn-danger btn-const">Kembali</a>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgressData" runat="server" AssociatedUpdatePanelID="UpdatePanelData">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressData" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script>
        $(document).ready(function () {
            $(".JumlahTolak").each(function () {
                $(this).blur(function () {
                    var daftarStok = document.getElementById("daftarPenerimaan");

                    for (var i = 1; i in daftarStok.rows; i++) {
                        var r = daftarStok.rows[i];
                        var SisaPesanan = r.getElementsByTagName("input")[0].value.replace(',', '');
                        var JumlahDatang = r.getElementsByTagName("input")[1].value.replace(',', '');
                        var JumlahTerima = r.getElementsByTagName("input")[2].value.replace(',', '');

                        //jumlah datang = 0
                        if (JumlahDatang == '' || JumlahDatang.length == 0) {
                            JumlahDatang = 0;
                        }
                        //jumlah terima = 0
                        if (JumlahTerima == '' || JumlahTerima.length == 0) {
                            JumlahTerima = 0;
                        }

                        if (parseInt(JumlahTerima) >= parseInt(JumlahDatang)) {
                            JumlahTerima = JumlahDatang;
                        }

                        r.getElementsByTagName("input")[1].value = parseInt(JumlahDatang).toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
                        r.getElementsByTagName("input")[2].value = parseInt(JumlahTerima).toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");

                        var hasil = parseInt(JumlahDatang) - parseInt(JumlahTerima);
                        r.getElementsByTagName("input")[3].value = parseInt(hasil).toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
                    }
                });
            });
        });

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                $(".select2").select2();

                $(".JumlahTolak").each(function () {
                    $(this).blur(function () {
                        var daftarStok = document.getElementById("daftarPenerimaan");

                        for (var i = 1; i in daftarStok.rows; i++) {
                            var r = daftarStok.rows[i];
                            var SisaPesanan = r.getElementsByTagName("input")[0].value.replace(',', '');
                            var JumlahDatang = r.getElementsByTagName("input")[1].value.replace(',', '');
                            var JumlahTerima = r.getElementsByTagName("input")[2].value.replace(',', '');

                            //jumlah datang = 0
                            if (JumlahDatang == '' || JumlahDatang.length == 0) {
                                JumlahDatang = 0;
                            }
                            //jumlah terima = 0
                            if (JumlahTerima == '' || JumlahTerima.length == 0) {
                                JumlahTerima = 0;
                            }

                            if (parseInt(JumlahTerima) >= parseInt(JumlahDatang)) {
                                JumlahTerima = JumlahDatang;
                            }

                            r.getElementsByTagName("input")[1].value = parseInt(JumlahDatang).toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
                            r.getElementsByTagName("input")[2].value = parseInt(JumlahTerima).toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");

                            var hasil = parseInt(JumlahDatang) - parseInt(JumlahTerima);
                            r.getElementsByTagName("input")[3].value = parseInt(hasil).toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
                        }
                    });
                });
            }
        }
    </script>
</asp:Content>

