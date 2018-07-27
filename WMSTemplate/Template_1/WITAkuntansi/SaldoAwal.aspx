<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="SaldoAwal.aspx.cs" Inherits="WITAkuntansi_SaldoAwal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Begining Balance
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
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="form-group">
                <div class="form-inline">
                    <div style="margin: 5px 5px 0 0" class="form-group">
                        <asp:DropDownList ID="DropDownListBulan" CssClass="select2" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownListTahun" CssClass="select2" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="ButtonCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="ButtonCari_Click" />
                        <asp:Button ID="ButtonSubmit" runat="server" CssClass="btn btn-success btn-sm" Text="Simpan" OnClick="ButtonSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div id="peringatan" class="alert alert-danger" runat="server" visible="false">
                <b>PERINGATAN :</b>
                <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Begening Balance
                </div>
                <div class="table-responsive">
                    <table class="table table-condensed table-hover table-bordered">
                        <thead>
                            <tr class="active">
                                <th>No</th>
                                <th>Kode</th>
                                <th>Nama</th>
                                <th>Nominal</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterLaporan" runat="server">
                                <ItemTemplate>
                                    <tr <%# Eval("ClassWarna") %>>
                                        <asp:HiddenField runat="server" ID="HiddenFieldIDAkun" Value='<%# Eval("IDAkun") %>' />
                                        <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("Grup") %>' />
                                        <td class="bold"><%# Eval("Nomor") %></td>
                                        <td <%# Eval("Grup").ToBool() == true ? "class='hidden'" : string.Empty %>><%# Eval("Kode") %></td>
                                        <td <%# Eval("Grup").ToBool() == true ? "colspan='2' style='font-weight: bold;'" : string.Empty %>><%# Eval("Nama") %></td>
                                        <td>
                                            <asp:TextBox ID="TextBoxNominalSaldoAwalAktiva" runat="server"
                                                CssClass="form-control InputDesimal input-sm text-right" Width="100%" onfocus="this.select();"
                                                Text='<%# Eval("Nominal").ToFormatHarga() %>'
                                                Visible='<%#Eval("Grup").ToBool() == true ? false : true %>'
                                                Enabled='<%#Eval("StatusSaldoAwal").ToBool() == true ? true : false%>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="cold-md-6">
            <div class="row form-row">
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Summary
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="table-responsive">
                                            <table class="table table-condensed table-hover table-bordered">
                                                <thead>
                                                    <tr class="active">
                                                        <th colspan="3" class="text-center" style="width: 50%;">ASSETS</th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" class="text-center"><strong>
                                                            <asp:Label ID="LabelTotalSaldoAktiva" runat="server" Text=""></asp:Label></strong></td>
                                                    </tr>
                                                    <tr class="active">
                                                        <th class="text-center">NO</th>
                                                        <th class="text-center">NAMA</th>
                                                        <th class="text-center">NOMINAL</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterLaporanAktiva" runat="server">
                                                        <ItemTemplate>
                                                            <tr <%# Eval("ClassWarna") %>>
                                                                <td class="bold"><%# Eval("Nomor") %></td>
                                                                <td <%# (Eval("Grup").ToBool()) == true ? "style='font-weight: bold;'" : string.Empty %>><%# Eval("Nama") %></td>
                                                                <td class="text-right"><%# (Eval("StatusParent").ToBool()) == true ? "" : (Eval("Nominal").ToFormatHarga()) %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="table-responsive">
                                            <table class="table table-condensed table-hover table-bordered">
                                                <thead>
                                                    <tr class="active">
                                                        <th colspan="3" class="text-center" style="width: 50%;">LIABILITIES</th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" class="text-center"><strong>
                                                            <asp:Label ID="LabelTotalSaldoPasiva" runat="server" Text=""></asp:Label></strong></td>
                                                    </tr>
                                                    <tr class="active">
                                                        <th class="text-center">NO</th>
                                                        <th class="text-center">NAMA</th>
                                                        <th class="text-center">NOMINAL</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterLaporanPasiva" runat="server">
                                                        <ItemTemplate>
                                                            <tr <%# Eval("ClassWarna") %>>
                                                                <td class="bold"><%# Eval("Nomor") %></td>
                                                                <td <%# (Eval("Grup").ToBool()) == true ? "style='font-weight: bold;'" : string.Empty %>><%# Eval("Nama") %></td>
                                                                <td class="text-right"><%# (Eval("StatusParent").ToBool()) == true ? "" : (Eval("Nominal").ToFormatHarga()) %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="success">
                                                        <td></td>
                                                        <td>Laba/Rugi Bulan Berjalan </td>
                                                        <td>
                                                            <asp:Label ID="LabelLabaRugiBulanBerjalan" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    <tr class="success">
                                                        <td></td>
                                                        <td>Laba/Rugi Bulan Sebelumnya </td>
                                                        <td>
                                                            <asp:Label ID="LabelLabaRugiBulanSebelumnya" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>


