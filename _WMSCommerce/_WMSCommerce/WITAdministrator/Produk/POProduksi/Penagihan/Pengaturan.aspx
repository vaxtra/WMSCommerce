<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Pengaturan.aspx.cs" Inherits="WITAdministrator_Produk_POProduksi_Penagihan_Pengaturan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Invoice Purchase Order Product
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
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                        <b>PERINGATAN :</b>
                        <asp:Label ID="LabelPeringatan" runat="server" Text="-"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <strong>Vendor</strong>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="form-label bold">Pegawai</label>
                                        <asp:TextBox ID="TextBoxPegawai" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label bold">Tanggal</label>
                                        <asp:TextBox ID="TextBoxTanggal" runat="server" CssClass="form-control input-sm Tanggal"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="form-label bold">Vendor</label>
                                <asp:DropDownList ID="DropDownListVendor" CssClass="select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListVendor_SelectedIndexChanged"></asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidatorVendor" runat="server" ErrorMessage="-" ControlToValidate="DropDownListVendor" ForeColor="Red"
                                    Display="Dynamic" OnServerValidate="CustomValidatorVendor_ServerValidate" ValidationGroup="simpan"></asp:CustomValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>Detail</strong>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Penerimaan</label>
                                        <div class="table-responsive">
                                            <table class="table the-table table-bordered table-condensed table-hover">
                                                <thead>
                                                    <tr class="active">
                                                        <th></th>
                                                        <th>No</th>
                                                        <th>ID</th>
                                                        <th>Tanggal</th>
                                                        <th>Grandtotal</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterDetailPenerimaan" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="text-center fitSize">
                                                                    <asp:CheckBox ID="CheckBoxPilihPenerimaan" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxPilihPenerimaan_CheckedChanged" /></td>
                                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                <td>
                                                                    <asp:Label ID="LabelIDPenerimaanPOProduksiProduk" runat="server" Text='<%# Eval("IDPenerimaanPOProduksiProduk") %>'></asp:Label></td>
                                                                <td><%# Eval("TanggalTerima").ToFormatTanggal() %></td>
                                                                <td class="text-right warning"><strong><%# Eval("Grandtotal").ToFormatHarga() %></strong></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="success bold">
                                                        <td colspan="4" class="text-center"><b>TOTAL</b></td>
                                                        <td class="text-right">
                                                            <asp:Label ID="LabelTotalPenerimaan" runat="server" Text="0"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Retur</label>
                                        <div class="table-responsive">
                                            <table class="table the-table table-bordered table-condensed table-hover">
                                                <thead>
                                                    <tr class="active">
                                                        <th></th>
                                                        <th>No</th>
                                                        <th>ID</th>
                                                        <th>Tanggal</th>
                                                        <th>Grandtotal</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterRetur" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="text-center fitSize">
                                                                    <asp:CheckBox ID="CheckBoxPilihRetur" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxPilihRetur_CheckedChanged" /></td>
                                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                <td>
                                                                    <asp:Label ID="LabelIDPOProduksiProdukRetur" runat="server" Text='<%# Eval("IDPOProduksiProdukRetur") %>'></asp:Label></td>
                                                                <td><%# Eval("TanggalRetur").ToFormatTanggal() %></td>
                                                                <td class="text-right danger"><strong><%# Eval("Grandtotal").ToFormatHarga() %></strong></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="success bold">
                                                        <td colspan="4" class="text-center"><b>TOTAL</b></td>
                                                        <td class="text-right">
                                                            <asp:Label ID="LabelTotalRetur" runat="server" Text="0"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Down Payment</label>
                                        <div class="table-responsive">
                                            <table class="table the-table table-bordered table-condensed table-hover">
                                                <thead>
                                                    <tr class="active">
                                                        <th>No</th>
                                                        <th>ID</th>
                                                        <th>Tanggal</th>
                                                        <th>Down Payment</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterDownPayment" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="text-center fitSize"><%# Container.ItemIndex + 1 %></td>
                                                                <td>
                                                                    <asp:Label ID="LabelIDPOProduksiProduk" runat="server" Text='<%# Eval("IDPOProduksiProduk") %>'></asp:Label></td>
                                                                <td><%# Eval("TanggalDownPayment").ToFormatTanggal() %></td>
                                                                <td class="text-right info"><strong><%# Eval("DownPayment").ToFormatHarga() %></strong></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="success bold">
                                                        <td colspan="3" class="text-center"><b>TOTAL</b></td>
                                                        <td class="text-right">
                                                            <asp:Label ID="LabelTotalDownPayment" runat="server" Text="0"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label bold">Keterangan</label>
                                        <asp:TextBox runat="server" ID="TextBoxKeterangan" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="form-label bold">Total Penagihan</label>
                                        <asp:TextBox ID="TextBoxTotalPenagihan" runat="server" Enabled="false" CssClass="form-control input-sm text-right" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <asp:Button ID="ButtonSimpan" runat="server" Text="Simpan" CssClass="btn btn-success btn-sm" OnClick="ButtonSimpan_Click" ValidationGroup="simpan" OnClientClick="if (!confirm('Are you sure to save this data?')) return false;" />
                    <asp:Button ID="ButtonKembali" runat="server" Text="Kembali" CssClass="btn btn-danger btn-sm" OnClick="ButtonKembali_Click" />
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

