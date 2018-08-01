<%@ Page Title="" Language="C#" MasterPageFile="~/frontend/MasterPage.master" AutoEventWireup="true" CodeFile="_Thankyou.aspx.cs" Inherits="_Thankyou" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <h2>Terimakasih atas Pemesanan Anda!</h2>

    Email konfirmasi telah dikirim ke alamat email <b>
        <asp:Literal ID="LiteralEmail" runat="server"></asp:Literal></b><br />
    Nomor order anda adalah:<b>#<asp:Literal ID="LiteralIDTransaksi" runat="server"></asp:Literal></b>

    <h3>Alamat Pengiriman</h3>
    <asp:Literal ID="LiteralNamaLengkap" runat="server"></asp:Literal><br />
    <asp:Literal ID="LiteralAlamat" runat="server"></asp:Literal>, 
    <asp:Literal ID="LiteralKecamatan" runat="server"></asp:Literal><br />
    <asp:Literal ID="LiteralKota" runat="server"></asp:Literal><br />
    <asp:Literal ID="LiteralProvinsi" runat="server"></asp:Literal>
    <asp:Literal ID="LiteralKodePos" runat="server"></asp:Literal><br />
    <asp:Literal ID="LiteralNegara" runat="server"></asp:Literal>

    <h3>Informasi Pembayaran</h3>
    Metode Pembayaran: <b>Bank Transfer</b><br />
    Status Pembayaran: <b>pending</b><br />

    <h3>Informasi Pembayaran</h3>
    Silahkan transfer pembayaran ke rekening dibawah ini :<br />
    <br />

    Bank BCA dan Bank Lainnya<br />
    Nomor Rekening : 0083222520<br />
    Atas Nama : Rendy Herdiawan<br />
    <br />
    Bank Mandiri<br />
    Nomor Rekening : 1300014799525<br />
    Atas Nama : Rendy Herdiawan<br />
    <br />
    Jenius / Bank BTPN<br />
    Nomor Rekening : 90010898929<br />
    Atas Nama : Rendy Herdiawan<br />
    <br />
    Konfirmasi Pembayaran / Butuh Bantuan<br />
    Setelah transfer, lakukan konfirmasi pembayaran ke nomor 0855-5942-2680 (Whatsapp/SMS) atau klik disini<br />

    <%-- <h2>Detail Pemesanan</h2>

    Camila
    1 x Camila
        Hitam / 37 

    Rp.99.000

    Subtotal

    Rp.99.000

    Biaya Pengiriman

    Rp.11.000

    Pajak

    Rp.0

    Total

    Rp.110.000

    <br />
    <br />--%>
    <a href="Default.aspx">Lanjutkan Belanja</a>
</asp:Content>
