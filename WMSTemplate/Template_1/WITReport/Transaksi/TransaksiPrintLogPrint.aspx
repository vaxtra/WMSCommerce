﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="TransaksiPrintLogPrint.aspx.cs" Inherits="WITReport_Transaksi_TransaksiPrintLogPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="table-responsive">
                <table class="table table-bordered table-condensed table-hover" style="font-size: 12px;">
                    <thead>
                        <tr class="active">
                            <th class="fitSize">No.</th>
                            <th class="text-center">ID Transaksi</th>
                            <th class="text-center">Log Print</th>
                            <th class="text-center">Tanggal Transaksi</th>
                            <th class="text-center">Pengguna</th>
                            <th class="text-center">Pelanggan</th>
                            <th class="text-center">Jenis</th>
                            <th class="text-center">Status</th>
                            <th class="text-center">Tanggal Pembayaran</th>
                            <th class="text-center">Biaya Shipping</th>
                            <th class="text-center">Keterangan</th>
                            <th class="fitSize text-center">Grand Total</th>
                            <th class="fitSize text-center">Total Pembayaran</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr style="font-weight: bold;">
                            <td class="success" colspan="9"></td>
                            <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalBiayaShipping"]) : "0" %></td>
                            <td class="success"></td>
                            <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalGrandTotal"]) : "0" %></td>
                            <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalPembayaran"]) : "0" %></td>
                        </tr>

                        <asp:Repeater ID="RepeaterLaporan" runat="server">
                            <itemtemplate>
                                        <tr>
                                            <asp:HiddenField ID="HiddenFieldIDTransaksi" runat="server" Value='<%# Eval("IDTransaksi") %>' />
                                            <td class="fitSize"><%# Container.ItemIndex + 1 %></td>
                                            <td class="fitSize"><a href='/WITPointOfSales/Detail.aspx?id=<%# Eval("IDTransaksi") %>'><%# Eval("IDTransaksi") %></a></td>
                                            <td class="fitSize warning">
                                                <asp:Repeater ID="RepeaterBody" runat="server" DataSource='<%# Eval("LogPrint") %>'>
                                                    <ItemTemplate>
                                                        <%# Eval("Tanggal").ToFormatTanggalJam() %> - 
                                                        <%# Eval("TBPengguna.NamaLengkap") %>  
                                                        (<%# Eval("EnumPrintLog").ToString() == "1" ? "Point of Sales" :
                                                        Eval("EnumPrintLog").ToString() == "2" ? "Invoice" : "Packing Slip"%>) 
                                                        <br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td class="fitSize"><%# Eval("TanggalTransaksi").ToFormatTanggalJam() %></td>
                                            <td class="fitSize"><%# Eval("PenggunaTransaksi") %></td>
                                            <td class="fitSize">Nama : <%# Eval("Pelanggan") %>
                                                <br />
                                                Alamat : <%# Eval("Alamat") %>
                                                <br />
                                                Telepon :  <%# Eval("Telepon") %> </td>
                                            <td class="fitSize"><%# Eval("JenisTransaksi") %></td>
                                            <td><%# Eval("StatusTransaksi") %></td>
                                            <td class="fitSize"><%# Eval("TanggalPembayaran").ToFormatTanggalJam() %></td>
                                            <td class="fitSize text-right"><%# Eval("BiayaShipping").ToFormatHarga() %></td>
                                            <td><%# Eval("Keterangan") %></td>
                                            <td class="fitSize text-right success"><%# Eval("GrandTotal").ToFormatHarga() %></td>
                                            <td class="fitSize text-right success"><%# Eval("TotalPembayaran").ToFormatHarga() %></td>
                                        </tr>
                                    </itemtemplate>
                        </asp:Repeater>

                        <tr style="font-weight: bold;">
                            <td class="success" colspan="9"></td>
                            <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalBiayaShipping"]) : "0" %></td>
                            <td class="success"></td>
                            <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalGrandTotal"]) : "0" %></td>
                            <td class="success text-right"><%= Result != null ? Parse.ToFormatHarga(Result["TotalPembayaran"]) : "0" %></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

