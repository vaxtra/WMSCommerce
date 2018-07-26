<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="RasioKeuangan.aspx.cs" Inherits="WITAkuntansi_RasioKeuangan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:Label ID="LabelHeader" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
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
                        <asp:DropDownList ID="DropDownListTahun" CssClass="select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <table class="table table-condensed table-hover table-bordered">
                    <thead>
                        <tr class="info">
                            <th class="text-left" style="font-size: x-large;"></th>
                            <th class="text-center" style="font-size: x-large;">2014
                            <asp:Label ID="Label1" runat="server"></asp:Label></th>
                            <th class="text-center" style="font-size: x-large;">2015
                            <asp:Label ID="Label2" runat="server"></asp:Label></th>
                            <th class="text-center" style="font-size: x-large;">Annual Growth </th>
                            <th class="text-center" style="font-size: x-large;">Description</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="active">
                            <th class="text-left" style="font-size: x-large;" colspan="4">Liquidity Ratios</th>
                            <th></th>
                        </tr>
                        <tr>
                            <th class="text-left">Current Ratio</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur kemampuan perusahaan dalam membayar kewajiban jangka pendek dengan aktiva lancar yang dimilikinya.
                            </td>
                        </tr>
                        <tr>
                            <th class="text-left">Quick Ratio</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur kemampuan perusahaan dalam membayar kewajiban yang harus segera dipenuhi dengan aktiva lancar yang lebih liquid (liquid assets).</td>
                        </tr>
                        <tr>
                            <th class="text-left">Cash Ratio</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur kemampuan perusahaan membayar kewajiban jangka pendek menggunakan kas dan surat berharga yang dimiliki.</td>
                        </tr>
                        <tr class="active" style="font-size: x-large;">
                            <th class="text-left" style="font-size: x-large;" colspan="5">Leverage Ratios</th>
                        </tr>
                        <tr>
                            <th class="text-left">Debt to Assets Ratio</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur banyaknya dana pinjaman  yang sudah dimanfaatkan untuk membiayai asset perusahaan.  Semakin rendah ratio yang didapat maka semakin baik.</td>
                        </tr>
                        <tr>
                            <th class="text-left">Debt to Equity Ratio</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur perbandingan total hutang perusahaan dengan modal perusahaan.</td>
                        </tr>
                        <tr>
                            <th class="text-left">Long-term Debt to Equity Ratio</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur seberapa banyak hutang jangka panjang perusahaan dibandingkan  dengan modal perusahaaan</td>
                        </tr>
                        <tr>
                            <th class="text-left">Time Interest Earned Ratio</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur seberapa baik perusahaan mampu memenuhi pembayaran bunga berdasarkan kas yang dihasilkan kegiatan operasional perusahaan.</td>
                        </tr>
                        <tr class="active" style="font-size: x-large;">
                            <th class="text-left" style="font-size: x-large;" colspan="5">Activity Ratios</th>
                        </tr>
                        <tr>
                            <th class="text-left">Receivable Turnover</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur efisiensi banyaknya penjualan  yang dihasilkan oleh setiap rupiah account receiveable perusahaan.</td>
                        </tr>
                        <tr>
                            <th class="text-left">Inventory Turnover</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur efisiensi penggunaan persediaan  untuk berputar dalam satu periode tertentu, semakin tinggi ratio ini maka semakin baik.</td>
                        </tr>
                        <tr>
                            <th class="text-left">Working Capital Turnover</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur berapa banyak penjualan yang dihasilkan oleh setiap rupiah modal kerja pada perusahaan.</td>
                        </tr>
                        <tr>
                            <th class="text-left">Fix Assets Turnover</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur berapa banyak penjualan yang dihasilkan oleh setip modal rupiah fixed asset pada perusahaan.</td>
                        </tr>
                        <tr>
                            <th class="text-left">Total Assets Turnover</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur efisiensi penggunaan dana  total asset dalam rangka menghasilkan penjualan.  semakin tinggi ratio maka semakin efisien dalam menggunakan asset</td>
                        </tr>
                    </tbody>
                    <tr class="active" style="font-size: x-large;">
                        <th class="text-left" style="font-size: x-large;" colspan="5">Profitability Ratios</th>
                    </tr>
                    <tbody>
                        <tr>
                            <th class="text-left">Gross Profit Margin</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Diguakan untuk mengukur berapa banyak margin yang memungkinkan perusahaan untuk menutup beban-beban  perusahaan dan masih dapat memperoleh profit.</td>
                        </tr>
                        <tr>
                            <th class="text-left">Return on Investment</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur seberapa efektif perusahaan menggunakan aset untuk menghasilkan keuntungan.</td>
                        </tr>
                        <tr>
                            <th class="text-left">Operating Profit Margin</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Mengukur kemampuan perusahaan dalam menghasilkan keuntungan operasi dibandingkan dengan penjualan yang dicapai</td>
                        </tr>
                        <tr>
                            <th class="text-left">Net Profit Margin</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Mengukur beberapa profit setelah pajak yang dihasilkan setiap rupiah pendapatan. Umumnya semakin tinggi ratio semakin baik</td>
                        </tr>
                        <tr>
                            <th class="text-left">Return On Assets</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Mengukur kemampuan perusahaan untuk menghasilkan keuntungan dari setiap satu rupiah asset yang digunakan. </td>
                        </tr>
                        <tr>
                            <th class="text-left">Return On Equity</th>
                            <td class="text-right">Test</td>
                            <td class="text-right">Test</td>
                            <td class="text-right">
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Pertumbuhan(Eval("TotalTransaksiTahunIni").ToDecimal() -  Eval("TotalTransaksiTahunLalu").ToDecimal()) %>'></asp:Label>--%>
                            </td>
                            <td>Digunakan untuk mengukur tingkat pengembalian dari investasi yang dilakukan shareholder terhadap perusahaan.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

