<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_BahanBaku_Transfer_Terima_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Penerimaan Transfer Bahan Baku
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <h3><span class="badge badge-info font-weight-normal">
        <asp:Label ID="LabelIDTransferBahanBaku" runat="server"></asp:Label></span></h3>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelTransfer" runat="server">
        <ContentTemplate>
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="card h-100">
                            <h4 class="card-header bg-smoke">Pengirim</h4>
                            <div class="card-body">
                                <div class="form-group">
                                    <label class="form-label bold text-muted">Tempat</label>
                                    <asp:DropDownList ID="DropDownListTempatPengirim" Style="width: 100%" CssClass="select2" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold text-muted">Pengirim</label>
                                    <asp:DropDownList ID="DropDownListPenggunaPengirim" Style="width: 100%" CssClass="select2" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold text-muted">Tanggal</label>
                                    <asp:TextBox ID="TextBoxTanggalKirim" CssClass="form-control input-sm TanggalJam" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="card h-100">
                            <h4 class="card-header bg-smoke">Penerima</h4>
                            <div class="card-body">
                                <div class="form-group">
                                    <label class="form-label bold text-muted">Tempat</label>
                                    <asp:DropDownList ID="DropDownListTempatPenerima" Style="width: 100%" CssClass="select2" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label class="form-label bold text-muted">Keterangan</label>
                                    <asp:TextBox ID="TextBoxKeterangan" CssClass="form-control input-sm" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="card">
                    <h4 class="card-header bg-smoke">Detail</h4>
                    <div class="table-responsive">
                        <table class="table table-sm table-hover table-bordered mb-0">
                            <thead>
                                <tr class="thead-light">
                                    <th>No.</th>
                                    <th>Bahan Baku</th>
                                    <th>Satuan</th>
                                    <th>Kategori</th>
                                    <th>Kode</th>
                                    <th>Harga</th>
                                    <th>Quantity</th>
                                    <th>Subtotal</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterTransferBahanBaku" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                            <td><%# Eval("BahanBaku") %></td>
                                            <td class="fitSize"><%# Eval("SatuanBesar") %></td>
                                            <td><%# Eval("Kategori") %></td>
                                            <td class="fitSize"><%# Eval("KodeBahanBaku") %></td>
                                            <td class="text-right fitSize"><%# Eval("HargaBeli").ToFormatHarga() %></td>
                                            <td class="text-right fitSize"><%# Eval("Jumlah").ToFormatHarga() %></td>
                                            <td class="text-right fitSize"><%# Eval("Subtotal").ToFormatHarga() %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <tr class="table-success">
                                    <td colspan="7" class="text-center font-weight-bold">TOTAL</td>
                                    <td class="text-right font-weight-bold">
                                        <asp:Label ID="LabelTotalNominal" runat="server"></asp:Label></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:Button ID="ButtonTerima" runat="server" Text="Terima" OnClick="ButtonTerima_Click" CssClass="btn btn-success btn-const" />
                <a href="Default.aspx" class="btn btn-danger btn-const">Kembali</a>
            </div>

            <asp:UpdateProgress ID="updateProgressTransfer" runat="server" AssociatedUpdatePanelID="UpdatePanelTransfer">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgressTransfer" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

