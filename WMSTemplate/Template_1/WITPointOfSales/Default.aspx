<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //Ketika memasukkan Kode Produk
        function Func_TextBoxInputProdukManual(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonConfirmManualProduk');
                if (bt) {
                    bt.click();
                    return false;
                }
            }//DELETE
            else if (evt.keyCode == 46) {
                var bt = document.getElementById('ButtonKeluarManualProduk');
                if (bt) {
                    bt.click();
                    return false;
                }
            } //ESCAPE
            else if (evt.keyCode == 27) {
                var bt = document.getElementById('ButtonKeluarManualProduk');
                if (bt) {
                    bt.click();
                    return false;
                }
            } //INSERT
            else if (evt.keyCode == 45) {
                var bt = document.getElementById('ButtonTunai');
                if (bt) {
                    bt.click();
                    return false;
                }
            } //F4
            else if (evt.keyCode == 115) {
                var bt = document.getElementById('ButtonOrderTunai');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function Func_TextBoxHelper(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonConfirmHelper');
                if (bt) {
                    bt.click();
                    return false;
                }
            }//DELETE
            else if (evt.keyCode == 46) {
                var bt = document.getElementById('ButtonKeluarHelper');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
            else if (evt.keyCode == 27) {
                var bt = document.getElementById('ButtonKeluarHelper');
                if (bt) {
                    bt.click();
                    return false;
                }
            } //INSERT
            else if (evt.keyCode == 45) {
                var bt = document.getElementById('ButtonTunai');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function Func_TextBoxKeterangan(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonSimpanKeterangan');
                if (bt) {
                    bt.click();
                    return false;
                }
            }//DELETE
            else if (evt.keyCode == 46) {
                var bt = document.getElementById('ButtonKeluarKeterangan');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
            else if (evt.keyCode == 27) {
                var bt = document.getElementById('ButtonKeluarKeterangan');
                if (bt) {
                    bt.click();
                    return false;
                }
            }//INSERT
            else if (evt.keyCode == 45) {
                var bt = document.getElementById('ButtonTunai');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function Func_TextBoxJumlahBayar(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonBayarTunai');
                if (bt) {
                    bt.click();
                    return false;
                }
            }//DELETE
            else if (evt.keyCode == 46) {
                var bt = document.getElementById('ButtonKeluarJenisPembayaran');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
            else if (evt.keyCode == 27) {
                var bt = document.getElementById('ButtonKeluarJenisPembayaran');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function Func_TextBoxPencarianProduk(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonCariProduk');
                if (bt) {
                    bt.click();
                    return false;
                }
            } //INSERT
            else if (evt.keyCode == 45) {
                var bt = document.getElementById('ButtonTunai');
                if (bt) {
                    bt.click();
                    return false;
                }
            } //F5
            else if (evt.keyCode == 116) {
                var bt = document.getElementById('ButtonPelanggan');
                if (bt) {
                    bt.click();
                    return false;
                }
            } //F6
            else if (evt.keyCode == 117) {
                var bt = document.getElementById('ButtonDiscount');
                if (bt) {
                    bt.click();
                    return false;
                }
            } //F4
            else if (evt.keyCode == 115) {
                var bt = document.getElementById('ButtonOrderTunai');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function Func_TextBoxZona(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonPilihKurir');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function Func_TextBoxWilayah(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonOkPelanggan');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function Func_TextBoxBiayaPengiriman(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonSimpanBiayaPengiriman');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        function Func_TextBoxPencarianPelanggan(e) {
            var evt = e ? e : window.event;

            //ENTER
            if (evt.keyCode == 13) {
                var bt = document.getElementById('ButtonPencarianPelanggan');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
            else if (evt.keyCode == 27) {
                var bt = document.getElementById('ButtonKeluarPelanggan');
                if (bt) {
                    bt.click();
                    return false;
                }
            } //INSERT
            else if (evt.keyCode == 45) {
                var bt = document.getElementById('ButtonTunai');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

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

        function Func_TextBoxPembayaranLainnya(e) {
            var evt = e ? e : window.event;

            if (evt.keyCode == 13) {
                //ENTER

                var bt = document.getElementById('ButtonSimpanPembayaranLainnya');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
            else if (evt.keyCode == 27) {
                //ESC

                var bt = document.getElementById('ButtonTunai');
                if (bt) {
                    bt.click();
                    return false;
                }
            }
        }

        //Fungsi untuk isi textbox dari button
        function setValueTextbox(id, value) {
            document.getElementById(id).value += value;
        }

        //Fungsi untuk menjumlahkan value yang sudah ada dengan value yang baru
        function jumlahValueTextbox(id, value) {
            if (document.getElementById(id).value == null || document.getElementById(id).value == "") {
                document.getElementById(id).value = "0";
            }

            document.getElementById(id).value = parseInt(document.getElementById(id).value) + parseInt(value);
        }

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=500,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }

        function OnClientPopulating(sender, e) {
            sender._element.className = "loading";
        }

        function OnClientCompleted(sender, e) {
            sender._element.className = "";
        }
    </script>

    <style>
        .completionListElement {
            visibility: hidden;
            margin: 0 !important;
            cursor: pointer;
            text-align: left;
            font-size: 15px;
            list-style-type: none;
            padding: 0;
        }

        .listItem {
            background-color: white;
            width: 500px;
            font-size: 14px;
            font-size: 15px;
            padding: 1px;
        }

        .highlightedListItem {
            background-color: #c3ebf9;
            width: 500px;
            padding: 1px;
            font-size: 15px;
            font-weight: bold;
        }

        .loading {
            background-image: url('/assets/images/loader.gif');
            background-position: right;
            background-repeat: no-repeat;
        }

        .tdHiddenBorderTop td {
            border-top: none !important;
            font-size: 14px;
            padding: 5px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelTransaksi" runat="server">
        <ContentTemplate>
            <%-- LAYOUT UTAMA --%>
            <div class="hidden-print">
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
                            <%--CHART PRODUK--%>
                            <div class="form-group">
                                <div class="table-responsive">
                                    <table cellpadding="0" cellspacing="0" border="0" class="display" id="tableProduk" style="background-color: transparent;">
                                        <thead>
                                            <tr>
                                                <th class="hidden"></th>
                                                <th>Product</th>
                                                <th>Price</th>
                                                <th>Discount</th>
                                                <th>Qty</th>
                                                <th>Subtotal</th>
                                                <th style="width: 1%;"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RepeaterDetailTransaksi" runat="server" OnItemCommand="RepeaterDetailTransaksi_ItemCommand">
                                                <ItemTemplate>
                                                    <tr class='<%# Parse.Int(Eval("JumlahDetail").ToString()) == (Container.ItemIndex + 1) ? "odd selected" : "odd" %>'>
                                                        <td class="hidden" style="padding: 9.1px 10px;">
                                                            <asp:Label ID="LabelIDDetailTransaksi" runat="server" Text='<%# Eval("IDDetailTransaksi") %>'></asp:Label>
                                                        </td>
                                                        <td style="padding: 9.1px 10px;">
                                                            <asp:Button ID="ButtonNamaProduk" class="btn btn-sm btn-default btn-outline" Style="font-weight: bold; width: 100%; text-align: left; white-space: pre-wrap; text-transform: uppercase;" CommandName="Deskripsi" runat="server" Text='<%# Eval("Nama") + ((decimal.Parse(Eval("PotonganHargaJual").ToString()) == 0) ? "" : " - " + Pengaturan.FormatHarga((decimal.Parse(Eval("HargaJual").ToString()) - decimal.Parse(Eval("HargaJualTampil").ToString())) / decimal.Parse(Eval("HargaJual").ToString()) * 100) + "%")  %>' />
                                                        </td>
                                                        <td class="text-right" style="padding: 9.1px 10px">
                                                            <asp:Button ID="ButtonEditHarga" Enabled='<%# Eval("IsDiscount") %>' class="btn btn-sm btn-default btn-outline" Style="font-weight: bold; width: 100%; text-align: right;" CommandName="EditHarga" CommandArgument='<%# Pengaturan.FormatHarga(Eval("HargaJual").ToString()) %>' runat="server" Text='<%# Pengaturan.FormatHarga(Eval("HargaJual")) %>' />
                                                        </td>
                                                        <td class="text-right" style="padding: 9.1px 10px;">
                                                            <asp:Button ID="ButtonPotonganHarga" Enabled='<%# Eval("IsDiscount") %>' class="btn btn-sm btn-default btn-outline" Style="font-weight: bold; width: 100%; color: red; text-align: right;" CommandName="PotonganHarga" CommandArgument='<%# Pengaturan.FormatHarga(Eval("PotonganHargaJual").ToString()) %>' runat="server" Text='<%# Pengaturan.FormatHarga(Eval("PotonganHargaJual")) %>' />
                                                        </td>
                                                        <td class="text-right" style="padding: 9.1px 10px;">
                                                            <asp:Button ID="ButtonEditJumlah" Enabled='<%# Eval("UbahQuantity") %>' class="btn btn-sm btn-default btn-outline" Style="font-weight: bold; width: 100%; text-align: right;" CommandName="EditJumlah" CommandArgument='<%# Eval("JumlahProduk") %>' runat="server" Text='<%# Eval("JumlahProduk") %>' ForeColor='<%# (int.Parse(Eval("JumlahProduk").ToString()) < 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black %>' />
                                                        </td>
                                                        <td class="text-right" style="padding: 9.1px 10px;">
                                                            <asp:Button ID="ButtonEditSubtotal" Enabled='<%# Eval("IsDiscount") %>' class="btn btn-sm btn-default btn-outline" Style="font-weight: bold; width: 100%; text-align: right;" CommandName="EditSubtotal" CommandArgument='<%# Pengaturan.FormatHarga(Eval("Subtotal").ToString()) %>' runat="server" Text='<%# Pengaturan.FormatHarga(Eval("Subtotal").ToString()) %>' ForeColor='<%# (Pengaturan.FormatAngkaInput(Eval("Subtotal").ToString()) < 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black %>' />
                                                        </td>
                                                        <td style="width: 1%; padding: 9.1px 10px;">
                                                            <asp:Button ID="ButtonCariProduk" runat="server" Text="X" Font-Bold="true" CssClass="btn btn-sm btn-danger" CommandName="Hapus" CommandArgument='<%# Eval("IDKombinasiProduk") %>' Visible='<%# Eval("UbahQuantity") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <%--PENCARIAN PRODUK--%>
                            <div class="form-group" style="margin-top: 20px;">
                                <div class="form-inline pull-left">
                                    <div class="form-group">
                                        <asp:Button ClientIDMode="Static" ID="ButtonProdukManual" runat="server" Text="Kode [ENTER]" CssClass="btn btn-sm btn-success" OnClick="ButtonProdukManual_Click" />
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="TextBoxTanggal" CssClass="form-control input-sm TanggalJam" Width="175px" runat="server" AutoPostBack="true" OnTextChanged="TextBoxTanggal_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="ButtonResetTanggal" runat="server" Text="Reset" CssClass="btn btn-danger btn-sm" OnClick="ButtonResetTanggal_Click" />
                                    </div>
                                </div>
                                <div class="form-inline pull-right">
                                    <div class="form-group">
                                        <asp:TextBox ID="TextBoxPencarianProduk" placeholder="[F2] pencarian" ClientIDMode="Static" runat="server" onFocus="this.select();" CssClass="form-control input-sm" Width="175px" Height="30px" onkeypress="return Func_TextBoxPencarianProduk(event)"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="PencarianKombinasiProduk"
                                            MinimumPrefixLength="2"
                                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                            TargetControlID="TextBoxPencarianProduk"
                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true"
                                            CompletionListCssClass="completionListElement"
                                            CompletionListItemCssClass="listItem"
                                            CompletionListHighlightedItemCssClass="highlightedListItem"
                                            OnClientHiding="OnClientCompleted"
                                            OnClientPopulated="OnClientCompleted"
                                            OnClientPopulating="OnClientPopulating"
                                            UseContextKey="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="ButtonCariProduk" ClientIDMode="Static" runat="server" Text="Cari" CssClass="btn btn-sm btn-success" OnClick="ButtonCariProduk_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <br />
                            </div>
                            <%--PILIH KATEGORI--%>
                            <div class="form-group">
                                <asp:HiddenField ID="HiddenFieldPageKategori" runat="server" />
                                <div class="table-responsive">
                                    <table style="width: 100%; background-color: transparent;">
                                        <tr>
                                            <td style="width: 50px">
                                                <asp:Button ID="ButtonKategoriKiri" runat="server" Text="<<" CssClass="btn btn-default btn-lg pull-left" Width="50px" Height="48px" Style="white-space: normal; margin: 5px 5px 0px 0px; text-transform: uppercase; font-size: 14px; font-weight: bold;" OnClick="ButtonKategoriKiri_Click" /></td>
                                            <td>
                                                <table style="width: 100%; background-color: transparent;">
                                                    <tr>
                                                        <asp:Repeater ID="RepeaterKategoriProduk" runat="server" OnItemCommand="RepeaterKategoriProduk_ItemCommand">
                                                            <ItemTemplate>
                                                                <td style="width: 25%;">
                                                                    <asp:Button ID="ImageButtonShow" Text='<%# Eval("Nama") %>' CommandName="Pilih" CommandArgument='<%# Eval("IDKategoriProduk") %>' runat="server" CssClass="btn btn-primary btn-lg" BorderColor="#F8F8F8" Width="100%" Height="50px" Style="white-space: normal; margin-top: 5px; text-transform: uppercase; font-size: 14px; font-weight: bold;" Visible='<%# Eval("IDKategoriProduk").ToString() != "-1" ? true : false %>' />
                                                                </td>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50px">
                                                <asp:Button ID="ButtonKategoriKanan" runat="server" Text=">>" CssClass="btn btn-default btn-lg pull-right" Width="50px" Height="48px" Style="white-space: normal; margin: 5px 0px 0px 5px; text-transform: uppercase; font-size: 14px; font-weight: bold;" OnClick="ButtonKategoriKanan_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:HiddenField ID="HiddenFieldPageKategoriAkhir" runat="server" />
                                <asp:HiddenField ID="HiddenFieldPilihKategori" runat="server" />
                            </div>
                            <%--PILIH PRODUK--%>
                            <div class="form-group">
                                <asp:HiddenField ID="HiddenFieldPageProduk" runat="server" />
                                <div class="table-responsive">
                                    <table style="width: 100%; background-color: transparent;">
                                        <tr>
                                            <td style="width: 50px">
                                                <asp:Button ID="ButtonProdukKiri" runat="server" Text="<<" CssClass="btn btn-default btn-lg" Width="50px" Height="160px" Style="white-space: normal; margin: 5px 6px 0px 0px; text-transform: uppercase; font-size: 14px; font-weight: bold;" OnClick="ButtonProdukKiri_Click" /></td>
                                            <td>
                                                <table style="width: 100%; background-color: transparent;">
                                                    <asp:Repeater ID="RepeaterProduk" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <asp:Repeater ID="RepeaterDetail" runat="server" OnItemCommand="RepeaterProduk_ItemCommand" DataSource='<%# Eval("baris") %>'>
                                                                    <ItemTemplate>
                                                                        <td style="width: 25%;">
                                                                            <asp:Button ID="ImageButtonShow" Text='<%# Eval("Nama") %>' CommandName="Pilih" CommandArgument='<%# Eval("IDProduk") %>' runat="server" CssClass="btn btn-default btn-outline btn-lg" Width="100%" Height="50px" Style="white-space: normal; margin-top: 5px; text-transform: uppercase; font-size: 14px; font-weight: bold;" Enabled='<%# Eval("IDProduk").ToString() != "-1" ? true : false %>' />
                                                                        </td>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </td>
                                            <td style="width: 50px">
                                                <asp:Button ID="ButtonProdukKanan" runat="server" Text=">>" CssClass="btn btn-default btn-lg" Width="50px" Height="160px" Style="white-space: normal; margin: 5px 0px 0px 6px; text-transform: uppercase; font-size: 14px; font-weight: bold;" OnClick="ButtonProdukKanan_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:HiddenField ID="HiddenFieldPageProdukAkhir" runat="server" />
                            </div>
                            <%--PILIH KOMBINASI PRODUK--%>
                            <asp:LinkButton ID="LinkButton6" runat="server"></asp:LinkButton>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderKombinasiProduk" runat="server" PopupControlID="KombinasiProduk" TargetControlID="LinkButton6" BackgroundCssClass="modalBackground">
                            </ajaxToolkit:ModalPopupExtender>
                            <div id="KombinasiProduk" class="form-group">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <h3 class="modal-title" style="font-weight: bold;">
                                                        <asp:Label ID="LabelNamaProduk" runat="server"></asp:Label>
                                                    </h3>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="ButtonKeluarProduk" runat="server" Text="Keluar" CssClass="btn btn-danger pull-right" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-body text-center">
                                            <asp:Repeater ID="RepeaterProdukKombinasi" runat="server" OnItemCommand="RepeaterProdukKombinasi_ItemCommand">
                                                <ItemTemplate>
                                                    <asp:Button
                                                        ID="ButtonPilih"
                                                        CssClass='<%# Parse.Int(Eval("Jumlah").ToString()) > 0 ? "btn btn-lg btn-default btn-outline infoTooltip" : "btn btn-lg btn-danger infoTooltip" %>'
                                                        Width="140px"
                                                        Height="60px"
                                                        Style="white-space: normal; margin: 2px 0 1px 0; text-transform: uppercase; font-size: 14px; font-weight: bold;"
                                                        runat="server"
                                                        Text='<%# Eval("Nama") %>'
                                                        CommandName="Tambah"
                                                        CommandArgument='<%# Eval("IDKombinasiProduk") %>'
                                                        data-placement="right" title='<%# "Stok : " + Eval("Jumlah") + " Harga : " + Pengaturan.FormatHarga(Eval("HargaJual")) %>' />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                            <%--INFO--%>
                            <div class="form-group">
                                <div class="table-responsive">
                                    <table class="table tdHiddenBorderTop" style="background-color: #e7e7e7;">
                                        <tr class="text-right">
                                            <td></td>
                                            <td style="font-size: 14pt">QUANTITY</td>
                                            <td style="font-size: 14pt">:</td>
                                            <td style="font-size: 14pt">
                                                <asp:Label ID="LabelQuantity" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="text-right">
                                            <td></td>
                                            <td style="font-size: 14pt">PELANGGAN</td>
                                            <td style="font-size: 14pt">:</td>
                                            <td style="font-size: 14pt">
                                                <asp:Label ID="LabelPelangganNama" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="text-right" id="DisplaySubtotal" runat="server">
                                            <td></td>
                                            <td style="font-size: 14pt">SUBTOTAL</td>
                                            <td style="font-size: 14pt">:</td>
                                            <td style="font-size: 14pt;">
                                                <asp:Label ID="LabelSubtotal" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="text-right" id="DisplayDiscountTransaksi" runat="server">
                                            <td></td>
                                            <td style="font-size: 14pt">DISCOUNT</td>
                                            <td style="font-size: 14pt">:</td>
                                            <td style="font-size: 14pt;">
                                                <asp:Label ID="LabelDiscountTransaksi" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="text-right">
                                            <td></td>
                                            <td style="font-size: 14pt">PENGIRIMAN</td>
                                            <td style="font-size: 14pt">:</td>
                                            <td style="font-size: 14pt;">
                                                <asp:Label ID="LabelBiayaPengiriman" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="text-right" id="DisplayBiayaTambahan1" runat="server">
                                            <td class="text-center" style="font-size: 16pt">
                                                <asp:CheckBox ID="CheckBoxBiayaTambahan1" Checked="false" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBoxBiayaTambahan1_CheckedChanged" /></td>
                                            <td style="font-size: 14pt">
                                                <asp:Label ID="LabelKeteranganBiayaTambahan1" runat="server"></asp:Label>
                                            </td>
                                            <td style="font-size: 14pt">:</td>
                                            <td style="font-size: 16pt;">
                                                <asp:Label ID="LabelBiayaTambahan1" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="text-right" id="DisplayBiayaTambahan2" runat="server">
                                            <td class="text-center">
                                                <asp:CheckBox ID="CheckBoxBiayaTambahan2" Checked="false" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBoxBiayaTambahan2_CheckedChanged" /></td>
                                            <td style="font-size: 14pt">
                                                <asp:Label ID="LabelKeteranganBiayaTambahan2" runat="server"></asp:Label>
                                            </td>
                                            <td style="font-size: 14pt">:</td>
                                            <td style="font-size: 16pt;">
                                                <asp:Label ID="LabelBiayaTambahan2" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="text-right" id="DisplayPembulatan" runat="server" visible="true">
                                            <td></td>
                                            <td style="font-size: 14pt">PEMBULATAN</td>
                                            <td style="font-size: 14pt">:</td>
                                            <td style="font-size: 14pt;">
                                                <asp:Label ID="LabelPembulatan" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr id="DisplayGrandTotal" runat="server" style="background: #46be8a; color: white;">
                                            <td colspan="4" class="text-right" style="font-size: 23pt;">
                                                <asp:Label ID="LabelGrandTotal" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <%--BUTTON ACTION--%>
                            <asp:Panel ID="PanelPembayaran1" runat="server">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:Button ID="ButtonPelanggan" ClientIDMode="Static" runat="server" Text="Pelanggan [F5]" CssClass="btn btn-lg btn-info btn-block" OnClick="ButtonPelanggan_Click" />
                                        </div>
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:Button ID="ButtonDiscount" ClientIDMode="Static" runat="server" Text="Discount [F6]" CssClass="btn btn-lg btn-info btn-block" OnClick="ButtonDiscount_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:Button ID="ButtonBiayaPengiriman" ClientIDMode="Static" runat="server" Text="Pengiriman [F7]" CssClass="btn btn-lg btn-info btn-block" OnClick="ButtonBiayaPengiriman_Click" />
                                        </div>
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:Button ID="ButtonTambahKeterangan" ClientIDMode="Static" runat="server" Text="Keterangan [F8]" CssClass="btn btn-lg btn-info btn-block" OnClick="ButtonTambahKeterangan_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:Button ID="ButtonOrderTunai" ClientIDMode="Static" runat="server" Text="Order [F4]" CssClass="btn btn-lg btn-warning btn-block" OnClick="ButtonOrderTunai_Click" />
                                        </div>
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <asp:Button ID="ButtonBatalBayar" runat="server" Text="Batal" CssClass="btn btn-lg btn-danger btn-block" OnClick="ButtonBatalBayar_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="ButtonTunai" ClientIDMode="Static" runat="server" Text="Bayar [INSERT]" Height="58px" CssClass="btn btn-lg btn-success btn-block" OnClick="ButtonTunai_Click" />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="ButtonConfirmPayment" ClientIDMode="Static" runat="server" Text="Confirm Payment [F3]" Height="58px" CssClass="btn btn-lg btn-success btn-outline btn-block" OnClick="ButtonConfirmPayment_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>


                <%--HELPER ANGKA--%>
                <asp:LinkButton ID="LinkButton3" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderHelper" runat="server" PopupControlID="HelperTransaksi" TargetControlID="LinkButton3" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <div id="HelperTransaksi" class="text-center">
                    <div class="col-lg-12">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <h3 class="modal-title text-left" style="font-weight: bold;">
                                                <asp:Label ID="LabelHeaderHelper" runat="server"></asp:Label>
                                            </h3>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ID="ButtonKeluarHelper" ClientIDMode="Static" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <asp:HiddenField ID="HiddenFieldIDHelper" runat="server" />
                                    <asp:Literal ID="LiteralWarningHelper" runat="server"></asp:Literal>
                                    <center>
                                <div class="form-group">
                                    <asp:TextBox ID="TextBoxHelper" CssClass="form-control input-lg" runat="server" Width="270px" ClientIDMode="Static" onkeypress="return Func_TextBoxHelper(event)" onFocus="this.select();"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <center>
                                    <table style="margin-top: -5px;">
                                        <tr>
                                            <td>
                                                <input id="ButtonCalculator1" type="button" value="1" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator2" type="button" value="2" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator3" type="button" value="3" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator16" type="button" value="Clear" class="btn btn-lg btn-danger" onclick="document.getElementById('TextBoxHelper').value = '';" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="ButtonCalculator5" type="button" value="4" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator6" type="button" value="5" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator7" type="button" value="6" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator8" type="button" value="%" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="ButtonCalculator9" type="button" value="7" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator10" type="button" value="8" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator11" type="button" value="9" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator4" type="button" value="←" class="btn btn-default btn-outline btn-lg" onclick="document.getElementById('TextBoxHelper').value = document.getElementById('TextBoxHelper').value.slice(0, document.getElementById('TextBoxHelper').value.length - 1);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="ButtonCalculator12" type="button" value="0" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator13" type="button" value="00" class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <input id="ButtonCalculator14" type="button" value="," class="btn btn-default btn-outline btn-lg" onclick="setValueTextbox('TextBoxHelper', this.value);" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" /></td>
                                            <td>
                                                <asp:Button CssClass="btn btn-lg btn-success" ClientIDMode="Static" ID="ButtonConfirmHelper" runat="server" Style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" Text="Ok" OnClick="ButtonConfirmHelper_Click" /></td>
                                        </tr>
                                    </table>
                                        </center>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--KETERANGAN PRODUK--%>
                <asp:LinkButton ID="LinkButton5" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderDeskripsiProduk" runat="server" PopupControlID="DeskripsiProduk" TargetControlID="LinkButton5" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <div id="DeskripsiProduk" class="text-center">
                    <div class="col-lg-12">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header text-center">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <h3 class="modal-title text-left" style="font-weight: bold;">Keterangan</h3>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ClientIDMode="Static" ID="ButtonKeluarDeskripsiProduk" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-7">
                                            <asp:HiddenField ID="HiddenFieldIDKombinasiProdukDeskripsi" runat="server" />
                                            <div class="form-group">
                                                <div class="row">
                                                    <asp:Repeater ID="RepeaterTemplateKeterangan" runat="server">
                                                        <ItemTemplate>
                                                            <div class="col-md-4">
                                                                <input id="ButtonKeterangan" type="button" value='<%# "\n# " + Eval("Isi") %>' style="margin-top: 5px; font-weight: bold;" class="btn btn-default btn-outline btn-block" onclick="setValueTextbox('TextBoxKeteranganTransaksiDetail', this.value);" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <asp:TextBox ID="TextBoxKeteranganTransaksiDetail" ClientIDMode="Static" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Style="height: 250px;"></asp:TextBox>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button ID="ButtonKeteranganTambahan" CssClass="btn btn-lg btn-success btn-block" runat="server" Text="Simpan" OnClick="ButtonKeteranganTambahan_Click" />
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <input id="ButtonHapusKeteranganTambahan" type="button" value="Hapus" class="btn btn-lg btn-warning btn-block" onclick="document.getElementById('TextBoxKeteranganTransaksiDetail').value = '';" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--SCAN BARCODE--%>
                <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderProdukManual" runat="server" PopupControlID="ManualProduk" TargetControlID="LinkButton2" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <div id="ManualProduk" class="text-center">
                    <div class="col-lg-12">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <h3 class="modal-title text-left" style="font-weight: bold;">SCAN CODE
                                            </h3>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ClientIDMode="Static" ID="ButtonKeluarManualProduk" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <asp:Literal ID="LiteralWarningInputProdukManual" runat="server"></asp:Literal>
                                    </div>
                                    <div class="form-inline">
                                        <asp:HiddenField ID="HiddenFieldIDDetailTransaksi" runat="server" />
                                        <div class="form-group">
                                            <asp:TextBox ID="TextBoxInputProdukManual" autocomplete="off" CssClass="form-control input-lg deletable" runat="server" onkeypress="return Func_TextBoxInputProdukManual(event)" onFocus="this.select();" Style="text-transform: uppercase; width: 449px;"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ClientIDMode="Static" CssClass="btn btn-primary btn-lg" ID="ButtonConfirmManualProduk" runat="server" Text="SCAN" OnClick="ButtonConfirmManualProduk_Click" />
                                        </div>
                                    </div>
                                    <div id="panelKombinasiProduk" runat="server" class="form-group" visible="false">
                                        <table cellpadding="0" cellspacing="0" border="0" class="display" id="tableKombinasi">
                                            <asp:Repeater ID="RepeaterKombinasiProduk" runat="server" OnItemCommand="RepeaterKombinasiProduk_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("Nama") %>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="ButtonPilih" runat="server" Text="Pilih" CommandName="Pilih" CssClass="btn btn-sm btn-success" CommandArgument='<%# Eval("IDKombinasiProduk") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="alert alert-info">
                                            <div class="text-left">
                                                <p>(+) : +2 <b>menambah quantity</b> sebanyak 2</p>
                                                <p>(-) : -1 <b>mengurangi quantity</b> sebanyak 1</p>
                                                <p>(=) : =10 <b>merubah quantity</b> menjadi 10</p>
                                                <p>(%) : 20% <b>discount</b> sebesar 20%</p>
                                                <p>(%) : %1000 <b>discount</b> sebesar 1000</p>
                                                <p>($) : $20.000 <b>merubah harga</b> menjadi 20.000</p>
                                                <p>($$) : $$100.000 <b>merubah subtotal</b> menjadi 100.000</p>
                                                <p>(#) : #testing <b>menambah keterangan</b> testing</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- PELANGGAN --%>
                <asp:LinkButton ID="LinkButton4" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderPelanggan" runat="server" PopupControlID="Pelanggan" TargetControlID="LinkButton4" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <div id="Pelanggan" class="text-center">
                    <asp:MultiView ID="MultiViewPelanggan" runat="server">
                        <asp:View ID="View5" runat="server">
                            <div class="col-lg-12">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <h3 class="modal-title text-left" style="font-weight: bold;">Pilih Pelanggan</h3>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="ButtonKeluarPelanggan" ClientIDMode="Static" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="DropDownListJenisTransaksi" Style="width: 100%" CssClass="select2 text-left" runat="server" OnSelectedIndexChanged="DropDownListJenisTransaksi_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-8">
                                                    <div class="form-inline pull-right">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="TextBoxPencarianPelanggan" runat="server" CssClass="form-control input-sm" onkeypress="return Func_TextBoxPencarianPelanggan(event)" onFocus="this.select();" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Button ID="ButtonPencarianPelanggan" ClientIDMode="Static" CssClass="btn btn-primary btn-sm proses" data-loading-text="Loading..." runat="server" Text="Cari" OnClick="ButtonPencarianPelanggan_Click" />
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Button ID="ButtonTambahPelanggan" CssClass="btn btn-success btn-sm" runat="server" Text="Tambah" OnClick="ButtonTambahPelanggan_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="margin-top: 10px; overflow-y: scroll; height: 250px;">
                                                <div class="table-responsive">
                                                    <table class="table table-condensed table-hover" style="font-size: 12px;">
                                                        <thead>
                                                            <tr class="active">
                                                                <th class="fitSize">No.</th>
                                                                <th>Nama</th>
                                                                <th>Grup</th>
                                                                <th>Deposit</th>
                                                                <th>Telepon</th>
                                                                <th class="text-center">Alamat</th>
                                                                <th class="fitSize"></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="RepeaterDataPelanggan" runat="server" OnItemCommand="RepeaterDataPelanggan_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="fitSize text-left"><%# Container.ItemIndex + 1 %></td>
                                                                        <td class="fitSize text-left"><%# Eval("NamaLengkap") %></td>
                                                                        <td class="text-left"><%# Eval("GrupPelanggan") %></td>
                                                                        <td class="text-right"><%# Pengaturan.FormatHarga(Eval("Deposit")) %></td>
                                                                        <td class="text-left"><%# Eval("Handphone") %></td>
                                                                        <td class="text-left"><%# Eval("AlamatLengkap") %></td>
                                                                        <td class="fitSize">
                                                                            <asp:Button ID="ButtonPilihPelanggan" runat="server" Text="Pilih" CommandName="Pilih" CommandArgument='<%# Eval("IDPelanggan") %>' CssClass="btn btn-sm btn-success" />
                                                                            <asp:Button ID="ButtonUbahPelanggan" runat="server" Text="Ubah" CommandName="Ubah" CommandArgument='<%# Eval("IDPelanggan") %>' CssClass="btn btn-sm btn-info" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <div class="col-lg-12">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h3 class="modal-title text-left" style="font-weight: bold;">
                                                <asp:Label ID="LabelKeteranganPelanggan" runat="server"></asp:Label>
                                                Pelanggan </h3>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group">
                                                <asp:DropDownList ID="DropDownListGrupPelanggan" Style="width: 100%" CssClass="select2 text-left" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:HiddenField ID="HiddenFieldIDPelanggan" runat="server" />
                                                <asp:TextBox ID="TextBoxNamaLengkap" class="form-control input-sm" runat="server" placeholder="Name"></asp:TextBox>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="TextBoxPelangganEmail" class="form-control input-sm" runat="server" placeholder="Email"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="TextBoxNomorTelepon" class="form-control input-sm" runat="server" placeholder="Telephone"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox ID="TextBoxAlamat" class="form-control input-sm" runat="server" placeholder="Address" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="TextBoxKodePos" class="form-control input-sm" runat="server" placeholder="Post Code"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="TextBoxWilayah" ClientIDMode="Static" runat="server" onFocus="this.select();" class="form-control input-sm" Width="100%" Height="30px" onkeypress="return Func_TextBoxWilayah(event)" placeholder="Region"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="PencarianWilayah"
                                                            MinimumPrefixLength="2"
                                                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                                            TargetControlID="TextBoxWilayah"
                                                            ID="AutoCompleteExtender3" runat="server" FirstRowSelected="true"
                                                            CompletionListCssClass="completionListElement"
                                                            CompletionListItemCssClass="listItem"
                                                            CompletionListHighlightedItemCssClass="highlightedListItem"
                                                            OnClientHiding="OnClientCompleted"
                                                            OnClientPopulated="OnClientCompleted"
                                                            OnClientPopulating="OnClientPopulating"
                                                            UseContextKey="true">
                                                        </ajaxToolkit:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Button
                                                            ID="ButtonOkPelanggan"
                                                            ClientIDMode="Static"
                                                            runat="server"
                                                            Text="Ok"
                                                            data-loading-text="Loading..."
                                                            CssClass="btn btn-success btn-block proses"
                                                            OnClick="ButtonOkPelanggan_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Button
                                                            ID="ButtonPelangganKembali"
                                                            runat="server"
                                                            Text="Kembali"
                                                            CssClass="btn btn-danger btn-block"
                                                            OnClick="ButtonPelanggan_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>

                <%-- PENGIRIMAN --%>
                <asp:LinkButton ID="LinkButton9" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderBiayaPengiriman" runat="server" PopupControlID="BiayaPengiriman" TargetControlID="LinkButton9" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <div id="BiayaPengiriman">
                    <div class="col-lg-12">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header text-center">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <h3 class="modal-title text-left" style="font-weight: bold;">Biaya Pengiriman
                                        <asp:Label ID="LabelTotalBeratShipping" runat="server"></asp:Label>
                                            </h3>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ClientIDMode="Static" ID="Button12" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group" runat="server" id="PanelTempatPengirim">
                                        <label>Pengirim</label>
                                        <asp:DropDownList ID="DropDownListTempatPengirim" Style="width: 100%" CssClass="select2 text-left" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-inline">
                                        <label>Pengiriman</label>
                                        <br />
                                        <div class="form-group">
                                            <asp:TextBox ID="TextBoxZona" ClientIDMode="Static" runat="server" onFocus="this.select();" CssClass="form-control input-sm" Style="width: 330px; height: 30px;" onkeypress="return Func_TextBoxZona(event)"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ServiceMethod="PencarianZona"
                                                MinimumPrefixLength="2"
                                                CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                                TargetControlID="TextBoxZona"
                                                ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true"
                                                CompletionListCssClass="completionListElement"
                                                CompletionListItemCssClass="listItem"
                                                CompletionListHighlightedItemCssClass="highlightedListItem"
                                                OnClientHiding="OnClientCompleted"
                                                OnClientPopulated="OnClientCompleted"
                                                OnClientPopulating="OnClientPopulating"
                                                UseContextKey="true">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button
                                                ID="ButtonPilihKurir"
                                                ClientIDMode="Static"
                                                runat="server"
                                                Text="Pilih"
                                                data-loading-text="Loading..."
                                                CssClass="btn btn-sm btn-primary proses"
                                                OnClick="ButtonPilihKurir_Click" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Biaya</label>
                                            <asp:TextBox ClientIDMode="Static" ID="TextBoxBiayaPengiriman" runat="server" CssClass="form-control input-sm" onkeypress="return Func_TextBoxBiayaPengiriman(event)" onFocus="this.select();"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Kurir</label>
                                            <asp:DropDownList ID="DropDownListKurir" Style="width: 100%" CssClass="select2 text-left" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Button
                                        ID="ButtonSimpanBiayaPengiriman"
                                        ClientIDMode="Static"
                                        runat="server"
                                        Text="Simpan"
                                        data-loading-text="Loading..."
                                        CssClass="btn btn-lg btn-success btn-block proses"
                                        OnClick="ButtonSimpanBiayaPengiriman_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--KETERANGAN--%>
                <asp:LinkButton ID="LinkButton8" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderKeterangan" runat="server" PopupControlID="Keterangan" TargetControlID="LinkButton8" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <div id="Keterangan" class="text-center">
                    <div class="col-lg-12">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <h3 class="modal-title text-left" style="font-weight: bold;">KETERANGAN
                                            </h3>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ClientIDMode="Static" ID="ButtonKeluarKeterangan" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <asp:TextBox ClientIDMode="Static" ID="TextBoxKeterangan" runat="server" CssClass="form-control input-sm" onkeypress="return Func_TextBoxKeterangan(event)" onFocus="this.select();" placeholder="Keterangan"></asp:TextBox>
                                    </div>
                                    <div class="form-group text-center">
                                        <asp:Button
                                            ID="ButtonSimpanKeterangan"
                                            ClientIDMode="Static"
                                            runat="server"
                                            Text="Simpan"
                                            data-loading-text="Loading..."
                                            CssClass="btn btn-lg btn-success btn-block proses"
                                            OnClick="ButtonSimpanKeterangan_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--PEMBAYARAN--%>
                <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderPembayaran" runat="server" PopupControlID="Pembayaran" TargetControlID="LinkButton1" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <div id="Pembayaran" class="text-center">
                    <asp:MultiView ID="MultiViewPembayaran" runat="server">
                        <%-- CASH OR PAYMENT --%>
                        <asp:View ID="View4" runat="server">
                            <div class="col-lg-12">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <h3 class="modal-title text-left" style="font-weight: bold;">Grand Total :
                                                <asp:Label ID="LabelGrandTotal1" runat="server"></asp:Label>
                                                    </h3>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ClientIDMode="Static" ID="ButtonKeluarJenisPembayaran" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                        <h4 style="margin: 0px;">CASH</h4>
                                                        <br />
                                                        <div class="form-group">
                                                            <asp:TextBox ClientIDMode="Static" CssClass="form-control input-lg auto" Style="width: 270px; margin-left: 5px;" data-v-min="0" data-v-max="922337203685477" data-a-sep="." data-a-dec="," ID="TextBoxJumlahBayar" runat="server" onkeypress="return Func_TextBoxJumlahBayar(event)" onFocus="this.select();"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                            <table style="margin-top: -5px;">
                                                                <tr>
                                                                    <td>
                                                                        <input id="Button9" type="button" value="100" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" class="btn btn-default btn-outline btn-lg" onclick="jumlahValueTextbox('TextBoxJumlahBayar', '100000');" /></td>
                                                                    <td>
                                                                        <input id="Button8" type="button" value="50" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" class="btn btn-default btn-outline btn-lg" onclick="jumlahValueTextbox('TextBoxJumlahBayar', '50000');" /></td>
                                                                    <td>
                                                                        <input id="Button7" type="button" value="20" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" class="btn btn-default btn-outline btn-lg" onclick="jumlahValueTextbox('TextBoxJumlahBayar', '20000');" /></td>
                                                                    <td>
                                                                        <input id="Button10" type="button" value="Clear" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" class="btn btn-danger btn-lg" onclick="document.getElementById('TextBoxJumlahBayar').value = '';" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input id="Button6" type="button" value="10" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" class="btn btn-default btn-outline btn-lg" onclick="jumlahValueTextbox('TextBoxJumlahBayar', '10000');" /></td>
                                                                    <td>
                                                                        <input id="Button5" type="button" value="5" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" class="btn btn-default btn-outline btn-lg" onclick="jumlahValueTextbox('TextBoxJumlahBayar', '5000');" /></td>
                                                                    <td>
                                                                        <input id="Button4" type="button" value="2" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" class="btn btn-default btn-outline btn-lg" onclick="jumlahValueTextbox('TextBoxJumlahBayar', '2000');" /></td>
                                                                    <td rowspan="2">
                                                                        <asp:Button ID="ButtonBayarTunai" ClientIDMode="Static" runat="server" Text="Cash" Style="width: 60px; height: 130px; margin: 5px; font-weight: bold; padding: 1px;" CssClass="btn btn-success btn-lg text-center proses" data-loading-text="Loading..." OnClick="ButtonBayarTunai_Click" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input id="Button3" type="button" value="1" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" class="btn btn-default btn-outline btn-lg" onclick="jumlahValueTextbox('TextBoxJumlahBayar', '1000');" /></td>
                                                                    <td>
                                                                        <input id="Button2" type="button" value="0.5" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" class="btn btn-default btn-outline btn-lg" onclick="jumlahValueTextbox('TextBoxJumlahBayar', '500');" /></td>
                                                                    <td>
                                                                        <input id="Button1" type="button" value="0.1" style="width: 60px; height: 60px; margin: 5px; font-weight: bold; padding: 1px;" class="btn btn-default btn-outline btn-lg" onclick="jumlahValueTextbox('TextBoxJumlahBayar', '100');" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                        <h4 style="margin: 0px;">OTHER PAYMENT</h4>
                                                        <br />
                                                        <asp:HiddenField ID="HiddenFieldIDJenisPembayaran" runat="server" />
                                                        <div class="form-group">
                                                            <asp:Button ID="ButtonSplitPayment" runat="server" Text="Split Payment" CssClass="btn btn-lg btn-warning btn-block" OnClick="ButtonSplitPayment_Click" />
                                                        </div>
                                                        <div id="PanelPilihanJenisPembayaran" runat="server">
                                                            <div class="form-group">
                                                                <asp:Button ID="ButtonPembayaranDeposit" runat="server" Visible="false" Text="Deposit" CssClass="btn btn-lg btn-warning btn-block" OnClick="ButtonPembayaranDeposit_Click" />
                                                            </div>
                                                            <asp:Repeater ID="RepeaterJenisPembayaran" runat="server" OnItemCommand="RepeaterJenisPembayaran_ItemCommand">
                                                                <ItemTemplate>
                                                                    <div class="form-group">
                                                                        <asp:Button ID="ButtonPembayaran" runat="server" Text='<%# Eval("Nama") %>' CssClass="btn btn-lg btn-primary btn-block" CommandName="Pilih" CommandArgument='<%# Eval("IDJenisPembayaran") %>' />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View6" runat="server">
                            <div class="col-lg-12">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <h3 class="modal-title text-left" style="font-weight: bold;">Grand Total :
                                                <asp:Label ID="LabelGrandTotal2" runat="server"></asp:Label>
                                                    </h3>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ClientIDMode="Static" ID="ButtonKeluarPembayaranLain" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group">
                                                <asp:TextBox ClientIDMode="Static" ID="TextBoxPembayaranLainnya" runat="server" CssClass="form-control input-sm" onkeypress="return Func_TextBoxPembayaranLainnya(event)" onFocus="this.select();" placeholder="Nomor Kartu"></asp:TextBox>
                                            </div>
                                            <div class="form-group text-center">
                                                <asp:Button
                                                    ID="ButtonSimpanPembayaranLainnya"
                                                    ClientIDMode="Static"
                                                    runat="server"
                                                    Text="Bayar"
                                                    CssClass="btn btn-lg btn-success btn-block proses"
                                                    data-loading-text="Loading..."
                                                    OnClick="ButtonSimpanPembayaranLainnya_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <%-- BUTTON PRINT --%>
                        <asp:View ID="View2" runat="server">
                            <div class="col-lg-12">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <h3 class="modal-title text-left" style="font-weight: bold;">Terima Kasih
                                                    </h3>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ClientIDMode="Static" ID="ButtonKeluarPrint" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" OnClick="ButtonKeluarPrint_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group">
                                                <h3 style="font-weight: bold;">Kembalian :
                                                <asp:Label ID="LabelJumlahKembalian" runat="server"></asp:Label></h3>
                                                <br />
                                                <h5 class="text-left">Keterangan :
                                                <asp:Label ID="LabelKeterangan" runat="server" Text="-"></asp:Label></h5>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:Button ID="ButtonPrint1" runat="server" Text="Print" CssClass="btn btn-lg btn-success btn-block" onkeypress="return Func_ButtonPrint1(event)" OnClientClick="window.print();" OnClick="ButtonPrint1_Click" />
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="ButtonPrint2" runat="server" Text="Print Invoice" CssClass="btn btn-lg btn-primary btn-block" onkeypress="return Func_ButtonPrint1(event)" />
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="ButtonPrint3" runat="server" Text="Print Packing Slip" CssClass="btn btn-lg btn-info btn-block" onkeypress="return Func_ButtonPrint1(event)" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>

                <%--SPLIT PAYMENT--%>
                <asp:LinkButton ID="LinkButton7" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderSplitPayment" runat="server" PopupControlID="SplitPayment" TargetControlID="LinkButton7" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <div id="SplitPayment">
                    <div class="col-lg-12">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header text-center">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <h3 class="modal-title text-left" style="font-weight: bold;">Split Payment : 
                                        <asp:Label ID="LabelSplitPaymentGrandTotal" runat="server"></asp:Label>
                                                <asp:Label ID="LabelSplitPaymentLebihKurang" runat="server"></asp:Label>

                                            </h3>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ClientIDMode="Static" ID="Button11" runat="server" Text="Keluar [ESC]" CssClass="btn btn-danger pull-right" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:Literal ID="LiteralSplitPaymentWarning" runat="server"></asp:Literal>
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Tanggal</label>
                                                                <asp:TextBox ID="TextBoxSplitPaymentTanggal" onfocus="this.select();" CssClass="form-control input-sm  DisableEnter" runat="server" placeholder="Tanggal"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="d MMMM yyyy" TargetControlID="TextBoxSplitPaymentTanggal" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Jam</label>
                                                                <asp:TextBox ID="TextBoxSplitPaymentJam" onfocus="this.select();" CssClass="form-control input-sm  DisableEnter" runat="server" placeholder="Jam"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Nominal</label>
                                                        <asp:TextBox ID="TextBoxSplitPaymentNominal" onfocus="this.select();" CssClass="form-control input-sm angka DisableEnter" runat="server" placeholder="Nominal"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="TextBoxSplitPaymentKeterangan" CssClass="form-control input-sm DisableEnter" runat="server" placeholder="Keterangan"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Button ID="ButtonSplitPaymentDeposit" runat="server" Visible="false" Text="Deposit" CssClass="btn btn-info btn-sm btn-block" OnClick="ButtonSplitPaymentDeposit_Click" />
                                                    </div>
                                                    <div class="row">
                                                        <asp:Repeater ID="RepeaterSplitPaymentJenisPembayaran" runat="server" OnItemCommand="RepeaterSplitPaymentJenisPembayaran_ItemCommand">
                                                            <ItemTemplate>
                                                                <div class="col-md-6">
                                                                    <asp:Button ID="ButtonSplitPaymentPembayaran" runat="server" Style="margin-top: 5px;" Text='<%# Eval("Nama") %>' CssClass="btn btn-primary btn-sm btn-block" CommandName="Pilih" CommandArgument='<%# Eval("IDJenisPembayaran") %>' />
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-7">
                                            <div style="overflow-y: scroll; height: 237px;">
                                                <div class="table-responsive">
                                                    <table class="table table-condensed table-hover table-bordered" style="font-size: 12px;">
                                                        <thead>
                                                            <tr class="active">
                                                                <th>No.</th>
                                                                <th>Tanggal</th>
                                                                <th>Pengguna</th>
                                                                <th>Jenis</th>
                                                                <th class="text-right">Total</th>
                                                                <th class="text-center">Keterangan</th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="RepeaterSplitPaymentPembayaran" runat="server" OnItemCommand="RepeaterSplitPaymentPembayaran_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                                        <td><%# Pengaturan.FormatTanggalRingkas(Eval("Tanggal")) %><br />
                                                                            <%# Pengaturan.FormatJam(Eval("Tanggal")) %></td>
                                                                        <td><%# Eval("NamaPengguna") %></td>
                                                                        <td><%# Eval("NamaJenisPembayaran") %></td>
                                                                        <%# Pengaturan.FormatHargaRepeater((decimal)Eval("Total")) %>
                                                                        <td><%# Eval("Keterangan") %></td>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButtonHapus" ImageUrl="img/cross.png" CommandName="Hapus" CommandArgument='<%# Eval("IDTransaksiJenisPembayaran") %>' runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <tr class="text-right success" style="font-weight: bold;">
                                                                <td colspan="4"></td>
                                                                <td class="text-right">
                                                                    <asp:Label ID="LabelSplitPaymentTotalPembayaran" runat="server"></asp:Label></td>
                                                                <td colspan="2"></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button ID="ButtonSplitPaymentBayar" CssClass="btn btn-lg btn-success btn-block" runat="server" Text="Bayar" OnClick="ButtonSplitPaymentBayar_Click" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="ButtonSplitPaymentOrder" CssClass="btn btn-lg btn-warning btn-block" runat="server" Text="Order" OnClick="ButtonOrderTunai_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--PRINT--%>
            <div class="row visible-print" style="font-size: 10pt; line-height: 12px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="2" class="text-center">
                                        <img src="/images/logo.jpg" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="text-center">
                                        <asp:Label ID="LabelPrintStore" runat="server"></asp:Label>
                                        -
                                <asp:Label ID="LabelPrintTempatNama" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="text-center">
                                        <asp:Label ID="LabelTempatAlamat" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="text-center">
                                        <asp:Label ID="LabelTempatTelepon" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="text-center">Order #<asp:Label ID="LabelPrintIDOrder" runat="server"></asp:Label><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="text-left">Meja :
                                        <asp:Label ID="LabelPrintMeja" runat="server"></asp:Label><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left"></td>
                                    <td class="text-right">
                                        <asp:Label ID="LabelPrintJenisPembayaran" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="text-left">
                                        <asp:Label ID="LabelPrintPengguna" runat="server"></asp:Label></td>
                                    <td class="text-right">
                                        <asp:Label ID="LabelPrintTanggal" runat="server"></asp:Label></td>
                                </tr>
                            </table>

                            <div id="PanelPelanggan" runat="server">
                                <tr>
                                    <td colspan="3" class="text-center">------------------------------------------------------</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table>
                                            <tr>
                                                <td style="vertical-align: top; font-weight: bold;">Nama</td>
                                                <td style="vertical-align: top;">:</td>
                                                <td>
                                                    <asp:Label ID="LabelPrintPelangganNama" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; font-weight: bold;">Telepon</td>
                                                <td style="vertical-align: top;">:</td>
                                                <td>
                                                    <asp:Label ID="LabelPrintPelangganTelepon" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; font-weight: bold;">Alamat</td>
                                                <td style="vertical-align: top;">:</td>
                                                <td>
                                                    <asp:Label ID="LabelPrintPelangganAlamat" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3" class="text-center">------------------------------------------------------</td>
                                </tr>
                                <asp:Repeater ID="RepeaterPrintTransaksiDetail" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("JumlahProduk") %>x</td>
                                            <td><%# Eval("Produk") %></td>
                                            <td class="text-right"><%# Pengaturan.FormatHarga(Eval("TotalTanpaPotonganHargaJual")) %></td>
                                        </tr>
                                        <tr runat="server" visible='<%# (Parse.Decimal(Eval("PotonganHargaJual").ToString()) == 0) ? false : true %>'>
                                            <td></td>
                                            <td class="text-right">Discount</td>
                                            <td class="text-right">-<%# Pengaturan.FormatHarga(Eval("TotalPotonganHargaJual").ToString()) %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3" class="text-center">------------------------------------------------------</td>
                                </tr>
                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;"><b>QUANTITY :</b></td>
                                    <td class="text-right" style="width: 100%;">
                                        <asp:Label ID="LabelPrintQuantity" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;"><b>SUBTOTAL :</b></td>
                                    <td>
                                        <asp:Label ID="LabelPrintSubtotal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <%--                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;"><b>DISCOUNT PRODUCT :</b></td>
                                    <td>
                                        <asp:Label ID="LabelDiscountProduct" runat="server"></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr class="text-right" id="PanelDiscountTransaksi" runat="server">
                                    <td></td>
                                    <td style="width: 100%;"><b>DISCOUNT :</b></td>
                                    <td>
                                        <asp:Label ID="LabelPrintDiscountTransaksi" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="text-right" id="PanelBiayaTambahan1" runat="server">
                                    <td></td>
                                    <td style="width: 100%;"><b>
                                        <asp:Label ID="LabelPrintKeteranganBiayaTambahan1" runat="server"></asp:Label>
                                        :</b>
                                    </td>
                                    <td class="text-right" style="width: 100%;">
                                        <asp:Label ID="LabelPrintBiayaTambahan1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="text-right" id="PanelBiayaTambahan2" runat="server">
                                    <td></td>
                                    <td style="width: 100%;"><b>
                                        <asp:Label ID="LabelPrintKeteranganBiayaTambahan2" runat="server"></asp:Label>
                                        :</b>
                                    </td>
                                    <td class="text-right" style="width: 100%;">
                                        <asp:Label ID="LabelPrintBiayaTambahan2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="text-right" id="PanelBiayaPengiriman" runat="server">
                                    <td></td>
                                    <td style="width: 100%;"><b>DELIVERY :</b></td>
                                    <td>
                                        <asp:Label ID="LabelPrintBiayaPengiriman" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="text-right" id="PanelPembulatan" runat="server">
                                    <td></td>
                                    <td style="width: 100%;"><b>PEMBULATAN :</b></td>
                                    <td>
                                        <asp:Label ID="LabelPrintPembulatan" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="text-right">
                                    <td></td>
                                    <td style="width: 100%;"><b>TOTAL :</b></td>
                                    <td class="text-right" style="width: 100%;">
                                        <b>
                                            <asp:Label ID="LabelPrintGrandTotal" runat="server"></asp:Label>
                                        </b>
                                    </td>
                                </tr>
                                <tr class="text-right" id="PanelPembayaran" runat="server">
                                    <td></td>
                                    <td style="width: 100%;"><b>CASH :</b></td>
                                    <td class="text-right" style="width: 100%;">
                                        <asp:Label ID="LabelPrintPembayaran" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="text-right" id="PanelKembalian" runat="server">
                                    <td></td>
                                    <td style="width: 100%;"><b>CHANGE :</b></td>
                                    <td class="text-right" style="width: 100%;">
                                        <asp:Label ID="LabelPrintKembalian" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="text-center">------------------------------------------------------</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table>
                                            <asp:Repeater ID="RepeaterPrintJenisPembayaran" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="font-weight: bold;"><%# Eval("NamaJenisPembayaran") %></td>
                                                        <td>&nbsp;:&nbsp;</td>
                                                        <td><%# Pengaturan.FormatHarga(Eval("Total")) %></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td><%# Eval("Keterangan") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="LabelPrintSisaPembayaran" runat="server" Style="margin-top: 5px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="PanelKeterangan" runat="server">
                                    <td colspan="3" class="text-center">------------------------------------------------------</td>
                                </tr>
                                <tr id="PanelKeterangan1" runat="server">
                                    <td colspan="3" class="text-center">
                                        <asp:Label ID="LabelPrintKeterangan" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="PanelFooter" runat="server">
                                    <td colspan="3" class="text-center">------------------------------------------------------</td>
                                </tr>
                                <tr id="PanelFooter1" runat="server">
                                    <td colspan="3">
                                        <asp:Label ID="LabelPrintFooter" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table style="width: 100%;">
                                <tr>
                                    <td class="text-center">THANK YOU</td>
                                </tr>
                                <tr>
                                    <td class="text-center">WIT. Management System Powered by WIT.</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>

            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanelTransaksi">
                <ProgressTemplate>
                    <div style='position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Black; filter: alpha(opacity=90); opacity: 0.5;'>
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="margin-top: 17%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJavascript" runat="Server">
</asp:Content>
