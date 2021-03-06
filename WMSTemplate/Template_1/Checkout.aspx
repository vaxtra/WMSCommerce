﻿<%@ Page Title="" Language="C#" MasterPageFile="frontend/MasterPage.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="CheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="goods-card">
        <div class="woocommerce">
            <div class="container">
                <div class="row">
                    <div class="col-md-8">
                        <div id="sectional" class="checkout-page">

                            <fieldset id="PanelPelanggan" runat="server">
                                <legend>1. INFORMASI PELANGGAN</legend>

                                <div class="form-section" id="FormPelanggan" runat="server">
                                    <div class="col2-set" id="customer_details">
                                        <div class="woocommerce-billing-fields">
                                            <div class="form-row form-row form-row-first col-md-12 nomargin">
                                                <h3 class="nomargin">Alamat Email Anda</h3>
                                                <br />
                                                <%--<p>Sudah mempunyai akun di website kami? <span class="logpop">Log in</span></p>--%>

                                                <asp:TextBox ID="TextBoxAlamatEmail" CssClass="input-text" runat="server" placeholder="Alamat Email"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAlamatEmail" runat="server" ErrorMessage="Mohon isi Field di atas" Display="Dynamic" ControlToValidate="TextBoxAlamatEmail" ValidationGroup="PersonalData" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorAlamatEmail" runat="server" ErrorMessage="Format email tidak sesuai" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBoxAlamatEmail" ValidationGroup="PersonalData" ForeColor="Red"></asp:RegularExpressionValidator>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="form-row form-row form-row-first col-md-12 nomargin">
                                                <h3>Alamat Pengiriman</h3>
                                            </div>
                                            <div class="form-row form-row form-row-first col-md-12">
                                                <asp:TextBox ID="TextBoxNamaLengkap" CssClass="input-text" runat="server" placeholder="Nama Lengkap"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNamaLengkap" runat="server" ErrorMessage="Mohon isi Field di atas" Display="Dynamic" ControlToValidate="TextBoxNamaLengkap" ValidationGroup="PersonalData" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear"></div>
                                            <div class="form-row form-row form-row-wide col-md-6">
                                                <asp:DropDownList ID="DropDownListNegara" CssClass="input-text" runat="server">
                                                    <asp:ListItem>Indonesia</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-row form-row form-row-first col-md-6">
                                                <asp:DropDownList ID="DropDownListProvinsi" CssClass="input-text" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListProvinsi_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">- Pilih Provinsi -</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-row form-row form-row-last col-md-6">
                                                <asp:DropDownList ID="DropDownListKota" CssClass="input-text" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListKota_SelectedIndexChanged" Visible="false">
                                                    <asp:ListItem value="0">- Pilih Kota -</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="clear"></div>
                                            <div class="form-row form-row form-row-wide col-md-6">
                                                <asp:DropDownList ID="DropDownListKecamatan" CssClass="input-text" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListKecamatan_SelectedIndexChanged" Visible="false">
                                                    <asp:ListItem Value="0">- Pilih Kecamatan -</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-row form-row form-row-wide col-md-12">
                                                <asp:TextBox ID="TextBoxAlamat" CssClass="input-text" runat="server" placeholder="Alamat Pengiriman"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAlamatPengiriman" runat="server" ErrorMessage="Mohon isi Field di atas" Display="Dynamic" ControlToValidate="TextBoxAlamat" ValidationGroup="PersonalData" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row form-row form-row-wide col-md-12">
                                                <asp:TextBox ID="TextBoxKodePos" CssClass="input-text" runat="server" placeholder="Kode Pos"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorKodePos" runat="server" ErrorMessage="Mohon isi Field di atas" Display="Dynamic" ControlToValidate="TextBoxKodePos" ValidationGroup="PersonalData" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row form-row form-row-wide col-md-12">
                                                <asp:TextBox ID="TextBoxNomorTelepon" CssClass="input-text" runat="server" placeholder="Nomor Telepon"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNomorTelepon" runat="server" ErrorMessage="Mohon isi Field di atas" Display="Dynamic" ControlToValidate="TextBoxNomorTelepon" ValidationGroup="PersonalData" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clearfix"></div>

                                        </div>
                                        <br />
                                        <span id="StatusValidasi" class="alert alert-warning" runat="server">
                                            <i class="icon icon_error-circle_alt"></i>
                                            <asp:Literal ID="LiteralStatusValidasi" runat="server"></asp:Literal>
                                        </span>

                                    </div>
                                    <nav class="form-section-nav col-md-12">
                                        
                                            <span class="btn-std form-nav-next"><asp:Button ID="ButtonLanjutPengiriman" runat="server" Text="Lanjut ke pengiriman" OnClick="ButtonLanjutkanKePengiriman_Click" ValidationGroup="PersonalData" /></span>
                                    </nav>
                                </div>

                            </fieldset>


                            <fieldset id="PanelPengiriman" runat="server">

                                <legend>2. DETAIL PENGIRIMAN</legend>
                                <div class="form-section" id="FormPengiriman" runat="server">
                                    <div class="woocommerce-billing-fields">
                                        <h3>Alamat Pengiriman </h3>
                                        <p><asp:Literal ID="LiteralNamaLengkap" runat="server"></asp:Literal></p>
                                        <p><asp:Literal ID="LiteralAlamat" runat="server"></asp:Literal></p>
                                        <p><asp:Literal ID="LiteralKecamatan" runat="server"></asp:Literal>, <asp:Literal ID="LiteralKota" runat="server"></asp:Literal></p>
                                        <p><asp:Literal ID="LiteralProvinsi" runat="server"></asp:Literal> <asp:Literal ID="LiteralKodePos" runat="server"></asp:Literal></p>
                                        <p><asp:Literal ID="LiteralNegara" runat="server"></asp:Literal></p>
                                        <p><asp:Literal ID="LiteralNomorTelepon" runat="server"></asp:Literal></p>
                                    </div>
                                    <div id="payment" class="woocommerce-checkout-payment">
                                        <h3>Pilih Jasa Pengiriman </h3>
                                        
                                        <ul class="wc_payment_methods payment_methods methods">
                                            <asp:RadioButtonList ID="RadioButtonListKurir" CssClass="wc_payment_methods payment_methods methods" runat="server">
                                            </asp:RadioButtonList>
                                        </ul>

                                    </div>
                                    <br />
                                    <span id="PengirimanValidasi" class="alert alert-warning" runat="server">
                                            <i class="icon icon_error-circle_alt"></i>
                                            <asp:Literal ID="LiteralWarningPilihJasaPengiriman" runat="server"></asp:Literal>
                                    </span>
                                    <nav class="form-section-nav">
                                        <span class="btn-secondary form-nav-prev"><asp:Button ID="ButtonKembaliKeInformasiPelanggan" runat="server" Text="Kembali ke Alamat" OnClick="ButtonKembaliKeInformasiPelanggan_Click" /></span>
                                        <span class="btn-std form-nav-next"><asp:Button ID="ButtonLanjutPembayaran" runat="server" Text="Lanjut ke Pembayaran" OnClick="ButtonLanjutkanKePembayaran_Click" /></span>
                                    </nav>

                                </div>
                            </fieldset>
                            <fieldset id="PanelPembayaran" runat="server">
                                <legend>3. METODDE PEMBAYARAN</legend>

                                <div class="form-section" id="FormPembayaran" runat="server">
                                    <div class="col-2">
                                        <div class="woocommerce-checkout-payment">
                                            <p>Pilih Metode Pembayaran yang dikehendaki.</p>
                                            <asp:RadioButtonList ID="RadioButtonListJenisPembayaran" runat="server">
                                            </asp:RadioButtonList>
                                            <%--<ul class="wc_payment_methods payment_methods methods">
                                                <li class="wc_payment_method payment_method_cheque" style="border: 1px solid #eee; padding: 10px;">
                                                    <input type="radio" class="input-radio" />
                                                    <img src="https://indoco.smasterapps.com/application/views/images/shipping/jnt.jpg" alt="logo" class="scroll-logo hidden-xs" />
                                                    <label for="payment_method_cheque">Bank Transfer </label>
                                                </li>

                                            </ul>--%>
                                        </div>
                                        <div class="woocommerce-shipping-fields">
                                            <h3>Additional Information</h3>
                                            <p class="form-row form-row">
                                                <label for="order_comments" class="">Order Notes</label>
                                                <textarea name="order_comments" class="input-text " id="order_comments" placeholder="Notes about your order, e.g. special notes for delivery."></textarea>
                                            </p>
                                        </div>
                                    </div>
                                    <br />
                                    <span id="PembayaranValidasi" class="alert alert-warning" runat="server">
                                        <i class="icon icon_error-circle_alt"></i>
                                        <asp:Literal ID="LiteralWarningPilihMetodePembayaran" runat="server"></asp:Literal>
                                    </span>
                                    <nav class="form-section-nav">
                                        <span class="btn-secondary form-nav-prev"><asp:Button ID="ButtonKembaliKeDetailPengiriman" runat="server" Text="Kembali ke Pengiriman" OnClick="ButtonKembaliKeDetailPengiriman_Click" /></span>
                                        <span class="btn-std form-nav-next"><asp:Button ID="ButtonProsesPemesanan" runat="server" Text="Konfirmasi Pemesanan" OnClick="ButtonProsesPemesanan_Click" /></span>
                                    </nav>
                                </div>
                            </fieldset>
                        </div>
                        <div class="woocommerce">

                            <div class="checkout_coupon" style="display: none" />
                            <p class="form-row form-row-first">
                                <input type="text" name="coupon_code" class="input-text" placeholder="Coupon code" id="coupon_code" value="" />
                            </p>
                            <p class="form-row form-row-last">
                                <input type="submit" class="button" name="apply_coupon" value="Apply Coupon" />
                            </p>
                            <div class="clear"></div>
                        </div>
                        <div class="checkout woocommerce-checkout" />


                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <aside class="l-sidebar cart-detail">
                    <h3>Detail Pemesanan</h3>
                    <asp:Repeater ID="RepeaterCart" runat="server">
                        <ItemTemplate>
                            <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding'>
                                    <div class=' col-lg-4 col-md-4 col-sm-4 nopadding'>
                                        <img src='<%# Eval("Foto") %>' alt='<%# Eval("Nama") %>' class="img-responsive" />
                                    </div>
                                    <div class=' col-lg-8 col-md-8 col-sm-8' id="item-detail">
                                        <p class="product-title"><a href='#'><%# Eval("Quantity") %> x <%# Eval("Nama") %></a></p>
                                    </div>
                                    <div class='clearfix'></div>
                                </div>
                                <div class='col-lg-4 col-md-4 col-sm-4 ws-value nopadding'>
                                    <p class='text-right'><small><%# Eval("Total").ToFormatHarga() %></small></p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="coupon">

                            <label for="coupon_code">Coupon:</label>
                            <input type="text" class="input-text" placeholder="Coupon code" />
                            <input type="submit" class="button" name="apply_coupon" value="Apply Coupon" />

                        </div>
                    </div>--%>
                    <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding'>
                            <div class=' col-lg-6 col-md-6 col-sm-6 nopadding'>
                                <p>Subtotal</p>
                            </div>
                            <p style="float: right;"><asp:Literal ID="LiteralTotal" runat="server"></asp:Literal></p>
                            <div class='clearfix'></div>
                        </div>
                    </div>
