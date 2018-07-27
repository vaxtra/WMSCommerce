<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Transaksi.aspx.cs" Inherits="WITPointOfSales_Transaksi" %>

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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row page-header" style="margin-top: -20px;">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h3>Transaksi</h3>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="form-inline">
                    <div class="btn-group" style="margin: 5px 5px 0 0">
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHari" runat="server" Text="Hari Ini" OnClick="ButtonHari_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMinggu" runat="server" Text="Minggu Ini" OnClick="ButtonMinggu_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulan" runat="server" Text="Bulan Ini" OnClick="ButtonBulan_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahun" runat="server" Text="Tahun Ini" OnClick="ButtonTahun_Click" />
                    </div>
                    <div class="btn-group" style="margin: 5px 5px 0 0">
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonHariSebelumnya" runat="server" Text="Kemarin" OnClick="ButtonHariSebelumnya_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMingguSebelumnya" runat="server" Text="Minggu Lalu" OnClick="ButtonMingguSebelumnya_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonBulanSebelumnya" runat="server" Text="Bulan Lalu" OnClick="ButtonBulanSebelumnya_Click" />
                        <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonTahunSebelumnya" runat="server" Text="Tahun Lalu" OnClick="ButtonTahunSebelumnya_Click" />
                    </div>
                    <div style="margin: 5px 5px 0 0" class="form-group">
                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                        <asp:TextBox CssClass="form-control input-sm Tanggal" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                        <asp:Button CssClass="btn btn-primary btn-sm" ID="ButtonCari" runat="server" Text="Cari" OnClick="ButtonCari_Click" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                    <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
                </div>
                <div class="table-responsive">
                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                        <thead>
                            <tr class="active">
                                <th class="text-center">No.</th>
                                <th class="text-center">ID Transaksi</th>
                                <th class="text-center">Jenis</th>
                                <th class="text-center"></th>
                                <th class="text-center">Tanggal</th>
                                <th class="text-center" runat="server" id="PanelPengirim1" visible="false">Pengirim</th>
                                <th class="text-center">
                                    <abbr title="Pengguna Transaksi">Pengguna</abbr>
                                </th>
                                <th class="text-center">Pelanggan</th>
                                <th class="text-center">Status</th>
                                <th class="text-center">Keterangan</th>
                                <th class="text-center">Grandtotal</th>
                                <th></th>
                            </tr>
                            <tr class="active">
                                <td></td>
                                <td class="text-center">
                                    <asp:TextBox ID="TextBoxIDTransaksi" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                </td>
                                <td class="text-center">
                                    <asp:TextBox ID="TextBoxJenisTransaksi" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                </td>
                                <td class="text-center">
                                    <asp:TextBox ID="TextBoxMeja" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td class="text-center" runat="server" id="PanelPengirim2" visible="false">
                                    <asp:TextBox ID="TextBoxPengirim" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                </td>
                                <td class="text-center">
                                    <asp:TextBox ID="TextBoxPenggunaTransaksi" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                </td>
                                <td class="text-center">
                                    <asp:TextBox ID="TextBoxPelanggan" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListStatusTransaksi" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="DropDownListStatusTransaksi_SelectedIndexChanged" CssClass="select2" runat="server">
                                        <asp:ListItem Text="- Semua Transaksi -" Selected="True" Value="0" />
                                        <asp:ListItem Text="Awaiting Payment" Value="2" />
                                        <asp:ListItem Text="Awaiting Payment Verification" Value="3" />
                                        <asp:ListItem Text="Complete" Value="5" />
                                        <asp:ListItem Text="Canceled" Value="6" />
                                    </asp:DropDownList></td>
                                <td class="text-center">
                                    <asp:TextBox ID="TextBoxKeterangan" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                </td>
                                <td class="text-center">
                                    <asp:TextBox ID="TextBoxGrandtotal" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterTransaksi" runat="server" OnItemCommand="RepeaterTransaksi_ItemCommand" OnItemDataBound="RepeaterTransaksi_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize text-center"><%# Container.ItemIndex + 1 %></td>
                                        <td class="fitSize" style="vertical-align: middle;"><a href="/WITPointOfSales/Detail.aspx?id=<%# Eval("IDTransaksi") %>"><%# Eval("IDTransaksi") %></a></td>
                                        <td class="fitSize" style="vertical-align: middle;"><%# Eval("JenisTransaksi") %></td>
                                        <td class="fitSize" style="vertical-align: middle;"><%# Eval("Meja") %></td>
                                        <td class="fitSize" style="vertical-align: middle;"><%# Pengaturan.FormatTanggalJam(Eval("TanggalTransaksi")) %></td>
                                        <td class="fitSize" style="vertical-align: middle;" runat="server" id="PanelPengirim" visible="false"><%# Eval("Pengirim") %></td>
                                        <td style="vertical-align: middle;"><%# Eval("PenggunaTransaksi") %></td>
                                        <td style="vertical-align: middle;"><%# Eval("Pelanggan") %></td>
                                        <td class='<%# Eval("ClassStatus") %>' style="vertical-align: middle;"><%# Eval("Status") %></td>
                                        <td><%# Eval("Keterangan") %></td>
                                        <td class="fitSize text-right" style="vertical-align: middle;"><strong><%# Pengaturan.FormatHarga(Eval("GrandTotal")) %></strong></td>
                                        <td class="fitSize">
                                            <a href="/WITPointOfSales/Default.aspx?id=<%# Eval("IDTransaksi") %>" class='<%# (bool)Eval("UbahOrder") ? "btn btn-sm btn-info" : "hidden" %>'>Ubah Order</a>
                                            <a href="/WITPointOfSales/Default.aspx?id=<%# Eval("IDTransaksi") %>" class='<%# (bool)Eval("TransaksiBaru") ? "btn btn-sm btn-primary" : "hidden" %>'>Transaksi Baru</a>
                                            <a href="/WITPointOfSales/Default.aspx?id=<%# Eval("IDTransaksi") %>&do=Retur" class="<%# (bool)Eval("Retur") ? "btn btn-sm btn-warning" : "hidden" %>">Retur</a>
                                            <asp:Button ID="ButtonBatal" CommandName="Batal" CommandArgument='<%# Eval("IDTransaksi") %>' runat="server" Text="Batal" Visible='<%# Eval("Batal") %>' CssClass="btn btn-sm btn-danger"
                                                OnClientClick='<%# "return confirm(\"Apakah Anda yakin ?\")" %>' />
                                            <a href="Detail.aspx?id=<%# Eval("IDTransaksi") %>" class="btn btn-sm btn-default btn-outline">Detail</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>

            <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderSupervisor" runat="server" PopupControlID="PanelSupervisor" TargetControlID="LinkButton1" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <div id="PanelSupervisor" class="text-center">
                <div class="col-lg-12">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="ButtonKeluarSupervisor" ClientIDMode="Static" runat="server" Text="Keluar" CssClass="btn btn-danger pull-right" />
                                <h4 class="modal-title">
                                    <asp:Label ID="LabelKeterangan" runat="server"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="text-center">
                                    <h4>Login Supervisor</h4>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Username</label>
                                            <div class="col-sm-5">
                                                <asp:HiddenField ID="HiddenFieldIDTransaksi" runat="server" />
                                                <asp:TextBox ID="TextBoxUsername" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Password</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="TextBoxPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"></label>
                                            <div class="col-sm-5 text-right">
                                                <asp:Button ID="ButtonBatal" runat="server" CssClass="btn btn-danger" Text="Batal Transaksi" OnClick="ButtonBatal_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div style='position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;'>
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
