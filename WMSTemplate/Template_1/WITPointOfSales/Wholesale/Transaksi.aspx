<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Transaksi.aspx.cs" Inherits="WITPointOfSales_Wholesale_Transaksi" %>

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

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="row page-header" style="margin-top: -20px;">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <h3 class="text-center">Wholesale</h3>
                            </div>
                            <div class="col-md-4">
                                <h3>
                                    <a href="Default.aspx" class="btn btn-success btn-sm pull-right">Tambah</a></h3>
                            </div>
                        </div>
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
                    <table class="table table-bordered table-hover table-condensed" style="font-size: 12px;">
                        <thead>
                            <tr class="active">
                                <th class="text-center fitSize">No.</th>
                                <th class="text-center">ID Transaksi</th>
                                <th class="text-center">Tanggal</th>
                                <th class="text-center">Pelanggan</th>
                                <th class="text-center">Status</th>
                                <th class="text-center">Keterangan</th>
                                <th class="text-center">Jumlah</th>
                                <th class="text-center">Grandtotal</th>
                                <th></th>
                            </tr>
                            <tr class="active">
                                <td></td>
                                <td class="text-center">
                                    <asp:TextBox ID="TextBoxIDTransaksi" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox></td>
                                <td></td>
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
                                    <asp:TextBox ID="TextBoxJumlah" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                </td>
                                <td class="text-center">
                                    <asp:TextBox ID="TextBoxGrandtotal" runat="server" CssClass="form-control input-sm" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterTransaksi" runat="server" OnItemCommand="RepeaterTransaksi_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td class="fitSize text-center"><%# Container.ItemIndex + 1 %></td>
                                        <td class="fitSize"><a href="/WITPointOfSales/Detail.aspx?id=<%# Eval("IDTransaksi") %>&returnUrl=/WITWholesale/Transaksi.aspx"><%# Eval("IDTransaksi") %></a></td>
                                        <td class="fitSize"><%# Pengaturan.FormatTanggal(Eval("TanggalTransaksi")) %></td>
                                        <td class="text-middle"><%# Eval("Pelanggan") %></td>
                                        <td class='<%# Eval("ClassStatus") %>'><%# Eval("Status") %></td>
                                        <td class="text-middle"><%# Eval("Keterangan") %></td>
                                        <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("JumlahProduk")) %></td>
                                        <td class="fitSize text-right"><%# Pengaturan.FormatHarga(Eval("GrandTotal")) %></td>
                                        <td class="fitSize">
                                            <a href="/WITPointOfSales/Default.aspx?id=<%# Eval("IDTransaksi") %>" class='<%# (bool)Eval("UbahOrder") ? "btn btn-sm btn-success" : "btn btn-sm btn-success hidden"  %>'>Point Of Sales >></a>
                                            <a href="Default.aspx?id=<%# Eval("IDTransaksi") %>" class='<%# (bool)Eval("UbahOrder") ? "btn btn-sm btn-info" : "btn btn-sm btn-info hidden" %>'>Ubah Order</a>
                                            <a href="Default.aspx?id=<%# Eval("IDTransaksi") %>" class='<%# (bool)Eval("TransaksiBaru") ? "btn btn-sm btn-primary" : "btn btn-sm btn-primary hidden" %>'>Transaksi Baru</a>
                                            <asp:Button ID="ButtonBatal" CommandName="Batal" CommandArgument='<%# Eval("IDTransaksi") %>' runat="server" Text="Batal" Visible='<%# Eval("Batal") %>' CssClass="btn btn-sm btn-danger" OnClientClick='<%# "return confirm(\"Apakah Anda yakin membatalkan transaksi\")" %>' />
                                            <a href="/WITPointOfSales/Detail.aspx?id=<%# Eval("IDTransaksi") %>" class="btn btn-sm btn-default btn-outline">Detail</a>
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
                                                <asp:TextBox ID="TextBoxUsername" CssClass="form-control" onkeypress="return Func_TextBoxUsername(event)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Password</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="TextBoxPassword" ClientIDMode="Static" onkeypress="return Func_TextBoxPassword(event)" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"></label>
                                            <div class="col-sm-5 text-right">
                                                <asp:Button ID="ButtonBatal" runat="server" ClientIDMode="Static" CssClass="btn btn-danger" Text="Batal Transaksi" OnClick="ButtonBatal_Click" />
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
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

