<%@ Page Title="" Language="C#" MasterPageFile="frontend/MasterPage.master" AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <%--    <div class="b-title-page b-title-page_w_bg">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="b-title-page__title">Men's Wear</h1>
                    <ul class="b-title-page__list list-inline">
                        <li><a href="./catalog-list.html">MEN</a></li>
                        <li><a href="./catalog-list.html">WOMEN</a></li>
                        <li><a href="./catalog-list.html">ACCESSORIES</a></li>
                        <li><a href="./catalog-list.html">GADGETS</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>--%>
    <!-- end b-title-page-->

    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <div class="b-goods-headers b-goods-headers_mod-a">
                    <button type="button" data-toggle="collapse" data-target="#wrap-filter" class="b-filter__btn btn btn-default btn-effect"><span class="badge">3</span><i class="icon fa fa-bars"></i> Filters</button>
                    <div class="b-goods-headers__view"><i class="b-goods-headers__view-item active fa fa-th js-view-col"></i><i class="b-goods-headers__view-item fa fa-list js-view-list"></i></div>
                    <div class="b-goods-headers__sorting">
                        <ol class="breadcrumb">
                            <li><a href="./Default.aspx">Home</a></li>
                            <li><a href="./Default.aspx">Catalog</a></li>
                            <li><a href="./Default.aspx">Clothong</a></li>
                            <li class="active">raincoats</li>
                        </ol>
                    </div>
                </div>
                <div id="wrap-filter" class="b-filter collapse">
                    <div class="form-filter" />
                    <div class="row">

                        <div class="col-md-6">
                            <section class="b-filter__section">
                                <h3 class="b-filter__title">Categories</h3>
                                <select data-width="100%" class="selectpicker">
                                    <option />
                                    T-Shirts
                         
                                    <option />
                                    Categories 2
                         
                                    <option />
                                    Categories 3
                       
                                </select>
                            </section>
                        </div>

                        <div class="col-md-6">
                            <section class="b-filter__section">
                                <h3 class="b-filter__title">brand</h3>
                                <select data-width="100%" class="selectpicker">
                                    <option />
                                    Tom Tailor
                         
                       
                                </select>
                            </section>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="b-goods-number-products pull-left">Showing 1 to 10 of 243 products</div>
                            <button type="reset" class="b-filter__btn-reset pull-right"><i class="icon fa fa-refresh color-primary"></i>Reser Filters</button>
                        </div>
                    </div>
                </div>

            </div>
            <div class="l-main-content">
                <div class="b-goods-catalog">
                    <asp:Repeater ID="RepeaterData" runat="server">
                        <ItemTemplate>
                            <section class="b-goods b-goods_mod-a b-goods_5-col">
                                <div class="b-goods__inner">
                                    <a href='<%# Eval("Source") %>' class="b-goods__img js-zoom-images">
                                        <img src="/frontend/assets/media/content/goods/mens/220x250/1.jpg" alt="goods" class="img-responsive" /></a>
                                    <div class="b-goods__wrap">
                                        <div class="b-goods__category">category</div>
                                        <h3 class="b-goods__name">Enceladus</h3>
                                        <div class="b-goods__description">Praesent quis vestibulum risus. Suspendisse non malesuada risus, ut venenatis nisi. Quisque aliquam justo in est tempor malesuada ac eu sem.</div>
                                        <div class="b-goods__price-old">$180.00</div>
                                        <div class="b-goods__price">$160</div>
                                        <div class="b-goods-links"><a href="./ProductDetail.aspx" class="b-goods-links__item b-goods-links__item_main">View Detail</a></div>
                                    </div>
                                </div>
                            </section>
                        </ItemTemplate>
                    </asp:Repeater>
                    <!-- end b-goods-->

                </div>
                <div class="text-right">
                    <ul class="pagination">
                        <li><a href="#">1</a></li>
                        <li class="active"><a href="#">2</a></li>
                        <li><a href="#">3</a></li>
                        <li><a href="#">4</a></li>
                        <li><a href="#">5</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>