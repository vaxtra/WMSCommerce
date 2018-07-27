<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="BlackBox.aspx.cs" Inherits="WITAdministrator_Pelanggan_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Black Box
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonExcel" Style="font-weight: bold;" CssClass="btn btn-primary" runat="server" Text="Export" OnClick="ButtonExcel_Click" />
    <br />
    <a id="LinkDownload" runat="server" style="font-size: 14px;" visible="false">Download File</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
    <asp:DropDownList ID="DropDownListJumlahData" CssClass="select2" Style="width: 80px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
        <asp:ListItem Text="10" Value="10" />
        <asp:ListItem Text="20" Value="20" />
        <asp:ListItem Text="50" Value="50" />
        <asp:ListItem Text="100" Value="100" />
    </asp:DropDownList>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
    <button id="ButtonPrevious" runat="server" type="submit" class="btn" onserverclick="ButtonPrevious_Click">
        <span aria-hidden="true" class="glyphicon glyphicon-chevron-left"></span>
    </button>

    <asp:DropDownList ID="DropDownListHalaman" CssClass="select2" Style="width: 100px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
    </asp:DropDownList>

    <button id="ButtonNext" runat="server" type="submit" class="btn" onserverclick="ButtonNext_Click">
        <span aria-hidden="true" class="glyphicon glyphicon-chevron-right"></span>
    </button>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
    <div class="form-inline">
        <div class="form-group">
            <asp:TextBox ID="TextBoxCari" runat="server" CssClass="form-control input-sm"></asp:TextBox>
        </div>
        <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn" OnClick="EventData" ClientIDMode="Static" />
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table class="table table-bordered table-condensed table-hover TableSorter">
        <thead>
            <tr class="active">
                <th class="fitSize">No.</th>
                <th>Tanggal</th>
                <th>Pesan</th>
                <th>Halaman</th>
                <th class="fitSize hidden-print"></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="RepeaterLaporan" runat="server" OnItemCommand="RepeaterLaporan_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex + 1 + ((DropDownListJumlahData.SelectedValue.ToInt() * (DropDownListHalaman.SelectedValue.ToInt())))%></td>
                        <td class="text-right"><%# Eval("Tanggal").ToFormatTanggalJam() %></td>
                        <td><%# Eval("Pesan") %></td>
                        <td><%# Eval("Halaman") %></td>
                        <td class="fitSize hidden-print">
                            <asp:Button ID="ButtonHapus" runat="server" Text="Hapus" CssClass="btn btn-xs btn-danger" CommandName="Hapus" CommandArgument='<%# Eval("IDBlackBox") %>' /></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>

