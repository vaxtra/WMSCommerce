<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WITReport_Niion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Forecast
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <a href="Pengaturan.aspx" class="btn btn-success btn-sm">Tambah</a>
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
            <ul id="myTab" class="nav nav-tabs">
                <li class="active"><a href="#tabForecast" id="Forecast-tab" data-toggle="tab">Forecast</a></li>
                <li><a href="Forecast.aspx">Setting Forcast</a></li>
            </ul>
            <br />
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane active" id="tabForecast">
                    <div class="form-group">
                        <h4>
                            <asp:Label ID="LabelPeriode" runat="server"></asp:Label></h4>
                    </div>
                    <div class="form-group">
                        <asp:DropDownList ID="DropDownListTahun" CssClass="select2" Style="width: 200px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListTahun_SelectedIndexChanged"></asp:DropDownList>
                        <div id="graph" style="width: 100%; height: 250px;"></div>
                    </div>
                    <div class="form-group">
                        <asp:Literal ID="LiteralTempat" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script src="/assets/plugins/Morris/raphael-min.js"></script>
    <script src="/assets/plugins/Morris/morris.min.js"></script>

    <script src="/assets/plugins/Highcharts/highcharts.js"></script>
    <script src="/assets/plugins/Highcharts/modules/exporting.js"></script>
    <asp:Literal ID="LiteralChart" runat="server"></asp:Literal>
</asp:Content>
