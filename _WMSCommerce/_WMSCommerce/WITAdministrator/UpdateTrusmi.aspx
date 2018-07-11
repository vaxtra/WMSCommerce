<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateTrusmi.aspx.cs" Inherits="WITAdministrator_UpdateTrusmi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" />
            <asp:Label ID="LabelNotif" runat="server" Text="Label" Visible="false"></asp:Label>
            <br />
            <br />
            <asp:Label ID="LabelTanggal" runat="server" Text="Label"></asp:Label>
            <br />
            <table border="1">
                <tr>
                    <th>No.</th>
                    <th>Tanggal</th>
                    <th>Tempat</th>
                    <th>Pegawai</th>
                    <th>Kode</th>
                    <th>Produk</th>
                    <th>Varian</th>
                    <th>Kategori</th>
                    <th>Jumlah</th>
                </tr>
                <asp:Repeater ID="RepeaterData" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.ItemIndex + 1 %></td>
                            <td><%# Eval("Tanggal") %></td>
                            <td><%# Eval("Tempat") %></td>
                            <td><%# Eval("Pengguna") %></td>
                            <td><%# Eval("KodeKombinasiProduk") %></td>
                            <td><%# Eval("Produk") %></td>
                            <td><%# Eval("AtributProduk") %></td>
                            <td><%# Eval("Kategori") %></td>
                            <td><%# Eval("Jumlah") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>
</body>
</html>
