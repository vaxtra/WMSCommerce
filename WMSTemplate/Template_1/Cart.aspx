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
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <h3>Keranjang belanja Anda kosong.</h3>
                                <br />
                                <a href="Default.aspx">Lanjutkan Belanja</a>
                            </asp:View>

                            <asp:View ID="View2" runat="server">
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
                                            <asp:Repeater ID="RepeaterCart" runat="server" OnItemCommand="RepeaterCart_ItemCommand">
                                                <ItemTemplate>
                                                    <tr class="cart_item">
                                                        <asp:HiddenField ID="HiddenFieldIDTransaksiECommerceDetail" runat="server" Value='<%# Eval("IDTransaksiECommerceDetail") %>' />
                                                        <td class="product-thumbnail">
                                                            <a href="./catalog-product.html">
                                                                <img width="80" height="auto" src='<%# Eval("Foto") %>' class="attachment-shop_thumbnail size-shop_thumbnail wp-post-image" />
                                                            </a></td>

                                                        <td class="product-name" data-title="Product">
                                                            <div class="caption">
                                                                <a class="product-name" href="http://pro-theme.com/wordpress/ismile/product/blu-vivo-5-smartphone/"><%# Eval("Nama") %><br /></a>
                                                            </div>
                                                        </td>

                                                        <td class="product-price" data-title="Price">
                                                            <span class="product-price total-price">
                                                                <span class="woocommerce-Price-amount amount"><%# Eval("Harga").ToFormatHarga() %></span>						    </span>
                                                        </td>

                                                        <td class="product-quantity" data-title="Quantity">

                                                            <div class="input-group btn-block qty-block" data-trigger="spinner">

                                                                <asp:TextBox ID="TextBoxQuantity" runat="server" Text='<%# Eval("Quantity") %>' TextMode="Number" CssClass="input-text qty text"></asp:TextBox>
                                                            </div>


                                                        </td>

                                                        <td class="product-subtotal" data-title="Total">
                                                            <span class="woocommerce-Price-amount amount"><%# Eval("Total").ToFormatHarga() %></span>						</td>
                                                        <td class="product-remove">
                                                            <asp:LinkButton ID="ButtonHapus" runat="server" CommandName="Hapus" CommandArgument='<%# Eval("IDTransaksiECommerceDetail") %>'><i class="fa fa-times fa-lg"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr class="cart_data">
                                                <td colspan="6" class="actions">

                                                    <div class="coupon">
                                                        <asp:Button ID="ButtonUpdate" runat="server" Text="Update Cart" CssClass="button" OnClick="ButtonUpdate_Click" /><br />
                                                    </div>


                                                    <input type="hidden" value="6bbd62fad3" /><input type="hidden" />
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>

                    <div class="cart-collaterals">
                        <div class="cart_totals calculated_shipping">
                            <h2>Cart Totals</h2>
                            <table cellspacing="0" class="shop_table shop_table_responsive">
                                <tbody>
                                    <tr class="order-total">
                                        <th>Total</th>
                                        <td data-title="Total"><strong><span class="woocommerce-Price-amount amount"><asp:Literal ID="LiteralTotal" runat="server"></asp:Literal></span></strong> </td>
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
