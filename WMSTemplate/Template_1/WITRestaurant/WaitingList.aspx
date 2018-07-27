<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="WaitingList.aspx.cs" Inherits="WITRestaurant_WaitingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-header" style="margin-top: -20px;">
                <h3>
                    <asp:DropDownList ID="DropDownListPilihanWaitingList" runat="server" OnSelectedIndexChanged="DropDownListPilihanWaitingList_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Pending" Value="1" />
                        <asp:ListItem Text="Selesai" Value="2" />
                        <asp:ListItem Text="Batal" Value="3" />
                        <asp:ListItem Text="Semua" Value="0" />
                    </asp:DropDownList>
                    <div class="pull-right">
                        <asp:Button ID="ButtonTambah" runat="server" Text="Tambah" CssClass="btn btn-success" OnClick="ButtonTambah_Click" />
                        <asp:Button ID="ButtonKeluar" runat="server" Text="Keluar" CssClass="btn btn-danger" OnClick="ButtonKeluar_Click" />
                    </div>
                </h3>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:MultiView ID="MultiViewWaitingList" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    Waiting List
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr class="active">
                                                <th>No.</th>
                                                <th>Tanggal</th>
                                                <th>Pelanggan</th>
                                                <th>Phone</th>
                                                <th>Meja</th>
                                                <th class="text-right">PAX</th>
                                                <th>Keterangan</th>
                                                <th style="width: 150px;"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterWaitingList" runat="server" OnItemCommand="RepeaterWaitingList_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td>
                                                            <asp:Button ID="ButtonUbah" runat="server" Text='<%# Pengaturan.FormatTanggal(Eval("TanggalMasuk")) %>' CssClass="btn btn-sm" CommandName="Ubah" CommandArgument='<%# Eval("IDWaitingList") %>' />
                                                        </td>
                                                        <td class="NoWrap"><%# Eval("TBPelanggan.NamaLengkap") %></td>
                                                        <td class="NoWrap"><a href='<%# "tel:" + Eval("TBPelanggan.Handphone") %>'><%# Eval("TBPelanggan.Handphone") %></a></td>
                                                        <td class="NoWrap"><%# Eval("TBMeja.Nama") %></td>
                                                        <td class="text-right"><%# Pengaturan.FormatHarga(Eval("JumlahTamu")) %></td>
                                                        <td><%# Eval("Keterangan") %></td>
                                                        <td>
                                                            <asp:Button ID="ButtonPending" runat="server" Text="Pending" CssClass="btn btn-sm btn-warning" CommandName="Pending" CommandArgument='<%# Eval("IDWaitingList") %>' Visible='<%# ((PilihanWaitingList)Eval("EnumStatusReservasi") == PilihanWaitingList.Batal || (PilihanWaitingList)Eval("EnumStatusReservasi") == PilihanWaitingList.Selesai) %>' />
                                                            <asp:Button ID="ButtonSelesai" runat="server" Text="Selesai" CssClass="btn btn-sm btn-success" CommandName="Selesai" CommandArgument='<%# Eval("IDWaitingList") %>' Visible='<%# ((PilihanWaitingList)Eval("EnumStatusReservasi") == PilihanWaitingList.Pending) %>' />
                                                            <asp:Button ID="ButtonBatal" runat="server" Text="Batal" CssClass="btn btn-sm btn-danger" CommandName="Batal" CommandArgument='<%# Eval("IDWaitingList") %>' Visible='<%# ((PilihanWaitingList)Eval("EnumStatusReservasi") == PilihanWaitingList.Pending || (PilihanWaitingList)Eval("EnumStatusReservasi") == PilihanWaitingList.Selesai) %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-6">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Data Waiting List
                                        </div>
                                        <div class="panel-body">
                                            <asp:HiddenField ID="HiddenFieldIDWaitingList" runat="server" />
                                            <div class="form-group">
                                                <label>Tanggal</label>
                                                <asp:TextBox ID="TextBoxTanggal" runat="server" class="form-control TanggalJam"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Pelanggan</label>
                                                <asp:TextBox ID="TextBoxNama" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Phone</label>
                                                <asp:TextBox ID="TextBoxPhone" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Meja</label>
                                                <asp:DropDownList ID="DropDownListMeja" Width="100%" CssClass="select2" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>PAX</label>
                                                <asp:TextBox ID="TextBoxPax" runat="server" class="form-control angka"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Keterangan</label>
                                                <asp:TextBox ID="TextBoxKeterangan" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <asp:Button ID="ButtonSimpan" CssClass="btn btn-success" runat="server" Text="Simpan" OnClick="ButtonSimpan_Click" />
                                            <asp:Button ID="ButtonKembali" runat="server" Text="Kembali" CssClass="btn btn-danger" OnClick="ButtonKembali_Click" />
                                        </div>
                                        <div class="col-md-3"></div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
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

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

