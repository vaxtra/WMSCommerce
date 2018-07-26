<%@ Page Title="" Language="C#" MasterPageFile="~/assets/MasterPageSidebar.master" AutoEventWireup="true" CodeFile="TransaksiKhusus.aspx.cs" Inherits="WITAkuntansi_Pemasukan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function Func_ButtonPrint1(e) {
            var evt = e ? e : window.event;

            if (evt.keyCode == 27) {
                var bt = document.getElementById('ButtonKeluarPrint');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    <asp:Label ID="LabelTitle" runat="server" Text=""></asp:Label>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTitleRight" runat="Server">
    <asp:Button ID="ButtonPrint" runat="server" CssClass="btn btn-default btn-sm" Text="Print" OnClick="ButtonPrint_Click" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderSubTitleLeft" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderSubTitleCenter" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderSubTitleRight" runat="Server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 hidden-print">
            <%--    <div class="form-group">
                <label class="form-label bold">Dokumen</label>
                <asp:FileUpload ID="FileUploadDokumen" runat="server" />
            </div>--%>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-3">
                        <label class="form-label bold">Tanggal</label>
                        <asp:TextBox ID="TextBoxTanggal" runat="server" CssClass="form-control Tanggal input-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="form-label bold">No Referensi</label>
                        <asp:TextBox ID="TextBoxReferensi" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group alert alert-error" id="panelLiteralError" runat="server">
                <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>
            </div>

            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                Input Data Debit
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <label class="form-label bold">Akun</label>
                                            <asp:DropDownList ID="DropDownListDebit" Width="100%" CssClass="select2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <label class="form-label bold">Nominal</label>
                                            <asp:TextBox ID="TextBoxNominalDebit" runat="server" Width="100%" CssClass="form-control input-sm InputDesimal"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:DropDownList ID="DropDownListDebit2" Width="100%" CssClass="select2" runat="server" OnSelectedIndexChanged="DropDownListDebit2_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:TextBox ID="TextBoxNominalDebit2" runat="server" Width="100%" CssClass="form-control input-sm InputDesimal" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">

                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:DropDownList ID="DropDownListDebit3" Width="100%" CssClass="select2" runat="server" OnSelectedIndexChanged="DropDownListDebit3_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>


                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:TextBox ID="TextBoxNominalDebit3" runat="server" Width="100%" CssClass="form-control input-sm InputDesimal" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">

                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:DropDownList ID="DropDownListDebit4" Width="100%" CssClass="select2" runat="server" OnSelectedIndexChanged="DropDownListDebit4_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:TextBox ID="TextBoxNominalDebit4" runat="server" Width="100%" CssClass="form-control input-sm InputDesimal" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="panel panel-danger">
                            <div class="panel-heading">
                                Input Data Kredit
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <label class="form-label bold">Akun</label>
                                            <asp:DropDownList ID="DropDownListKredit" Width="100%" CssClass="select2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <label class="form-label bold">Nominal</label>
                                            <asp:TextBox ID="TextBoxNominalKredit" Width="100%" runat="server" CssClass="form-control input-sm InputDesimal"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:DropDownList ID="DropDownListKredit2" Width="100%" CssClass="select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListKredit2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:TextBox ID="TextBoxNominalKredit2" runat="server" Width="100%" CssClass="form-control input-sm InputDesimal" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">

                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:DropDownList ID="DropDownListKredit3" CssClass=" select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListKredit3_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>


                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:TextBox ID="TextBoxNominalKredit3" runat="server" Width="100%" CssClass="form-control input-sm InputDesimal" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">

                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:DropDownList ID="DropDownListKredit4" runat="server" Width="100%" AutoPostBack="true" CssClass=" select2" Label="" OnSelectedIndexChanged="DropDownListKredit4_SelectedIndexChanged" server="">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:TextBox ID="TextBoxNominalKredit4" runat="server" Width="100%" CssClass="form-control input-sm InputDesimal" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>Catatan</label>
                <asp:TextBox ID="TextBoxKeterangan" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"></asp:TextBox>
            </div>
            <asp:Button ID="ButtonOk" runat="server" CssClass="btn btn-success btn-sm" Text="Simpan" OnClick="ButtonOk_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

