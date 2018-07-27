<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="WITReport_Niion_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelPeriode" runat="server"></asp:Label>
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
            <div class="panel panel-success">
                <div class="panel-heading">
                    Bulanan
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:DropDownList ID="DropDownListKategoriTempat" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="DropDownListTahunBulanan" CssClass="select2" Style="width: 100%;" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="form-inline">
                            <div class="btn-group" style="margin: 5px 5px 0 0;">
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonJanuari" runat="server" Text="Januari" OnClick="ButtonJanuari_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonFebruari" runat="server" Text="Februari" OnClick="ButtonFebruari_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMaret" runat="server" Text="Maret" OnClick="ButtonMaret_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonApril" runat="server" Text="April" OnClick="ButtonApril_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonMei" runat="server" Text="Mei" OnClick="ButtonMei_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonJuni" runat="server" Text="Juni" OnClick="ButtonJuni_Click" />
                            </div>
                            <div class="btn-group" style="margin: 5px 5px 0 0">
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonJuli" runat="server" Text="Juli" OnClick="ButtonJuli_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonAgustus" runat="server" Text="Agustus" OnClick="ButtonAgustus_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonSeptember" runat="server" Text="September" OnClick="ButtonSeptember_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonOktober" runat="server" Text="Oktober" OnClick="ButtonOktober_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonNopember" runat="server" Text="Nopember" OnClick="ButtonNopember_Click" />
                                <asp:Button CssClass="btn btn-sm btn-outline btn-default" ID="ButtonDesember" runat="server" Text="Desember" OnClick="ButtonDesember_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                    Tahunan
                </div>
                <div class="panel-body">
                    <asp:DropDownList ID="DropDownListTahun" CssClass="select2" Style="width: 200px;" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <%--<asp:DropDownList ID="DropDownListStatusTransaksiTahunan" CssClass="select2" Style="width: 250px;" runat="server" AutoPostBack="true"></asp:DropDownList>--%>
                    <asp:Button CssClass="btn btn-sm btn-primary" ID="ButtonPrint" runat="server" Text="Annual Report" OnClick="ButtonPrint_Click" Style="margin-left: 9%;" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