<%--                    <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding'>
                            <div class=' col-lg-6 col-md-6 col-sm-6 nopadding'>
                                <p>Biaya Pengiriman</p>
                            </div>
                            <p style="float: right;">Rp. 1666.000</p>
                            <div class='clearfix'></div>
                        </div>
                    </div>
                    <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding'>
                            <div class=' col-lg-6 col-md-6 col-sm-6 nopadding'>
                                <b>TOTAL</b>
                            </div>
                            <p style="float: right;">Rp. 1666.000</p>
                            <div class='clearfix'></div>
                        </div>
                    </div>--%>
                </aside>
                <!-- end .sidebar-->
            </div>
        </div>
    </div>
    <div class="overlaypoplog">
        <div class="poplog">
            <div class="panel-login">
                <h3>Login</h3>
                <p>Masukan email password anda</p>
                <div class="form-row form-row form-row-wide col-md-12">
                    <asp:TextBox ID="TextBoxEmailLogin" CssClass="input-text" runat="server" placeholder="Email"></asp:TextBox>
                </div>
                <div class="form-row form-row form-row-wide col-md-12">
                    <asp:TextBox ID="TextBoxPassword" CssClass="input-text" runat="server" placeholder="Password"></asp:TextBox>
                </div>
                <div class="form-row form-row form-row-wide col-md-12">
                    <label class="forgtoggle">Lupa Password?</label>
                </div>
                <div class="form-row form-row form-row-wide col-md-12">
                    <input type="button" class="btn-auth btn-cancel-login" value="Kembali" />
                    <asp:Button ID="ButtonLogin" CssClass="btn-auth" runat="server" Text="Login" />
                </div>
            </div>
            <div class="panel-forgot">
                <h3>Lupa password</h3>
                <p>Masukan email anda</p>
                <div class="form-row form-row form-row-wide col-md-12">
                    <asp:TextBox ID="TextBoxEmailLupaPassword" CssClass="input-text" runat="server" placeholder="Email"></asp:TextBox>
                </div>
                <div class="form-row form-row form-row-wide col-md-12">
                    <input type="button" class="btn-auth btn-cancel-forgot" value="Kembali" />
                    <asp:Button ID="ButtonLupaPassword" CssClass="btn-auth" runat="server" Text="Kirim" />
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script src="/frontend/assets/plugins/wizard-WIT/formalize.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".forgtoggle").click(function () {
                $(".panel-login").fadeOut();
                $(".panel-forgot").delay(400).fadeIn();
            });

            $(".logpop").click(function () {
                $(".overlaypoplog").fadeIn();
            });

            $(".btn-cancel-login").click(function () {
                $(".overlaypoplog").fadeOut();
            });

            $(".btn-cancel-forgot").click(function () {
                $(".panel-forgot").fadeOut();
                $(".panel-login").delay(400).fadeIn();
            });

            //$('#sectional').formalize({
            //    timing: 300,
            //    nextCallBack: function () {
            //        if (validateEmpty($('#sectional .open'))) {
            //            scrollToNewSection($('#sectional .open'));
            //            return true;
            //        };
            //        return false;
            //    },
            //    prevCallBack: function () {
            //        return scrollToNewSection($('#sectional .open').prev())
            //    }
            //});
            //$('#global').formalize({
            //    navType: 'global',
            //    prevNav: '#global-nav-prev',
            //    nextNav: '#global-nav-next',
            //    timing: 300,
            //    nextCallBack: function () {
            //        return validateEmpty($('#global .open'));
            //    }
            //});

            //$('#btn-global').on('click', function () {
            //    $('#btn-sectional').removeClass('disabled');
            //    $(this).addClass('disabled');
            //    $('#sectional').hide();
            //    $('#global').show();
            //});

            //$('#btn-sectional').on('click', function () {
            //    $('#btn-global').removeClass('disabled');
            //    $(this).addClass('disabled');
            //    $('#global').hide();
            //    $('#sectional').show();
            //});

            //$('input').on('keyup change', function () {
            //    $(this).closest($('.valid')).removeClass('valid');
            //});

            //function validateEmpty(section) {
            //    var errors = 0;
            //    section.find($('.required-field')).each(function () {
            //        var $this = $(this),
            //            input = $this.find($('input'));
            //        if (input.val() === "") {
            //            errors++;
            //            $this.addClass('field-error');
            //            $this.append('\<div class="form-error-msg">This field is required!\</div>');
            //        }
            //    });
            //    if (errors > 0) {
            //        section.removeClass('valid');
            //        return false;
            //    }
            //    section.find($('.field-error')).each(function () {
            //        $(this).removeClass('field-error');
            //    });
            //    section.find($('.form-error-msg')).each(function () {
            //        $(this).remove();
            //    });
            //    section.addClass('valid');
            //    return true;
            //}

            //function scrollToNewSection(section) {
            //    var top = section.offset().top;
            //    $("html, body").animate({
            //        scrollTop: top
            //    }, '200');
            //    return true;
            //}
        });
    </script>
</asp:Content>

