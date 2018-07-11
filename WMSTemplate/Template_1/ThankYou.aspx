<%@ Page Title="" Language="C#" MasterPageFile="frontend/MasterPage.master" AutoEventWireup="true" CodeFile="ThankYou.aspx.cs" Inherits="CheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .kelasGarisCheckout {
            border-bottom: 1px solid #d9d9d9;
            padding-bottom: 12px;
            padding-top: 12px;
            background: #eee;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="goods-card">
        <div class="container">
            <div class="row">
                <div class="col-md-8 ">
                    <div class="woocommerce">
                        <div class='ws-left' id='order-detail'>
                            <div id='thank-you' class='col-lg-12'>
                                <div class='text-center'>

                                    <h2>Terimakasih Atas Pemesanan Anda!</h2>
                                    <p>Email konfirmasi telah dikirim ke alamat email <strong>ganajr@gmail.com</strong></p>
                                    <p>Nomor order anda adalah:#<strong>1018</strong></p>
                                </div>
                                <hr />
                                <div class='col-lg-12 col-md-12 col-sm-12 information'>
                                    <div class='col-lg-6 col-md-6 col-sm-6 '>
                                        <h3>Alamat Pengiriman</h3>
                                        Ganjar Rizkiawan,
								<br />
                                        Jl Triwuasan,								Langsa Kota,							
                                        <br />
                                        Kota Langsa,
								<br />
                                        Aceh 42054,
								<br />
                                        Indonesia
                                    </div>
                                    <div class='col-lg-6 col-md-6 col-sm-6 '>
                                        <h3>Informasi Pembayaran</h3>
                                        <p>Metode Pembayaran: <strong>Bank Transfer</strong></p>
                                        <p>
                                            Status Pembayaran:
																<strong>pending</strong>
                                        </p>

                                    </div>
                                </div>


                                <div class='col-lg-12 col-md-12 col-sm-12'>
                                    <hr />
                                    <h3>Informasi Pembayaran</h3>
                                    <p>
                                        Silahkan transfer pembayaran ke rekening dibawah ini :<b><br>
                                            <br>
                                            Bank BCA dan Bank Lainnya</b><nama_pemilik_rekening><br>Nomor Rekening :<nomor_rekening> 0083222520<br></nomor_rekening></nama_pemilik_rekening>Atas Nama : Rendy Herdiawan<br>
                                        <nama_pemilik_rekening><nomor_rekening></nomor_rekening></nama_pemilik_rekening>
                                    </p>

                                </div>




                                <div class='col-lg-12 col-md-12 col-sm-12'>
                                    <hr />
                                    <div class='col-lg-4 col-md-4 col-sm-4 col-sm-offset-4'><a href='/collections/all' class='btn ws-btn'>Lanjutkan Belanja</a> </div>
                                </div>
                            </div>
                        </div>
                        <!---order-detail!--->
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
            <aside class="l-sidebar">
                <h3>Detail Pemesanan</h3>
                <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                        <div class=' col-lg-4 col-md-4 col-sm-4'>
                            <img src='//cdn.shopify.com/s/files/1/0072/3071/8067/products/8d7f3decefd2e286d8da7c9d544b3974_small.jpg?v=1529817727' alt='Claudia - Putih / 37' class="img-responsive" />
                        </div>
                        <div class=' col-lg-8 col-md-8 col-sm-8' id="item-detail">
                            <p><a href='#'>1 x Claudia</a></p>
                            <ol class='breadcrumb'>
                                <li>Putih / 37</li>
                            </ol>
                        </div>
                        <div class='clearfix'></div>
                    </div>
                    <div class='col-lg-4 col-md-4 col-sm-4 ws-value'>
                        <p class='text-right'><small>Rp 99.000,00</small></p>
                    </div>
                </div>
                <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                        <div class=' col-lg-4 col-md-4 col-sm-4'>
                            <img src='//cdn.shopify.com/s/files/1/0072/3071/8067/products/8d7f3decefd2e286d8da7c9d544b3974_small.jpg?v=1529817727' alt='Claudia - Putih / 37' class="img-responsive" />
                        </div>
                        <div class=' col-lg-8 col-md-8 col-sm-8' id="item-detail">
                            <p><a href='#'>1 x Claudia</a></p>
                            <ol class='breadcrumb'>
                                <li>Putih / 37</li>
                            </ol>
                        </div>
                        <div class='clearfix'></div>
                    </div>
                    <div class='col-lg-4 col-md-4 col-sm-4 ws-value'>
                        <p class='text-right'><small>Rp 99.000,00</small></p>
                    </div>
                </div>
                <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                        <div class=' col-lg-6 col-md-6 col-sm-6'>
                            <p>Subtotal</p>
                        </div>
                        <p style="float: right;">Rp. 666.000</p>
                        <div class='clearfix'></div>
                    </div>
                </div>
                <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                        <div class=' col-lg-6 col-md-6 col-sm-6'>
                            <p>Biaya Pengiriman</p>
                        </div>
                        <p style="float: right;">Rp. 1666.000</p>
                        <div class='clearfix'></div>
                    </div>
                </div>
                <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                        <div class=' col-lg-6 col-md-6 col-sm-6'>
                            <b>TOTAL</b>
                        </div>
                        <p style="float: right;">Rp. 1666.000</p>
                        <div class='clearfix'></div>
                    </div>
                </div>
            </aside>
            <!-- end .sidebar-->
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
    <script src="/frontend/assets/plugins/wizard-WIT/formalize.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#sectional').formalize({
                timing: 300,
                nextCallBack: function () {
                    if (validateEmpty($('#sectional .open'))) {
                        scrollToNewSection($('#sectional .open'));
                        return true;
                    };
                    return false;
                },
                prevCallBack: function () {
                    return scrollToNewSection($('#sectional .open').prev())
                }
            });
            $('#global').formalize({
                navType: 'global',
                prevNav: '#global-nav-prev',
                nextNav: '#global-nav-next',
                timing: 300,
                nextCallBack: function () {
                    return validateEmpty($('#global .open'));
                }
            });

            $('#btn-global').on('click', function () {
                $('#btn-sectional').removeClass('disabled');
                $(this).addClass('disabled');
                $('#sectional').hide();
                $('#global').show();
            });

            $('#btn-sectional').on('click', function () {
                $('#btn-global').removeClass('disabled');
                $(this).addClass('disabled');
                $('#global').hide();
                $('#sectional').show();
            });

            $('input').on('keyup change', function () {
                $(this).closest($('.valid')).removeClass('valid');
            });

            function validateEmpty(section) {
                var errors = 0;
                section.find($('.required-field')).each(function () {
                    var $this = $(this),
                        input = $this.find($('input'));
                    if (input.val() === "") {
                        errors++;
                        $this.addClass('field-error');
                        $this.append('\<div class="form-error-msg">This field is required!\</div>');
                    }
                });
                if (errors > 0) {
                    section.removeClass('valid');
                    return false;
                }
                section.find($('.field-error')).each(function () {
                    $(this).removeClass('field-error');
                });
                section.find($('.form-error-msg')).each(function () {
                    $(this).remove();
                });
                section.addClass('valid');
                return true;
            }

            function scrollToNewSection(section) {
                var top = section.offset().top;
                $("html, body").animate({
                    scrollTop: top
                }, '200');
                return true;
            }
        });
    </script>
</asp:Content>
