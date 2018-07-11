<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Proses.aspx.cs" Inherits="WITAdministrator_BahanBaku_POProduksi_ProduksiSendiri_Proses" %>

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
            <div class="form-group">
                <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                    <b>PERINGATAN :</b>
                    <asp:Label ID="LabelPeringatan" runat="server" Text="-"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-3">
                                    <label class="font-weight-bold text-muted">Proyeksi</label>
                                    <asp:TextBox ID="TextBoxIDProyeksi" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="font-weight-bold text-muted">ID Produksi</label>
                                    <asp:TextBox ID="TextBoxIDPOProduksiBahanBaku" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="font-weight-bold text-muted">Pegawai</label>
                                    <asp:TextBox ID="TextBoxPegawaiProses" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="font-weight-bold text-muted">Tanggal Proses</label>
                                    <asp:TextBox ID="TextBoxTanggalProses" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="font-weight-bold text-muted">Bahan Baku</label>
                            <div class="table-responsive">
                                <table class="table table-sm table-hover table-bordered mb-0">
                                    <thead>
                                        <tr class="thead-light">
                                            <th>No</th>
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
                                                    <td class="fitSize align-middle">
                                                        <asp:Label ID="LabelIDBahanBaku" runat="server" CssClass="d-none" Text='<%# Eval("IDBahanBaku") %>'></asp:Label><%# Container.ItemIndex + 1 %></td>
                                                    <td class="fitSize align-middle"><%# Eval("BahanBaku") %></td>
                                                    <td>
                                                        <div class="input-group w-auto">
                                                            <asp:TextBox ID="TextBoxSisa" CssClass="form-control text-right input-sm" runat="server" Text='<%# Eval("Sisa") %>' Enabled="false"></asp:TextBox>
                                                            <div class="input-group-prepend">
                                                                <div class="input-group-text" style="width: 80px;">
                                                                    <asp:Label ID="LabelSatuanSisa" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small" Text='<%# Eval("SatuanPO") %>'></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td <%# Eval("Stok").ToDecimal() < Eval("Sisa").ToDecimal() ? "class='text-right danger'" : "class='text-right'" %>>
                                                        <div class="input-group w-auto">
                                                            <asp:TextBox ID="TextBoxStok" CssClass="form-control text-right input-sm" runat="server" Text='<%# Eval("Stok") %>' Enabled="false"></asp:TextBox>
                                                            <div class="input-group-prepend">
                                                                <div class="input-group-text" style="width: 80px;">
                                                                    <asp:Label ID="LabelSatuanStok" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small" Text='<%# Eval("SatuanStok") %>'></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="input-group w-auto">
                                                            <asp:TextBox ID="TextBoxKirim" onfocus="this.select();" CssClass="form-control InputDesimal text-right JumlahTolak input-sm" runat="server" Text='<%# Eval("Sisa").ToDecimal() >= Eval("Stok").ToDecimal() ? Eval("Stok").ToDecimal() <= 0 ? 0 : Eval("Stok") : Eval("Sisa") %>'></asp:TextBox>
                                                            <div class="input-group-prepend">
                                                                <div class="input-group-text" style="width: 80px;">
                                                                    <asp:Label ID="LabelSatuanKirim" runat="server" CssClass="form-label" Font-Bold="true" Font-Size="Small" Text='<%# Eval("SatuanPO") %>'></asp:Label>
                                                                </div>
                                                            </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-const" OnClick="ButtonSimpan_Click" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                            <a href="Default.aspx" class="btn btn-danger btn-const">Kembali</a>
                    </div>
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

