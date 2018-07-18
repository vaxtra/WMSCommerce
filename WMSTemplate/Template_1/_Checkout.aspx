<%@ Page Title="" Language="C#" MasterPageFile="~/frontend/MasterPage.master" AutoEventWireup="true" CodeFile="_Checkout.aspx.cs" Inherits="_Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <h2>Informasi Pelanggan</h2>
    <h3>Alamat email anda</h3>

    Alamat Email
    <asp:TextBox ID="TextBoxAlamatEmail" runat="server"></asp:TextBox><br />

    <h3>Alamat Pengiriman</h3>

    <%--    Nama Depan
    <asp:TextBox ID="TextBoxNamaDepan" runat="server"></asp:TextBox><br />

    Nama Belakang
    <asp:TextBox ID="TextBoxNamaBelakang" runat="server"></asp:TextBox><br />--%>

    Nama Lengkap
    <asp:TextBox ID="TextBoxNamaLengkap" runat="server"></asp:TextBox><br />

    Negara
    <asp:DropDownList ID="DropDownListNegara" runat="server"></asp:DropDownList><br />

    Provinsi
    <asp:DropDownList ID="DropDownListProvinsi" runat="server"></asp:DropDownList><br />

    Kota
    <asp:DropDownList ID="DropDownListKota" runat="server"></asp:DropDownList><br />

    Kecamatan
    <asp:DropDownList ID="DropDownListKecamatan" runat="server"></asp:DropDownList><br />

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

    <table>
        <asp:Repeater ID="RepeaterKurir" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:RadioButton ID="RadioButtonKurir" runat="server" GroupName="Kurir" />
                    </td>
                    <td><%# Eval("IDKurir") %></td>
                    <td><%# Eval("Nama") %></td>
                    <td><%# Eval("Biaya") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>

    <%--    (Logo JNT)
    Regular Service Rp 11.000--%>

    <asp:Button ID="ButtonKembaliKeInformasiPelanggan" runat="server" Text="Kembali ke Informasi Pelanggan" OnClick="ButtonKembaliKeInformasiPelanggan_Click" />
    <asp:Button ID="ButtonLanjutkanKePembayaran" runat="server" Text="Lanjutkan ke Pembayaran" OnClick="ButtonLanjutkanKePembayaran_Click" />

    <h2>Metode Pembayaran</h2>
    <h3>Semua transaksi dienkripsi. Data kartu kredit tidak pernah disimpan.</h3>

    <b>Bank Transfer</b><br />
    Pembayaran Lewat ATM atau internet Banking. Verifikasi manual.
    <br />
    <br />

    <asp:CheckBox ID="CheckBoxSubscribe" runat="server" Text="Pilih untuk Berlangganan Newsletter" />
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

    <%--  Masukan Kode Voucher Anda
    <asp:TextBox ID="TextBoxVoucher" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Button" />--%>

    Subtotal
    <asp:Label ID="LabelSubtotal" runat="server"></asp:Label><br />

    Biaya Pengiriman
    <asp:Label ID="LabelBiayaPengiriman" runat="server"></asp:Label><br />

    <%--Pajak
Rp 0--%>

    Total
  <asp:Label ID="LabelTotal" runat="server"></asp:Label><br />
</asp:Content>
