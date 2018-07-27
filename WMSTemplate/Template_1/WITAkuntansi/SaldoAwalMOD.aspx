<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="SaldoAwalMOD.aspx.cs" Inherits="WITAkuntansi_SaldoAwal" %>

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
                                        <td><%# Eval("Nomor") %></td>
                                        <td <%# Eval("Grup").ToBool() == true ? "class='hidden'" : string.Empty %>><%# Eval("Kode") %></td>
                                        <td <%# Eval("Grup").ToBool() == true ? "colspan='2' style='font-weight: bold;'" : string.Empty %>><%# Eval("Nama") %></td>
                                        <td class="text-right">
                                            <asp:TextBox ID="TextBoxNominalSaldoAwalAktiva" runat="server" Width="100%" onfocus="this.select();" CssClass="form-control InputDesimal input-sm text-right" Style="margin-top: 10px;"
                                                Text='<%# Eval("Nominal").ToFormatHarga() %>' Visible='<%# Eval("Grup").ToBool() == true ? false : true %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

