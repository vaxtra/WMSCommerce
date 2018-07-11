<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Birthday.aspx.cs" Inherits="WITAdministrator_Pelanggan_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Birthday Pelanggan
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
    <asp:DropDownList ID="DropDownListJumlahData" CssClass="select2" Style="width: 80px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
        <asp:ListItem Text="10" Value="10" />
        <asp:ListItem Text="20" Value="20" />
        <asp:ListItem Text="50" Value="50" />
        <asp:ListItem Text="100" Value="100" />
    </asp:DropDownList>

    <asp:DropDownList ID="DropDownListBulan" CssClass="select2" Style="width: 150px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EventData">
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
            <asp:TextBox ID="TextBoxCari" runat="server" CssClass="form-control" onkeypress="return Func_ButtonCari(event)"></asp:TextBox>
        </div>
        <asp:Button ID="ButtonCari" runat="server" Text="Cari" CssClass="btn" OnClick="EventData" ClientIDMode="Static" />
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="table-responsive">
        <table class="table table-bordered table-condensed table-hover TableSorter">
            <thead>
                <tr class="active">
                    <th class="fitSize">No.</th>
                    <th>Grup</th>
                    <th>Nama</th>
                    <th>Tanggal Lahir</th>
                    <th>Quantity</th>
                    <th>Transaksi</th>
                    <th>Nominal</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RepeaterPelanggan" runat="server" OnItemCommand="RepeaterPelanggan_ItemCommand">
                    <ItemTemplate>
                        <tr class='<%# Eval("TanggalLahir") == null ? "" : Eval("TanggalLahir").ToDateTime().Day == DateTime.Now.Day ? "success" : "" %>'>
                            <td><%# Container.ItemIndex + 1 + (DropDownListJumlahData.SelectedValue.ToInt() * (DropDownListHalaman.SelectedValue.ToInt()))%></td>
                            <td><%# Eval("Grup") %></td>
                            <td><%# Eval("NamaLengkap") %></td>
                            <td class="ParseDate"><%# Eval("TanggalLahir") %></td>
                            <td class="OutputDesimal text-right"><%# Eval("Quantity") %></td>
                            <td class="OutputDesimal text-right"><%# Eval("Transaksi") %></td>
                            <td class="OutputDesimal text-right"><%# Eval("Nominal") %></td>
                            <td><%# Pengaturan.FormatDataStatus(Eval("Status")) %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

