<%@ Page Title="" Language="C#" MasterPageFile="~/WITPointOfSales/MasterPage.master" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="WITSurvey_Import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12 text-center">
            <h3>Import</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Literal ID="LiteralWarning" runat="server"></asp:Literal>

            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="col-sm-2"></label>
                    <div class="col-sm-10">
                        <a href="/file_excel/Template/(Format) Soal.xls">Download Template</a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">File</label>
                    <div class="col-sm-10">
                        <asp:FileUpload ID="FileUploadExcel" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button ID="ButtonImport" runat="server" Text="Import" class="btn btn-sm btn-primary" OnClick="ButtonImport_Click" />
                        <a href="Default.aspx" class="btn btn-sm btn-danger">Keluar</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>

