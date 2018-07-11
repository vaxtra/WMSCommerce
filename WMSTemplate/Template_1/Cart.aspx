<%@ Page Title="" Language="C#" MasterPageFile="frontend/MasterPage.master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="goods-card">
        <div class="container">
            <h2 class="b-about-main__title text-center">Keranjang Belanja</h2>
            <div class="row">
                <div class="col-md-12 ">
                    <div class="woocommerce">
                        <div class="cart-table " />


                        <div class="b-table b-cart-table ">
                            <table class="shop_table shop_table_responsive cart table" cellspacing="0">
                                <thead>
                                    <tr>
                                        <td class="product-thumbnail">&nbsp;</td>
                                        <td class="product-name"><span>Product</span></td>
                                        <td class="product-price"><span>Price</span></td>
                                        <td class="product-quantity"><span>Quantity</span></td>
                                        <td class="product-subtotal"><span>Total</span></td>
                                        <td class="product-remove"><span>remove</span></td>
                                    </tr>
                                </thead>
                                <tbody>

                                    <tr class="cart_item">
                                        <td class="product-thumbnail">
                                            <a href="./catalog-product.html">
                                                <img width="80" height="auto" src="/frontend/assets/media/content/goods-carousel/main/1.jpg" class="attachment-shop_thumbnail size-shop_thumbnail wp-post-image" />
                                            </a></td>

                                        <td class="product-name" data-title="Product">
                                            <div class="caption">
                                                <a class="product-name" href="http://pro-theme.com/wordpress/ismile/product/blu-vivo-5-smartphone/">Hyperion</a>
                                            </div>
                                        </td>

                                        <td class="product-price" data-title="Price">
                                            <span class="product-price total-price">
                                                <span class="woocommerce-Price-amount amount"><span class="woocommerce-Price-currencySymbol">£</span>875.00</span>						    </span>
                                        </td>

                                        <td class="product-quantity" data-title="Quantity">

                                            <div class="input-group btn-block qty-block" data-trigger="spinner">
                                                <i class="fa fa-minus"></i>
                                                <input type="text" data-rule="quantity" value="1" title="Qty" class="input-text qty text" />
                                                <i class="fa fa-plus"></i>
                                            </div>


                                        </td>

                                        <td class="product-subtotal" data-title="Total">
                                            <span class="woocommerce-Price-amount amount"><span class="woocommerce-Price-currencySymbol">£</span>875.00</span>						</td>
                                        <td class="product-remove">

                                            <a href="http://templines.rocks/" class="btn btn-remove" title="Remove this item"><i class="fa fa-times fa-lg"></i></a></td>
                                    </tr>
                                    <tr class="cart_item">
                                        <td class="product-thumbnail">
                                            <a href="./catalog-product.html">
                                                <img width="80" height="auto" src="/frontend/assets/media/content/goods-carousel/main/1.jpg" class="attachment-shop_thumbnail size-shop_thumbnail wp-post-image" />
                                            </a></td>

                                        <td class="product-name" data-title="Product">
                                            <div class="caption">
                                                <a class="product-name" href="http://pro-theme.com/wordpress/ismile/product/blu-vivo-5-smartphone/">Hyperion</a>
                                            </div>
                                        </td>

                                        <td class="product-price" data-title="Price">
                                            <span class="product-price total-price">
                                                <span class="woocommerce-Price-amount amount"><span class="woocommerce-Price-currencySymbol">£</span>875.00</span>						    </span>
                                        </td>

                                        <td class="product-quantity" data-title="Quantity">

                                            <div class="input-group btn-block qty-block" data-trigger="spinner">
                                                <i class="fa fa-minus"></i>
                                                <input type="text" data-rule="quantity" value="1" title="Qty" class="input-text qty text" />
                                                <i class="fa fa-plus"></i>
                                            </div>


                                        </td>

                                        <td class="product-subtotal" data-title="Total">
                                            <span class="woocommerce-Price-amount amount"><span class="woocommerce-Price-currencySymbol">£</span>875.00</span>						</td>
                                        <td class="product-remove">

                                            <a href="http://templines.rocks/" class="btn btn-remove" title="Remove this item"><i class="fa fa-times fa-lg"></i></a></td>
                                    </tr>
                                    <tr class="cart_data">
                                        <td colspan="6" class="actions">

                                            <div class="coupon">

                                                <label for="coupon_code">Coupon:</label>
                                                <input type="text" class="input-text" placeholder="Coupon code" />
                                                <input type="submit" class="button" name="apply_coupon" value="Apply Coupon" />

                                            </div>


                                            <input type="hidden" value="6bbd62fad3" /><input type="hidden" />
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>


                    </div>

                    <div class="cart-collaterals">
                        <div class="cart_totals calculated_shipping">
                            <h2>Cart Totals</h2>
                            <table cellspacing="0" class="shop_table shop_table_responsive">
                                <tbody>
                                    <tr class="cart-subtotal">
                                        <th>Subtotal</th>
                                        <td data-title="Subtotal"><span class="woocommerce-Price-amount amount"><span class="woocommerce-Price-currencySymbol">£</span>875.00</span></td>
                                    </tr>
                                    <tr class="order-total">
                                        <th>Total</th>
                                        <td data-title="Total"><strong><span class="woocommerce-Price-amount amount"><span class="woocommerce-Price-currencySymbol">£</span>875.00</span></strong> </td>
                                    </tr>
                                </tbody>
                            </table>

                            <div class="wc-proceed-to-checkout">
                                <a href="./Checkout.aspx" class="checkout-button button alt wc-forward">Proceed to Checkout</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
