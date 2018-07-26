<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="WITAdministrator_Import_Produk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Import Excel
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
                <a runat="server" id="excelProduk">download template</a>
            </div>
            <div class="form-group">
                <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
            </div>
            <div class="form-inline">
                <div class="form-group">
                    <asp:FileUpload ID="FileUploadExcel" runat="server" />
                </div>
                <div class="form-group">
                    <asp:Button ID="ButtonUpload" CssClass="btn btn-success btn-sm" runat="server" Text="Upload" OnClick="ButtonUpload_Click" />
                    <a href="/WITAkuntansi/Default.aspx" class="btn btn-danger btn-sm">Kembali</a>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="table-responsive">
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr class="active">
                            <th class="text-center">Tanggal</th>
                            <%--<th class="text-center">Nama Lengkap</th>--%>
                            <th></th>
                            <th class="text-center">Keterangan</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterJurnal" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Tanggal").ToFormatTanggal() %></td>
                                    <%--<td><%# Eval("TBPengguna.NamaLengkap") %></td>--%>
                                    <td>
                                        <table class="table table-bordered table-condensed">
                                            <thead>
                                                <tr>
                                                    <th class="text-center">Akun</th>
                                                    <th class="text-right">Debit</th>
                                                    <th class="text-right">Kredit</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RepeaterDetail" runat="server" DataSource='<%# Eval("TBJurnalDetails") %>'>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("TBAkun.Kode") + " - " + Eval("TBAkun.Nama") %></td>
                                                            <td class="text-right"><%# (Eval("Debit").ToFormatHarga() != "0") ? Eval("Debit").ToFormatHarga() : "" %></td>
                                                            <td class="text-right"><%# (Eval("Kredit").ToFormatHarga() != "0") ? Eval("Kredit").ToFormatHarga() : "" %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td>
                                        <div><%# Eval("Keterangan") %></div>
                                        <br />
                                        <div><em>Ref : </em><%# Eval("Referensi") %></div>

                                        <asp:Repeater ID="RepeaterDokumen" runat="server" DataSource='<%# Eval("TBJurnalDokumens") %>'>
                                            <ItemTemplate>
                                                <br />
                                                <%# "<a href='/files/Akuntansi/" + Eval("IDJurnalDokumen") + Eval("Format") + "'>Download</a>"  %>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
