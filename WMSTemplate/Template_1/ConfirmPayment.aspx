<%@ Page Title="" Language="C#" MasterPageFile="frontend/MasterPage.master" AutoEventWireup="true" CodeFile="ConfirmPayment.aspx.cs" Inherits="ConfirmPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="b-title-page b-title-page_mrg-btn_sm">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="b-title-page__title shuffle">Konfirmasi Pembayaran</h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <section class="section-policy">
                    <div class="row">
                        <div class="col-md-8 col-md-offset-2 copay">
                            <div class="form-row form-row form-row-wide col-md-12">
                                <asp:TextBox ID="TextBoxNomorOrder" CssClass="input-text" runat="server" placeholder="Nomor Order"></asp:TextBox>
                            </div>
                            <div class="form-row form-row form-row-wide col-md-12">
                                <asp:TextBox ID="TextBoxEmail" CssClass="input-text" runat="server" placeholder="Email"></asp:TextBox>
                            </div>
                            <div class="form-row form-row form-row-wide col-md-12">
                                <asp:TextBox ID="TextBoxNamaPemilik" CssClass="input-text" runat="server" placeholder="Nama Pemilik Rekening"></asp:TextBox>
                            </div>
                            <div class="form-row form-row form-row-wide col-md-12">
                                <asp:TextBox ID="TextBoxNoRekening" CssClass="input-text" runat="server" placeholder="Nomor Rekening"></asp:TextBox>
                            </div>
                            <div class="form-row form-row form-row-wide col-md-12">
                                <asp:FileUpload ID="FileUploadBuktiTransfer" runat="server" />
                            </div>
                            <div class="form-row form-row form-row-wide col-md-12 align-right">
                                <input type="button" class="btn-auth btn-cancel-forgot" value="Kembali" />
                                <asp:Button ID="ButtonLupaPassword" CssClass="btn-auth" runat="server" Text="Kirim" />
                            </div>
                        </div>
                    </div>
                    <br />
                </section>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
