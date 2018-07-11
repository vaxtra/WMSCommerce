<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Proses.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_ProduksiKeVendor_Proses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Pengurangan Bahan Baku Untuk Produksi
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
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="form-label bold">Proyeksi</label>
                                <asp:TextBox ID="TextBoxIDProyeksi" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label bold">ID Produksi</label>
                                <asp:TextBox ID="TextBoxIDPOProduksiProduk" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label bold">Pegawai</label>
                                <asp:TextBox ID="TextBoxPegawaiProses" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label bold">Tanggal Proses</label>
                                <asp:TextBox ID="TextBoxTanggalProses" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>Komposisi</strong>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-condensed table-hover table-bordered">
                                <thead>
                                    <tr class="active">
                                        <th>No</th>
                                        <th class="text-center hidden"></th>
                                        <th class="fitSize">Bahan Baku</th>
                                        <th>Belum Dikirim</th>
                                        <th>Stok</th>
                                        <th>Kirim</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepeaterKomposisi" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                                <td class="hidden">
                                                    <asp:Label ID="LabelIDBahanBaku" runat="server" Text='<%# Eval("IDBahanBaku") %>'></asp:Label></td>
                                                <td class="fitSize"><%# Eval("BahanBaku") %></td>
                                                <td>
                                                    <div class="input-group" style="width: 100%;">
                                                        <asp:TextBox ID="TextBoxSisa" CssClass="form-control text-right input-sm" runat="server" Text='<%# Eval("Sisa") %>' Enabled="false"></asp:TextBox>
                                                        <span class="input-group-addon input-sm" style="width: 50px;">
                                                            <asp:Label ID="LabelSatuanSisa" runat="server" CssClass="form-label" Font-Size="Small" Font-Bold="true" Text='<%# Eval("SatuanPO") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td <%# Eval("Stok").ToDecimal() < Eval("Sisa").ToDecimal() ? "class='text-right danger'" : "class='text-right'" %>>
                                                    <div class="input-group" style="width: 100%;">
                                                        <asp:TextBox ID="TextBoxStok" CssClass="form-control text-right input-sm" runat="server" Text='<%# Eval("Stok") %>' Enabled="false"></asp:TextBox>
                                                        <span class="input-group-addon input-sm" style="width: 50px;">
                                                            <asp:Label ID="LabelSatuanStok" runat="server" CssClass="form-label" Font-Size="Small" Font-Bold="true" Text='<%# Eval("SatuanStok") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="input-group" style="width: 100%;">
                                                        <asp:TextBox ID="TextBoxKirim" onfocus="this.select();" CssClass="form-control InputDesimal text-right JumlahTolak input-sm" runat="server" Text='<%# Eval("Sisa").ToDecimal() >= Eval("Stok").ToDecimal() ? Eval("Stok").ToDecimal() <= 0 ? 0 : Eval("Stok") : Eval("Sisa") %>'></asp:TextBox>
                                                        <span class="input-group-addon input-sm" style="width: 50px;">
                                                            <asp:Label ID="LabelSatuanKirim" runat="server" CssClass="form-label" Font-Size="Small" Font-Bold="true" Text='<%# Eval("SatuanPO") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-sm" OnClick="ButtonSimpan_Click" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                    <a href="Default.aspx" class="btn btn-danger btn-sm">Kembali</a>
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
</asp:Content>

