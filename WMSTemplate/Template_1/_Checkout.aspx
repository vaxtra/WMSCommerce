<%@ Page Title="" Language="C#" MasterPageFile="~/frontend/MasterPage.master" AutoEventWireup="true" CodeFile="_Checkout.aspx.cs" Inherits="_Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <h2>Informasi Pelanggan</h2>
    <h3>Alamat email anda</h3>

    Alamat Email
    <asp:TextBox ID="TextBoxAlamatEmail" runat="server"></asp:TextBox><br />

    <h3>Alamat Pengiriman</h3>

    Nama Lengkap
    <asp:TextBox ID="TextBoxNamaLengkap" runat="server"></asp:TextBox><br />

    Negara
    <asp:DropDownList ID="DropDownListNegara" runat="server">
        <asp:ListItem Selected="True" Text="INDONESIA" Value="1"></asp:ListItem>
    </asp:DropDownList><br />

    Provinsi
    <asp:DropDownList ID="DropDownListProvinsi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListProvinsi_SelectedIndexChanged"></asp:DropDownList><br />

    Kota
    <asp:DropDownList ID="DropDownListKota" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListKota_SelectedIndexChanged"></asp:DropDownList><br />

    Kecamatan
    <asp:DropDownList ID="DropDownListKecamatan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListKecamatan_SelectedIndexChanged"></asp:DropDownList><br />

    Alamat
    <asp:TextBox ID="TextBoxAlamat" runat="server"></asp:TextBox><br />

    Kode Pos
    <asp:TextBox ID="TextBoxKodePos" runat="server"></asp:TextBox><br />

    Nomor Telepon
    <asp:TextBox ID="TextBoxNomorTelepon" runat="server"></asp:TextBox><br />

    <br />

    <asp:Button ID="ButtonKembaliKeKeranjangBelanja" runat="server" Text="Kembali ke Keranjang Belanja" OnClick="ButtonKembaliKeKeranjangBelanja_Click" />
    <asp:Button ID="ButtonLanjutkanKePengiriman" runat="server" Text="Lanjutkan ke Pengiriman" OnClick="ButtonLanjutkanKePengiriman_Click" />

    <br />
    <br />

    <h2>Detail Pengiriman</h2>

    <h3>Alamat Pengiriman</h3>

    <asp:Literal ID="LiteralNamaLengkap" runat="server"></asp:Literal><br />

    <asp:Literal ID="LiteralAlamat" runat="server"></asp:Literal><br />

    <asp:Literal ID="LiteralKecamatan" runat="server"></asp:Literal>,
    <asp:Literal ID="LiteralKota" runat="server"></asp:Literal><br />

    <asp:Literal ID="LiteralProvinsi" runat="server"></asp:Literal>
    <asp:Literal ID="LiteralKodePos" runat="server"></asp:Literal><br />

    <asp:Literal ID="LiteralNegara" runat="server"></asp:Literal><br />

    <asp:Literal ID="LiteralNomorTelepon" runat="server"></asp:Literal><br />

    <h2>Pilih Jasa Pengiriman</h2>
    <asp:Literal ID="LiteralWarningPilihJasaPengiriman" runat="server"></asp:Literal>
    <asp:RadioButtonList ID="RadioButtonListKurir" runat="server">
    </asp:RadioButtonList>

    <asp:Button ID="ButtonKembaliKeInformasiPelanggan" runat="server" Text="Kembali ke Informasi Pelanggan" OnClick="ButtonKembaliKeInformasiPelanggan_Click" />
    <asp:Button ID="ButtonLanjutkanKePembayaran" runat="server" Text="Lanjutkan ke Pembayaran" OnClick="ButtonLanjutkanKePembayaran_Click" />

    <h2>Metode Pembayaran</h2>
    <h3>Semua transaksi dienkripsi. Data kartu kredit tidak pernah disimpan.</h3>
    <asp:Literal ID="LiteralWarningPilihMetodePembayaran" runat="server"></asp:Literal>

    <asp:RadioButtonList ID="RadioButtonListJenisPembayaran" runat="server">
    </asp:RadioButtonList>
    <br />
    <br />

    <asp:Button ID="ButtonKembaliKeDetailPengiriman" runat="server" Text="Kembali ke Detail Pengiriman" OnClick="ButtonKembaliKeDetailPengiriman_Click" />
    <asp:Button ID="ButtonProsesPemesanan" runat="server" Text="Proses Pemesanan" OnClick="ButtonProsesPemesanan_Click" />

    <h2>DETAIL PEMESANAN</h2>

    <table>
        <thead>
            <tr>
                <th>Produk</th>
                <th>Jumlah</th>
                <th>Total</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="RepeaterCart" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("Nama") %><br />
                            <img src='<%# Eval("Foto") %>' style="height: 100px; width: 100px;" /><br />
                        </td>
                        <td><%# Eval("Quantity") %></td>
                        <td><%# Eval("Total").ToFormatHarga() %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    Subtotal
    <asp:Label ID="LabelSubtotal" runat="server"></asp:Label><br />

    Biaya Pengiriman
    <asp:Label ID="LabelBiayaPengiriman" runat="server"></asp:Label><br />

    Total
  <asp:Label ID="LabelTotal" runat="server"></asp:Label><br />
</asp:Content>
