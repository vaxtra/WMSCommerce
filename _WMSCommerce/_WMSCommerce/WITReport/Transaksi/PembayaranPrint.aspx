<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePrint.master" AutoEventWireup="true" CodeFile="PembayaranPrint.aspx.cs" Inherits="WITReport_Top_ProdukVarianPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-condensed laporan">
                <thead>
                    <asp:Literal ID="LiteralHeader" runat="server"></asp:Literal>
                </thead>
                <tbody>
                    <asp:Literal ID="LiteralSumary1" runat="server"></asp:Literal>
                    <asp:Literal ID="LiteralBody" runat="server"></asp:Literal>
                    <asp:Literal ID="LiteralSumary2" runat="server"></asp:Literal>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

