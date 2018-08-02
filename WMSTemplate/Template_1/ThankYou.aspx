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
                                    <p>Email konfirmasi telah dikirim ke alamat email <strong>
                                        <asp:Literal ID="LiteralEmail" runat="server"></asp:Literal></strong></p>
                                    <p>Nomor order anda adalah:#<strong><asp:Literal ID="LiteralIDTransaksi" runat="server"></asp:Literal></strong></p>
                                </div>
                                <hr />
                                <div class='col-lg-12 col-md-12 col-sm-12 information'>
                                    <div class='col-lg-6 col-md-6 col-sm-6 '>
                                        <h3>Alamat Pengiriman</h3>
                                        <asp:Literal ID="LiteralNamaLengkap" runat="server"></asp:Literal>,
								<br />
                                        <asp:Literal ID="LiteralAlamat" runat="server"></asp:Literal>,
                                        <asp:Literal ID="LiteralKecamatan" runat="server"></asp:Literal>,							
                                        <br />
                                        <asp:Literal ID="LiteralKota" runat="server"></asp:Literal>,
								<br />
                                        <asp:Literal ID="LiteralProvinsi" runat="server"></asp:Literal>
                                        <asp:Literal ID="LiteralKodePos" runat="server"></asp:Literal>,
								<br />
                                        <asp:Literal ID="LiteralNegara" runat="server"></asp:Literal>
                                    </div>
                                    <div class='col-lg-6 col-md-6 col-sm-6 '>
                                        <h3>Informasi Pembayaran</h3>
                                        <p>Metode Pembayaran: <strong>Bank Transfer</strong></p>
                                        <p>
                                            Status Pembayaran: <strong>
                                                <asp:Literal ID="LiteralStatusPembayaran" runat="server"></asp:Literal></strong>
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
                <asp:ListView ID="ListViewDetailTransaksi" runat="server">
                    <ItemTemplate>
                        <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                <div class=' col-lg-4 col-md-4 col-sm-4'>
                                    <img src='<%# Eval("Foto") %>' alt='<%# Eval("Nama") %>' class="img-responsive" />
                                </div>
                                <div class=' col-lg-8 col-md-8 col-sm-8' id="item-detail">
                                    <p><a href='#'><%# Eval("Quantity") %> x <%# Eval("Nama") %></a></p>
                                </div>
                                <div class='clearfix'></div>
                            </div>
                            <div class='col-lg-4 col-md-4 col-sm-4 ws-value'>
                                <p class='text-right'><small><%# Eval("Total").ToFormatHarga() %></small></p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>

                <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                        <div class=' col-lg-6 col-md-6 col-sm-6'>
                            <p>Subtotal</p>
                        </div>
                        <p style="float: right;"><asp:Literal ID="LiteralTotal" runat="server"></asp:Literal></p>
                        <div class='clearfix'></div>
                    </div>
                </div>
                <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                        <div class=' col-lg-6 col-md-6 col-sm-6'>
                            <p>Biaya Pengiriman</p>
                        </div>
                        <p style="float: right;"><asp:Literal ID="LiteralBiayaPengiriman" runat="server"></asp:Literal></p>
                        <div class='clearfix'></div>
                    </div>
                </div>
                <div class="kelasGarisCheckout col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                        <div class=' col-lg-6 col-md-6 col-sm-6'>
                            <b>TOTAL</b>
                        </div>
                        <p style="float: right;"><asp:Literal ID="LiteralSubotal" runat="server"></asp:Literal></p>
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
