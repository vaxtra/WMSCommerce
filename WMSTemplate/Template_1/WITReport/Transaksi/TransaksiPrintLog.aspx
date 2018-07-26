<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="TransaksiPrintLog.aspx.cs" Inherits="WITReport_Transaksi_TransaksiPringLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
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

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 text-center">
                    <h3>Laporan Log Print <%= Result != null ? Result["Periode"] : "" %></h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:ListBox ID="ListBoxTempat" runat="server" SelectionMode="Multiple" class="BootstrapMultiselect" multiple="multiple"></asp:ListBox>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ListBoxJenisTransaksi" runat="server" SelectionMode="Multiple" class="BootstrapMultiselect" multiple="multiple"></asp:ListBox>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ListBoxStatusTransaksi" runat="server" SelectionMode="Single" class="BootstrapMultiselect"></asp:ListBox>
                </div>
            </div>
            <div class="row form-row">
                <div class="col-md-12">
                    <br />
                    <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table>
                        <tr>
                            <td>
                                <div style="margin: 5px 5px 0 0">
                                    <asp:TextBox CssClass="form-control input-sm TanggalJam" Width="165px" ID="TextBoxTanggalAwal" runat="server"></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <div style="margin: 5px 5px 0 0">
                                    <asp:TextBox CssClass="form-control input-sm TanggalJam" Width="165px" ID="TextBoxTanggalAkhir" runat="server"></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <asp:Button CssClass="btn btn-primary btn-sm" Style="margin: 5px 5px 0 0" ID="ButtonCariTanggal" runat="server" Text="Cari" OnClick="ButtonCariTanggal_Click" />
                            </td>
<%--                            <td>
                                <div style="margin: 5px 5px 0 0">
                                    <asp:Button ID="ButtonExcel" runat="server" Text="Excel" CssClass="btn btn-info btn-sm" Style="font-weight: bold;" OnClick="ButtonExcel_Click" />

                                </div>
                            </td>--%>
                            <td>
                                <div style="margin: 5px 5px 0 0">
                                    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="btn btn-info btn-sm" Style="font-weight: bold;" />
                                </div>
                            </td>
                            <td>
                                <div style="margin: 5px 5px 0 0">
                                    <a id="LinkDownload" runat="server" visible="false">Download File</a>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table class="table table-bordered table-condensed table-hover" style="font-size: 12px;">
                            <thead>
                                <tr class="active">
                                    <th class="fitSize">No.</th>
                                    <th class="text-center">ID Transaksi</th>
                                    <th class="text-center">Log Print</th>
                                    <th class="text-center">Tanggal Transaksi</th>
                                    <th class="text-center">Pengguna</th>
                                    <th class="text-center">Pelanggan</th>
                                    <th class="text-center">Jenis</th>
                                    <th class="text-center">Status</th>
                                    <th class="text-center">Tanggal Pembayaran</th>
                                    <th class="text-center">Biaya Shipping</th>
                                    <th class="text-center">Keterangan</th>
                                    <th class="fitSize text-center">Grand Total</th>
                                    <th class="fitSize text-center">Total Pembayaran</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr style="font-weight: bold;">
                                    <td class="success" colspan="9"></td>
                                    <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalBiayaShipping"]) : "0" %></td>
                                    <td class="success"></td>
                                    <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalGrandTotal"]) : "0" %></td>
                                    <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalPembayaran"]) : "0" %></td>
                                </tr>

                                <asp:Repeater ID="RepeaterLaporan" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <asp:HiddenField ID="HiddenFieldIDTransaksi" runat="server" Value='<%# Eval("IDTransaksi") %>' />
                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                            <td class="fitSize"><a href='/WITPointOfSales/Detail.aspx?id=<%# Eval("IDTransaksi") %>'><%# Eval("IDTransaksi") %></a></td>
                                            <td class="fitSize warning">
                                                <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("LogPrint") %>'>
                                                    <ItemTemplate>
                                                        <%# Eval("Tanggal").ToFormatTanggalJam() %> - 
                                                        <%# Eval("TBPengguna.NamaLengkap") %>  
                                                        (<%# Eval("EnumPrintLog").ToString() == "1" ? "Point of Sales" :
                                                        Eval("EnumPrintLog").ToString() == "2" ? "Invoice" : "Packing Slip"%>) 
                                                        <br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td class="fitSize"><%# Eval("TanggalTransaksi").ToFormatTanggalJam() %></td>
                                            <td class="fitSize"><%# Eval("PenggunaTransaksi") %></td>
                                            <td class="fitSize">Nama : <%# Eval("Pelanggan") %>
                                                <br />
                                                Alamat : <%# Eval("Alamat") %>
                                                <br />
                                                Telepon :  <%# Eval("Telepon") %> </td>
                                            <td class="fitSize"><%# Eval("JenisTransaksi") %></td>
                                            <td><%# Eval("StatusTransaksi") %></td>
                                            <td class="fitSize"><%# Eval("TanggalPembayaran").ToFormatTanggalJam() %></td>
                                            <td class="fitSize text-right"><%# Eval("BiayaShipping").ToFormatHarga() %></td>
                                            <td><%# Eval("Keterangan") %></td>
                                            <td class="fitSize text-right success"><%# Eval("GrandTotal").ToFormatHarga() %></td>
                                            <td class="fitSize text-right success"><%# Eval("TotalPembayaran").ToFormatHarga() %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <tr style="font-weight: bold;">
                                    <td class="success" colspan="9"></td>
                                    <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalBiayaShipping"])  : "0" %></td>
                                    <td class="success"></td>
                                    <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalGrandTotal"]) : "0" %></td>
                                    <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalPembayaran"]) : "0" %></td>
                                </tr>
                            </tbody>
                        </table>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

