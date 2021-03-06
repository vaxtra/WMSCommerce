GO
USE [DBWITEnterpriseSystem]
GO
/****** Object:  UserDefinedFunction [dbo].[Func_IDTransaksi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Func_IDTransaksi]
(
	@IDTempat INT,
	@Nomor INT,
	@TanggalTransaksi DATETIME
)
RETURNS varchar(30)
AS
BEGIN
	-- Kode Store - Tanggal Bulan Tahun - Auto Increment
	RETURN CONVERT(VARCHAR(3), @IDTempat) + '-' + REPLACE(CONVERT(VARCHAR(10), @TanggalTransaksi, 104), '.', '') + '-' + CONVERT(VARCHAR(15), @Nomor)
END



GO
/****** Object:  Table [dbo].[TBAkun]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAkun](
	[IDAkun] [int] IDENTITY(1,1) NOT NULL,
	[IDAkunGrup] [int] NULL,
	[IDTempat] [int] NULL,
	[Kode] [varchar](250) NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBAkuntansiAkun] PRIMARY KEY CLUSTERED 
(
	[IDAkun] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAkunGrup]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAkunGrup](
	[IDAkunGrup] [int] IDENTITY(1,1) NOT NULL,
	[IDAkunGrupParent] [int] NULL,
	[Nama] [varchar](250) NULL,
	[EnumJenisAkunGrup] [int] NULL,
	[EnumSaldoNormal] [int] NULL,
 CONSTRAINT [PK_TBAkuntansiGrupAkun] PRIMARY KEY CLUSTERED 
(
	[IDAkunGrup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAkunSaldoAwal]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBAkunSaldoAwal](
	[IDAkunSaldoAwal] [int] IDENTITY(1,1) NOT NULL,
	[IDTempat] [int] NULL,
	[IDAkun] [int] NULL,
	[IDPengguna] [int] NULL,
	[TanggalSaldoAwal] [date] NULL,
	[TanggalDaftar] [datetime] NULL,
 CONSTRAINT [PK_TBAkunSaldoAwal] PRIMARY KEY CLUSTERED 
(
	[IDAkunSaldoAwal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBAkuntansiAkun]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAkuntansiAkun](
	[IDAkuntansiAkun] [int] IDENTITY(1,1) NOT NULL,
	[IDAkuntansiAkunParent] [int] NULL,
	[IDAkuntansiAkunTipe] [int] NOT NULL,
	[Nomor] [varchar](250) NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[SaldoAwal] [money] NOT NULL,
	[Keterangan] [text] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_TBAkuntansiAkun_1] PRIMARY KEY CLUSTERED 
(
	[IDAkuntansiAkun] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAkuntansiAkunTipe]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAkuntansiAkunTipe](
	[IDAkuntansiAkunTipe] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[SaldoNormal] [int] NOT NULL,
	[Bertambah] [int] NOT NULL,
	[Berkurang] [int] NOT NULL,
 CONSTRAINT [PK_TBAkuntansiTipeAkun] PRIMARY KEY CLUSTERED 
(
	[IDAkuntansiAkunTipe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAkuntansiDokumen]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAkuntansiDokumen](
	[IDAkuntansiDokumen] [int] IDENTITY(1,1) NOT NULL,
	[IDAkuntansiJurnal] [int] NOT NULL,
	[Format] [varchar](250) NOT NULL,
 CONSTRAINT [PK_TBAkuntansiDokumen] PRIMARY KEY CLUSTERED 
(
	[IDAkuntansiDokumen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAkuntansiJurnal]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAkuntansiJurnal](
	[IDAkuntansiJurnal] [int] IDENTITY(1,1) NOT NULL,
	[IDTempat] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDWMS] [uniqueidentifier] NOT NULL,
	[TanggalDaftar] [datetime] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[Keterangan] [varchar](250) NOT NULL,
 CONSTRAINT [PK_TBAkuntansiJurnal_1] PRIMARY KEY CLUSTERED 
(
	[IDAkuntansiJurnal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAkuntansiJurnalDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBAkuntansiJurnalDetail](
	[IDAkuntansiJurnalDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDAkuntansiJurnal] [int] NOT NULL,
	[IDAkuntansiAkun] [int] NOT NULL,
	[Debit] [money] NOT NULL,
	[Kredit] [money] NOT NULL,
 CONSTRAINT [PK_TBAkuntansiJurnalDetail_1] PRIMARY KEY CLUSTERED 
(
	[IDAkuntansiJurnalDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBAlamat]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAlamat](
	[IDAlamat] [int] IDENTITY(1,1) NOT NULL,
	[IDPelanggan] [int] NULL,
	[IDNegara] [int] NULL,
	[IDProvinsi] [int] NULL,
	[IDKota] [int] NULL,
	[NamaLengkap] [varchar](250) NULL,
	[AlamatLengkap] [text] NULL,
	[Provinsi] [varchar](250) NULL,
	[Kota] [varchar](250) NULL,
	[KodePos] [varchar](250) NULL,
	[Handphone] [varchar](250) NULL,
	[TeleponLain] [varchar](250) NULL,
	[Keterangan] [text] NULL,
	[BiayaPengiriman] [money] NULL,
 CONSTRAINT [PK_TBAlamat] PRIMARY KEY CLUSTERED 
(
	[IDAlamat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAnggaran]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAnggaran](
	[IDAnggaran] [int] IDENTITY(1,1) NOT NULL,
	[IDPeriode] [int] NULL,
	[Nama] [varchar](250) NULL,
	[NominalAnggaran] [money] NULL,
	[NominalRealisasi] [money] NULL,
 CONSTRAINT [PK_TBAnggaran] PRIMARY KEY CLUSTERED 
(
	[IDAnggaran] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAtribut]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAtribut](
	[IDAtribut] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IDAtributGrup] [int] NOT NULL,
	[TanggalDaftar] [datetime] NOT NULL,
	[TanggalUpdate] [datetime] NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[Pilihan] [bit] NOT NULL,
 CONSTRAINT [PK_TBAtribut_1] PRIMARY KEY CLUSTERED 
(
	[IDAtribut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAtributGrup]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAtributGrup](
	[IDAtributGrup] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[TanggalDaftar] [datetime] NOT NULL,
	[TanggalUpdate] [datetime] NOT NULL,
	[Nama] [varchar](250) NOT NULL,
 CONSTRAINT [PK_TBAtributGrup] PRIMARY KEY CLUSTERED 
(
	[IDAtributGrup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAtributPilihan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAtributPilihan](
	[IDAtributPilihan] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IDAtribut] [int] NOT NULL,
	[TanggalDaftar] [datetime] NOT NULL,
	[TanggalUpdate] [datetime] NOT NULL,
	[Nama] [varchar](250) NOT NULL,
 CONSTRAINT [PK_TBAtributPilihan] PRIMARY KEY CLUSTERED 
(
	[IDAtributPilihan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAtributPilihanBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBAtributPilihanBahanBaku](
	[IDAtributPilihan] [int] NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
 CONSTRAINT [PK_TBAtributPilihanBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDAtributPilihan] ASC,
	[IDBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBAtributPilihanPelanggan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBAtributPilihanPelanggan](
	[IDAtributPilihan] [int] NOT NULL,
	[IDPelanggan] [int] NOT NULL,
 CONSTRAINT [PK_TBAtributPilihanPelanggan] PRIMARY KEY CLUSTERED 
(
	[IDAtributPilihan] ASC,
	[IDPelanggan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBAtributPilihanPengguna]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBAtributPilihanPengguna](
	[IDAtributPilihan] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
 CONSTRAINT [PK_TBAtributPilihanPengguna] PRIMARY KEY CLUSTERED 
(
	[IDAtributPilihan] ASC,
	[IDPengguna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBAtributPilihanProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBAtributPilihanProduk](
	[IDAtributPilihan] [int] NOT NULL,
	[IDProduk] [int] NOT NULL,
 CONSTRAINT [PK_TBAtributPilihanProduk] PRIMARY KEY CLUSTERED 
(
	[IDAtributPilihan] ASC,
	[IDProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBAtributPilihanStore]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBAtributPilihanStore](
	[IDAtributPilihan] [int] NOT NULL,
	[IDStore] [int] NOT NULL,
 CONSTRAINT [PK_TBAtributPilihanStore] PRIMARY KEY CLUSTERED 
(
	[IDAtributPilihan] ASC,
	[IDStore] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBAtributPilihanTempat]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBAtributPilihanTempat](
	[IDAtributPilihan] [int] NOT NULL,
	[IDTempat] [int] NOT NULL,
 CONSTRAINT [PK_TBAtributPilihanTempat] PRIMARY KEY CLUSTERED 
(
	[IDAtributPilihan] ASC,
	[IDTempat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBAtributProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAtributProduk](
	[IDAtributProduk] [int] IDENTITY(1,1) NOT NULL,
	[IDAtributProdukGrup] [int] NULL,
	[Nama] [varchar](250) NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBAtribut] PRIMARY KEY CLUSTERED 
(
	[IDAtributProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBAtributProdukGrup]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBAtributProdukGrup](
	[IDAtributProdukGrup] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMSAtributProdukGrup] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBAtributProdukGrup] PRIMARY KEY CLUSTERED 
(
	[IDAtributProdukGrup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBBahanBaku](
	[IDBahanBaku] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] NOT NULL,
	[IDSatuan] [int] NOT NULL,
	[IDSatuanKonversi] [int] NOT NULL,
	[TanggalDaftar] [datetime] NULL,
	[TanggalUpdate] [datetime] NULL,
	[Urutan] [int] NULL,
	[KodeBahanBaku] [varchar](250) NULL,
	[Nama] [varchar](250) NULL,
	[Berat] [decimal](18, 2) NULL,
	[Konversi] [decimal](18, 2) NULL,
	[Deskripsi] [text] NULL,
 CONSTRAINT [PK_TBMaterial] PRIMARY KEY CLUSTERED 
(
	[IDBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBBerlangganan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBBerlangganan](
	[IDBerlangganan] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](250) NULL,
	[NoTelepon] [varchar](250) NULL,
 CONSTRAINT [PK_TBBerlangganan] PRIMARY KEY CLUSTERED 
(
	[IDBerlangganan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBBlackBox]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBBlackBox](
	[IDBlackBox] [int] IDENTITY(1,1) NOT NULL,
	[Tanggal] [datetime] NULL,
	[Pesan] [text] NULL,
	[Halaman] [text] NULL,
 CONSTRAINT [PK_TBLogError] PRIMARY KEY CLUSTERED 
(
	[IDBlackBox] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBDiscount]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBDiscount](
	[IDDiscount] [int] IDENTITY(1,1) NOT NULL,
	[IDDiscountEvent] [int] NOT NULL,
	[IDStokProduk] [int] NOT NULL,
	[EnumDiscountStore] [int] NOT NULL,
	[DiscountStore] [money] NOT NULL,
	[EnumDiscountKonsinyasi] [int] NOT NULL,
	[DiscountKonsinyasi] [money] NOT NULL,
 CONSTRAINT [PK_TBDiscount] PRIMARY KEY CLUSTERED 
(
	[IDDiscount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBDiscountEvent]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBDiscountEvent](
	[IDDiscountEvent] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDTempat] [int] NOT NULL,
	[TanggalDaftar] [datetime] NOT NULL,
	[TanggalUpdate] [datetime] NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[TanggalAwal] [datetime] NOT NULL,
	[TanggalAkhir] [datetime] NOT NULL,
	[EnumStatusDiscountEvent] [int] NOT NULL,
 CONSTRAINT [PK_TBDiscountEvent] PRIMARY KEY CLUSTERED 
(
	[IDDiscountEvent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBDiscountKombinasiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBDiscountKombinasiProduk](
	[IDDiscountKombinasiProduk] [int] IDENTITY(1,1) NOT NULL,
	[IDGrupPelanggan] [int] NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[Discount] [money] NOT NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBDiscountKombinasiProduk] PRIMARY KEY CLUSTERED 
(
	[IDGrupPelanggan] ASC,
	[IDKombinasiProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBDiscountProdukKategori]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBDiscountProdukKategori](
	[IDDiscountProdukKategori] [int] IDENTITY(1,1) NOT NULL,
	[IDGrupPelanggan] [int] NOT NULL,
	[IDProdukKategori] [int] NOT NULL,
	[Discount] [money] NOT NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBDiscountProdukKategori] PRIMARY KEY CLUSTERED 
(
	[IDGrupPelanggan] ASC,
	[IDProdukKategori] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBForecast]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBForecast](
	[IDForecast] [int] IDENTITY(1,1) NOT NULL,
	[IDTempat] [int] NOT NULL,
	[Tanggal] [date] NOT NULL,
	[Nominal] [money] NOT NULL,
	[Quantity] [money] NOT NULL,
 CONSTRAINT [PK_TBForecast] PRIMARY KEY CLUSTERED 
(
	[IDForecast] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBFotoKombinasiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBFotoKombinasiProduk](
	[IDKombinasiProduk] [int] NOT NULL,
	[IDFotoProduk] [int] NOT NULL,
 CONSTRAINT [PK_TBGambarKombinasi] PRIMARY KEY CLUSTERED 
(
	[IDKombinasiProduk] ASC,
	[IDFotoProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBFotoProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBFotoProduk](
	[IDFotoProduk] [int] IDENTITY(1,1) NOT NULL,
	[IDProduk] [int] NOT NULL,
	[ExtensiFoto] [varchar](250) NOT NULL,
	[FotoUtama] [bit] NOT NULL,
 CONSTRAINT [PK_TBGambarProduk] PRIMARY KEY CLUSTERED 
(
	[IDFotoProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBGrupPelanggan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBGrupPelanggan](
	[IDGrupPelanggan] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
	[EnumBonusGrupPelanggan] [int] NULL,
	[Persentase] [decimal](18, 2) NULL,
	[LimitTransaksi] [money] NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBGrupPelanggan] PRIMARY KEY CLUSTERED 
(
	[IDGrupPelanggan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBGrupPengguna]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBGrupPengguna](
	[IDGrupPengguna] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
	[DefaultURL] [text] NULL,
 CONSTRAINT [PK_TBJabatan] PRIMARY KEY CLUSTERED 
(
	[IDGrupPengguna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBGrupWilayah]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBGrupWilayah](
	[IDGrupWilayah] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBGrupWilayah] PRIMARY KEY CLUSTERED 
(
	[IDGrupWilayah] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBHargaSupplier]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBHargaSupplier](
	[IDHargaSupplier] [int] IDENTITY(1,1) NOT NULL,
	[IDStokBahanBaku] [int] NULL,
	[IDSupplier] [int] NULL,
	[Tanggal] [datetime] NULL,
	[Harga] [money] NULL,
 CONSTRAINT [PK_TBRelasiStokBahanBakuSupplier] PRIMARY KEY CLUSTERED 
(
	[IDHargaSupplier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBHargaVendor]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBHargaVendor](
	[IDHargaVendor] [int] IDENTITY(1,1) NOT NULL,
	[IDStokProduk] [int] NULL,
	[IDVendor] [int] NULL,
	[Tanggal] [datetime] NULL,
	[Harga] [money] NULL,
 CONSTRAINT [PK_TBRelasiStokProdukVendor] PRIMARY KEY CLUSTERED 
(
	[IDHargaVendor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBJenisBebanBiaya]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBJenisBebanBiaya](
	[IDJenisBebanBiaya] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBJenisBebanBiaya] PRIMARY KEY CLUSTERED 
(
	[IDJenisBebanBiaya] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBJenisBiayaProduksi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBJenisBiayaProduksi](
	[IDJenisBiayaProduksi] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBJenisBiayaProduksi] PRIMARY KEY CLUSTERED 
(
	[IDJenisBiayaProduksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBJenisBiayaProyeksi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBJenisBiayaProyeksi](
	[IDJenisBiayaProyeksi] [int] IDENTITY(1,1) NOT NULL,
	[Urutan] [int] NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBJenisBiayaProyeksi] PRIMARY KEY CLUSTERED 
(
	[IDJenisBiayaProyeksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBJenisBiayaProyeksiDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBJenisBiayaProyeksiDetail](
	[IDJenisBiayaProyeksiDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDJenisBiayaProyeksi] [int] NULL,
	[EnumBiayaProyeksi] [int] NULL,
	[Persentase] [decimal](18, 2) NULL,
	[Nominal] [money] NULL,
	[StatusBatas] [int] NULL,
 CONSTRAINT [PK_TBJenisBiayaProyeksiDetail] PRIMARY KEY CLUSTERED 
(
	[IDJenisBiayaProyeksiDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBJenisPembayaran]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBJenisPembayaran](
	[IDJenisPembayaran] [int] IDENTITY(1,1) NOT NULL,
	[IDAkun] [int] NULL,
	[IDJenisBebanBiaya] [int] NULL,
	[Nama] [varchar](250) NULL,
	[PersentaseBiaya] [decimal](18, 2) NULL,
 CONSTRAINT [PK_TBJenisPembayaran] PRIMARY KEY CLUSTERED 
(
	[IDJenisPembayaran] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBJenisPerpindahanStok]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBJenisPerpindahanStok](
	[IDJenisPerpindahanStok] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_TBJenisPerpindahanStok] PRIMARY KEY CLUSTERED 
(
	[IDJenisPerpindahanStok] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBJenisPOProduksi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBJenisPOProduksi](
	[IDJenisPOProduksi] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[Urutan] [int] NOT NULL,
 CONSTRAINT [PK_TBJenisPOProduksi] PRIMARY KEY CLUSTERED 
(
	[IDJenisPOProduksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBJenisStokMutasi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBJenisStokMutasi](
	[IDJenisStokMutasi] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NOT NULL,
 CONSTRAINT [PK_TBJenisStokMutasi] PRIMARY KEY CLUSTERED 
(
	[IDJenisStokMutasi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBJenisTransaksi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBJenisTransaksi](
	[IDJenisTransaksi] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBJenisTransaksi] PRIMARY KEY CLUSTERED 
(
	[IDJenisTransaksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBJurnal]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBJurnal](
	[IDJurnal] [int] IDENTITY(1,1) NOT NULL,
	[IDPengguna] [int] NULL,
	[IDTempat] [int] NULL,
	[Tanggal] [datetime] NULL,
	[Referensi] [varchar](250) NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBAkuntansiJurnal] PRIMARY KEY CLUSTERED 
(
	[IDJurnal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBJurnalDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBJurnalDetail](
	[IDJurnalDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDJurnal] [int] NULL,
	[IDAkun] [int] NULL,
	[Debit] [money] NULL,
	[Kredit] [money] NULL,
 CONSTRAINT [PK_TBAkuntansiJurnalDetail] PRIMARY KEY CLUSTERED 
(
	[IDJurnalDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBJurnalDokumen]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBJurnalDokumen](
	[IDJurnalDokumen] [int] IDENTITY(1,1) NOT NULL,
	[IDJurnal] [int] NULL,
	[Format] [varchar](250) NULL,
 CONSTRAINT [PK_TBJurnalDokumen] PRIMARY KEY CLUSTERED 
(
	[IDJurnalDokumen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBJurnalHutangPiutang]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBJurnalHutangPiutang](
	[IDJurnal] [int] NOT NULL,
	[JatuhTempo] [datetime] NULL,
	[EnumJenisHutangPiutang] [int] NULL,
 CONSTRAINT [PK_TBAkuntansiJurnalHutang] PRIMARY KEY CLUSTERED 
(
	[IDJurnal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBKategoriBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKategoriBahanBaku](
	[IDKategoriBahanBaku] [int] IDENTITY(1,1) NOT NULL,
	[IDKategoriBahanBakuParent] [int] NULL,
	[Nama] [varchar](250) NULL,
	[Deskripsi] [varchar](250) NULL,
 CONSTRAINT [PK_TBKategoriBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDKategoriBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKategoriKonten]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKategoriKonten](
	[IDKategoriKonten] [int] IDENTITY(1,1) NOT NULL,
	[IDKategoriKontenParent] [int] NULL,
	[Nama] [varchar](250) NULL,
	[Deskripsi] [varchar](250) NULL,
 CONSTRAINT [PK_TBKategoriKonten] PRIMARY KEY CLUSTERED 
(
	[IDKategoriKonten] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKategoriProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKategoriProduk](
	[IDKategoriProduk] [int] IDENTITY(1,1) NOT NULL,
	[IDKategoriProdukParent] [int] NULL,
	[Nama] [varchar](250) NULL,
	[Deskripsi] [text] NULL,
 CONSTRAINT [PK_TBKategoriProduk] PRIMARY KEY CLUSTERED 
(
	[IDKategoriProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKategoriTempat]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBKategoriTempat](
	[IDKategoriTempat] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [nvarchar](250) NULL,
 CONSTRAINT [PK_TBKategoriTempat] PRIMARY KEY CLUSTERED 
(
	[IDKategoriTempat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBKombinasiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKombinasiProduk](
	[IDKombinasiProduk] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] NOT NULL,
	[IDProduk] [int] NOT NULL,
	[IDAtributProduk] [int] NOT NULL,
	[IDAtributProduk1] [int] NULL,
	[IDAtributProduk2] [int] NULL,
	[IDAtributProduk3] [int] NULL,
	[TanggalDaftar] [datetime] NULL,
	[TanggalUpdate] [datetime] NULL,
	[Urutan] [int] NULL,
	[KodeKombinasiProduk] [varchar](250) NULL,
	[Nama] [varchar](250) NULL,
	[Berat] [decimal](18, 2) NULL,
	[Deskripsi] [text] NULL,
 CONSTRAINT [PK_TBKombinasi] PRIMARY KEY CLUSTERED 
(
	[IDKombinasiProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKomposisiBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBKomposisiBahanBaku](
	[IDBahanBakuProduksi] [int] NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
	[Jumlah] [decimal](18, 2) NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBKomposisiBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDBahanBakuProduksi] ASC,
	[IDBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBKomposisiKombinasiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBKomposisiKombinasiProduk](
	[IDBahanBaku] [int] NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[Jumlah] [decimal](18, 2) NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBKomposisiProduk] PRIMARY KEY CLUSTERED 
(
	[IDBahanBaku] ASC,
	[IDKombinasiProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBKonfigurasi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKonfigurasi](
	[IDKonfigurasi] [int] IDENTITY(1,1) NOT NULL,
	[IDKonfigurasiKategori] [int] NULL,
	[Urutan] [int] NULL,
	[Nama] [varchar](250) NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBKonfigurasi] PRIMARY KEY CLUSTERED 
(
	[IDKonfigurasi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKonfigurasiAkun]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKonfigurasiAkun](
	[IDKonfigurasiAkun] [int] IDENTITY(1,1) NOT NULL,
	[IDAkun] [int] NULL,
	[IDTempat] [int] NULL,
	[Nama] [varchar](50) NULL,
 CONSTRAINT [PK_TBKonfigurasiAkun] PRIMARY KEY CLUSTERED 
(
	[IDKonfigurasiAkun] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKonfigurasiKategori]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKonfigurasiKategori](
	[IDKonfigurasiKategori] [int] IDENTITY(1,1) NOT NULL,
	[Urutan] [int] NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBKonfigurasiKategori] PRIMARY KEY CLUSTERED 
(
	[IDKonfigurasiKategori] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKonfigurasiPrinter]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKonfigurasiPrinter](
	[IDKonfigurasiPrinter] [int] IDENTITY(1,1) NOT NULL,
	[NamaPrinter] [varchar](250) NOT NULL,
	[Kategori] [varchar](250) NOT NULL,
 CONSTRAINT [PK_TBKonfigurasiPrinter] PRIMARY KEY CLUSTERED 
(
	[IDKonfigurasiPrinter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKonfigurasiTomahawk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKonfigurasiTomahawk](
	[IDKonfigurasiTomahawk] [int] IDENTITY(1,1) NOT NULL,
	[Urutan] [int] NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[DurasiProses] [int] NOT NULL,
	[TanggalTerakhirProses] [datetime] NOT NULL,
 CONSTRAINT [PK_TBKonfigurasiTomahawk] PRIMARY KEY CLUSTERED 
(
	[IDKonfigurasiTomahawk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKonten]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKonten](
	[IDKonten] [int] IDENTITY(1,1) NOT NULL,
	[EnumKontenJenis] [int] NULL,
	[Tanggal] [datetime] NULL,
	[Judul] [varchar](250) NULL,
	[IsiSingkat] [text] NULL,
	[Isi] [text] NULL,
 CONSTRAINT [PK_TBKonten] PRIMARY KEY CLUSTERED 
(
	[IDKonten] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKontenKategori]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBKontenKategori](
	[IDKonten] [int] NOT NULL,
	[IDKategoriKonten] [int] NOT NULL,
 CONSTRAINT [PK_TBKontenKategori] PRIMARY KEY CLUSTERED 
(
	[IDKonten] ASC,
	[IDKategoriKonten] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBKurir]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBKurir](
	[IDKurir] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[Deskripsi] [varchar](max) NOT NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBKurir] PRIMARY KEY CLUSTERED 
(
	[IDKurir] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBKurirBiaya]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBKurirBiaya](
	[IDKurirBiaya] [int] IDENTITY(1,1) NOT NULL,
	[IDKurir] [int] NOT NULL,
	[IDWilayah] [int] NOT NULL,
	[Biaya] [money] NOT NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBKurirBiaya] PRIMARY KEY CLUSTERED 
(
	[IDKurirBiaya] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLogPengguna]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLogPengguna](
	[IDLogPengguna] [int] IDENTITY(1,1) NOT NULL,
	[IDLogPenggunaTipe] [int] NULL,
	[IDPengguna] [int] NULL,
	[Tanggal] [datetime] NULL,
 CONSTRAINT [PK_TBLogPengguna] PRIMARY KEY CLUSTERED 
(
	[IDLogPengguna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLogPenggunaTipe]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBLogPenggunaTipe](
	[IDLogPenggunaTipe] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBLogPenggunaTipe] PRIMARY KEY CLUSTERED 
(
	[IDLogPenggunaTipe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBMeja]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBMeja](
	[IDMeja] [int] IDENTITY(1,1) NOT NULL,
	[IDStatusMeja] [int] NULL,
	[Nama] [varchar](250) NULL,
	[JumlahKursi] [int] NULL,
	[VIP] [bit] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_TBMeja] PRIMARY KEY CLUSTERED 
(
	[IDMeja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBMenu]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBMenu](
	[IDMenu] [int] IDENTITY(1,1) NOT NULL,
	[IDMenuParent] [int] NULL,
	[IDMenuGrup] [int] NULL,
	[Urutan] [int] NULL,
	[Icon] [varchar](250) NULL,
	[Nama] [varchar](250) NULL,
	[Url] [text] NULL,
 CONSTRAINT [PK_TBMenu] PRIMARY KEY CLUSTERED 
(
	[IDMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBMenubar]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBMenubar](
	[IDMenubar] [int] IDENTITY(1,1) NOT NULL,
	[IDMenubarParent] [int] NULL,
	[EnumMenubarModul] [int] NOT NULL,
	[LevelMenu] [int] NOT NULL,
	[Urutan] [int] NOT NULL,
	[Kode] [varchar](250) NULL,
	[Nama] [varchar](250) NOT NULL,
	[Url] [varchar](max) NULL,
	[Icon] [varchar](250) NULL,
 CONSTRAINT [PK_TBMenubar] PRIMARY KEY CLUSTERED 
(
	[IDMenubar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBMenubarPenggunaGrup]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBMenubarPenggunaGrup](
	[IDMenubar] [int] NOT NULL,
	[IDGrupPengguna] [int] NOT NULL,
 CONSTRAINT [PK_TBMenubarPenggunaGrup] PRIMARY KEY CLUSTERED 
(
	[IDMenubar] ASC,
	[IDGrupPengguna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBMenuGrup]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBMenuGrup](
	[IDMenuGrup] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBMenuGrup] PRIMARY KEY CLUSTERED 
(
	[IDMenuGrup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBNotifikasi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBNotifikasi](
	[IDNotifikasi] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IDPenggunaPengirim] [int] NOT NULL,
	[IDPenggunaPenerima] [int] NOT NULL,
	[TanggalDaftar] [datetime] NOT NULL,
	[TanggalDibaca] [datetime] NULL,
	[EnumAlert] [int] NOT NULL,
	[Isi] [text] NOT NULL,
 CONSTRAINT [PK_TBNotifikasi] PRIMARY KEY CLUSTERED 
(
	[IDNotifikasi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBPelanggan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPelanggan](
	[IDPelanggan] [int] IDENTITY(1,1) NOT NULL,
	[IDGrupPelanggan] [int] NOT NULL,
	[IDPenggunaPIC] [int] NOT NULL,
	[NamaLengkap] [varchar](250) NULL,
	[Username] [varchar](250) NULL,
	[Password] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[Handphone] [varchar](250) NULL,
	[TeleponLain] [varchar](250) NULL,
	[TanggalLahir] [date] NULL,
	[TanggalDaftar] [datetime] NULL,
	[Deposit] [money] NULL,
	[Catatan] [text] NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBPelanggan] PRIMARY KEY CLUSTERED 
(
	[IDPelanggan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPemilikProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPemilikProduk](
	[IDPemilikProduk] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
	[Alamat] [varchar](max) NULL,
	[Email] [varchar](250) NULL,
	[Telepon1] [varchar](250) NULL,
	[Telepon2] [varchar](250) NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBPemilikProduk] PRIMARY KEY CLUSTERED 
(
	[IDPemilikProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPenerimaanPOProduksiBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPenerimaanPOProduksiBahanBaku](
	[IDPenerimaanPOProduksiBahanBaku] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPOProduksiBahanBaku] [varchar](30) NULL,
	[IDPenggunaDatang] [int] NOT NULL,
	[IDPenggunaTerima] [int] NULL,
	[IDPOProduksiBahanBakuPenagihan] [varchar](30) NULL,
	[TanggalDatang] [datetime] NOT NULL,
	[TanggalTerima] [datetime] NULL,
	[TotalDatang] [decimal](18, 2) NULL,
	[TotalDiterima] [decimal](18, 2) NULL,
	[TotalTolakKeSupplier] [decimal](18, 2) NULL,
	[TotalTolakKeGudang] [decimal](18, 2) NULL,
	[TotalSisa] [decimal](18, 2) NULL,
	[SubtotalBiayaTambahan] [money] NULL,
	[SubtotalTotalHPP] [money] NULL,
	[SubtotalTotalHargaSupplier] [money] NULL,
	[Grandtotal] [money] NULL,
	[EnumStatusPenerimaan] [int] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPenerimaanPOProduksiBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDPenerimaanPOProduksiBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPenerimaanPOProduksiBahanBakuDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPenerimaanPOProduksiBahanBakuDetail](
	[IDPenerimaanPOProduksiBahanBakuDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDPenerimaanPOProduksiBahanBaku] [varchar](30) NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
	[IDSatuan] [int] NOT NULL,
	[HargaPokokKomposisi] [money] NOT NULL,
	[BiayaTambahan] [money] NOT NULL,
	[TotalHPP] [money] NOT NULL,
	[HargaSupplier] [money] NOT NULL,
	[PotonganHargaSupplier] [money] NOT NULL,
	[TotalHargaSupplier] [money] NOT NULL,
	[Datang] [decimal](18, 2) NOT NULL,
	[Diterima] [decimal](18, 2) NOT NULL,
	[TolakKeSupplier] [decimal](18, 2) NOT NULL,
	[Sisa] [decimal](18, 2) NOT NULL,
	[SubtotalHPP]  AS ([TotalHPP]*[Diterima]),
	[SubtotalHargaSupplier]  AS ([TotalHargaSupplier]*[Diterima]),
 CONSTRAINT [PK_TBPenerimaanPOProduksiBahanBakuDetail] PRIMARY KEY CLUSTERED 
(
	[IDPenerimaanPOProduksiBahanBakuDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPenerimaanPOProduksiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPenerimaanPOProduksiProduk](
	[IDPenerimaanPOProduksiProduk] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPOProduksiProduk] [varchar](30) NULL,
	[IDPenggunaDatang] [int] NOT NULL,
	[IDPenggunaTerima] [int] NULL,
	[IDPOProduksiProdukPenagihan] [varchar](30) NULL,
	[TanggalDatang] [datetime] NOT NULL,
	[TanggalTerima] [datetime] NULL,
	[TotalDatang] [int] NULL,
	[TotalDiterima] [int] NULL,
	[TotalTolakKeVendor] [int] NULL,
	[TotalTolakKeGudang] [int] NULL,
	[TotalSisa] [int] NULL,
	[SubtotalBiayaTambahan] [money] NULL,
	[SubtotalTotalHPP] [money] NULL,
	[SubtotalTotalHargaVendor] [money] NULL,
	[Grandtotal] [money] NULL,
	[EnumStatusPenerimaan] [int] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPenerimaanPOProduksiProduk] PRIMARY KEY CLUSTERED 
(
	[IDPenerimaanPOProduksiProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPenerimaanPOProduksiProdukDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPenerimaanPOProduksiProdukDetail](
	[IDPenerimaanPOProduksiProdukDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDPenerimaanPOProduksiProduk] [varchar](30) NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[HargaPokokKomposisi] [money] NOT NULL,
	[BiayaTambahan] [money] NOT NULL,
	[TotalHPP] [money] NOT NULL,
	[HargaVendor] [money] NOT NULL,
	[PotonganHargaVendor] [money] NOT NULL,
	[TotalHargaVendor] [money] NOT NULL,
	[Datang] [int] NOT NULL,
	[Diterima] [int] NOT NULL,
	[TolakKeVendor] [int] NOT NULL,
	[TolakKeGudang] [int] NOT NULL,
	[Sisa] [int] NOT NULL,
	[SubtotalHPP]  AS ([TotalHPP]*[Diterima]),
	[SubtotalHargaVendor]  AS ([TotalHargaVendor]*[Diterima]),
 CONSTRAINT [PK_TBPenerimaanPOProduksiProdukDetail] PRIMARY KEY CLUSTERED 
(
	[IDPenerimaanPOProduksiProdukDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPengguna]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPengguna](
	[IDPengguna] [int] IDENTITY(1,1) NOT NULL,
	[IDPenggunaParent] [int] NULL,
	[IDGrupPengguna] [int] NOT NULL,
	[IDTempat] [int] NOT NULL,
	[NomorIdentitas] [varchar](250) NULL,
	[NomorNPWP] [varchar](250) NULL,
	[NamaLengkap] [varchar](250) NULL,
	[NomorRekening] [varchar](250) NULL,
	[NamaRekening] [varchar](250) NULL,
	[NamaBank] [varchar](250) NULL,
	[TempatLahir] [varchar](250) NULL,
	[TanggalLahir] [date] NOT NULL,
	[JenisKelamin] [bit] NULL,
	[Alamat] [varchar](250) NULL,
	[Agama] [varchar](250) NULL,
	[Telepon] [varchar](250) NULL,
	[Handphone] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[StatusPerkawinan] [varchar](250) NULL,
	[Kewarganegaraan] [varchar](250) NULL,
	[PendidikanTerakhir] [varchar](250) NULL,
	[TanggalBekerja] [date] NOT NULL,
	[Username] [varchar](250) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[PIN] [varchar](250) NOT NULL,
	[SidikJari] [varchar](4000) NULL,
	[RFID] [varchar](250) NULL,
	[EkstensiFoto] [varchar](250) NULL,
	[GajiPokok] [money] NULL,
	[TunjanganTransportasi] [money] NULL,
	[TunjanganMakan] [money] NULL,
	[TunjanganHariRaya] [money] NULL,
	[JaminanKecelakaan] [money] NULL,
	[JaminanHariTua] [money] NULL,
	[PPH21] [money] NULL,
	[Catatan] [varchar](max) NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBPengguna] PRIMARY KEY CLUSTERED 
(
	[IDPengguna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPenggunaJadwal]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBPenggunaJadwal](
	[IDJamKerja] [int] IDENTITY(1,1) NOT NULL,
	[IDPengguna] [int] NULL,
	[NamaHari] [nvarchar](50) NULL,
	[JadwalJamMasuk] [datetime] NULL,
	[JadwalJamKeluar] [datetime] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPenggunaJadwal] PRIMARY KEY CLUSTERED 
(
	[IDJamKerja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBPenggunaLogKehadiran]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBPenggunaLogKehadiran](
	[IDPenggunaLogKehadiran] [int] IDENTITY(1,1) NOT NULL,
	[IDPengguna] [int] NULL,
	[JadwalJamMasuk] [datetime] NULL,
	[JamMasuk] [datetime] NULL,
	[JadwalJamKeluar] [datetime] NULL,
	[JamKeluar] [datetime] NULL,
	[TotalJamKerja]  AS ([JamKeluar]-[JamMasuk]),
	[TotalJamLembur]  AS ([JamKeluar]-[JadwalJamKeluar]),
	[TotalJamKeterlambatan]  AS ([JamMasuk]-[JadwalJamMasuk]),
	[Keterangan] [text] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_TBPenggunaLogKehadiran] PRIMARY KEY CLUSTERED 
(
	[IDPenggunaLogKehadiran] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBPengirimanEmail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPengirimanEmail](
	[IDPengirimanEmail] [int] IDENTITY(1,1) NOT NULL,
	[TanggalKirim] [date] NOT NULL,
	[Tujuan] [varchar](250) NOT NULL,
	[Judul] [varchar](250) NOT NULL,
	[Isi] [text] NOT NULL,
 CONSTRAINT [PK_TBPengirimanEmail] PRIMARY KEY CLUSTERED 
(
	[IDPengirimanEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPengirimanPesan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPengirimanPesan](
	[IDPengirimanPesan] [int] IDENTITY(1,1) NOT NULL,
	[TanggalKirim] [date] NOT NULL,
	[Tujuan] [varchar](250) NOT NULL,
	[Isi] [text] NOT NULL,
 CONSTRAINT [PK_TBPengirimanSMS] PRIMARY KEY CLUSTERED 
(
	[IDPengirimanPesan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPengirimanPOProduksiBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPengirimanPOProduksiBahanBaku](
	[IDPengirimanPOProduksiBahanBaku] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPOProduksiBahanBaku] [varchar](30) NULL,
	[IDPengguna] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[Grandtotal] [money] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPengirimanPOProduksiBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDPengirimanPOProduksiBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPengirimanPOProduksiBahanBakuDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPengirimanPOProduksiBahanBakuDetail](
	[IDPengirimanPOProduksiBahanBakuDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDPengirimanPOProduksiBahanBaku] [varchar](30) NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
	[IDSatuan] [int] NOT NULL,
	[HargaBeli] [money] NOT NULL,
	[Kirim] [decimal](18, 2) NOT NULL,
	[SubtotalKirim]  AS ([HargaBeli]*[Kirim]),
 CONSTRAINT [PK_TBPengirimanPOProduksiBahanBakuDetail] PRIMARY KEY CLUSTERED 
(
	[IDPengirimanPOProduksiBahanBakuDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPengirimanPOProduksiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPengirimanPOProduksiProduk](
	[IDPengirimanPOProduksiProduk] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPOProduksiProduk] [varchar](30) NULL,
	[IDPengguna] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[Grandtotal] [money] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPengirimanPOProduksiProduk] PRIMARY KEY CLUSTERED 
(
	[IDPengirimanPOProduksiProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPengirimanPOProduksiProdukDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPengirimanPOProduksiProdukDetail](
	[IDPengirimanPOProduksiProdukDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDPengirimanPOProduksiProduk] [varchar](30) NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
	[IDSatuan] [int] NOT NULL,
	[HargaBeli] [money] NOT NULL,
	[Kirim] [decimal](18, 2) NOT NULL,
	[SubtotalKirim]  AS ([HargaBeli]*[Kirim]),
 CONSTRAINT [PK_TBPengirimanPOProduksiProdukDetail] PRIMARY KEY CLUSTERED 
(
	[IDPengirimanPOProduksiProdukDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPerpindahanStokBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBPerpindahanStokBahanBaku](
	[IDPerpindahanStokBahanBaku] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] NOT NULL,
	[IDJenisPerpindahanStok] [int] NOT NULL,
	[IDStokBahanBaku] [int] NOT NULL,
	[IDTempat] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDSatuan] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[HargaBeli] [money] NOT NULL,
	[Jumlah] [decimal](18, 2) NOT NULL,
	[Keterangan] [text] NOT NULL,
 CONSTRAINT [PK_TBPerpindahanStokBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDPerpindahanStokBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBPerpindahanStokProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBPerpindahanStokProduk](
	[IDPerpindahanStokProduk] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] NOT NULL,
	[IDJenisPerpindahanStok] [int] NOT NULL,
	[IDStokProduk] [int] NOT NULL,
	[IDTempat] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[Jumlah] [int] NOT NULL,
	[Keterangan] [text] NOT NULL,
 CONSTRAINT [PK_TBPerpindahanStok] PRIMARY KEY CLUSTERED 
(
	[IDPerpindahanStokProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBPesanPrint]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBPesanPrint](
	[IDPesanPrint] [int] IDENTITY(1,1) NOT NULL,
	[IDKonfigurasiPrinter] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[Isi] [text] NOT NULL,
 CONSTRAINT [PK_TBPesanPrint] PRIMARY KEY CLUSTERED 
(
	[IDPesanPrint] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBPOProduksiBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiBahanBaku](
	[IDPOProduksiBahanBaku] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDProyeksi] [varchar](30) NULL,
	[IDSupplier] [int] NULL,
	[IDTempat] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDPenggunaPIC] [int] NULL,
	[IDPenggunaDP] [int] NULL,
	[IDJenisPOProduksi] [int] NULL,
	[IDPOProduksiBahanBakuPenagihan] [varchar](30) NULL,
	[IDJenisPembayaran] [int] NULL,
	[EnumJenisProduksi] [int] NOT NULL,
	[LevelProduksi] [int] NULL,
	[Tanggal] [datetime] NOT NULL,
	[TanggalDownPayment] [datetime] NULL,
	[TanggalJatuhTempo] [datetime] NULL,
	[TanggalPengiriman] [datetime] NULL,
	[TotalJumlah] [decimal](18, 2) NULL,
	[EnumJenisHPP] [int] NULL,
	[SubtotalBiayaTambahan] [money] NULL,
	[SubtotalTotalHPP] [money] NULL,
	[SubtotalTotalHargaSupplier] [money] NULL,
	[PotonganPOProduksiBahanBaku] [money] NULL,
	[BiayaLainLain] [money] NULL,
	[PersentaseTax] [decimal](18, 2) NULL,
	[Tax] [money] NULL,
	[Grandtotal] [money] NULL,
	[DownPayment] [money] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPOProduksiBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiBahanBakuBiayaTambahan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiBahanBakuBiayaTambahan](
	[IDPOProduksiBahanBaku] [varchar](30) NOT NULL,
	[IDJenisBiayaProduksi] [int] NOT NULL,
	[Nominal] [money] NOT NULL,
 CONSTRAINT [PK_TBPOProduksiBahanBakuBiayaTambahan] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiBahanBaku] ASC,
	[IDJenisBiayaProduksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiBahanBakuDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiBahanBakuDetail](
	[IDPOProduksiBahanBakuDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDPOProduksiBahanBaku] [varchar](30) NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
	[IDSatuan] [int] NOT NULL,
	[HargaPokokKomposisi] [money] NOT NULL,
	[BiayaTambahan] [money] NOT NULL,
	[TotalHPP] [money] NOT NULL,
	[HargaSupplier] [money] NOT NULL,
	[PotonganHargaSupplier] [money] NOT NULL,
	[TotalHargaSupplier] [money] NOT NULL,
	[Jumlah] [decimal](18, 2) NOT NULL,
	[SubtotalHPP]  AS ([TotalHPP]*[Jumlah]),
	[SubtotalHargaSupplier]  AS ([TotalHargaSupplier]*[Jumlah]),
	[Sisa] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TBPOProduksiBahanBakuDetail] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiBahanBakuDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiBahanBakuKomposisi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiBahanBakuKomposisi](
	[IDPOProduksiBahanBaku] [varchar](30) NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
	[IDSatuan] [int] NOT NULL,
	[HargaBeli] [money] NOT NULL,
	[Kebutuhan] [decimal](18, 2) NOT NULL,
	[Subtotal]  AS ([HargaBeli]*[Kebutuhan]),
	[Kirim] [decimal](18, 2) NOT NULL,
	[SubtotalKirim]  AS ([HargaBeli]*[Kirim]),
	[Sisa] [decimal](18, 2) NOT NULL,
	[SubtotalSisa]  AS ([HargaBeli]*[Sisa]),
 CONSTRAINT [PK_TBPOProduksiBahanBakuKomposisi] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiBahanBaku] ASC,
	[IDBahanBaku] ASC,
	[IDSatuan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiBahanBakuPenagihan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiBahanBakuPenagihan](
	[IDPOProduksiBahanBakuPenagihan] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDSupplier] [int] NOT NULL,
	[IDTempat] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[TotalPenerimaan] [money] NULL,
	[TotalRetur] [money] NULL,
	[TotalDownPayment] [money] NULL,
	[TotalTagihan]  AS (([TotalPenerimaan]-[TotalRetur])-[TotalDownPayment]),
	[TotalBayar] [money] NULL,
	[StatusPembayaran] [bit] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPOProduksiBahanBakuPenagihan] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiBahanBakuPenagihan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiBahanBakuPenagihanDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiBahanBakuPenagihanDetail](
	[IDPOProduksiBahanBakuPenagihanDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDPOProduksiBahanBakuPenagihan] [varchar](30) NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDJenisPembayaran] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[Bayar] [money] NOT NULL,
 CONSTRAINT [PK_TBPOProduksiBahanBakuPenagihanDetail] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiBahanBakuPenagihanDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiBahanBakuRetur]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiBahanBakuRetur](
	[IDPOProduksiBahanBakuRetur] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPenerimaanPOProduksiBahanBaku] [varchar](30) NULL,
	[IDPOProduksiBahanBakuPenagihan] [varchar](30) NULL,
	[IDTempat] [int] NULL,
	[IDSupplier] [int] NULL,
	[IDPengguna] [int] NULL,
	[TanggalRetur] [datetime] NULL,
	[Grandtotal] [money] NULL,
	[EnumJenisRetur] [int] NULL,
	[EnumStatusRetur] [int] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPOProduksiBahanBakuRetur] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiBahanBakuRetur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiBahanBakuReturDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiBahanBakuReturDetail](
	[IDPOProduksiBahanBakuReturDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDPOProduksiBahanBakuRetur] [varchar](30) NULL,
	[IDStokBahanBaku] [int] NULL,
	[IDSatuan] [int] NULL,
	[HargaBeli] [money] NULL,
	[HargaRetur] [money] NULL,
	[Jumlah] [decimal](18, 4) NULL,
	[Subtotal]  AS ([HargaBeli]*[Jumlah]),
 CONSTRAINT [PK_TBPOProduksiBahanBakuReturDetail] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiBahanBakuReturDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiProduk](
	[IDPOProduksiProduk] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDProyeksi] [varchar](30) NULL,
	[IDVendor] [int] NULL,
	[IDTempat] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDPenggunaPIC] [int] NULL,
	[IDPenggunaDP] [int] NULL,
	[IDJenisPOProduksi] [int] NULL,
	[IDPOProduksiProdukPenagihan] [varchar](30) NULL,
	[IDJenisPembayaran] [int] NULL,
	[EnumJenisProduksi] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[TanggalDownPayment] [datetime] NULL,
	[TanggalJatuhTempo] [datetime] NULL,
	[TanggalPengiriman] [datetime] NULL,
	[TotalJumlah] [int] NULL,
	[EnumJenisHPP] [int] NULL,
	[SubtotalBiayaTambahan] [money] NULL,
	[SubtotalTotalHPP] [money] NULL,
	[SubtotalTotalHargaVendor] [money] NULL,
	[PotonganPOProduksiProduk] [money] NULL,
	[BiayaLainLain] [money] NULL,
	[PersentaseTax] [decimal](18, 2) NULL,
	[Tax] [money] NULL,
	[Grandtotal] [money] NULL,
	[DownPayment] [money] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPOProduksiProduk] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiProdukBiayaTambahan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiProdukBiayaTambahan](
	[IDPOProduksiProduk] [varchar](30) NOT NULL,
	[IDJenisBiayaProduksi] [int] NOT NULL,
	[Nominal] [money] NOT NULL,
 CONSTRAINT [PK_TBPOProduksiProdukBiayaTambahan] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiProduk] ASC,
	[IDJenisBiayaProduksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiProdukDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiProdukDetail](
	[IDPOProduksiProdukDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDPOProduksiProduk] [varchar](30) NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[HargaPokokKomposisi] [money] NOT NULL,
	[BiayaTambahan] [money] NOT NULL,
	[TotalHPP] [money] NOT NULL,
	[HargaVendor] [money] NOT NULL,
	[PotonganHargaVendor] [money] NOT NULL,
	[TotalHargaVendor] [money] NOT NULL,
	[Jumlah] [int] NOT NULL,
	[SubtotalHPP]  AS ([TotalHPP]*[Jumlah]),
	[SubtotalHargaVendor]  AS ([TotalHargaVendor]*[Jumlah]),
	[Sisa] [int] NOT NULL,
 CONSTRAINT [PK_TBPOProduksiProdukDetail] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiProdukDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiProdukKomposisi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiProdukKomposisi](
	[IDPOProduksiProduk] [varchar](30) NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
	[IDSatuan] [int] NOT NULL,
	[HargaBeli] [money] NOT NULL,
	[Kebutuhan] [decimal](18, 2) NOT NULL,
	[Subtotal]  AS ([HargaBeli]*[Kebutuhan]),
	[Kirim] [decimal](18, 2) NOT NULL,
	[SubtotalKirim]  AS ([HargaBeli]*[Kirim]),
	[Sisa] [decimal](18, 2) NOT NULL,
	[SubtotalSisa]  AS ([HargaBeli]*[Sisa]),
 CONSTRAINT [PK_TPOProduksiProdukKomposisi] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiProduk] ASC,
	[IDBahanBaku] ASC,
	[IDSatuan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiProdukPenagihan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiProdukPenagihan](
	[IDPOProduksiProdukPenagihan] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDVendor] [int] NOT NULL,
	[IDTempat] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[TotalPenerimaan] [money] NULL,
	[TotalRetur] [money] NULL,
	[TotalDownPayment] [money] NULL,
	[TotalTagihan]  AS (([TotalPenerimaan]-[TotalRetur])-[TotalDownPayment]),
	[TotalBayar] [money] NULL,
	[StatusPembayaran] [bit] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPOProduksiProdukPenagihan] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiProdukPenagihan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiProdukPenagihanDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiProdukPenagihanDetail](
	[IDPOProduksiProdukPenagihanDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDPOProduksiProdukPenagihan] [varchar](30) NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDJenisPembayaran] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[Bayar] [money] NOT NULL,
 CONSTRAINT [PK_TBPOProduksiProdukPenagihanDetail] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiProdukPenagihanDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiProdukRetur]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiProdukRetur](
	[IDPOProduksiProdukRetur] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPenerimaanPOProduksiProduk] [varchar](30) NULL,
	[IDPOProduksiProdukPenagihan] [varchar](30) NULL,
	[IDTempat] [int] NULL,
	[IDVendor] [int] NULL,
	[IDPengguna] [int] NULL,
	[TanggalRetur] [datetime] NULL,
	[Grandtotal] [money] NULL,
	[EnumJenisRetur] [int] NULL,
	[EnumStatusRetur] [int] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBPOProduksiProdukRetur] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiProdukRetur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPOProduksiProdukReturDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBPOProduksiProdukReturDetail](
	[IDPOProduksiProdukReturDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDPOProduksiProdukRetur] [varchar](30) NULL,
	[IDStokProduk] [int] NULL,
	[HargaBeli] [money] NULL,
	[HargaRetur] [money] NULL,
	[HargaJual] [money] NULL,
	[Jumlah] [int] NULL,
	[Subtotal]  AS ([HargaBeli]*[Jumlah]),
 CONSTRAINT [PK_TBPOProduksiProdukReturDetail] PRIMARY KEY CLUSTERED 
(
	[IDPOProduksiProdukReturDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBPrintBarcode]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBPrintBarcode](
	[IDStokProduk] [int] NOT NULL,
	[Jumlah] [int] NOT NULL,
 CONSTRAINT [PK_TBPrintBarcode] PRIMARY KEY CLUSTERED 
(
	[IDStokProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBProduk](
	[IDProduk] [int] IDENTITY(1,1) NOT NULL,
	[IDWarna] [int] NOT NULL,
	[IDPemilikProduk] [int] NOT NULL,
	[IDProdukKategori] [int] NOT NULL,
	[KodeProduk] [varchar](250) NULL,
	[Nama] [varchar](250) NULL,
	[Deskripsi] [varchar](max) NULL,
	[DeskripsiSingkat] [varchar](300) NULL,
	[Dilihat] [int] NOT NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBProduk] PRIMARY KEY CLUSTERED 
(
	[IDProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBProdukKategori]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBProdukKategori](
	[IDProdukKategori] [int] IDENTITY(1,1) NOT NULL,
	[IDProdukKategoriParent] [int] NULL,
	[Nama] [varchar](250) NOT NULL,
	[Deskripsi] [varchar](max) NOT NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBProdukKategori] PRIMARY KEY CLUSTERED 
(
	[IDProdukKategori] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBProject]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBProject](
	[IDProject] [varchar](50) NOT NULL,
	[IDTransaksi] [varchar](30) NULL,
	[IDWMS] [uniqueidentifier] NULL,
	[IDTempat] [int] NOT NULL,
	[IDPelanggan] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDPenggunaUpdate] [int] NULL,
	[Nama] [varchar](250) NULL,
	[Tanggal] [datetime] NOT NULL,
	[TanggalUpdate] [datetime] NULL,
	[Progress] [int] NULL,
	[Status] [bit] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBProject] PRIMARY KEY CLUSTERED 
(
	[IDProject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBProyeksi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBProyeksi](
	[IDProyeksi] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPengguna] [int] NULL,
	[IDTempat] [int] NOT NULL,
	[IDPemilikProduk] [int] NULL,
	[TanggalProyeksi] [datetime] NOT NULL,
	[TanggalTarget] [datetime] NULL,
	[TanggalSelesai] [datetime] NULL,
	[TotalJumlah] [int] NULL,
	[GrandTotalHargaJual] [money] NULL,
	[EnumStatusProyeksi] [int] NULL,
	[UrutanProduksi] [int] NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBProyeksi] PRIMARY KEY CLUSTERED 
(
	[IDProyeksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBProyeksiDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBProyeksiDetail](
	[IDProyeksiDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDProyeksi] [varchar](30) NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[HargaJual] [money] NOT NULL,
	[Jumlah] [int] NOT NULL,
	[Sisa] [int] NOT NULL,
	[Subtotal]  AS ([HargaJual]*[Jumlah]),
 CONSTRAINT [PK_TBProyeksiDetail] PRIMARY KEY CLUSTERED 
(
	[IDProyeksiDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBProyeksiKomposisi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBProyeksiKomposisi](
	[IDProyeksi] [varchar](30) NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
	[IDSatuan] [int] NOT NULL,
	[LevelProduksi] [int] NOT NULL,
	[BahanBakuDasar] [bit] NOT NULL,
	[Jumlah] [decimal](18, 2) NOT NULL,
	[Stok] [decimal](18, 2) NOT NULL,
	[Kurang] [decimal](18, 2) NOT NULL,
	[Sisa] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TProyeksiKomposisi] PRIMARY KEY CLUSTERED 
(
	[IDProyeksi] ASC,
	[IDBahanBaku] ASC,
	[IDSatuan] ASC,
	[LevelProduksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBQuotation]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBQuotation](
	[IDQuotation] [varchar](50) NOT NULL,
	[IDProject] [varchar](50) NULL,
	[Tanggal] [datetime] NULL,
	[JumlahProduk] [int] NULL,
	[GrandTotal] [money] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_TBQuotation] PRIMARY KEY CLUSTERED 
(
	[IDQuotation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBQuotationDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBQuotationDetail](
	[IDQuotationDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDQuotation] [varchar](50) NULL,
	[IDKombinasiProduk] [int] NULL,
	[HargaJual] [money] NULL,
	[Jumlah] [int] NULL,
	[Subtotal]  AS ([HargaJual]*[Jumlah]),
	[Keterangan1] [text] NULL,
	[Keterangan2] [text] NULL,
	[Keterangan3] [text] NULL,
	[Keterangan4] [text] NULL,
 CONSTRAINT [PK_TBQuotationDetail] PRIMARY KEY CLUSTERED 
(
	[IDQuotationDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBRekomendasiKategoriProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBRekomendasiKategoriProduk](
	[IDKategoriProduk1] [int] NOT NULL,
	[IDKategoriProduk2] [int] NOT NULL,
	[Jumlah] [int] NOT NULL,
	[Nilai] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TBRekomendasiKategoriProduk] PRIMARY KEY CLUSTERED 
(
	[IDKategoriProduk1] ASC,
	[IDKategoriProduk2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBRekomendasiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBRekomendasiProduk](
	[IDProduk1] [int] NOT NULL,
	[IDProduk2] [int] NOT NULL,
	[Jumlah] [int] NOT NULL,
	[Nilai] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TBRekomendasiProduk] PRIMARY KEY CLUSTERED 
(
	[IDProduk1] ASC,
	[IDProduk2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBRelasiBahanBakuKategoriBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBRelasiBahanBakuKategoriBahanBaku](
	[IDBahanBaku] [int] NOT NULL,
	[IDKategoriBahanBaku] [int] NOT NULL,
 CONSTRAINT [PK_TBRelasiBahanBakuKategoriBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDBahanBaku] ASC,
	[IDKategoriBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBRelasiJenisBiayaProduksiBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBRelasiJenisBiayaProduksiBahanBaku](
	[IDJenisBiayaProduksi] [int] NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
	[EnumBiayaProduksi] [int] NULL,
	[Persentase] [decimal](18, 2) NULL,
	[Nominal] [money] NULL,
 CONSTRAINT [PK_TBRelasiJenisBiayaProduksiBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDJenisBiayaProduksi] ASC,
	[IDBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBRelasiJenisBiayaProduksiKombinasiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBRelasiJenisBiayaProduksiKombinasiProduk](
	[IDJenisBiayaProduksi] [int] NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[EnumBiayaProduksi] [int] NULL,
	[Persentase] [decimal](18, 2) NULL,
	[Nominal] [money] NULL,
 CONSTRAINT [PK_TBRelasiJenisBiayaProduksiKombinasiProduk] PRIMARY KEY CLUSTERED 
(
	[IDJenisBiayaProduksi] ASC,
	[IDKombinasiProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBRelasiPenggunaMenu]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBRelasiPenggunaMenu](
	[IDPengguna] [int] NOT NULL,
	[IDMenu] [int] NOT NULL,
 CONSTRAINT [PK_TBRelasiPenggunaMenu] PRIMARY KEY CLUSTERED 
(
	[IDPengguna] ASC,
	[IDMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBRelasiProdukKategoriProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBRelasiProdukKategoriProduk](
	[IDProduk] [int] NOT NULL,
	[IDKategoriProduk] [int] NOT NULL,
 CONSTRAINT [PK_TBRelasiProdukKategoriProduk] PRIMARY KEY CLUSTERED 
(
	[IDProduk] ASC,
	[IDKategoriProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBRestaurantOrder]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBRestaurantOrder](
	[NomorOrder] [varchar](30) NOT NULL,
	[IDMeja] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[Kategori] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[JenisMeja] [int] NOT NULL,
	[StatusOrder] [int] NOT NULL,
 CONSTRAINT [PK_TBRestaurantOrder] PRIMARY KEY CLUSTERED 
(
	[NomorOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBRestaurantOrderDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBRestaurantOrderDetail](
	[IDRestaurantOrderDetail] [varchar](30) NOT NULL,
	[NomorOrder] [varchar](30) NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Kategori] [varchar](250) NOT NULL,
	[Catatan] [text] NULL,
 CONSTRAINT [PK_TBRestaurantOrderDetail] PRIMARY KEY CLUSTERED 
(
	[IDRestaurantOrderDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBSatuan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBSatuan](
	[IDSatuan] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBSatuan] PRIMARY KEY CLUSTERED 
(
	[IDSatuan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBSoal]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBSoal](
	[IDSoal] [int] IDENTITY(1,1) NOT NULL,
	[IDTempat] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[Judul] [text] NOT NULL,
	[Keterangan] [text] NOT NULL,
	[TanggalPembuatan] [datetime] NOT NULL,
	[TanggalMulai] [datetime] NOT NULL,
	[TanggalSelesai] [datetime] NULL,
	[EnumStatusSoal] [int] NOT NULL,
 CONSTRAINT [PK_TBSoal] PRIMARY KEY CLUSTERED 
(
	[IDSoal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBSoalJawaban]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBSoalJawaban](
	[IDSoalJawaban] [int] IDENTITY(1,1) NOT NULL,
	[IDSoalPertanyaan] [int] NOT NULL,
	[Isi] [text] NOT NULL,
	[Bobot] [int] NOT NULL,
 CONSTRAINT [PK_TBSoalJawaban] PRIMARY KEY CLUSTERED 
(
	[IDSoalJawaban] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBSoalJawabanPelanggan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBSoalJawabanPelanggan](
	[IDSoalJawabanPelanggan] [int] IDENTITY(1,1) NOT NULL,
	[IDSoalJawaban] [int] NOT NULL,
	[IDPelanggan] [int] NOT NULL,
 CONSTRAINT [PK_TBSoalJawabanPelanggan] PRIMARY KEY CLUSTERED 
(
	[IDSoalJawabanPelanggan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBSoalPertanyaan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBSoalPertanyaan](
	[IDSoalPertanyaan] [int] IDENTITY(1,1) NOT NULL,
	[IDSoal] [int] NOT NULL,
	[Isi] [text] NOT NULL,
	[Nomor] [int] NULL,
 CONSTRAINT [PK_TBPertanyaan] PRIMARY KEY CLUSTERED 
(
	[IDSoalPertanyaan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBStatusMeja]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBStatusMeja](
	[IDStatusMeja] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBStatusMeja] PRIMARY KEY CLUSTERED 
(
	[IDStatusMeja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBStatusTransaksi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBStatusTransaksi](
	[IDStatusTransaksi] [int] IDENTITY(1,1) NOT NULL,
	[Urutan] [int] NULL,
	[Nama] [varchar](250) NULL,
 CONSTRAINT [PK_TBStatusTransaksi] PRIMARY KEY CLUSTERED 
(
	[IDStatusTransaksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBStokBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBStokBahanBaku](
	[IDStokBahanBaku] [int] IDENTITY(1,1) NOT NULL,
	[IDTempat] [int] NULL,
	[IDBahanBaku] [int] NULL,
	[HargaBeli] [money] NULL,
	[Jumlah] [decimal](18, 2) NULL,
	[JumlahMinimum] [decimal](18, 2) NULL,
 CONSTRAINT [PK_TBStokBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDStokBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBStokProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBStokProduk](
	[IDStokProduk] [int] IDENTITY(1,1) NOT NULL,
	[IDTempat] [int] NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[IDStokProdukPenyimpanan] [int] NULL,
	[HargaBeli] [money] NULL,
	[HargaJual] [money] NULL,
	[PersentaseKonsinyasi] [decimal](18, 2) NULL,
	[EnumDiscountStore] [int] NOT NULL,
	[DiscountStore] [money] NOT NULL,
	[EnumDiscountKonsinyasi] [int] NOT NULL,
	[DiscountKonsinyasi] [money] NOT NULL,
	[PajakPersentase] [decimal](18, 2) NOT NULL,
	[PajakNominal]  AS (([HargaBeli]*[PajakPersentase])/(100)),
	[Jumlah] [int] NULL,
	[JumlahMinimum] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_TBStok] PRIMARY KEY CLUSTERED 
(
	[IDStokProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBStokProdukMutasi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBStokProdukMutasi](
	[IDStokProdukMutasi] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IDStokProduk] [int] NOT NULL,
	[IDJenisStokMutasi] [int] NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
	[Debit] [decimal](18, 2) NOT NULL,
	[Kredit] [decimal](18, 2) NOT NULL,
	[HargaJual] [money] NOT NULL,
	[HargaBeli] [money] NOT NULL,
	[Keterangan] [text] NOT NULL,
 CONSTRAINT [PK_TBStokProdukMutasi] PRIMARY KEY CLUSTERED 
(
	[IDStokProdukMutasi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBStokProdukPenyimpanan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBStokProdukPenyimpanan](
	[IDStokProdukPenyimpanan] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[Keterangan] [text] NOT NULL,
 CONSTRAINT [PK_TBStokProdukPenyimpanan] PRIMARY KEY CLUSTERED 
(
	[IDStokProdukPenyimpanan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBStore]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBStore](
	[IDStore] [int] IDENTITY(1,1) NOT NULL,
	[IDWMS] [uniqueidentifier] NOT NULL,
	[Nama] [varchar](250) NULL,
	[Alamat] [text] NULL,
	[Email] [varchar](250) NULL,
	[KodePos] [varchar](250) NULL,
	[Handphone] [varchar](250) NULL,
	[TeleponLain] [varchar](250) NULL,
	[Website] [varchar](250) NULL,
	[SMTPServer] [varchar](250) NULL,
	[SMTPPort] [int] NULL,
	[SMTPUser] [varchar](250) NULL,
	[SMTPPassword] [varchar](250) NULL,
	[SecureSocketsLayer] [bit] NULL,
 CONSTRAINT [PK_TBStore] PRIMARY KEY CLUSTERED 
(
	[IDStore] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBStoreKey]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBStoreKey](
	[IDStoreKey] [uniqueidentifier] NOT NULL,
	[IDStore] [int] NOT NULL,
	[IDPenggunaAktif] [int] NULL,
	[TanggalKey] [date] NOT NULL,
	[TanggalAktif] [datetime] NULL,
	[IsAktif] [bit] NOT NULL,
 CONSTRAINT [PK_TBStoreKey] PRIMARY KEY CLUSTERED 
(
	[IDStoreKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBStoreKonfigurasi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBStoreKonfigurasi](
	[IDStoreKonfigurasi] [int] IDENTITY(1,1) NOT NULL,
	[IDStore] [int] NULL,
	[Nama] [varchar](250) NULL,
	[Pengaturan] [varchar](250) NULL,
 CONSTRAINT [PK_TBStoreKonfigurasi] PRIMARY KEY CLUSTERED 
(
	[IDStoreKonfigurasi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBSupplier]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBSupplier](
	[IDSupplier] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[Alamat] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[Telepon1] [varchar](250) NULL,
	[Telepon2] [varchar](250) NULL,
	[PersentaseTax] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TBSupplier] PRIMARY KEY CLUSTERED 
(
	[IDSupplier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBSync]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBSync](
	[IDSync] [int] IDENTITY(1,1) NOT NULL,
	[EnumStatus] [int] NOT NULL,
	[EnumJenis] [int] NOT NULL,
	[IDWMS] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TBSync] PRIMARY KEY CLUSTERED 
(
	[IDSync] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBSyncData]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBSyncData](
	[IDSyncData] [int] IDENTITY(1,1) NOT NULL,
	[IDWMSSyncData] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IDWMSStore] [uniqueidentifier] NOT NULL,
	[IDWMSTempat] [uniqueidentifier] NOT NULL,
	[IDWMSPengguna] [uniqueidentifier] NOT NULL,
	[TanggalUpload] [datetime] NOT NULL,
	[TanggalUploadFinish] [datetime] NULL,
	[TanggalSync] [datetime] NULL,
	[Data] [varchar](max) NOT NULL,
 CONSTRAINT [PK_TBSyncData] PRIMARY KEY CLUSTERED 
(
	[IDWMSSyncData] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBSyncDatabaseLog]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBSyncDatabaseLog](
	[IDSyncDatabaseLog] [bigint] IDENTITY(1,1) NOT NULL,
	[IDPengguna] [int] NULL,
	[TanggalMulai] [datetime] NULL,
	[TanggalSelesai] [datetime] NULL,
	[Pesan] [varchar](max) NULL,
 CONSTRAINT [PK_TBSyncDatabaseLog] PRIMARY KEY CLUSTERED 
(
	[IDSyncDatabaseLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTempat]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTempat](
	[IDTempat] [int] IDENTITY(1,1) NOT NULL,
	[IDStore] [int] NOT NULL,
	[IDKategoriTempat] [int] NOT NULL,
	[Kode] [varchar](250) NULL,
	[Nama] [varchar](250) NULL,
	[Alamat] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[Telepon1] [varchar](250) NULL,
	[Telepon2] [varchar](250) NULL,
	[EnumBiayaTambahan1] [int] NULL,
	[KeteranganBiayaTambahan1] [varchar](250) NULL,
	[BiayaTambahan1] [money] NULL,
	[EnumBiayaTambahan2] [int] NULL,
	[KeteranganBiayaTambahan2] [varchar](250) NULL,
	[BiayaTambahan2] [money] NULL,
	[EnumBiayaTambahan3] [int] NULL,
	[KeteranganBiayaTambahan3] [varchar](250) NULL,
	[BiayaTambahan3] [money] NULL,
	[EnumBiayaTambahan4] [int] NULL,
	[KeteranganBiayaTambahan4] [varchar](250) NULL,
	[BiayaTambahan4] [money] NULL,
	[Latitude] [varchar](250) NULL,
	[Longitude] [varchar](250) NULL,
	[FooterPrint] [varchar](max) NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBTempat] PRIMARY KEY CLUSTERED 
(
	[IDTempat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTempatJarak]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBTempatJarak](
	[IDTempatAwal] [int] NOT NULL,
	[IDTempatTujuan] [int] NOT NULL,
	[Jarak] [text] NOT NULL,
	[JarakNilai] [int] NOT NULL,
	[Durasi] [text] NOT NULL,
	[DurasiNilai] [int] NOT NULL,
 CONSTRAINT [PK_TBTempatJarak] PRIMARY KEY CLUSTERED 
(
	[IDTempatAwal] ASC,
	[IDTempatTujuan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBTemplateKeterangan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBTemplateKeterangan](
	[IDTemplateKeterangan] [int] IDENTITY(1,1) NOT NULL,
	[Isi] [text] NOT NULL,
 CONSTRAINT [PK_TBTemplateKeterangan] PRIMARY KEY CLUSTERED 
(
	[IDTemplateKeterangan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBTransaksi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransaksi](
	[IDTransaksi] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPenggunaTransaksi] [int] NULL,
	[IDPenggunaUpdate] [int] NULL,
	[IDPenggunaPembayaran] [int] NULL,
	[IDPenggunaMarketing] [int] NULL,
	[IDPenggunaBatal] [int] NULL,
	[IDTempat] [int] NULL,
	[IDTempatPengirim] [int] NULL,
	[IDPelanggan] [int] NOT NULL,
	[IDAlamat] [int] NULL,
	[IDKurir] [int] NULL,
	[IDJenisPembayaran] [int] NULL,
	[IDStatusTransaksi] [int] NULL,
	[IDJenisTransaksi] [int] NULL,
	[IDJenisBebanBiaya] [int] NULL,
	[IDMeja] [int] NULL,
	[TanggalOperasional] [date] NULL,
	[TanggalTransaksi] [datetime] NULL,
	[TanggalUpdate] [datetime] NULL,
	[TanggalBatal] [datetime] NULL,
	[TanggalPembayaran] [datetime] NULL,
	[KodePengiriman] [varchar](250) NULL,
	[JumlahTamu] [int] NULL,
	[JumlahProduk] [int] NULL,
	[BeratSubtotal] [decimal](18, 2) NOT NULL,
	[BeratPembulatan] [decimal](18, 2) NOT NULL,
	[BeratTotal]  AS ([BeratSubtotal]+[BeratPembulatan]),
	[EnumBiayaTambahan1] [int] NULL,
	[NilaiBiayaTambahan1] [money] NULL,
	[BiayaTambahan1] [money] NULL,
	[EnumBiayaTambahan2] [int] NULL,
	[NilaiBiayaTambahan2] [money] NULL,
	[BiayaTambahan2] [money] NULL,
	[EnumBiayaTambahan3] [int] NULL,
	[NilaiBiayaTambahan3] [money] NULL,
	[BiayaTambahan3] [money] NULL,
	[EnumBiayaTambahan4] [int] NULL,
	[NilaiBiayaTambahan4] [money] NULL,
	[BiayaTambahan4] [money] NULL,
	[EnumBiayaJenisPembayaran] [int] NULL,
	[PersentaseBiayaJenisPembayaran] [money] NULL,
	[BiayaJenisPembayaran] [money] NULL,
	[EnumPotonganTransaksi] [int] NULL,
	[PersentasePotonganTransaksi] [money] NULL,
	[PotonganTransaksi] [money] NULL,
	[TotalPotonganHargaJualDetail] [money] NULL,
	[TotalDiscountVoucher] [money] NULL,
	[BiayaPengiriman] [money] NULL,
	[Pembulatan] [money] NULL,
	[Subtotal] [money] NULL,
	[GrandTotal]  AS (((((((([Subtotal]+[BiayaPengiriman])+[BiayaTambahan1])+[BiayaTambahan2])+[BiayaTambahan3])+[BiayaTambahan4])-[PotonganTransaksi])-[TotalDiscountVoucher])+[Pembulatan]) PERSISTED,
	[TotalPembayaran] [money] NULL,
	[Keterangan] [text] NULL,
	[AlasanBatal] [varchar](max) NULL,
 CONSTRAINT [PK_TBTransaksi] PRIMARY KEY CLUSTERED 
(
	[IDTransaksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransaksiDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransaksiDetail](
	[IDDetailTransaksi] [int] IDENTITY(1,1) NOT NULL,
	[IDTransaksi] [varchar](30) NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[IDStokProduk] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Berat] [decimal](18, 2) NOT NULL,
	[HargaBeliKotor] [money] NOT NULL,
	[HargaBeli]  AS ([HargaBeliKotor]-[DiscountKonsinyasi]),
	[HargaJual] [money] NOT NULL,
	[HargaJualBersih]  AS ([HargaJual]-([DiscountStore]+[DiscountKonsinyasi])),
	[DiscountStore] [money] NOT NULL,
	[DiscountKonsinyasi] [money] NOT NULL,
	[Discount]  AS ([DiscountStore]+[DiscountKonsinyasi]),
	[Revenue]  AS (([HargaJual]-([DiscountStore]+[DiscountKonsinyasi]))-([HargaBeliKotor]-[DiscountKonsinyasi])),
	[PajakNominal] [money] NOT NULL,
	[TotalBerat]  AS ([Quantity]*[Berat]),
	[TotalHargaBeliKotor]  AS ([Quantity]*[HargaBeliKotor]),
	[TotalHargaBeli]  AS ([Quantity]*([HargaBeliKotor]-[DiscountKonsinyasi])),
	[TotalHargaJual]  AS ([Quantity]*[HargaJual]),
	[TotalDiscount]  AS ([Quantity]*([DiscountStore]+[DiscountKonsinyasi])),
	[TotalRevenue]  AS ([Quantity]*(([HargaJual]-([DiscountStore]+[DiscountKonsinyasi]))-([HargaBeliKotor]-[DiscountKonsinyasi]))),
	[TotalPajakNominal]  AS ([Quantity]*[PajakNominal]),
	[Subtotal]  AS ([Quantity]*([HargaJual]-([DiscountStore]+[DiscountKonsinyasi]))),
	[Keterangan] [text] NOT NULL,
 CONSTRAINT [PK_TBTransaksiDetail] PRIMARY KEY CLUSTERED 
(
	[IDDetailTransaksi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransaksiDetailTemp]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransaksiDetailTemp](
	[IDDetailTransaksiTemp] [int] IDENTITY(1,1) NOT NULL,
	[IDTransaksiTemp] [varchar](30) NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[IDStokProduk] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Berat] [decimal](18, 2) NOT NULL,
	[HargaBeliKotor] [money] NOT NULL,
	[HargaBeli]  AS ([HargaBeliKotor]-[DiscountKonsinyasi]),
	[HargaJual] [money] NOT NULL,
	[HargaJualBersih]  AS ([HargaJual]-([DiscountStore]+[DiscountKonsinyasi])),
	[DiscountStore] [money] NOT NULL,
	[DiscountKonsinyasi] [money] NOT NULL,
	[Discount]  AS ([DiscountStore]+[DiscountKonsinyasi]),
	[Revenue]  AS (([HargaJual]-([DiscountStore]+[DiscountKonsinyasi]))-([HargaBeliKotor]-[DiscountKonsinyasi])),
	[PajakNominal] [money] NOT NULL,
	[TotalBerat]  AS ([Quantity]*[Berat]),
	[TotalHargaBeliKotor]  AS ([Quantity]*[HargaBeliKotor]),
	[TotalHargaBeli]  AS ([Quantity]*([HargaBeliKotor]-[DiscountKonsinyasi])),
	[TotalHargaJual]  AS ([Quantity]*[HargaJual]),
	[TotalDiscount]  AS ([Quantity]*([DiscountStore]+[DiscountKonsinyasi])),
	[TotalRevenue]  AS ([Quantity]*(([HargaJual]-([DiscountStore]+[DiscountKonsinyasi]))-([HargaBeliKotor]-[DiscountKonsinyasi]))),
	[TotalPajakNominal]  AS ([Quantity]*[PajakNominal]),
	[Subtotal]  AS ([Quantity]*([HargaJual]-([DiscountStore]+[DiscountKonsinyasi]))),
	[Keterangan] [varchar](max) NOT NULL,
 CONSTRAINT [PK_TBTransaksiDetailTemp] PRIMARY KEY CLUSTERED 
(
	[IDDetailTransaksiTemp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransaksiECommerce]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBTransaksiECommerce](
	[IDTransaksiECommerce] [bigint] IDENTITY(1,1) NOT NULL,
	[IDPelanggan] [int] NOT NULL,
	[_IDWMSPelanggan] [uniqueidentifier] NOT NULL,
	[_TanggalInsert] [datetime] NOT NULL,
 CONSTRAINT [PK_TBTransaksiECommerce] PRIMARY KEY CLUSTERED 
(
	[IDTransaksiECommerce] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBTransaksiECommerceDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBTransaksiECommerceDetail](
	[IDTransaksiECommerceDetail] [bigint] IDENTITY(1,1) NOT NULL,
	[IDTransaksiECommerce] [bigint] NOT NULL,
	[IDStokProduk] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[_TanggalInsert] [datetime] NOT NULL,
 CONSTRAINT [PK_TBTransaksiECommerceDetail] PRIMARY KEY CLUSTERED 
(
	[IDTransaksiECommerceDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBTransaksiJenisPembayaran]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransaksiJenisPembayaran](
	[IDTransaksiJenisPembayaran] [int] IDENTITY(1,1) NOT NULL,
	[IDTransaksi] [varchar](30) NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDJenisPembayaran] [int] NOT NULL,
	[IDJenisBebanBiaya] [int] NOT NULL,
	[Tanggal] [datetime] NULL,
	[TanggalJatuhTempo] [datetime] NULL,
	[TanggalDaftar] [datetime] NULL,
	[EnumBiayaJenisPembayaran] [int] NULL,
	[PersentaseBiayaJenisPembayaran] [decimal](18, 2) NULL,
	[BiayaJenisPembayaran] [money] NULL,
	[Bayar] [money] NULL,
	[Total]  AS ([Bayar]+[BiayaJenisPembayaran]) PERSISTED,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBTransaksiJenisPembayaran] PRIMARY KEY CLUSTERED 
(
	[IDTransaksiJenisPembayaran] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransaksiPrint]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransaksiPrint](
	[IDTransaksiPrint] [int] IDENTITY(1,1) NOT NULL,
	[IDTransaksi] [varchar](30) NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[Station] [varchar](250) NULL,
	[Quantity] [int] NOT NULL,
	[Keterangan] [text] NULL,
	[EnumStatusPrint] [int] NOT NULL,
 CONSTRAINT [PK_TBTransaksiPrint] PRIMARY KEY CLUSTERED 
(
	[IDTransaksiPrint] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransaksiPrintECommerce]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransaksiPrintECommerce](
	[IDTransaksiPrintECommerce] [int] IDENTITY(1,1) NOT NULL,
	[IDTransaksi] [varchar](30) NOT NULL,
	[IDStatusTransaksi] [int] NOT NULL,
	[Keterangan] [varchar](max) NOT NULL,
 CONSTRAINT [PK_TBTransaksiPrintECommerce] PRIMARY KEY CLUSTERED 
(
	[IDTransaksiPrintECommerce] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransaksiPrintLog]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransaksiPrintLog](
	[IDTransaksiPrintLog] [int] IDENTITY(1,1) NOT NULL,
	[IDTransaksi] [varchar](30) NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDTempat] [int] NOT NULL,
	[EnumPrintLog] [int] NOT NULL,
	[IDWMS] [uniqueidentifier] NOT NULL,
	[Tanggal] [datetime] NOT NULL,
 CONSTRAINT [PK_TBTransaksiPrintLog] PRIMARY KEY CLUSTERED 
(
	[IDTransaksiPrintLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransaksiTemp]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransaksiTemp](
	[IDTransaksiTemp] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPenggunaTransaksi] [int] NOT NULL,
	[IDPenggunaUpdate] [int] NOT NULL,
	[IDPenggunaMarketing] [int] NOT NULL,
	[IDPenggunaBatal] [int] NULL,
	[IDTempat] [int] NOT NULL,
	[IDTempatPengirim] [int] NOT NULL,
	[IDPelanggan] [int] NOT NULL,
	[IDAlamat] [int] NOT NULL,
	[IDKurir] [int] NOT NULL,
	[IDStatusTransaksi] [int] NOT NULL,
	[IDJenisTransaksi] [int] NOT NULL,
	[IDMeja] [int] NOT NULL,
	[IDWMS] [uniqueidentifier] NOT NULL,
	[TanggalOperasional] [date] NOT NULL,
	[TanggalTransaksi] [datetime] NOT NULL,
	[TanggalUpdate] [datetime] NOT NULL,
	[TanggalBatal] [datetime] NULL,
	[KodePengiriman] [varchar](250) NOT NULL,
	[JumlahTamu] [int] NOT NULL,
	[JumlahProduk] [int] NOT NULL,
	[BeratSubtotal] [decimal](18, 2) NOT NULL,
	[BeratPembulatan] [decimal](18, 2) NOT NULL,
	[BeratTotal]  AS ([BeratSubtotal]+[BeratPembulatan]),
	[EnumBiayaTambahan1] [int] NOT NULL,
	[NilaiBiayaTambahan1] [money] NOT NULL,
	[BiayaTambahan1] [money] NOT NULL,
	[EnumBiayaTambahan2] [int] NOT NULL,
	[NilaiBiayaTambahan2] [money] NOT NULL,
	[BiayaTambahan2] [money] NOT NULL,
	[EnumBiayaTambahan3] [int] NOT NULL,
	[NilaiBiayaTambahan3] [money] NOT NULL,
	[BiayaTambahan3] [money] NOT NULL,
	[EnumBiayaTambahan4] [int] NOT NULL,
	[NilaiBiayaTambahan4] [money] NOT NULL,
	[BiayaTambahan4] [money] NOT NULL,
	[EnumDiscountTransaksi] [int] NOT NULL,
	[PersentaseDiscountTransaksi] [money] NOT NULL,
	[DiscountTransaksi] [money] NOT NULL,
	[TotalDiscountTransaksiDetail] [money] NOT NULL,
	[TotalDiscountVoucher] [money] NOT NULL,
	[BiayaPengiriman] [money] NOT NULL,
	[Pembulatan] [money] NOT NULL,
	[Subtotal] [money] NOT NULL,
	[GrandTotal]  AS (((((((([Subtotal]+[BiayaPengiriman])+[BiayaTambahan1])+[BiayaTambahan2])+[BiayaTambahan3])+[BiayaTambahan4])-[DiscountTransaksi])-[TotalDiscountVoucher])+[Pembulatan]) PERSISTED,
	[TotalPembayaran] [money] NOT NULL,
	[Keterangan] [varchar](max) NOT NULL,
	[AlasanBatal] [varchar](max) NOT NULL,
 CONSTRAINT [PK_TBTransaksiTemp] PRIMARY KEY CLUSTERED 
(
	[IDTransaksiTemp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransaksiVoucher]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransaksiVoucher](
	[IDTransaksi] [varchar](30) NOT NULL,
	[IDVoucher] [int] NOT NULL,
 CONSTRAINT [PK_TBTransaksiVoucher] PRIMARY KEY CLUSTERED 
(
	[IDTransaksi] ASC,
	[IDVoucher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransferBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransferBahanBaku](
	[IDTransferBahanBaku] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPengirim] [int] NOT NULL,
	[IDPenerima] [int] NULL,
	[IDTempatPengirim] [int] NOT NULL,
	[IDTempatPenerima] [int] NOT NULL,
	[TanggalDaftar] [datetime] NOT NULL,
	[TanggalUpdate] [datetime] NOT NULL,
	[EnumJenisTransfer] [int] NOT NULL,
	[TanggalKirim] [datetime] NOT NULL,
	[TanggalTerima] [datetime] NULL,
	[TotalJumlah] [decimal](18, 2) NOT NULL,
	[GrandTotal] [money] NOT NULL,
	[Keterangan] [text] NOT NULL,
 CONSTRAINT [PK_TBTransferBahanBaku] PRIMARY KEY CLUSTERED 
(
	[IDTransferBahanBaku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransferBahanBakuDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransferBahanBakuDetail](
	[IDTransferBahanBakuDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDTransferBahanBaku] [varchar](30) NOT NULL,
	[IDBahanBaku] [int] NOT NULL,
	[IDSatuan] [int] NOT NULL,
	[HargaBeli] [money] NOT NULL,
	[Jumlah] [decimal](18, 2) NOT NULL,
	[Subtotal]  AS ([HargaBeli]*[Jumlah]) PERSISTED,
 CONSTRAINT [PK_TBTransferBahanBakuDetail] PRIMARY KEY CLUSTERED 
(
	[IDTransferBahanBakuDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransferProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransferProduk](
	[IDTransferProduk] [varchar](30) NOT NULL,
	[Nomor] [int] IDENTITY(1,1) NOT NULL,
	[IDPengirim] [int] NOT NULL,
	[IDPenerima] [int] NULL,
	[IDTempatPengirim] [int] NOT NULL,
	[IDTempatPenerima] [int] NOT NULL,
	[TanggalDaftar] [datetime] NOT NULL,
	[TanggalUpdate] [datetime] NOT NULL,
	[EnumJenisTransfer] [int] NOT NULL,
	[TanggalKirim] [datetime] NOT NULL,
	[TanggalTerima] [datetime] NULL,
	[TotalJumlah] [int] NOT NULL,
	[GrandTotalHargaBeli] [money] NOT NULL,
	[GrandTotalHargaJual] [money] NOT NULL,
	[Keterangan] [text] NOT NULL,
 CONSTRAINT [PK_TBTransferBarang] PRIMARY KEY CLUSTERED 
(
	[IDTransferProduk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBTransferProdukDetail]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBTransferProdukDetail](
	[IDTransferProdukDetail] [int] IDENTITY(1,1) NOT NULL,
	[IDTransferProduk] [varchar](30) NOT NULL,
	[IDKombinasiProduk] [int] NOT NULL,
	[HargaBeli] [money] NOT NULL,
	[HargaJual] [money] NOT NULL,
	[Jumlah] [int] NOT NULL,
	[SubtotalHargaBeli]  AS ([HargaBeli]*[Jumlah]) PERSISTED,
	[SubtotalHargaJual]  AS ([HargaJual]*[Jumlah]) PERSISTED,
 CONSTRAINT [PK_TBDetailTransferBarang] PRIMARY KEY CLUSTERED 
(
	[IDTransferProdukDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBVendor]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBVendor](
	[IDVendor] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[Alamat] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[Telepon1] [varchar](250) NULL,
	[Telepon2] [varchar](250) NULL,
	[PersentaseTax] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TBVendor] PRIMARY KEY CLUSTERED 
(
	[IDVendor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBVoucher]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBVoucher](
	[IDVoucher] [int] IDENTITY(1,1) NOT NULL,
	[IDPelanggan] [int] NULL,
	[Kode] [varchar](250) NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[TanggalAwal] [date] NOT NULL,
	[TanggalAkhir] [date] NOT NULL,
	[EnumJenisDiscount] [int] NOT NULL,
	[NilaiDiscount] [money] NOT NULL,
	[Discount] [money] NOT NULL,
	[Pemakaian] [int] NOT NULL,
	[BatasPemakaian] [int] NOT NULL,
	[Keterangan] [text] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_TBVoucher_1] PRIMARY KEY CLUSTERED 
(
	[IDVoucher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBWaitingList]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBWaitingList](
	[IDWaitingList] [int] IDENTITY(1,1) NOT NULL,
	[IDPengguna] [int] NOT NULL,
	[IDPelanggan] [int] NOT NULL,
	[IDMeja] [int] NOT NULL,
	[TanggalPencatatan] [datetime] NOT NULL,
	[TanggalMasuk] [datetime] NOT NULL,
	[JumlahTamu] [int] NOT NULL,
	[EnumStatusReservasi] [int] NOT NULL,
	[Keterangan] [text] NULL,
 CONSTRAINT [PK_TBWaitingList] PRIMARY KEY CLUSTERED 
(
	[IDWaitingList] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBWarna]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBWarna](
	[IDWarna] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [varchar](250) NULL,
	[Nama] [varchar](250) NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBWarna] PRIMARY KEY CLUSTERED 
(
	[IDWarna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBWholesale]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBWholesale](
	[IDWholesale] [int] IDENTITY(1,1) NOT NULL,
	[IDKombinasiProduk] [int] NULL,
	[Jumlah] [int] NULL,
	[HargaJualSatuan] [money] NULL,
 CONSTRAINT [PK_TBWholesale] PRIMARY KEY CLUSTERED 
(
	[IDWholesale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBWilayah]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBWilayah](
	[IDWilayah] [int] IDENTITY(1,1) NOT NULL,
	[IDWilayahParent] [int] NULL,
	[IDGrupWilayah] [int] NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[_IDWMSStore] [uniqueidentifier] NULL,
	[_IDWMS] [uniqueidentifier] NOT NULL,
	[_Urutan] [int] NULL,
	[_TanggalInsert] [datetime] NOT NULL,
	[_IDTempatInsert] [int] NOT NULL,
	[_IDPenggunaInsert] [int] NOT NULL,
	[_TanggalUpdate] [datetime] NOT NULL,
	[_IDTempatUpdate] [int] NOT NULL,
	[_IDPenggunaUpdate] [int] NOT NULL,
	[_IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TBWilayah] PRIMARY KEY CLUSTERED 
(
	[IDWilayah] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[Func_PencarianStokProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[Func_PencarianStokProduk]
(	
	@IDTempat INT,
	@NamaProduk VARCHAR(250),
	@JumlahPencarian INT
)
RETURNS TABLE 
AS
RETURN 
(
SELECT TOP (@JumlahPencarian)
	dbo.TBKombinasiProduk.KodeKombinasiProduk, 
	dbo.TBKombinasiProduk.Nama, 
	dbo.TBStokProduk.Jumlah, 
	REPLACE(FORMAT(dbo.TBStokProduk.HargaJual,'C', 'id-ID'), 'Rp', '') AS HargaJual

FROM
	dbo.TBKombinasiProduk
	
INNER JOIN dbo.TBProduk
	ON dbo.TBKombinasiProduk.IDProduk = dbo.TBProduk.IDProduk

INNER JOIN dbo.TBStokProduk 
	ON dbo.TBKombinasiProduk.IDKombinasiProduk = dbo.TBStokProduk.IDKombinasiProduk

WHERE
	(dbo.TBProduk._IsActive = 'true') AND
	(dbo.TBStokProduk.IDTempat = @IDTempat) AND
	(dbo.TBKombinasiProduk.Nama LIKE '%' + @NamaProduk + '%')
)
GO
SET IDENTITY_INSERT [dbo].[TBAkun] ON 

INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (387, 7, 1, NULL, N'KAS')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (388, 11, 1, NULL, N'Penjualan')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (389, 10, 1, NULL, N'Modal Disetor')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (393, 9, 1, NULL, N'Hutang Pembelian')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (394, 9, 1, NULL, N'Giro Keluar Belum Jatuh Tempo')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (395, 9, 1, NULL, N'Hutang Biaya')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (396, 9, 1, NULL, N'PPN Keluaran')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (397, 9, 1, NULL, N'Kewajiban Lain-lain')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (398, 8, 1, NULL, N'Tanah')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (399, 8, 1, NULL, N'Bangunan')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (400, 7, 1, NULL, N'Persediaan')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (401, 12, 1, NULL, N'Pendapatan lain-lain')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (402, 13, 1, NULL, N'Retur Penjualan')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (403, 13, 1, NULL, N'Potongan/Diskon Penjualan')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (404, 14, 1, NULL, N'Harga Pokok Penjualan')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (405, 15, 1, NULL, N'Biaya Pemasaran')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (406, 16, 1, NULL, N'Biaya Gaji & Upah')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (407, 16, 1, NULL, N'Biaya Depresiasi')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (408, 16, 1, NULL, N'Biaya Administrasi Umum Lainnya')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (409, 17, 1, NULL, N'Bunga Pinjaman')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (410, 18, 1, NULL, N'Biaya Pajak')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (411, 8, 1, NULL, N'Kendaraan')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (412, 7, 1, NULL, N'Piutang')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (413, 9, 1, NULL, N'Hutang Tax')
INSERT [dbo].[TBAkun] ([IDAkun], [IDAkunGrup], [IDTempat], [Kode], [Nama]) VALUES (414, 9, 1, NULL, N'Hutang Services')
SET IDENTITY_INSERT [dbo].[TBAkun] OFF
SET IDENTITY_INSERT [dbo].[TBAkunGrup] ON 

INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (1, NULL, N'Assets', 1, 1)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (2, NULL, N'Liability & Owner''s Equity', 2, 2)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (3, NULL, N'Modal', 2, 2)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (4, NULL, N'Income', 1, 2)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (5, NULL, N'Expenses', 2, 1)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (6, NULL, N'Piutang', 1, 1)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (7, 1, N'Current Assets', 1, 1)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (8, 1, N'Fixed Assets', 1, 1)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (9, 2, N'Liability', 2, 2)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (10, 2, N'Owner''s Equity', 2, 2)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (11, 4, N'Operating Income', 1, 2)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (12, 4, N'Others Income & Gain', 1, 2)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (13, 5, N'Return and Sales Discount', 2, 1)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (14, 5, N'Cost of Goods Sold', 2, 1)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (15, 5, N'Sales Expenses', 2, 1)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (16, 5, N'Administration & General Expenses', 2, 1)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (17, 5, N'Other Expenses & Loss', 2, 1)
INSERT [dbo].[TBAkunGrup] ([IDAkunGrup], [IDAkunGrupParent], [Nama], [EnumJenisAkunGrup], [EnumSaldoNormal]) VALUES (18, 5, N'Taxation', 2, 1)
SET IDENTITY_INSERT [dbo].[TBAkunGrup] OFF
SET IDENTITY_INSERT [dbo].[TBAkuntansiAkun] ON 

INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (1, NULL, 1, N'1000', N'Kas & Bank', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (2, NULL, 1, N'1100', N'Piutang', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (3, NULL, 2, N'2000', N'Hutang Jangka Pendek', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (4, NULL, 2, N'2100', N'Hutang Jangka Panjang', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (5, NULL, 3, N'3000', N'Ekuitas', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (6, NULL, 4, N'4000', N'Pendapatan', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (7, NULL, 5, N'5000', N'Beban', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (8, 1, 1, N'1001', N'Kas', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (9, 1, 1, N'1002', N'Bank', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (10, 1, 1, N'1003', N'Kas Transfer', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (11, 2, 1, N'1101', N'Account Receivable', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (12, NULL, 1, N'1200', N'Persediaan', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (13, 12, 1, N'1201', N'Persediaan Barang Dagang', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (14, 12, 1, N'1202', N'Persediaan Bahan Baku', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (15, 12, 1, N'1203', N'Barang Terkirim', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (16, 12, 1, N'1204', N'Bahan Baku Terkirim', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (17, NULL, 1, N'1300', N'Asset Lainnya', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (18, 17, 1, N'1301', N'PPn Masukan', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (19, NULL, 1, N'1400', N'Asset Tidak Lancar', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (20, 19, 1, N'1401', N'Tanah', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (21, 19, 1, N'1402', N'Bangunan', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (22, 19, 1, N'1403', N'Kendaraan', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (23, 19, 1, N'1404', N'Peralatan', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (24, 19, 1, N'1405', N'Inventaris Kantor', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (25, 3, 2, N'2001', N'Account Payable', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (26, 3, 2, N'2002', N'PPn Keluaran', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (27, 4, 2, N'2101', N'Hutang Jangka Panjang', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (28, 5, 3, N'3001', N'Opening Balance Equity', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (29, 5, 3, N'3002', N'Retained Earning', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (30, 5, 3, N'3003', N'Deviden', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (31, 6, 4, N'4001', N'Penjualan', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (32, 6, 4, N'4002', N'Pendapatan Jasa', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (33, 6, 4, N'4003', N'Retur Penjualan', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (34, 6, 4, N'4004', N'Potongan Penjualan', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (35, 6, 4, N'4005', N'Pendapatan Lainnya', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (36, 7, 5, N'5001', N'Beban Gaji', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (37, 7, 5, N'5002', N'Beban Pemeliharaan', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (38, 7, 5, N'5003', N'Biaya Operasional', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (39, 7, 5, N'5004', N'Cost of Goods Sold', 0.0000, N'', 1)
INSERT [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun], [IDAkuntansiAkunParent], [IDAkuntansiAkunTipe], [Nomor], [Nama], [SaldoAwal], [Keterangan], [Status]) VALUES (40, 7, 5, N'5005', N'Biaya Pemasaran', 0.0000, N'', 1)
SET IDENTITY_INSERT [dbo].[TBAkuntansiAkun] OFF
SET IDENTITY_INSERT [dbo].[TBAkuntansiAkunTipe] ON 

INSERT [dbo].[TBAkuntansiAkunTipe] ([IDAkuntansiAkunTipe], [Nama], [SaldoNormal], [Bertambah], [Berkurang]) VALUES (1, N'Assets / Harta', 1, 1, 2)
INSERT [dbo].[TBAkuntansiAkunTipe] ([IDAkuntansiAkunTipe], [Nama], [SaldoNormal], [Bertambah], [Berkurang]) VALUES (2, N'Liabilities / Kewajiban', 2, 2, 1)
INSERT [dbo].[TBAkuntansiAkunTipe] ([IDAkuntansiAkunTipe], [Nama], [SaldoNormal], [Bertambah], [Berkurang]) VALUES (3, N'Capital / Modal', 2, 2, 1)
INSERT [dbo].[TBAkuntansiAkunTipe] ([IDAkuntansiAkunTipe], [Nama], [SaldoNormal], [Bertambah], [Berkurang]) VALUES (4, N'Revenues / Pendapatan', 2, 2, 1)
INSERT [dbo].[TBAkuntansiAkunTipe] ([IDAkuntansiAkunTipe], [Nama], [SaldoNormal], [Bertambah], [Berkurang]) VALUES (5, N'Expenses / Beban', 1, 1, 2)
SET IDENTITY_INSERT [dbo].[TBAkuntansiAkunTipe] OFF
SET IDENTITY_INSERT [dbo].[TBAlamat] ON 

INSERT [dbo].[TBAlamat] ([IDAlamat], [IDPelanggan], [IDNegara], [IDProvinsi], [IDKota], [NamaLengkap], [AlamatLengkap], [Provinsi], [Kota], [KodePos], [Handphone], [TeleponLain], [Keterangan], [BiayaPengiriman]) VALUES (5, 6, NULL, NULL, NULL, N'', N'', NULL, NULL, N'', N'', N'', N'', 0.0000)
SET IDENTITY_INSERT [dbo].[TBAlamat] OFF
SET IDENTITY_INSERT [dbo].[TBAtributGrup] ON 

INSERT [dbo].[TBAtributGrup] ([IDAtributGrup], [IDWMS], [TanggalDaftar], [TanggalUpdate], [Nama]) VALUES (1, N'470e9761-214f-49c7-a2e8-95d4e6bffcb0', CAST(N'2016-04-24T11:19:33.597' AS DateTime), CAST(N'2016-04-24T11:19:33.597' AS DateTime), N'Produk')
INSERT [dbo].[TBAtributGrup] ([IDAtributGrup], [IDWMS], [TanggalDaftar], [TanggalUpdate], [Nama]) VALUES (2, N'26f6b9d4-19c3-4458-96db-2d9cef0711dd', CAST(N'2016-04-24T11:19:56.610' AS DateTime), CAST(N'2016-04-24T11:19:56.610' AS DateTime), N'Bahan Baku')
INSERT [dbo].[TBAtributGrup] ([IDAtributGrup], [IDWMS], [TanggalDaftar], [TanggalUpdate], [Nama]) VALUES (3, N'aebe8283-20f3-4d10-86f7-f8a6159e2a77', CAST(N'2016-04-24T11:20:02.937' AS DateTime), CAST(N'2016-04-24T11:20:02.937' AS DateTime), N'Pengguna')
INSERT [dbo].[TBAtributGrup] ([IDAtributGrup], [IDWMS], [TanggalDaftar], [TanggalUpdate], [Nama]) VALUES (4, N'd86e6e26-1388-42c4-b15d-e259c522b765', CAST(N'2016-04-24T11:20:08.240' AS DateTime), CAST(N'2016-04-24T11:20:08.240' AS DateTime), N'Pelanggan')
INSERT [dbo].[TBAtributGrup] ([IDAtributGrup], [IDWMS], [TanggalDaftar], [TanggalUpdate], [Nama]) VALUES (5, N'1b115eaa-c4e1-4d5e-ac81-2c70c1093a2a', CAST(N'2016-04-24T11:20:13.973' AS DateTime), CAST(N'2016-04-24T11:20:13.973' AS DateTime), N'Store')
INSERT [dbo].[TBAtributGrup] ([IDAtributGrup], [IDWMS], [TanggalDaftar], [TanggalUpdate], [Nama]) VALUES (6, N'28a4eda9-2f72-4009-9fd7-6b06df2fdfc0', CAST(N'2016-04-24T11:20:21.123' AS DateTime), CAST(N'2016-04-24T11:20:21.123' AS DateTime), N'Tempat')
SET IDENTITY_INSERT [dbo].[TBAtributGrup] OFF
SET IDENTITY_INSERT [dbo].[TBAtributProduk] ON 

INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (1, 1, N'S', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'f24fd987-e8dc-4f82-b896-7d3f886c9db2', 1, CAST(N'2018-07-10T10:14:45.563' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.563' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (2, 1, N'M', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'083a23fa-a63d-4f7b-9e59-3d2848977c32', 2, CAST(N'2018-07-10T10:14:45.830' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.830' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (3, 1, N'L', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'1fac7273-ba49-458e-9787-3fb3a17de33d', 3, CAST(N'2018-07-10T10:14:45.887' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.887' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (4, 1, N'XL', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'475336b0-7753-4934-9e22-05dd9278fb5b', 4, CAST(N'2018-07-10T10:14:45.930' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.930' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (5, 1, N'28', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'e6b85370-190f-4551-adc8-eb3d36278060', 5, CAST(N'2018-07-10T10:14:46.307' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.307' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (6, 1, N'29', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'f13aee8a-19da-48ab-88b2-7d7e142a31e0', 6, CAST(N'2018-07-10T10:14:46.360' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.360' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (7, 1, N'30', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'8f5c6b99-7409-48ac-9370-44f792c17912', 7, CAST(N'2018-07-10T10:14:46.397' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.397' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (8, 1, N'31', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'6a63e50e-f64a-44c0-b258-fb60fcdfe8f0', 8, CAST(N'2018-07-10T10:14:46.437' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.437' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (9, 1, N'32', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'6edebe23-11bc-4219-9340-661c5a1b7022', 9, CAST(N'2018-07-10T10:14:46.483' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.483' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (10, 1, N'40', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'83dc4304-7a86-4b09-9b4e-f18d0b18c445', 10, CAST(N'2018-07-10T10:14:46.577' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.577' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (11, 1, N'41', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'265425eb-c777-4a80-a6b1-f7e33088e629', 11, CAST(N'2018-07-10T10:14:46.637' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.637' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (12, 1, N'42', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'78565957-dfe1-4d41-bb4f-2c15341466ce', 12, CAST(N'2018-07-10T10:14:46.683' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.683' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (13, 1, N'43', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'41beb9e0-be86-43c3-9a38-7bbc0ff16ec9', 13, CAST(N'2018-07-10T10:14:46.723' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.723' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBAtributProduk] ([IDAtributProduk], [IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (14, 1, N'44', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'c0c4c063-ed24-4d6c-bb8a-a13e0c0b44a4', 14, CAST(N'2018-07-10T10:14:46.770' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.770' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TBAtributProduk] OFF
SET IDENTITY_INSERT [dbo].[TBAtributProdukGrup] ON 

INSERT [dbo].[TBAtributProdukGrup] ([IDAtributProdukGrup], [Nama], [_IDWMSStore], [_IDWMSAtributProdukGrup], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (1, N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'da319e37-787a-4199-bfba-0328c8fad992', 1, CAST(N'2018-07-10T10:14:45.550' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.553' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TBAtributProdukGrup] OFF
SET IDENTITY_INSERT [dbo].[TBBlackBox] ON 

INSERT [dbo].[TBBlackBox] ([IDBlackBox], [Tanggal], [Pesan], [Halaman]) VALUES (5, CAST(N'2018-07-10T09:22:29.307' AS DateTime), N'String was not recognized as a valid Boolean.    at System.Boolean.Parse(String value)
   at frontend_MasterPage.Page_Init(Object sender, EventArgs e) in d:\UPDATE WMS COMMERCE\_WITEnterpriseSystem\frontend\MasterPage.master.cs:line 21
   at System.Web.Util.CalliEventHandlerDelegateProxy.Callback(Object sender, EventArgs e)
   at System.Web.UI.Control.OnInit(EventArgs e)
   at System.Web.UI.UserControl.OnInit(EventArgs e)
   at System.Web.UI.Control.InitRecursive(Control namingContainer)
   at System.Web.UI.Control.InitRecursive(Control namingContainer)
   at System.Web.UI.Page.ProcessRequestMain(Boolean includeStagesBeforeAsyncPoint, Boolean includeStagesAfterAsyncPoint)', N'http://localhost:49506/default.aspx')
INSERT [dbo].[TBBlackBox] ([IDBlackBox], [Tanggal], [Pesan], [Halaman]) VALUES (6, CAST(N'2018-07-10T10:07:20.623' AS DateTime), N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_TBPelanggan_TBGrupPelanggan". The conflict occurred in database "DBWITEnterpriseSystem", table "dbo.TBGrupPelanggan", column ''IDGrupPelanggan''.
The statement has been terminated.    at System.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at System.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at System.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at System.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at System.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at System.Data.SqlClient.SqlDataReader.get_MetaData()
   at System.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at System.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean async, Int32 timeout, Task& task, Boolean asyncWrite, Boolean inRetry, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at System.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, String method, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry)
   at System.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, String method)
   at System.Data.SqlClient.SqlCommand.ExecuteReader(CommandBehavior behavior, String method)
   at System.Data.SqlClient.SqlCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbCommand.ExecuteReader()
   at System.Data.Linq.SqlClient.SqlProvider.Execute(Expression query, QueryInfo queryInfo, IObjectReaderFactory factory, Object[] parentArgs, Object[] userArgs, ICompiledSubQuery[] subQueries, Object lastResult)
   at System.Data.Linq.SqlClient.SqlProvider.ExecuteAll(Expression query, QueryInfo[] queryInfos, IObjectReaderFactory factory, Object[] userArguments, ICompiledSubQuery[] subQueries)
   at System.Data.Linq.SqlClient.SqlProvider.System.Data.Linq.Provider.IProvider.Execute(Expression query)
   at System.Data.Linq.ChangeDirector.StandardChangeDirector.DynamicInsert(TrackedObject item)
   at System.Data.Linq.ChangeDirector.StandardChangeDirector.Insert(TrackedObject item)
   at System.Data.Linq.ChangeProcessor.SubmitChanges(ConflictMode failureMode)
   at System.Data.Linq.DataContext.SubmitChanges(ConflictMode failureMode)
   at System.Data.Linq.DataContext.SubmitChanges()
   at frontend_MasterPage.MembuatPelanggan() in d:\UPDATE WMS COMMERCE\_WITEnterpriseSystem\frontend\MasterPage.master.cs:line 95
   at frontend_MasterPage.Page_Init(Object sender, EventArgs e) in d:\UPDATE WMS COMMERCE\_WITEnterpriseSystem\frontend\MasterPage.master.cs:line 48
   at System.Web.Util.CalliEventHandlerDelegateProxy.Callback(Object sender, EventArgs e)
   at System.Web.UI.Control.OnInit(EventArgs e)
   at System.Web.UI.UserControl.OnInit(EventArgs e)
   at System.Web.UI.Control.InitRecursive(Control namingContainer)
   at System.Web.UI.Control.InitRecursive(Control namingContainer)
   at System.Web.UI.Page.ProcessRequestMain(Boolean includeStagesBeforeAsyncPoint, Boolean includeStagesAfterAsyncPoint)', N'http://localhost:49506/default.aspx')
INSERT [dbo].[TBBlackBox] ([IDBlackBox], [Tanggal], [Pesan], [Halaman]) VALUES (7, CAST(N'2018-07-11T00:13:00.940' AS DateTime), N'The file ''/Product.aspx'' does not exist.    at System.Web.UI.Util.CheckVirtualFileExists(VirtualPath virtualPath)
   at System.Web.Compilation.BuildManager.GetVPathBuildResultInternal(VirtualPath virtualPath, Boolean noBuild, Boolean allowCrossApp, Boolean allowBuildInPrecompile, Boolean throwIfNotFound, Boolean ensureIsUpToDate)
   at System.Web.Compilation.BuildManager.GetVPathBuildResultWithNoAssert(HttpContext context, VirtualPath virtualPath, Boolean noBuild, Boolean allowCrossApp, Boolean allowBuildInPrecompile, Boolean throwIfNotFound, Boolean ensureIsUpToDate)
   at System.Web.Compilation.BuildManager.GetVirtualPathObjectFactory(VirtualPath virtualPath, HttpContext context, Boolean allowCrossApp, Boolean throwIfNotFound)
   at System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath(VirtualPath virtualPath, Type requiredBaseType, HttpContext context, Boolean allowCrossApp)
   at System.Web.UI.PageHandlerFactory.GetHandlerHelper(HttpContext context, String requestType, VirtualPath virtualPath, String physicalPath)
   at System.Web.UI.PageHandlerFactory.GetHandler(HttpContext context, String requestType, String virtualPath, String path)
   at System.Web.HttpApplication.MaterializeHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()
   at System.Web.HttpApplication.ExecuteStepImpl(IExecutionStep step)
   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean& completedSynchronously)', N'http://localhost:51131/Product.aspx?id=5')
INSERT [dbo].[TBBlackBox] ([IDBlackBox], [Tanggal], [Pesan], [Halaman]) VALUES (8, CAST(N'2018-07-11T00:15:22.350' AS DateTime), N'The file ''/Product.aspx'' does not exist.    at System.Web.UI.Util.CheckVirtualFileExists(VirtualPath virtualPath)
   at System.Web.Compilation.BuildManager.GetVPathBuildResultInternal(VirtualPath virtualPath, Boolean noBuild, Boolean allowCrossApp, Boolean allowBuildInPrecompile, Boolean throwIfNotFound, Boolean ensureIsUpToDate)
   at System.Web.Compilation.BuildManager.GetVPathBuildResultWithNoAssert(HttpContext context, VirtualPath virtualPath, Boolean noBuild, Boolean allowCrossApp, Boolean allowBuildInPrecompile, Boolean throwIfNotFound, Boolean ensureIsUpToDate)
   at System.Web.Compilation.BuildManager.GetVirtualPathObjectFactory(VirtualPath virtualPath, HttpContext context, Boolean allowCrossApp, Boolean throwIfNotFound)
   at System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath(VirtualPath virtualPath, Type requiredBaseType, HttpContext context, Boolean allowCrossApp)
   at System.Web.UI.PageHandlerFactory.GetHandlerHelper(HttpContext context, String requestType, VirtualPath virtualPath, String physicalPath)
   at System.Web.UI.PageHandlerFactory.GetHandler(HttpContext context, String requestType, String virtualPath, String path)
   at System.Web.HttpApplication.MaterializeHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()
   at System.Web.HttpApplication.ExecuteStepImpl(IExecutionStep step)
   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean& completedSynchronously)', N'http://localhost:51131/Product.aspx?id=5')
SET IDENTITY_INSERT [dbo].[TBBlackBox] OFF
SET IDENTITY_INSERT [dbo].[TBFotoProduk] ON 

INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (14, 5, N'.jpg', 1)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (15, 5, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (16, 5, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (17, 6, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (18, 6, N'.jpg', 1)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (19, 6, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (20, 7, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (21, 7, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (22, 7, N'.jpg', 1)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (23, 8, N'.jpg', 1)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (24, 8, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (25, 8, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (26, 9, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (27, 9, N'.jpg', 1)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (28, 9, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (29, 10, N'.jpg', 0)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (30, 10, N'.jpg', 1)
INSERT [dbo].[TBFotoProduk] ([IDFotoProduk], [IDProduk], [ExtensiFoto], [FotoUtama]) VALUES (31, 10, N'.jpg', 0)
SET IDENTITY_INSERT [dbo].[TBFotoProduk] OFF
SET IDENTITY_INSERT [dbo].[TBGrupPelanggan] ON 

INSERT [dbo].[TBGrupPelanggan] ([IDGrupPelanggan], [Nama], [EnumBonusGrupPelanggan], [Persentase], [LimitTransaksi], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (1, N'Customer', 1, CAST(0.00 AS Decimal(18, 2)), 0.0000, NULL, N'f8eff18e-b752-4f0a-9c34-8dd2eea0a087', NULL, CAST(N'2017-02-15T21:41:06.270' AS DateTime), 1, 1, CAST(N'2017-02-15T21:41:06.270' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBGrupPelanggan] ([IDGrupPelanggan], [Nama], [EnumBonusGrupPelanggan], [Persentase], [LimitTransaksi], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (2, N'Dropshipper', 1, CAST(0.00 AS Decimal(18, 2)), 0.0000, NULL, N'a18e6d6e-59ae-4243-8c18-2e2effe48203', NULL, CAST(N'2017-02-15T21:41:06.270' AS DateTime), 1, 1, CAST(N'2017-02-15T21:41:06.270' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBGrupPelanggan] ([IDGrupPelanggan], [Nama], [EnumBonusGrupPelanggan], [Persentase], [LimitTransaksi], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (3, N'Guest', 1, CAST(0.00 AS Decimal(18, 2)), 0.0000, N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'355553d5-9f5d-4bb6-bab2-cd008e27c0ab', 3, CAST(N'2018-07-02T00:00:00.000' AS DateTime), 1, 1, CAST(N'2018-07-02T00:00:00.000' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TBGrupPelanggan] OFF
SET IDENTITY_INSERT [dbo].[TBGrupPengguna] ON 

INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (1, N'Super Administrator', N'')
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (2, N'Administrator', NULL)
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (4, N'Product Manager', NULL)
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (5, N'Material Manager', NULL)
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (10, N'Cashier Retail', N'/WITPointOfSales/')
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (12, N'Employee', NULL)
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (13, N'Supervisor', N'')
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (14, N'Waitress', N'/WITRestaurantV2/')
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (15, N'Warehouse Keeper', N'/WITWarehouse/')
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (16, N'Cashier Wholesale', N'/WITPointOfSales/')
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (18, N'Cashier Restaurant', N'/WITRestaurantV2/')
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (20, N'Consignment', NULL)
INSERT [dbo].[TBGrupPengguna] ([IDGrupPengguna], [Nama], [DefaultURL]) VALUES (22, N'Manager - WMS Mobile', N'')
SET IDENTITY_INSERT [dbo].[TBGrupPengguna] OFF
SET IDENTITY_INSERT [dbo].[TBGrupWilayah] ON 

INSERT [dbo].[TBGrupWilayah] ([IDGrupWilayah], [Nama]) VALUES (1, N'Negara')
INSERT [dbo].[TBGrupWilayah] ([IDGrupWilayah], [Nama]) VALUES (2, N'Provinsi')
INSERT [dbo].[TBGrupWilayah] ([IDGrupWilayah], [Nama]) VALUES (3, N'Kota')
INSERT [dbo].[TBGrupWilayah] ([IDGrupWilayah], [Nama]) VALUES (4, N'Zona')
SET IDENTITY_INSERT [dbo].[TBGrupWilayah] OFF
SET IDENTITY_INSERT [dbo].[TBJenisBebanBiaya] ON 

INSERT [dbo].[TBJenisBebanBiaya] ([IDJenisBebanBiaya], [Nama]) VALUES (1, N'Tidak ada beban biaya')
INSERT [dbo].[TBJenisBebanBiaya] ([IDJenisBebanBiaya], [Nama]) VALUES (2, N'Beban biaya kepada customer')
INSERT [dbo].[TBJenisBebanBiaya] ([IDJenisBebanBiaya], [Nama]) VALUES (3, N'Beban biaya kepada store')
SET IDENTITY_INSERT [dbo].[TBJenisBebanBiaya] OFF
SET IDENTITY_INSERT [dbo].[TBJenisPembayaran] ON 

INSERT [dbo].[TBJenisPembayaran] ([IDJenisPembayaran], [IDAkun], [IDJenisBebanBiaya], [Nama], [PersentaseBiaya]) VALUES (1, NULL, 1, N'Cash', CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[TBJenisPembayaran] ([IDJenisPembayaran], [IDAkun], [IDJenisBebanBiaya], [Nama], [PersentaseBiaya]) VALUES (2, NULL, 1, N'Deposit', CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[TBJenisPembayaran] ([IDJenisPembayaran], [IDAkun], [IDJenisBebanBiaya], [Nama], [PersentaseBiaya]) VALUES (3, NULL, 1, N'Credit Card', CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[TBJenisPembayaran] ([IDJenisPembayaran], [IDAkun], [IDJenisBebanBiaya], [Nama], [PersentaseBiaya]) VALUES (4, NULL, 1, N'Debit Card', CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[TBJenisPembayaran] ([IDJenisPembayaran], [IDAkun], [IDJenisBebanBiaya], [Nama], [PersentaseBiaya]) VALUES (5, NULL, 1, N'Transfer Bank', CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[TBJenisPembayaran] OFF
SET IDENTITY_INSERT [dbo].[TBJenisPerpindahanStok] ON 

INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (1, N'Stok Baru Bertambah', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (2, N'Transfer Stok Masuk', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (3, N'Transfer Stok Keluar', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (4, N'Transaksi', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (5, N'Transaksi Batal', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (6, N'Stok Hilang', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (7, N'Salah Memasukkan', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (8, N'Bertambah', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (9, N'Berkurang', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (10, N'Retur dari Pembeli', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (11, N'Stok Opname Berkurang', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (12, N'Stok Opname Bertambah', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (13, N'Pengurangan Produksi', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (14, N'Retur ke Tempat Produksi', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (15, N'Pengganti Retur Pembeli', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (16, N'Produk Kadaluarsa', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (17, N'Produk Berjamur', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (18, N'Pembuangan Barang Rusak', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (19, N'Retur dari Pembeli Dibuang', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (20, N'Penyusutan', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (21, N'Pembatalan Produksi', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (22, N'Perubahan Transaksi', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (23, N'Stok Baru Berkurang', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (24, N'Transfer Batal', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (25, N'Retur dari Pembeli Batal', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (26, N'Hasil Produksi', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (27, N'Perubahan Retur dari Pembeli', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (28, N'Import Excel Bertambah', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (29, N'Import Excel Berkurang', 0)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (30, N'Restock Barang', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (31, N'Penerimaan PO', 1)
INSERT [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok], [Nama], [Status]) VALUES (32, N'Tolak Penerimaan PO', 0)
SET IDENTITY_INSERT [dbo].[TBJenisPerpindahanStok] OFF
SET IDENTITY_INSERT [dbo].[TBJenisStokMutasi] ON 

INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (1, N'Transaksi')
INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (2, N'Stok Opname')
INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (4, N'Retur Pelanggan')
INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (5, N'Restock')
INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (6, N'Purchase Order')
INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (7, N'Produksi')
INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (8, N'Retur Purchase Order')
INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (9, N'Retur Produksi')
INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (10, N'Pembuangan Rusak')
INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (12, N'Transfer')
INSERT [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi], [Nama]) VALUES (13, N'Pengurangan Untuk Produksi')
SET IDENTITY_INSERT [dbo].[TBJenisStokMutasi] OFF
SET IDENTITY_INSERT [dbo].[TBJenisTransaksi] ON 

INSERT [dbo].[TBJenisTransaksi] ([IDJenisTransaksi], [Nama]) VALUES (1, N'Point Of Sales')
INSERT [dbo].[TBJenisTransaksi] ([IDJenisTransaksi], [Nama]) VALUES (2, N'ECommerce')
INSERT [dbo].[TBJenisTransaksi] ([IDJenisTransaksi], [Nama]) VALUES (3, N'Wholesale')
INSERT [dbo].[TBJenisTransaksi] ([IDJenisTransaksi], [Nama]) VALUES (10, N'Line')
INSERT [dbo].[TBJenisTransaksi] ([IDJenisTransaksi], [Nama]) VALUES (11, N'SMS')
INSERT [dbo].[TBJenisTransaksi] ([IDJenisTransaksi], [Nama]) VALUES (12, N'Marketing')
INSERT [dbo].[TBJenisTransaksi] ([IDJenisTransaksi], [Nama]) VALUES (13, N'Karyawan')
INSERT [dbo].[TBJenisTransaksi] ([IDJenisTransaksi], [Nama]) VALUES (14, N'Point Of Sales Mobile')
INSERT [dbo].[TBJenisTransaksi] ([IDJenisTransaksi], [Nama]) VALUES (15, N'Gojek')
INSERT [dbo].[TBJenisTransaksi] ([IDJenisTransaksi], [Nama]) VALUES (16, N'Grab')
SET IDENTITY_INSERT [dbo].[TBJenisTransaksi] OFF
SET IDENTITY_INSERT [dbo].[TBKategoriProduk] ON 

INSERT [dbo].[TBKategoriProduk] ([IDKategoriProduk], [IDKategoriProdukParent], [Nama], [Deskripsi]) VALUES (5, NULL, N'Baju', N'')
INSERT [dbo].[TBKategoriProduk] ([IDKategoriProduk], [IDKategoriProdukParent], [Nama], [Deskripsi]) VALUES (6, NULL, N'Kemeja', N'')
INSERT [dbo].[TBKategoriProduk] ([IDKategoriProduk], [IDKategoriProdukParent], [Nama], [Deskripsi]) VALUES (7, NULL, N'Jaket', N'')
INSERT [dbo].[TBKategoriProduk] ([IDKategoriProduk], [IDKategoriProdukParent], [Nama], [Deskripsi]) VALUES (8, NULL, N'Celana', N'')
INSERT [dbo].[TBKategoriProduk] ([IDKategoriProduk], [IDKategoriProdukParent], [Nama], [Deskripsi]) VALUES (9, NULL, N'Pendek', N'')
INSERT [dbo].[TBKategoriProduk] ([IDKategoriProduk], [IDKategoriProdukParent], [Nama], [Deskripsi]) VALUES (10, NULL, N'Sepatu', N'')
SET IDENTITY_INSERT [dbo].[TBKategoriProduk] OFF
SET IDENTITY_INSERT [dbo].[TBKategoriTempat] ON 

INSERT [dbo].[TBKategoriTempat] ([IDKategoriTempat], [Nama]) VALUES (1, N'Warehouse')
INSERT [dbo].[TBKategoriTempat] ([IDKategoriTempat], [Nama]) VALUES (2, N'ECommerce')
INSERT [dbo].[TBKategoriTempat] ([IDKategoriTempat], [Nama]) VALUES (3, N'Store')
INSERT [dbo].[TBKategoriTempat] ([IDKategoriTempat], [Nama]) VALUES (4, N'Event')
INSERT [dbo].[TBKategoriTempat] ([IDKategoriTempat], [Nama]) VALUES (5, N'Consignment')
INSERT [dbo].[TBKategoriTempat] ([IDKategoriTempat], [Nama]) VALUES (7, N'Dropshipper')
SET IDENTITY_INSERT [dbo].[TBKategoriTempat] OFF
SET IDENTITY_INSERT [dbo].[TBKombinasiProduk] ON 

INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (1, N'609a5a27-f703-448d-a08e-e025aa007647', 5, 1, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:45.567' AS DateTime), CAST(N'2018-07-10T10:14:45.567' AS DateTime), 1, N'180711', N'Best Clohtes White (S)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (2, N'd0d67732-5df0-4044-b947-f7ee347a170e', 5, 2, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:45.830' AS DateTime), CAST(N'2018-07-10T10:14:45.830' AS DateTime), 2, N'180712', N'Best Clohtes White (M)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (3, N'10fb927b-73ba-4cc3-bb30-d6aa489bbab8', 5, 3, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:45.887' AS DateTime), CAST(N'2018-07-10T10:14:45.887' AS DateTime), 3, N'180713', N'Best Clohtes White (L)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (4, N'ded8cd15-dd3b-4c16-b620-c94e14f15e00', 5, 4, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:45.930' AS DateTime), CAST(N'2018-07-10T10:14:45.930' AS DateTime), 4, N'180714', N'Best Clohtes White (XL)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (5, N'a23a538e-450f-4465-807b-64ca969ddb1f', 6, 1, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.020' AS DateTime), CAST(N'2018-07-10T10:14:46.020' AS DateTime), 5, N'180715', N'Simple Shirt Black (S)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (6, N'a3934e02-9a6d-4e3f-8c5a-3c1315a4e42a', 7, 2, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.090' AS DateTime), CAST(N'2018-07-10T10:14:46.090' AS DateTime), 6, N'180716', N'Simple Shirt (M)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (7, N'481b1628-a9be-4ed4-8a38-60078af1b1ed', 7, 3, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.123' AS DateTime), CAST(N'2018-07-10T10:14:46.123' AS DateTime), 7, N'180717', N'Simple Shirt (L)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (8, N'9f0d8798-84ed-4623-80c2-34b7e868cd50', 7, 4, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.160' AS DateTime), CAST(N'2018-07-10T10:14:46.160' AS DateTime), 8, N'180718', N'Simple Shirt (XL)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (9, N'92c86c4b-4cec-4a0a-990b-a5ded2830442', 8, 1, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.197' AS DateTime), CAST(N'2018-07-10T10:14:46.197' AS DateTime), 9, N'180719', N'Bomber Jacket Blue (S)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (10, N'ccb9cee9-1f65-455b-9737-b5b2ac910fb0', 8, 2, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.233' AS DateTime), CAST(N'2018-07-10T10:14:46.233' AS DateTime), 10, N'1807110', N'Bomber Jacket Blue (M)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (11, N'44b02d6e-aa3a-4132-88dc-8b1263e69bbf', 8, 3, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.263' AS DateTime), CAST(N'2018-07-10T10:14:46.263' AS DateTime), 11, N'1807111', N'Bomber Jacket Blue (L)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (12, N'1bf9fd73-7e58-41b1-8578-01222162981e', 9, 5, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.307' AS DateTime), CAST(N'2018-07-10T10:14:46.307' AS DateTime), 12, N'1807112', N'Cino Pant Brown (28)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (13, N'2fe51b06-25bc-4772-be0a-d79eaf122f30', 9, 6, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.360' AS DateTime), CAST(N'2018-07-10T10:14:46.360' AS DateTime), 13, N'1807113', N'Cino Pant Brown (29)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (14, N'd0c4a296-ec27-415f-875e-513299b3d230', 9, 7, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.397' AS DateTime), CAST(N'2018-07-10T10:14:46.397' AS DateTime), 14, N'1807114', N'Cino Pant Brown (30)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (15, N'2b7b85e4-7622-45b7-b330-5c12fd595f21', 9, 8, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.437' AS DateTime), CAST(N'2018-07-10T10:14:46.437' AS DateTime), 15, N'1807115', N'Cino Pant Brown (31)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (16, N'e464a5c8-425c-40fd-bced-ec31250de9fe', 9, 9, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.483' AS DateTime), CAST(N'2018-07-10T10:14:46.483' AS DateTime), 16, N'1807116', N'Cino Pant Brown (32)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (17, N'59259d12-e221-42e2-a8f9-8e20ac6e0cd5', 10, 10, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.577' AS DateTime), CAST(N'2018-07-10T10:14:46.577' AS DateTime), 17, N'1807117', N'Boot Shoes Black (40)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (18, N'6cbf6fd0-0979-493e-9236-90de982e249a', 10, 11, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.637' AS DateTime), CAST(N'2018-07-10T10:14:46.637' AS DateTime), 18, N'1807118', N'Boot Shoes Black (41)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (19, N'dd42c7e5-fe70-4ea0-bef5-cb312c515dea', 10, 12, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.683' AS DateTime), CAST(N'2018-07-10T10:14:46.683' AS DateTime), 19, N'1807119', N'Boot Shoes Black (42)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (20, N'251be41b-c201-4958-b4fd-8c5a1cdf0ad3', 10, 13, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.723' AS DateTime), CAST(N'2018-07-10T10:14:46.723' AS DateTime), 20, N'1807120', N'Boot Shoes Black (43)', CAST(0.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[TBKombinasiProduk] ([IDKombinasiProduk], [IDWMS], [IDProduk], [IDAtributProduk], [IDAtributProduk1], [IDAtributProduk2], [IDAtributProduk3], [TanggalDaftar], [TanggalUpdate], [Urutan], [KodeKombinasiProduk], [Nama], [Berat], [Deskripsi]) VALUES (21, N'a8019098-1354-4819-80bd-9167f7ecdbc2', 10, 14, NULL, NULL, NULL, CAST(N'2018-07-10T10:14:46.770' AS DateTime), CAST(N'2018-07-10T10:14:46.770' AS DateTime), 21, N'1807121', N'Boot Shoes Black (44)', CAST(0.00 AS Decimal(18, 2)), N'')
SET IDENTITY_INSERT [dbo].[TBKombinasiProduk] OFF
SET IDENTITY_INSERT [dbo].[TBKonfigurasi] ON 

INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (2, NULL, 2, N'Aktifitas Transaksi', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (4, NULL, 4, N'Transaksi Terakhir', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (7, NULL, 7, N'Stok Produk Habis', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (8, NULL, 8, N'POS Daftar Produk', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (10, NULL, 10, N'Membatalkan Transaksi', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (11, NULL, 11, N'Void Item', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (12, NULL, 12, N'Print POS', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (13, NULL, 13, N'Print Invoice', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (14, NULL, 14, N'Print Packing Slip', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (15, NULL, 15, N'Melihat COGS Net Revenue', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (16, NULL, 16, N'POS Muncul Popup Quantity', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (17, NULL, 17, N'POS Discount', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (18, NULL, 18, N'POS Pembayaran', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (19, NULL, 19, N'POS Unique Code', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (20, NULL, 20, N'POS Tempat Pengirim', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (21, NULL, 21, N'POS Payment Verification', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (22, NULL, 22, N'PO Bahan Baku Jatuh Tempo', NULL)
INSERT [dbo].[TBKonfigurasi] ([IDKonfigurasi], [IDKonfigurasiKategori], [Urutan], [Nama], [Keterangan]) VALUES (23, NULL, 23, N'PO Produk Jatuh Tempo', NULL)
SET IDENTITY_INSERT [dbo].[TBKonfigurasi] OFF
SET IDENTITY_INSERT [dbo].[TBKonfigurasiAkun] ON 

INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (1, 387, 1, N'KAS')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (2, 403, 1, N'POTONGAN/DISKON PENJUALAN')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (3, 404, 1, N'HPP')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (4, 388, 1, N'PENJUALAN')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (5, 400, 1, N'PERSEDIAAN')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (6, 412, 1, N'PIUTANG')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (7, 393, 1, N'HUTANG DAGANG')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (8, 393, 1, N'HUTANG DAGANG')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (9, 413, 1, N'HUTANG TAX')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (10, 414, 1, N'HUTANG SERVICE')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (11, 387, 1, N'1')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (12, 387, 1, N'2')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (13, 387, 1, N'3')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (14, 387, 1, N'4')
INSERT [dbo].[TBKonfigurasiAkun] ([IDKonfigurasiAkun], [IDAkun], [IDTempat], [Nama]) VALUES (15, 387, 1, N'5')
SET IDENTITY_INSERT [dbo].[TBKonfigurasiAkun] OFF
SET IDENTITY_INSERT [dbo].[TBKonfigurasiPrinter] ON 

INSERT [dbo].[TBKonfigurasiPrinter] ([IDKonfigurasiPrinter], [NamaPrinter], [Kategori]) VALUES (2, N'EPSON TM-T82 Receipt', N'Makanan')
INSERT [dbo].[TBKonfigurasiPrinter] ([IDKonfigurasiPrinter], [NamaPrinter], [Kategori]) VALUES (3, N'EPSON TM-T82 Receipt', N'Minuman')
SET IDENTITY_INSERT [dbo].[TBKonfigurasiPrinter] OFF
SET IDENTITY_INSERT [dbo].[TBKonfigurasiTomahawk] ON 

INSERT [dbo].[TBKonfigurasiTomahawk] ([IDKonfigurasiTomahawk], [Urutan], [Nama], [DurasiProses], [TanggalTerakhirProses]) VALUES (1, 1, N'KirimEmail', 1, CAST(N'2016-03-06T16:12:00.000' AS DateTime))
INSERT [dbo].[TBKonfigurasiTomahawk] ([IDKonfigurasiTomahawk], [Urutan], [Nama], [DurasiProses], [TanggalTerakhirProses]) VALUES (4, 2, N'LaporanPenjualan', 1440, CAST(N'2016-06-22T08:00:00.000' AS DateTime))
INSERT [dbo].[TBKonfigurasiTomahawk] ([IDKonfigurasiTomahawk], [Urutan], [Nama], [DurasiProses], [TanggalTerakhirProses]) VALUES (6, 3, N'LaporanPOProduksi', 1440, CAST(N'2016-06-22T08:00:00.000' AS DateTime))
INSERT [dbo].[TBKonfigurasiTomahawk] ([IDKonfigurasiTomahawk], [Urutan], [Nama], [DurasiProses], [TanggalTerakhirProses]) VALUES (7, 4, N'UpdateDiscountSelesai', 1, CAST(N'2016-06-03T23:01:38.477' AS DateTime))
INSERT [dbo].[TBKonfigurasiTomahawk] ([IDKonfigurasiTomahawk], [Urutan], [Nama], [DurasiProses], [TanggalTerakhirProses]) VALUES (11, 5, N'UpdateDiscountMulai', 1, CAST(N'2016-06-03T23:01:38.477' AS DateTime))
INSERT [dbo].[TBKonfigurasiTomahawk] ([IDKonfigurasiTomahawk], [Urutan], [Nama], [DurasiProses], [TanggalTerakhirProses]) VALUES (14, 6, N'GenerateStoreKey', 1, CAST(N'2016-03-06T16:06:00.000' AS DateTime))
INSERT [dbo].[TBKonfigurasiTomahawk] ([IDKonfigurasiTomahawk], [Urutan], [Nama], [DurasiProses], [TanggalTerakhirProses]) VALUES (17, 7, N'SyncTransaksiMobile', 1, CAST(N'2000-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[TBKonfigurasiTomahawk] OFF
SET IDENTITY_INSERT [dbo].[TBKurir] ON 

INSERT [dbo].[TBKurir] ([IDKurir], [Nama], [Deskripsi], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (1, N'Kurir', N'', NULL, N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', NULL, CAST(N'2017-04-07T18:33:16.297' AS DateTime), 1, 1, CAST(N'2017-04-07T18:33:16.297' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TBKurir] OFF
SET IDENTITY_INSERT [dbo].[TBLogPengguna] ON 

INSERT [dbo].[TBLogPengguna] ([IDLogPengguna], [IDLogPenggunaTipe], [IDPengguna], [Tanggal]) VALUES (5, 1, 1, CAST(N'2018-07-10T10:14:01.343' AS DateTime))
INSERT [dbo].[TBLogPengguna] ([IDLogPengguna], [IDLogPenggunaTipe], [IDPengguna], [Tanggal]) VALUES (6, 1, 1, CAST(N'2018-07-11T00:12:17.827' AS DateTime))
INSERT [dbo].[TBLogPengguna] ([IDLogPengguna], [IDLogPenggunaTipe], [IDPengguna], [Tanggal]) VALUES (7, 2, 1, CAST(N'2018-07-11T00:20:29.700' AS DateTime))
INSERT [dbo].[TBLogPengguna] ([IDLogPengguna], [IDLogPenggunaTipe], [IDPengguna], [Tanggal]) VALUES (8, 1, 1, CAST(N'2018-07-11T00:20:36.147' AS DateTime))
SET IDENTITY_INSERT [dbo].[TBLogPengguna] OFF
SET IDENTITY_INSERT [dbo].[TBLogPenggunaTipe] ON 

INSERT [dbo].[TBLogPenggunaTipe] ([IDLogPenggunaTipe], [Nama]) VALUES (1, N'Login ke aplikasi')
INSERT [dbo].[TBLogPenggunaTipe] ([IDLogPenggunaTipe], [Nama]) VALUES (2, N'Logout dari aplikasi')
SET IDENTITY_INSERT [dbo].[TBLogPenggunaTipe] OFF
SET IDENTITY_INSERT [dbo].[TBMeja] ON 

INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (1, 1, N'Without Table', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (2, 1, N'Take Away', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (3, 1, N'A1', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (4, 1, N'A2', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (5, 1, N'A3', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (6, 1, N'A4', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (7, 1, N'A5', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (8, 1, N'A6', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (9, 1, N'A7', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (10, 1, N'A8', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (11, 1, N'B1', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (12, 1, N'B2', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (13, 1, N'B3', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (14, 1, N'B4', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (15, 1, N'C1', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (16, 1, N'C2', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (17, 1, N'C3', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (18, 1, N'C4', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (19, 1, N'1', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (23, 1, N'2', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (24, 1, N'3', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (25, 1, N'4', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (26, 1, N'5', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (27, 1, N'6', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (28, 1, N'7', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (29, 1, N'8', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (30, 1, N'9', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (31, 1, N'10', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (32, 1, N'11', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (33, 1, N'12', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (34, 1, N'13', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (35, 1, N'14', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (36, 1, N'15', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (37, 1, N'16', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (38, 1, N'17', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (39, 1, N'18', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (40, 1, N'19', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (41, 1, N'20', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (42, 1, N'21', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (43, 1, N'22', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (44, 1, N'23', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (45, 1, N'24', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (47, 1, N'Rendy', 1, 1, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (48, 1, N'Banu', 1, 1, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (49, 1, N'Arie', 1, 1, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (50, 1, N'Eri', 1, 1, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (52, 1, N'25', 1, 0, 1)
INSERT [dbo].[TBMeja] ([IDMeja], [IDStatusMeja], [Nama], [JumlahKursi], [VIP], [Status]) VALUES (53, 1, N'26', 1, 0, 1)
SET IDENTITY_INSERT [dbo].[TBMeja] OFF
SET IDENTITY_INSERT [dbo].[TBMenu] ON 

INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (1, NULL, NULL, 1, N'icon-home', N'Beranda', N'/WITAdministrator/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (2, NULL, NULL, 2, N'icon-group', N'Pegawai', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (3, 2, NULL, 2, N'', N'Pegawai', N'/WITAdministrator/Pengguna/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (4, NULL, NULL, 3, N'icon-user', N'Pelanggan', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (5, 4, NULL, 1, N'', N'Grup', N'/WITAdministrator/Pelanggan/Grup/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (6, 4, NULL, 2, N'', N'Pelanggan', N'/WITAdministrator/Pelanggan/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (7, NULL, NULL, 5, N'icon-archive', N'Produk', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (8, 7, NULL, 2, N'', N'Produk', N'/WITAdministrator/Produk/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (9, 7, NULL, 1, N'', N'Kategori', N'/WITAdministrator/Produk/Kategori/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (11, 7, NULL, 5, N'', N'Pemilik Produk', N'/WITAdministrator/Produk/Pemilik/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (12, 7, NULL, 8, N'', N'Vendor', N'/WITAdministrator/Produk/Vendor/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (13, NULL, NULL, 7, N'icon-tasks', N'Stok Produk', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (15, NULL, NULL, 15, N'icon-shopping-cart', N'Point Of Sales', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (16, 15, NULL, 1, N'', N'Retail', N'/WITPointOfSales/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (17, 15, NULL, 2, N'', N'Transaksi', N'/WITPointOfSales/Transaksi.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (18, 15, NULL, 3, N'', N'Jenis Pembayaran', N'/WITAdministrator/JenisPembayaran/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (19, NULL, NULL, 8, N'icon-exchange', N'Aliran Stok Produk', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (20, 19, NULL, 3, N'', N'Perpindahan Stok', N'/WITReport/PerpindahanStok/Produk.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (21, 19, NULL, 1, N'', N'Transfer Produk', N'/WITWarehouse/Produk/Transfer/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (22, 19, NULL, 2, N'', N'Penerimaan Transfer', N'/WITWarehouse/Produk/Penerimaan/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (24, NULL, NULL, 18, N'icon-book', N'Laporan', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (34, NULL, NULL, 19, N'icon-map-marker', N'Store', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (35, 34, NULL, 1, N'', N'Store', N'/WITAdministrator/Store/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (36, 34, NULL, 4, N'', N'Lokasi', N'/WITAdministrator/Store/Tempat/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (37, NULL, NULL, 20, N'icon-key', N'Ubah Password', N'/WITAdministrator/Pengguna/Password.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (38, NULL, NULL, 21, N'icon-off', N'Logout', N'/WITAdministrator/Login.aspx?do=logout')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (39, 2, NULL, 1, N'', N'Grup', N'/WITAdministrator/Pengguna/Grup/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (40, NULL, NULL, 10, N'icon-archive', N'Bahan Baku', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (41, 40, NULL, 1, N'', N'Kategori', N'/WITAdministrator/BahanBaku/Kategori/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (42, 40, NULL, 2, N'', N'Bahan Baku', N'/WITAdministrator/BahanBaku/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (43, 40, NULL, 3, N'', N'Komposisi', N'/WITAdministrator/BahanBaku/Komposisi/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (44, 40, NULL, 4, N'', N'Produksi', N'/WITAdministrator/BahanBaku/Produksi/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (45, 40, NULL, 5, N'', N'Supplier', N'/WITAdministrator/BahanBaku/Supplier/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (46, 7, NULL, 4, N'', N'Komposisi', N'/WITAdministrator/Produk/Komposisi/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (47, 7, NULL, 4, N'', N'Produksi', N'/WITAdministrator/Produk/Produksi/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (48, NULL, NULL, 12, N'icon-list', N'PO Bahan Baku', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (49, 48, NULL, 1, N'', N'Purchase Order', N'/WITAdministrator/BahanBaku/PurchaseOrder/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (50, 48, NULL, 2, N'', N'Penerimaan', N'/WITAdministrator/BahanBaku/PurchaseOrder/Penerimaan/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (51, NULL, NULL, 8, N'icon-list', N'PO Produk', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (52, 51, NULL, 1, N'', N'Purchase Order', N'/WITAdministrator/Produk/PurchaseOrder/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (53, 51, NULL, 2, N'', N'Penerimaan', N'/WITAdministrator/Produk/PurchaseOrder/Penerimaan/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (54, NULL, NULL, 12, N'icon-tasks', N'Stok Bahan Baku', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (55, 54, NULL, 1, N'', N'Stok', N'/WITAdministrator/BahanBaku/Stok/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (56, 54, NULL, 2, N'', N'Stock Opname', N'/WITAdministrator/BahanBaku/Stok/Pengaturan.aspx?do=opname')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (57, 54, NULL, 3, N'', N'Restock', N'/WITAdministrator/BahanBaku/Stok/Pengaturan.aspx?do=restock')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (58, 13, NULL, 1, N'', N'Stok', N'/WITWarehouse/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (66, NULL, NULL, 13, N'icon-exchange', N'Aliran Stok Bahan Baku', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (67, 66, NULL, 1, N'', N'Transfer Bahan Baku', N'/WITWarehouse/BahanBaku/Transfer/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (70, 66, NULL, 3, N'', N'Perpindahan Stok', N'/WITReport/PerpindahanStok/BahanBaku.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (83, 2, NULL, 4, N'', N'Menu', N'/WITAdministrator/Pengguna/Menu.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (84, 24, NULL, 3, N'', N'Transaksi', N'/WITReport/Transaksi/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (85, 24, NULL, 4, N'', N'Penjualan Produk', N'/WITReport/Transaksi/PenjualanProduk.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (86, 24, NULL, 1, N'', N'Ringkasan Penjualan', N'/WITReport/Transaksi/RingkasanPenjualan.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (87, 24, NULL, 6, N'', N'Rush Hour', N'/WITReport/Transaksi/RushHour.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (88, 7, NULL, 6, N'', N'Varian', N'/WITAdministrator/Produk/Atribut/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (89, 2, NULL, 5, N'', N'Konfigurasi', N'/WITAdministrator/Pengguna/Konfigurasi.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (91, NULL, NULL, 4, N'icon-money', N'Akuntansi', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (92, 91, NULL, 1, N'', N'Beranda', N'/WITAkuntansi/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (93, 91, NULL, 2, N'', N'Journal Entry', N'/WITAkuntansi/TransaksiKhusus.aspx?do=Journal Entry')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (94, 91, NULL, 3, N'', N'General Ledger', N'/WITAkuntansi/BukuBesar.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (95, 91, NULL, 4, N'', N'Income Statement', N'/WITAkuntansi/LabaRugi.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (96, 91, NULL, 5, N'', N'Balance Sheet', N'/WITAkuntansi/Neraca.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (97, 91, NULL, 6, N'', N'Journal History', N'/WITAkuntansi/JurnalUmum.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (98, 91, NULL, 7, N'', N'Chart of Account', N'/WITAkuntansi/Akun/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (99, 91, NULL, 8, N'', N'Transaksi Lainnya', N'/WITAkuntansi/TransaksiKhusus.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (100, 91, NULL, 10, N'', N'Buku Besar', N'/WITAkuntansi/BukuBesar.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (101, 91, NULL, 11, N'', N'Neraca Saldo', N'/WITAkuntansi/NeracaSaldo.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (102, 91, NULL, 12, N'', N'Laba Rugi', N'/WITAkuntansi/LabaRugi.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (103, 91, NULL, 13, N'', N'Perubahan Modal', N'/WITAkuntansi/ModalPerubahan.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (104, 91, NULL, 14, N'', N'Neraca', N'/WITAkuntansi/Neraca.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (105, 91, NULL, 15, N'', N'Arus Kas', N'/WITAkuntansi/ArusKas.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (106, 91, NULL, 16, N'', N'Jurnal Umum', N'/WITAkuntansi/JurnalUmum.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (107, 91, NULL, 17, N'', N'Daftar Hutang', N'/WITAkuntansi/JurnalHutang.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (108, 91, NULL, 18, N'', N'Daftar Piutang', N'/WITAkuntansi/JurnalPiutang.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (109, 91, NULL, 19, N'', N'Akun', N'/WITAkuntansi/Akun/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (110, 91, NULL, 9, N'', N'Import', N'/WITAkuntansi/Import.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (111, 7, NULL, 3, N'', N'Discount', N'/WITAdministrator/Produk/Discount.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (112, 7, NULL, 7, N'', N'Warna', N'/WITAdministrator/Produk/Warna/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (115, 13, NULL, 7, N'', N'Minimum Stok Produk', N'/WITAdministrator/Produk/Stok/Minimum.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (116, 24, NULL, 5, N'', N'Jenis Pembayaran', N'/WITReport/Transaksi/JenisPembayaran/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (117, 34, NULL, 2, N'', N'Konfigurasi', N'/WITAdministrator/Store/Konfigurasi.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (118, 2, NULL, 3, N'', N'Pindah Lokasi', N'/WITAdministrator/Pengguna/PindahLokasi.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (119, 13, NULL, 8, N'', N'Kondisi Stok', N'/WITAdministrator/Produk/Stok/Kondisi.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (120, 34, NULL, 3, N'', N'Konfigurasi Printer', N'/WITAdministrator/Store/Printer.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (121, 34, NULL, 5, N'', N'Template Keterangan', N'/WITAdministrator/Store/Keterangan/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (122, 19, NULL, 5, N'', N'Keluar Masuk', N'/WITReport/PerpindahanStok/ProdukKeluarMasuk.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (123, 19, NULL, 4, N'', N'Perpindahan Stok Detail', N'/WITReport/PerpindahanStok/ProdukDetail.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (124, 54, NULL, 4, N'', N'Retur ke Tempat Produksi', N'/WITAdministrator/BahanBaku/Stok/Pengaturan.aspx?do=return')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (125, 54, NULL, 5, N'', N'Bertambah', N'/WITAdministrator/BahanBaku/Stok/Bertambah.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (126, 54, NULL, 5, N'', N'Pembuangan Produk Rusak', N'/WITAdministrator/BahanBaku/Stok/Pengaturan.aspx?do=waste')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (127, 66, NULL, 2, N'', N'Penerimaan Transfer', N'/WITWarehouse/BahanBaku/Penerimaan/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (128, 7, NULL, 5, N'', N'Produksi', N'/WITAdministrator/Produk/ProduksiDenganPO/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (129, 24, NULL, 7, N'', N'Laporan Detail', N'/WITReport/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (130, 24, NULL, 2, N'', N'Net Revenue', N'/WITReport/NetRevenue/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (132, NULL, NULL, 17, N'icon-shopping-cart', N'Wholesale', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (133, 132, NULL, 1, N'', N'Retail', N'/WITWholesale/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (134, 132, NULL, 2, N'', N'Transaksi', N'/WITWholesale/Transaksi.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (135, 13, NULL, 3, N'', N'Stock Opname', N'/WITAdministrator/Produk/Stok/Pengaturan.aspx?do=opname')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (136, 13, NULL, 4, N'', N'Restock', N'/WITAdministrator/Produk/Stok/Pengaturan.aspx?do=restock')
GO
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (137, 13, NULL, 5, N'', N'Retur ke Tempat Produksi', N'/WITAdministrator/Produk/Stok/Pengaturan.aspx?do=return')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (138, 13, NULL, 6, N'', N'Pembuangan Produk Rusak', N'/WITAdministrator/Produk/Stok/Pengaturan.aspx?do=waste')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (139, 13, NULL, 2, N'', N'Multistore', N'/WITWarehouse/Multistore.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (140, 34, NULL, 6, N'', N'Menu', N'/WITAdministrator/Menu/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (141, NULL, NULL, 16, N'icon-shopping-cart', N'Marketing', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (142, 141, NULL, 1, N'', N'Retail', N'/WITMarketing/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (143, 141, NULL, 2, N'', N'Transaksi', N'/WITMarketing/Transaksi.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (144, 66, NULL, 4, N'', N'Perpindahan Stok Detail', N'/WITReport/PerpindahanStok/BahanBakuDetail.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (145, 66, NULL, 5, N'', N'Keluar Masuk', N'/WITReport/PerpindahanStok/BahanBakuKeluarMasuk.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (146, NULL, NULL, 6, N'icon-list', N'Produksi Produk', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (147, NULL, NULL, 11, N'icon-list', N'Produksi Bahan Baku', N'')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (148, 146, NULL, 1, N'', N'Proyeksi', N'/WITAdministrator/Produk/Proyeksi/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (149, 146, NULL, 2, N'', N'Purchase Order', N'/WITAdministrator/Produk/POProduksi/PurchaseOrder/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (150, 146, NULL, 3, N'', N'In-House Production', N'/WITAdministrator/Produk/POProduksi/ProduksiSendiri/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (154, 146, NULL, 4, N'', N'Production to Vendor', N'/WITAdministrator/Produk/POProduksi/ProduksiKeVendor/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (155, 147, NULL, 1, N'', N'Purchase Order', N'/WITAdministrator/BahanBaku/POProduksi/PurchaseOrder/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (156, 147, NULL, 2, N'', N'In-House Production', N'/WITAdministrator/BahanBaku/POProduksi/ProduksiSendiri/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (157, 147, NULL, 3, N'', N'Production to Supplier', N'/WITAdministrator/BahanBaku/POProduksi/ProduksiKeSupplier/Default.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (158, 2, NULL, 6, N'', N'Role', N'/WITAdministrator/Pengguna/Role.aspx')
INSERT [dbo].[TBMenu] ([IDMenu], [IDMenuParent], [IDMenuGrup], [Urutan], [Icon], [Nama], [Url]) VALUES (159, 91, NULL, 1, NULL, N'Begining Balance', N'/WITAkuntansi/SaldoAwal.aspx')
SET IDENTITY_INSERT [dbo].[TBMenu] OFF
SET IDENTITY_INSERT [dbo].[TBMenubar] ON 

INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (127, NULL, 1, 1, 1, N'', N'Beranda', N'/WITAdministrator/Default.aspx', N'home')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (128, NULL, 1, 1, 26, N'', N'Ubah Password', N'/WITAdministrator/Pengguna/Password.aspx', N'shuffle')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (129, NULL, 1, 1, 27, N'', N'Logout', N'/Login.aspx?do=logout', N'log-out')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (131, NULL, 1, 1, 2, N'', N'User', N'', N'users')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (132, 131, 1, 2, 1, N'', N'Pegawai', N'/WITAdministrator/Pengguna/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (133, 131, 1, 2, 3, N'', N'Pindah Lokasi', N'/WITAdministrator/Pengguna/PindahLokasi.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (134, 131, 1, 2, 4, N'', N'Hak Akses', N'/WITAdministrator/Pengguna/Role.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (135, 131, 1, 2, 5, N'', N'Konfigurasi', N'/WITAdministrator/Pengguna/Konfigurasi.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (137, 131, 1, 2, 2, N'', N'Pelanggan', N'/WITAdministrator/Pelanggan/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (138, NULL, 1, 1, 4, N'', N'Akuntansi', N'', N'dollar-sign')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (139, 138, 1, 2, 1, N'', N'Begining Balance', N'/WITAkuntansi/SaldoAwal.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (140, 138, 1, 2, 2, N'', N'Journal Entry', N'/WITAkuntansi/TransaksiKhusus.aspx?do=JournalEntry', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (141, 138, 1, 2, 3, N'', N'General Ledger', N'/WITAkuntansi/BukuBesar.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (142, 138, 1, 2, 4, N'', N'Income Statement', N'/WITAkuntansi/LabaRugi.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (143, 138, 1, 2, 5, N'', N'Balance Sheet', N'/WITAkuntansi/Neraca.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (144, 138, 1, 2, 6, N'', N'Journal History', N'/WITAkuntansi/JurnalUmum.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (145, 138, 1, 2, 7, N'', N'Chart of Account', N'/WITAkuntansi/Akun/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (146, NULL, 1, 1, 5, N'', N'Bahan Baku', N'', N'package')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (147, 146, 1, 2, 1, N'', N'Bahan Baku', N'/WITAdministrator/BahanBaku/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (148, 146, 1, 2, 2, N'', N'Komposisi', N'/WITAdministrator/BahanBaku/Komposisi/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (149, NULL, 1, 1, 6, N'', N'Stok Bahan Baku', N'', N'box')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (150, 149, 1, 2, 1, N'', N'Stok', N'/WITAdministrator/BahanBaku/Stok/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (151, 149, 1, 2, 7, N'', N'Kelola Stok', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (152, 151, 1, 3, 1, N'', N'Stock Opname', N'/WITAdministrator/BahanBaku/Stok/Pengaturan.aspx?do=opname', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (153, 151, 1, 3, 2, N'', N'Restock', N'/WITAdministrator/BahanBaku/Stok/Pengaturan.aspx?do=restock', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (154, 151, 1, 3, 3, N'', N'Retur ke Tempat Produksi', N'/WITAdministrator/BahanBaku/Stok/Pengaturan.aspx?do=return', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (155, 151, 1, 3, 4, N'', N'Pembuangan Produk Rusak', N'/WITAdministrator/BahanBaku/Stok/Pengaturan.aspx?do=waste', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (156, 149, 1, 2, 8, N'', N'Produksi', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (157, 156, 1, 3, 1, N'', N'Purchase Order', N'/WITAdministrator/BahanBaku/POProduksi/PurchaseOrder/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (158, 156, 1, 3, 2, N'', N'In-House Production', N'/WITAdministrator/BahanBaku/POProduksi/ProduksiSendiri/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (159, 156, 1, 3, 3, N'', N'Production to Supplier', N'/WITAdministrator/BahanBaku/POProduksi/ProduksiKeSupplier/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (160, 149, 1, 2, 9, N'', N'Transfer', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (161, 160, 1, 3, 1, N'', N'Kirim', N'/WITAdministrator/BahanBaku/Transfer/Kirim.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (162, 160, 1, 3, 2, N'', N'Terima', N'/WITAdministrator/BahanBaku/Transfer/Terima.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (163, NULL, 1, 1, 10, N'', N'Produk', N'', N'archive')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (164, 163, 1, 2, 1, N'', N'Produk', N'/WITAdministrator/Produk/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (165, 163, 1, 2, 2, N'', N'Komposisi', N'/WITAdministrator/Produk/Komposisi/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (166, 163, 1, 2, 3, N'', N'Discount', N'/WITAdministrator/Produk/Discount.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (167, NULL, 1, 1, 11, N'', N'Stok Produk', N'', N'shopping-bag')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (168, 167, 1, 2, 1, N'', N'Stok', N'/WITAdministrator/Produk/Stok/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (169, 167, 1, 2, 2, N'', N'Multistore', N'/WITAdministrator/Produk/Stok/Multistore.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (170, 167, 1, 2, 12, N'', N'Kelola Stok', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (171, 170, 1, 3, 1, N'', N'Stock Opname', N'/WITAdministrator/Produk/Stok/Pengaturan.aspx?do=opname', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (172, 170, 1, 3, 2, N'', N'Restock', N'/WITAdministrator/Produk/Stok/Pengaturan.aspx?do=restock', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (173, 170, 1, 3, 3, N'', N'Retur ke Tempat Produksi', N'/WITAdministrator/Produk/Stok/Pengaturan.aspx?do=return', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (174, 170, 1, 3, 4, N'', N'Pembuangan Produk Rusak', N'/WITAdministrator/Produk/Stok/Pengaturan.aspx?do=waste', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (175, 170, 1, 3, 5, N'', N'Minimum Stok Produk', N'/WITAdministrator/Produk/Stok/Minimum.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (176, 170, 1, 3, 6, N'', N'Kondisi Stok', N'/WITAdministrator/Produk/Stok/Kondisi.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (177, 167, 1, 2, 13, N'', N'Produksi', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (178, 177, 1, 3, 1, N'', N'Proyeksi', N'/WITAdministrator/Produk/Proyeksi/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (179, 177, 1, 3, 2, N'', N'Purchase Order', N'/WITAdministrator/Produk/POProduksi/PurchaseOrder/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (180, 177, 1, 3, 3, N'', N'In-House Production', N'/WITAdministrator/Produk/POProduksi/ProduksiSendiri/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (181, 177, 1, 3, 4, N'', N'Production to Vendor', N'/WITAdministrator/Produk/POProduksi/ProduksiKeVendor/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (182, 167, 1, 2, 14, N'', N'Transfer', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (183, 182, 1, 3, 1, N'', N'Kirim', N'/WITAdministrator/Produk/Transfer/Kirim.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (184, 182, 1, 3, 2, N'', N'Terima', N'/WITAdministrator/Produk/Transfer/Terima.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (185, NULL, 1, 1, 15, N'', N'Point Of Sales', N'', N'shopping-cart')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (186, 185, 1, 2, 1, N'', N'Jenis Pembayaran', N'/WITAdministrator/JenisPembayaran/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (187, 185, 1, 2, 16, N'', N'Point Of Sales', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (188, 187, 1, 3, 1, N'', N'Retail', N'/WITPointOfSales/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (189, 187, 1, 3, 2, N'', N'Restaurant', N'/WITRestaurant/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (190, 187, 1, 3, 3, N'', N'Transaksi', N'/WITPointOfSales/Transaksi.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (191, 185, 1, 2, 17, N'', N'Marketing', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (192, 191, 1, 3, 1, N'', N'Marketing', N'/WITPointOfSales/Marketing/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (193, 191, 1, 3, 2, N'', N'Transaksi', N'/WITPointOfSales/Marketing/Transaksi.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (194, 185, 1, 2, 18, N'', N'Wholesale', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (195, 194, 1, 3, 1, N'', N'Wholesale', N'/WITPointOfSales/Wholesale/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (196, 194, 1, 3, 2, N'', N'Transaksi', N'/WITPointOfSales/Wholesale/Transaksi.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (197, 232, 1, 2, 19, N'', N'Summary', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (198, 197, 1, 3, 1, N'', N'PO Bahan Baku', N'/WITReport/Supplier/Ringkasan.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (199, 197, 1, 3, 2, N'', N'PO Produk', N'/WITReport/Vendor/Ringkasan.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (200, 197, 1, 3, 3, N'', N'Meja', N'/WITReport/Master/Meja.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (201, 197, 1, 3, 4, N'', N'Sales', N'/WITReport/Transaksi/RingkasanPenjualan.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (202, 197, 1, 3, 5, N'', N'Revenue', N'/WITReport/Transaksi/Ringkasan.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (203, 197, 1, 3, 6, N'', N'Top Produk', N'/WITReport/Top/ProdukVarian.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (204, 232, 1, 2, 20, N'', N'Stok', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (205, 204, 1, 3, 1, N'', N'Bahan Baku', N'/WITReport/PerpindahanStok/BahanBaku.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (206, 204, 1, 3, 2, N'', N'Produk', N'/WITReport/PerpindahanStok/Produk.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (207, 232, 1, 2, 21, N'', N'Purchase', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (208, 207, 1, 3, 1, N'', N'Performa', N'/WITReport/POProduksi/PerformancePengguna.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (209, 207, 1, 3, 2, N'', N'Proyeksi', N'/WITReport/Proyeksi/Proyeksi.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (210, 207, 1, 3, 3, N'', N'Bahan Baku', N'/WITReport/Supplier/PurchaseOrder.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (211, 207, 1, 3, 4, N'', N'Produk', N'/WITReport/Vendor/PurchaseOrder.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (212, 232, 1, 2, 22, N'', N'Penjualan', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (213, 212, 1, 3, 1, N'', N'Rush Hour', N'/WITReport/Transaksi/RushHour.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (214, 212, 1, 3, 2, N'', N'Transaksi', N'/WITReport/Transaksi/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (215, 212, 1, 3, 3, N'', N'Consignment', N'/WITReport/Consignment/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (216, 212, 1, 3, 4, N'', N'Forecast', N'/WITReport/Forecast.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (217, 232, 1, 2, 23, N'', N'Trend Analysis', N'', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (218, 217, 1, 3, 1, N'', N'Kategori', N'/WITReport/SalesAnalysis/TrendAnalysisKategori.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (219, 217, 1, 3, 2, N'', N'Produk', N'/WITReport/SalesAnalysis/TrendAnalysisProduk.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (220, NULL, 1, 1, 24, N'', N'Store', N'', N'map-pin')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (221, 220, 1, 2, 1, N'', N'Store', N'/WITAdministrator/Store/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (222, 220, 1, 2, 2, N'', N'Lokasi', N'/WITAdministrator/Store/Tempat/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (225, NULL, 1, 1, 25, N'', N'Konfigurasi', N'', N'settings')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (226, 225, 1, 2, 1, N'', N'Menu', N'/WITAdministrator/Menubar/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (227, 225, 1, 2, 2, N'', N'Layout Meja', N'/WITAdministrator/Meja/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (228, 225, 1, 2, 3, N'', N'Printer Bar & Kitchen', N'/WITAdministrator/Store/Printer.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (229, 225, 1, 2, 4, N'', N'Konfigurasi', N'/WITAdministrator/Store/Konfigurasi.aspx', N'')
GO
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (230, 225, 1, 2, 5, N'', N'Modifier', N'/WITAdministrator/Store/Keterangan/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (231, 131, 1, 2, 6, N'', N'Pengaturan Manager', N'/WITAdministrator/Pengguna/Manager.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (232, NULL, 1, 1, 15, N'', N'Laporan', N'', N'activity')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (233, 156, 1, 3, 5, N'', N'Down Payment', N'/WITAdministrator/BahanBaku/POProduksi/DownPayment/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (234, 156, 1, 3, 6, N'', N'Invoice', N'/WITAdministrator/BahanBaku/POProduksi/Penagihan/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (235, 177, 1, 3, 6, N'', N'Down Payment', N'/WITAdministrator/Produk/POProduksi/DownPayment/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (236, 177, 1, 3, 7, N'', N'Invoice', N'/WITAdministrator/Produk/POProduksi/Penagihan/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (237, 156, 1, 3, 4, N'', N'Retur', N'/WITAdministrator/BahanBaku/POProduksi/Retur/Default.aspx', N'')
INSERT [dbo].[TBMenubar] ([IDMenubar], [IDMenubarParent], [EnumMenubarModul], [LevelMenu], [Urutan], [Kode], [Nama], [Url], [Icon]) VALUES (238, 177, 1, 3, 5, N'', N'Retur', N'/WITAdministrator/Produk/POProduksi/Retur/Default.aspx', N'')
SET IDENTITY_INSERT [dbo].[TBMenubar] OFF
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (127, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (127, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (128, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (128, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (129, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (131, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (131, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (132, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (132, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (133, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (133, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (134, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (134, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (135, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (135, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (137, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (137, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (138, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (138, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (139, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (139, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (140, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (140, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (141, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (141, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (142, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (142, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (143, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (143, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (144, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (144, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (145, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (145, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (146, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (146, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (147, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (147, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (148, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (148, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (149, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (149, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (150, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (150, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (151, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (151, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (152, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (152, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (153, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (153, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (154, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (154, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (155, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (155, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (156, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (156, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (157, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (157, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (158, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (158, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (159, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (159, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (160, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (160, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (161, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (161, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (162, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (162, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (163, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (163, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (164, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (164, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (165, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (165, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (166, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (166, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (167, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (167, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (168, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (168, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (169, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (169, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (170, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (170, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (171, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (171, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (172, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (172, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (173, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (173, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (174, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (174, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (175, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (175, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (176, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (176, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (177, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (177, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (178, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (178, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (179, 1)
GO
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (179, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (180, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (180, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (181, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (181, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (182, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (182, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (183, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (183, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (184, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (184, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (185, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (185, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (186, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (186, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (187, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (187, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (188, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (188, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (189, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (189, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (190, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (190, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (191, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (191, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (192, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (192, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (193, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (193, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (194, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (194, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (195, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (195, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (196, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (196, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (197, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (197, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (198, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (198, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (199, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (199, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (200, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (200, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (201, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (201, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (202, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (202, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (203, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (203, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (204, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (204, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (205, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (205, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (206, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (206, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (207, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (207, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (208, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (208, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (209, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (209, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (210, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (210, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (211, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (211, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (212, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (212, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (213, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (213, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (214, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (214, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (215, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (215, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (216, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (216, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (217, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (217, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (218, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (218, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (219, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (219, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (220, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (220, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (221, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (221, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (222, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (222, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (225, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (225, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (226, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (226, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (227, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (227, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (228, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (228, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (229, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (229, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (230, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (230, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (231, 1)
GO
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (231, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (232, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (232, 2)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (233, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (234, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (235, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (236, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (237, 1)
INSERT [dbo].[TBMenubarPenggunaGrup] ([IDMenubar], [IDGrupPengguna]) VALUES (238, 1)
SET IDENTITY_INSERT [dbo].[TBMenuGrup] ON 

INSERT [dbo].[TBMenuGrup] ([IDMenuGrup], [Nama]) VALUES (1, N'Backend')
INSERT [dbo].[TBMenuGrup] ([IDMenuGrup], [Nama]) VALUES (2, N'Point of Sales Retail')
INSERT [dbo].[TBMenuGrup] ([IDMenuGrup], [Nama]) VALUES (3, N'Point of Sales Restaurant')
INSERT [dbo].[TBMenuGrup] ([IDMenuGrup], [Nama]) VALUES (4, N'Warehouse')
INSERT [dbo].[TBMenuGrup] ([IDMenuGrup], [Nama]) VALUES (5, N'Report')
SET IDENTITY_INSERT [dbo].[TBMenuGrup] OFF
SET IDENTITY_INSERT [dbo].[TBNotifikasi] ON 

INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (6, N'e63a628a-bad2-4346-8e56-9f2538a56378', 1, 1, CAST(N'2018-07-10T10:10:19.870' AS DateTime), CAST(N'2018-07-10T10:14:02.257' AS DateTime), 3, N'Tambah Pelanggan  berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (7, N'6b54c4e3-d36b-4c48-bc3a-d9d12fa0d017', 1, 1, CAST(N'2018-07-10T10:14:45.400' AS DateTime), CAST(N'2018-07-10T10:14:46.803' AS DateTime), 3, N'Tambah Kategori Produk Baju berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (8, N'a8be9649-b2b9-48d7-adf1-bc1ee36283a7', 1, 1, CAST(N'2018-07-10T10:14:45.430' AS DateTime), CAST(N'2018-07-10T10:14:46.803' AS DateTime), 3, N'Tambah Warna Putih berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (9, N'98890da6-872b-41de-8351-f410fb83ccba', 1, 1, CAST(N'2018-07-10T10:14:45.450' AS DateTime), CAST(N'2018-07-10T10:14:46.803' AS DateTime), 3, N'Tambah Pemilik Produk WIT. berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (10, N'1536f508-ad53-4559-b7f7-f1e7a449b123', 1, 1, CAST(N'2018-07-10T10:14:45.467' AS DateTime), CAST(N'2018-07-10T10:14:46.803' AS DateTime), 3, N'Tambah Produk Best Clohtes White berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (11, N'257c4e9c-d014-4ec5-9147-ca015305e832', 1, 1, CAST(N'2018-07-10T10:14:45.547' AS DateTime), CAST(N'2018-07-10T10:14:46.803' AS DateTime), 3, N'Tambah Grup Atribut Produk  berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (12, N'46ff307a-f9c4-4b41-a5d2-6ea286d58e49', 1, 1, CAST(N'2018-07-10T10:14:45.557' AS DateTime), CAST(N'2018-07-10T10:14:51.467' AS DateTime), 3, N'Tambah Atribut Produk S berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (13, N'9b05d033-5d50-46e6-a5f2-5c5498111ab2', 1, 1, CAST(N'2018-07-10T10:14:45.787' AS DateTime), CAST(N'2018-07-10T10:14:51.467' AS DateTime), 3, N'Ubah Produk Best Clohtes White berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (14, N'ce11d704-dfbd-4762-9ebc-2ad2aa6419e8', 1, 1, CAST(N'2018-07-10T10:14:45.830' AS DateTime), CAST(N'2018-07-10T10:14:51.467' AS DateTime), 3, N'Tambah Atribut Produk M berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (15, N'b38d4ab1-e2bc-44a6-9019-7c66ea772fba', 1, 1, CAST(N'2018-07-10T10:14:45.873' AS DateTime), CAST(N'2018-07-10T10:14:51.467' AS DateTime), 3, N'Ubah Produk Best Clohtes White berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (16, N'b845f3a2-99f2-4025-9078-d501612a0365', 1, 1, CAST(N'2018-07-10T10:14:45.883' AS DateTime), CAST(N'2018-07-10T10:14:51.467' AS DateTime), 3, N'Tambah Atribut Produk L berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (17, N'25b24b14-fbdd-41ae-b7ba-00f2685b08ba', 1, 1, CAST(N'2018-07-10T10:14:45.917' AS DateTime), CAST(N'2018-07-10T10:15:47.043' AS DateTime), 3, N'Ubah Produk Best Clohtes White berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (18, N'00690c72-4aa8-457b-b8ac-d52bb2f7c31b', 1, 1, CAST(N'2018-07-10T10:14:45.930' AS DateTime), CAST(N'2018-07-10T10:15:47.043' AS DateTime), 3, N'Tambah Atribut Produk XL berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (19, N'4f4da9bd-f189-4ad0-a79d-f7129527cece', 1, 1, CAST(N'2018-07-10T10:14:45.963' AS DateTime), CAST(N'2018-07-10T10:15:47.043' AS DateTime), 3, N'Tambah Kategori Produk Kemeja berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (20, N'88f4a585-9029-4711-bf4a-ce2206546eec', 1, 1, CAST(N'2018-07-10T10:14:45.973' AS DateTime), CAST(N'2018-07-10T10:15:47.043' AS DateTime), 3, N'Tambah Warna Hitam berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (21, N'41342b6e-b373-43e6-8400-e8835b76e9d6', 1, 1, CAST(N'2018-07-10T10:14:45.997' AS DateTime), CAST(N'2018-07-10T10:15:47.043' AS DateTime), 3, N'Tambah Produk Simple Shirt Black berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (22, N'0985241c-8e65-4e15-8c8e-05e58d70c910', 1, 1, CAST(N'2018-07-10T10:14:46.083' AS DateTime), CAST(N'2018-07-10T10:15:51.443' AS DateTime), 3, N'Tambah Produk Simple Shirt berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (23, N'94227098-00ae-4772-ac46-6f0fbf24bc73', 1, 1, CAST(N'2018-07-10T10:14:46.117' AS DateTime), CAST(N'2018-07-10T10:15:51.443' AS DateTime), 3, N'Ubah Produk Simple Shirt berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (24, N'd35713d1-0e4e-4415-b051-979afa53d6c9', 1, 1, CAST(N'2018-07-10T10:14:46.153' AS DateTime), CAST(N'2018-07-10T10:15:51.443' AS DateTime), 3, N'Ubah Produk Simple Shirt berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (25, N'48e91ebb-5cb4-4429-a3a6-eb27bff4cb90', 1, 1, CAST(N'2018-07-10T10:14:46.183' AS DateTime), CAST(N'2018-07-10T10:15:51.443' AS DateTime), 3, N'Tambah Kategori Produk Jaket berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (26, N'ab536250-a812-4474-9e7d-d2471d3a9558', 1, 1, CAST(N'2018-07-10T10:14:46.183' AS DateTime), CAST(N'2018-07-10T10:15:51.443' AS DateTime), 3, N'Tambah Warna Biru berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (27, N'1fe2549f-5865-46e6-a813-583a9f32f20f', 1, 1, CAST(N'2018-07-10T10:14:46.187' AS DateTime), CAST(N'2018-07-10T10:16:08.173' AS DateTime), 3, N'Tambah Produk Bomber Jacket Blue berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (28, N'7ff990e4-bc36-492f-b567-f87ed029d98b', 1, 1, CAST(N'2018-07-10T10:14:46.227' AS DateTime), CAST(N'2018-07-10T10:16:08.173' AS DateTime), 3, N'Ubah Produk Bomber Jacket Blue berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (29, N'3c10b32c-c6bf-4ffb-b732-fbbc315b8f46', 1, 1, CAST(N'2018-07-10T10:14:46.253' AS DateTime), CAST(N'2018-07-10T10:16:08.173' AS DateTime), 3, N'Ubah Produk Bomber Jacket Blue berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (30, N'a2552e1d-dc5f-4510-b858-1a69a7e22354', 1, 1, CAST(N'2018-07-10T10:14:46.283' AS DateTime), CAST(N'2018-07-10T10:16:08.173' AS DateTime), 3, N'Tambah Kategori Produk Celana berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (31, N'16af5b47-8b6a-4a92-bd8f-db99462a5909', 1, 1, CAST(N'2018-07-10T10:14:46.287' AS DateTime), CAST(N'2018-07-10T10:16:08.173' AS DateTime), 3, N'Tambah Warna Coklat berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (32, N'b523749f-c480-47cf-a6e5-2a24231843b8', 1, 1, CAST(N'2018-07-10T10:14:46.290' AS DateTime), CAST(N'2018-07-10T10:16:08.423' AS DateTime), 3, N'Tambah Produk Cino Pant Brown berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (33, N'd8ec837b-cb0d-4c52-8cc1-7385656b35f5', 1, 1, CAST(N'2018-07-10T10:14:46.307' AS DateTime), CAST(N'2018-07-10T10:16:08.423' AS DateTime), 3, N'Tambah Atribut Produk 28 berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (34, N'3ec4bbe7-ab0a-48ae-aa3e-54a863f603a0', 1, 1, CAST(N'2018-07-10T10:14:46.347' AS DateTime), CAST(N'2018-07-10T10:16:08.423' AS DateTime), 3, N'Ubah Produk Cino Pant Brown berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (35, N'6fe7ece0-9aa1-4d1a-98de-4310ec96edac', 1, 1, CAST(N'2018-07-10T10:14:46.360' AS DateTime), CAST(N'2018-07-10T10:16:08.423' AS DateTime), 3, N'Tambah Atribut Produk 29 berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (36, N'2399d39e-b756-4d72-aea5-205bcd1aa37b', 1, 1, CAST(N'2018-07-10T10:14:46.383' AS DateTime), CAST(N'2018-07-10T10:16:08.423' AS DateTime), 3, N'Ubah Produk Cino Pant Brown berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (37, N'12cdce85-1a61-4cb1-964e-8c26a56502b1', 1, 1, CAST(N'2018-07-10T10:14:46.397' AS DateTime), CAST(N'2018-07-10T10:16:08.667' AS DateTime), 3, N'Tambah Atribut Produk 30 berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (38, N'321924e6-e661-4213-8c05-c657fd0d26fe', 1, 1, CAST(N'2018-07-10T10:14:46.420' AS DateTime), CAST(N'2018-07-10T10:16:08.667' AS DateTime), 3, N'Ubah Produk Cino Pant Brown berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (39, N'1dcc37b7-1cb2-4046-9e90-466a32f3ac77', 1, 1, CAST(N'2018-07-10T10:14:46.437' AS DateTime), CAST(N'2018-07-10T10:16:08.667' AS DateTime), 3, N'Tambah Atribut Produk 31 berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (40, N'e1a2e28c-4680-4831-8ead-d127fe8198f6', 1, 1, CAST(N'2018-07-10T10:14:46.467' AS DateTime), CAST(N'2018-07-10T10:16:08.667' AS DateTime), 3, N'Ubah Produk Cino Pant Brown berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (41, N'1f159de0-d947-4260-b38c-7cd783503ad3', 1, 1, CAST(N'2018-07-10T10:14:46.483' AS DateTime), CAST(N'2018-07-10T10:16:08.667' AS DateTime), 3, N'Tambah Atribut Produk 32 berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (42, N'1f7ae6dd-5b76-423f-962b-10a39cc6e19f', 1, 1, CAST(N'2018-07-10T10:14:46.520' AS DateTime), CAST(N'2018-07-10T10:16:08.883' AS DateTime), 3, N'Tambah Kategori Produk Sepatu berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (43, N'c1b4c2be-b074-44ab-81b0-cc88015bfe30', 1, 1, CAST(N'2018-07-10T10:14:46.540' AS DateTime), CAST(N'2018-07-10T10:16:08.883' AS DateTime), 3, N'Tambah Produk Boot Shoes Black berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (44, N'5a4046a3-d37d-4653-b174-533bccec6a84', 1, 1, CAST(N'2018-07-10T10:14:46.573' AS DateTime), CAST(N'2018-07-10T10:16:08.883' AS DateTime), 3, N'Tambah Atribut Produk 40 berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (45, N'c69a84c2-6e7c-45e2-a90b-8300fffb431b', 1, 1, CAST(N'2018-07-10T10:14:46.627' AS DateTime), CAST(N'2018-07-10T10:16:08.883' AS DateTime), 3, N'Ubah Produk Boot Shoes Black berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (46, N'0bfed34a-37ab-47a9-be98-93bb856275ac', 1, 1, CAST(N'2018-07-10T10:14:46.637' AS DateTime), CAST(N'2018-07-10T10:16:08.883' AS DateTime), 3, N'Tambah Atribut Produk 41 berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (47, N'6273005d-3005-4ba1-a4a7-68739b01c01c', 1, 1, CAST(N'2018-07-10T10:14:46.667' AS DateTime), CAST(N'2018-07-10T10:16:09.083' AS DateTime), 3, N'Ubah Produk Boot Shoes Black berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (48, N'360fc2d9-55e8-472e-92fd-c3d228f013f5', 1, 1, CAST(N'2018-07-10T10:14:46.680' AS DateTime), CAST(N'2018-07-10T10:16:09.083' AS DateTime), 3, N'Tambah Atribut Produk 42 berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (49, N'c42c1369-db08-4057-8027-d0cb1aa4e7e8', 1, 1, CAST(N'2018-07-10T10:14:46.713' AS DateTime), CAST(N'2018-07-10T10:16:09.083' AS DateTime), 3, N'Ubah Produk Boot Shoes Black berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (50, N'd7ab2b38-2ef9-4981-a0c0-c9d82954aff0', 1, 1, CAST(N'2018-07-10T10:14:46.723' AS DateTime), CAST(N'2018-07-10T10:16:09.083' AS DateTime), 3, N'Tambah Atribut Produk 43 berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (51, N'ba8fb008-b5f0-4d7a-aee6-37c68fd44df7', 1, 1, CAST(N'2018-07-10T10:14:46.757' AS DateTime), CAST(N'2018-07-10T10:16:09.083' AS DateTime), 3, N'Ubah Produk Boot Shoes Black berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (52, N'ad882f62-34b3-4086-83eb-086421551064', 1, 1, CAST(N'2018-07-10T10:14:46.767' AS DateTime), CAST(N'2018-07-10T10:16:10.693' AS DateTime), 3, N'Tambah Atribut Produk 44 berhasil')
INSERT [dbo].[TBNotifikasi] ([IDNotifikasi], [IDWMS], [IDPenggunaPengirim], [IDPenggunaPenerima], [TanggalDaftar], [TanggalDibaca], [EnumAlert], [Isi]) VALUES (53, N'03c3ad0e-dd7f-4517-af8f-3d8d2e7dd4c2', 1, 1, CAST(N'2018-07-11T00:20:24.710' AS DateTime), CAST(N'2018-07-11T00:20:24.823' AS DateTime), 3, N'Ubah Tempat Bandung berhasil')
SET IDENTITY_INSERT [dbo].[TBNotifikasi] OFF
SET IDENTITY_INSERT [dbo].[TBPelanggan] ON 

INSERT [dbo].[TBPelanggan] ([IDPelanggan], [IDGrupPelanggan], [IDPenggunaPIC], [NamaLengkap], [Username], [Password], [Email], [Handphone], [TeleponLain], [TanggalLahir], [TanggalDaftar], [Deposit], [Catatan], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (1, 1, 1, N'General Customer', N'-', N'-', N'test@test.com', N'0', N'0', CAST(N'0001-01-01' AS Date), CAST(N'2013-10-30T13:51:12.000' AS DateTime), 0.0000, N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'2f71e44d-c92a-4ae5-8801-3aa59b99bef5', NULL, CAST(N'2017-01-01T01:45:44.473' AS DateTime), 1, 1, CAST(N'2017-01-01T01:45:44.473' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBPelanggan] ([IDPelanggan], [IDGrupPelanggan], [IDPenggunaPIC], [NamaLengkap], [Username], [Password], [Email], [Handphone], [TeleponLain], [TanggalLahir], [TanggalDaftar], [Deposit], [Catatan], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (6, 3, 1, N'', N'', N'', N'', N'', N'', CAST(N'2018-07-10' AS Date), CAST(N'2018-07-10T10:10:19.870' AS DateTime), 0.0000, N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'ce8d6eda-302c-42b5-800a-ffa7d69734af', 2, CAST(N'2018-07-10T10:10:19.870' AS DateTime), 1, 1, CAST(N'2018-07-10T10:10:19.870' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TBPelanggan] OFF
SET IDENTITY_INSERT [dbo].[TBPemilikProduk] ON 

INSERT [dbo].[TBPemilikProduk] ([IDPemilikProduk], [Nama], [Alamat], [Email], [Telepon1], [Telepon2], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (5, N'WIT.', N'', N'', N'', N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'1555d7f1-2d6f-488a-841f-abf535561eb8', 1, CAST(N'2018-07-10T10:14:45.453' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.453' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TBPemilikProduk] OFF
SET IDENTITY_INSERT [dbo].[TBPengguna] ON 

INSERT [dbo].[TBPengguna] ([IDPengguna], [IDPenggunaParent], [IDGrupPengguna], [IDTempat], [NomorIdentitas], [NomorNPWP], [NamaLengkap], [NomorRekening], [NamaRekening], [NamaBank], [TempatLahir], [TanggalLahir], [JenisKelamin], [Alamat], [Agama], [Telepon], [Handphone], [Email], [StatusPerkawinan], [Kewarganegaraan], [PendidikanTerakhir], [TanggalBekerja], [Username], [Password], [PIN], [SidikJari], [RFID], [EkstensiFoto], [GajiPokok], [TunjanganTransportasi], [TunjanganMakan], [TunjanganHariRaya], [JaminanKecelakaan], [JaminanHariTua], [PPH21], [Catatan], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (1, NULL, 1, 1, N'', N'', N'Rendy Herdiawan', N'', N'', N'', N'', CAST(N'1991-04-08' AS Date), 1, N'', N'', N'', N'', N'herdiawan.rendy@gmail.com', N'', N'', N'', CAST(N'2010-10-30' AS Date), N'herdiawanrendy', N'rendyrasta12345*', N'batter99', N'', N'', N'', 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'44e4cfde-2d53-4ccf-9d83-9b54788537cd', NULL, CAST(N'2017-01-01T02:11:08.917' AS DateTime), 1, 1, CAST(N'2017-01-01T02:11:08.917' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBPengguna] ([IDPengguna], [IDPenggunaParent], [IDGrupPengguna], [IDTempat], [NomorIdentitas], [NomorNPWP], [NamaLengkap], [NomorRekening], [NamaRekening], [NamaBank], [TempatLahir], [TanggalLahir], [JenisKelamin], [Alamat], [Agama], [Telepon], [Handphone], [Email], [StatusPerkawinan], [Kewarganegaraan], [PendidikanTerakhir], [TanggalBekerja], [Username], [Password], [PIN], [SidikJari], [RFID], [EkstensiFoto], [GajiPokok], [TunjanganTransportasi], [TunjanganMakan], [TunjanganHariRaya], [JaminanKecelakaan], [JaminanHariTua], [PPH21], [Catatan], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (2, NULL, 10, 1, N'', N'', N'Pengguna 1', N'', N'', N'', N'', CAST(N'1991-04-08' AS Date), 1, N'Alamat 1', N'', N'', N'123456789012', N'', N'', N'', N'', CAST(N'2014-05-03' AS Date), N'Username 1', N'Password 1', N'8888', N'', N'', N'', 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'7bc0a060-2ab3-4c1d-bf3a-cfa00aeaf428', NULL, CAST(N'2017-01-01T02:11:08.917' AS DateTime), 1, 1, CAST(N'2017-10-09T11:32:40.203' AS DateTime), 5, 2, 1)
SET IDENTITY_INSERT [dbo].[TBPengguna] OFF
SET IDENTITY_INSERT [dbo].[TBPengirimanEmail] ON 

INSERT [dbo].[TBPengirimanEmail] ([IDPengirimanEmail], [TanggalKirim], [Tujuan], [Judul], [Isi]) VALUES (2259, CAST(N'2017-01-30' AS Date), N'herdiawan.rendy@gmail.com', N'Store Key WIT.', N'Dear All,<br/><br/>Berikut Store Key untuk WIT. : <br/><br/><b>30 Januari 2017</b><br/>c5e22136-2777-4486-9f1a-e11dd1b7e18f<br/><br/><b>28 Februari 2017</b><br/>3cb93e02-f076-424c-903c-c56286bffa94<br/><br/><b>28 Maret 2017</b><br/>a3ed7838-2bea-4588-9f64-cdf7c35be345<br/><br/><b>28 April 2017</b><br/>7a4b5ee4-71fb-453c-8621-d08b66d44682<br/><br/><b>28 Mei 2017</b><br/>dcb4562e-f374-4488-9c88-f5eebf931d8b<br/><br/><b>28 Juni 2017</b><br/>cd394649-681f-4db0-a55e-e6d04be02098<br/><br/><b>28 Juli 2017</b><br/>f149a79b-f859-44ec-8672-dae86623648e<br/><br/><b>28 Agustus 2017</b><br/>fff099ba-545b-4013-8a56-72606b9c80a2<br/><br/><b>28 September 2017</b><br/>5b4c546c-47aa-47cd-a0f8-ff69b750d6a5<br/><br/><b>28 Oktober 2017</b><br/>ea8f03fe-111d-43cb-960d-13cadde3377d<br/><br/><b>28 November 2017</b><br/>34901c97-8ac5-4547-a28e-6acb7a5391d3<br/><br/><b>28 Desember 2017</b><br/>9097d94f-357e-44f8-a9c1-cc87977a987f<br/><br/>')
INSERT [dbo].[TBPengirimanEmail] ([IDPengirimanEmail], [TanggalKirim], [Tujuan], [Judul], [Isi]) VALUES (2260, CAST(N'2017-02-05' AS Date), N'herdiawan.rendy@gmail.com', N'Store Key WIT.', N'Dear All,<br/><br/>Berikut Store Key untuk WIT. : <br/><br/><b>5 Februari 2017</b><br/>64d87d0b-541b-4921-96b2-786dabfeb7ff<br/><br/><b>5 Maret 2017</b><br/>049820da-4a9d-45e9-bf5b-5d11598910a5<br/><br/><b>5 April 2017</b><br/>31d592f5-f53e-4d83-b477-f938ae426456<br/><br/><b>5 Mei 2017</b><br/>6ecf9354-b193-4514-8999-3a1ea72faf46<br/><br/><b>5 Juni 2017</b><br/>f2a62ddd-c32e-43f5-89f8-85de1e03eed6<br/><br/><b>5 Juli 2017</b><br/>d6f6ab92-d59c-4d88-85f5-fddcfbe7b3bf<br/><br/><b>5 Agustus 2017</b><br/>32976194-ad89-4969-a4a3-c355091b3187<br/><br/><b>5 September 2017</b><br/>43908bc7-60d5-43de-8321-3257ae0ba09b<br/><br/><b>5 Oktober 2017</b><br/>da47444e-b4c2-47af-aaf7-5faff89251a7<br/><br/><b>5 November 2017</b><br/>2ba9b42d-fb28-4589-bc58-0fd8971f01c7<br/><br/><b>5 Desember 2017</b><br/>669486e7-5d2c-4d48-a44d-8856f8764b92<br/><br/><b>5 Januari 2018</b><br/>25213b29-e843-4458-94a2-003942678807<br/><br/>')
INSERT [dbo].[TBPengirimanEmail] ([IDPengirimanEmail], [TanggalKirim], [Tujuan], [Judul], [Isi]) VALUES (2261, CAST(N'2017-05-24' AS Date), N'herdiawan.rendy@gmail.com', N'Store Key WIT.', N'Dear All,<br/><br/>Berikut Store Key untuk WIT. : <br/><br/><b>24 Mei 2017</b><br/>14630301-2b6f-4a48-b6f0-dd907e7a3248<br/><br/><b>24 Juni 2017</b><br/>4462f8fa-2fe5-4f30-a288-292d4d7ae9e4<br/><br/><b>24 Juli 2017</b><br/>4cf06533-d9eb-4797-8a8b-48a40bf70168<br/><br/><b>24 Agustus 2017</b><br/>38e1ccda-da77-44d8-8f43-5bbf0a0de622<br/><br/><b>24 September 2017</b><br/>9fbfaa70-2986-41fc-ab33-f121f79f4f65<br/><br/><b>24 Oktober 2017</b><br/>316659a8-df46-4aca-87e3-a88187ba36f2<br/><br/><b>24 November 2017</b><br/>3b3c342c-d974-492c-ae52-21812edc08fc<br/><br/><b>24 Desember 2017</b><br/>90e3b852-1c7d-4d96-9e45-7350d64b07c5<br/><br/><b>24 Januari 2018</b><br/>14b73238-c01e-42f2-be07-f24fa015c0c0<br/><br/><b>24 Februari 2018</b><br/>1cc5c36e-d7a3-45c4-bd35-6c9d76d1a936<br/><br/><b>24 Maret 2018</b><br/>b074f376-eff8-4916-8576-c3eb8a170cd4<br/><br/><b>24 April 2018</b><br/>1da821e0-ab41-4e0a-bc12-0aed4a74a1cc<br/><br/>')
INSERT [dbo].[TBPengirimanEmail] ([IDPengirimanEmail], [TanggalKirim], [Tujuan], [Judul], [Isi]) VALUES (2262, CAST(N'2017-08-22' AS Date), N'herdiawan.rendy@gmail.com', N'Store Key WIT.', N'Dear All,<br/><br/>Berikut Store Key untuk WIT. : <br/><br/><b>22 August 2017</b><br/>bad16bd1-f00a-4d70-9052-eff0d5f8cb0e<br/><br/><b>22 September 2017</b><br/>d39f42e1-c3c9-40ab-bf88-fe2f2a6dc9a8<br/><br/><b>22 October 2017</b><br/>e2ebe533-2ff3-47b6-8bb8-6a0e45a9eec8<br/><br/><b>22 November 2017</b><br/>e133c04f-7bd9-4578-8ee9-ae3a3fdc9ce8<br/><br/><b>22 December 2017</b><br/>15b3d131-ea16-463e-b713-c1ecf223534b<br/><br/><b>22 January 2018</b><br/>74c6c572-a4ea-4c2d-a1ab-440283fa95a3<br/><br/><b>22 February 2018</b><br/>ef52b814-203d-497f-8715-7be5b0e4b00b<br/><br/><b>22 March 2018</b><br/>acf057d3-65da-4d95-acb5-22e5a8602c94<br/><br/><b>22 April 2018</b><br/>90486ab9-a085-4f4a-851e-03416fa68362<br/><br/><b>22 May 2018</b><br/>9a9a40fd-e523-48b5-9683-e39f46092a21<br/><br/><b>22 June 2018</b><br/>032c5512-919e-4e8d-9bcb-739ef8b27379<br/><br/><b>22 July 2018</b><br/>3bb1d987-a100-427e-9bef-9ecd571191e3<br/><br/>')
INSERT [dbo].[TBPengirimanEmail] ([IDPengirimanEmail], [TanggalKirim], [Tujuan], [Judul], [Isi]) VALUES (2263, CAST(N'2018-04-03' AS Date), N'herdiawan.rendy@gmail.com', N'Store Key WIT.', N'Dear All,<br/><br/>Berikut Store Key untuk WIT. : <br/><br/><b>3 April 2018</b><br/>3a6d56b9-8e1d-4821-b002-5cbd5cf7fa48<br/><br/><b>3 May 2018</b><br/>41be326a-d5b7-4890-b07f-503b039dfb62<br/><br/><b>3 June 2018</b><br/>9a8e65ca-0678-4896-aa7a-c79379529315<br/><br/><b>3 July 2018</b><br/>963009d4-944d-4cc9-bcc5-e4f92640d333<br/><br/><b>3 August 2018</b><br/>27d38bf1-ed6f-4c8d-bc2d-9828c5976221<br/><br/><b>3 September 2018</b><br/>f9cd31d5-d4b2-4de5-8052-4a7f810d79c2<br/><br/><b>3 October 2018</b><br/>af3bf7cc-98f3-45be-88b5-8118e10eecb0<br/><br/><b>3 November 2018</b><br/>3b50fe30-c7a9-4090-8318-b7f311371384<br/><br/><b>3 December 2018</b><br/>f6514deb-91ad-4926-a144-439da1bb2770<br/><br/><b>3 January 2019</b><br/>d9eeab6b-0d1c-4b99-bea6-97ecb9f25741<br/><br/><b>3 February 2019</b><br/>318fc4a7-0207-49b1-a2fd-3f741d759302<br/><br/><b>3 March 2019</b><br/>b2a0d79c-98bc-43e6-aac9-1a2222d4d5cb<br/><br/>')
SET IDENTITY_INSERT [dbo].[TBPengirimanEmail] OFF
INSERT [dbo].[TBPrintBarcode] ([IDStokProduk], [Jumlah]) VALUES (22, 4)
INSERT [dbo].[TBPrintBarcode] ([IDStokProduk], [Jumlah]) VALUES (23, 30)
INSERT [dbo].[TBPrintBarcode] ([IDStokProduk], [Jumlah]) VALUES (24, 40)
SET IDENTITY_INSERT [dbo].[TBProduk] ON 

INSERT [dbo].[TBProduk] ([IDProduk], [IDWarna], [IDPemilikProduk], [IDProdukKategori], [KodeProduk], [Nama], [Deskripsi], [DeskripsiSingkat], [Dilihat], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (5, 5, 5, 5, N'', N'Best Clohtes White', N'', N'', 0, N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'f3dc1600-633b-449b-bb2d-89daf30469a8', 1, CAST(N'2018-07-10T10:14:45.470' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.917' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBProduk] ([IDProduk], [IDWarna], [IDPemilikProduk], [IDProdukKategori], [KodeProduk], [Nama], [Deskripsi], [DeskripsiSingkat], [Dilihat], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (6, 6, 5, 6, N'', N'Simple Shirt Black', N'', N'', 0, N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'15942640-4a85-4355-8c69-e0d8227e9dcf', 2, CAST(N'2018-07-10T10:14:45.997' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.997' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBProduk] ([IDProduk], [IDWarna], [IDPemilikProduk], [IDProdukKategori], [KodeProduk], [Nama], [Deskripsi], [DeskripsiSingkat], [Dilihat], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (7, 6, 5, 6, N'', N'Simple Shirt', N'', N'', 0, N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'3de0a821-a06d-40c0-957e-5d0ca080f911', 3, CAST(N'2018-07-10T10:14:46.083' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.153' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBProduk] ([IDProduk], [IDWarna], [IDPemilikProduk], [IDProdukKategori], [KodeProduk], [Nama], [Deskripsi], [DeskripsiSingkat], [Dilihat], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (8, 7, 5, 7, N'', N'Bomber Jacket Blue', N'', N'', 0, N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'c672647f-317d-4b52-bf0d-52459355188d', 4, CAST(N'2018-07-10T10:14:46.190' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.253' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBProduk] ([IDProduk], [IDWarna], [IDPemilikProduk], [IDProdukKategori], [KodeProduk], [Nama], [Deskripsi], [DeskripsiSingkat], [Dilihat], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (9, 8, 5, 8, N'', N'Cino Pant Brown', N'', N'', 0, N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'af7a177c-3615-4817-b12e-9358c1c29ab6', 5, CAST(N'2018-07-10T10:14:46.290' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.467' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBProduk] ([IDProduk], [IDWarna], [IDPemilikProduk], [IDProdukKategori], [KodeProduk], [Nama], [Deskripsi], [DeskripsiSingkat], [Dilihat], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (10, 6, 5, 9, N'', N'Boot Shoes Black', N'', N'', 0, N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'da0d94cd-0ddf-40ec-8cd4-33aecbc6575b', 6, CAST(N'2018-07-10T10:14:46.543' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.757' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TBProduk] OFF
SET IDENTITY_INSERT [dbo].[TBProdukKategori] ON 

INSERT [dbo].[TBProdukKategori] ([IDProdukKategori], [IDProdukKategoriParent], [Nama], [Deskripsi], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (5, NULL, N'Baju', N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'88c1b481-a47d-4e0a-9e91-3871598ec782', 1, CAST(N'2018-07-10T10:14:45.407' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.407' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBProdukKategori] ([IDProdukKategori], [IDProdukKategoriParent], [Nama], [Deskripsi], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (6, NULL, N'Kemeja', N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'3632ba83-6863-4d52-a53d-4f42ae9af2b4', 2, CAST(N'2018-07-10T10:14:45.967' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.967' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBProdukKategori] ([IDProdukKategori], [IDProdukKategoriParent], [Nama], [Deskripsi], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (7, NULL, N'Jaket', N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'61366599-c489-4d41-aed4-72f6739a1c43', 3, CAST(N'2018-07-10T10:14:46.183' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.183' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBProdukKategori] ([IDProdukKategori], [IDProdukKategoriParent], [Nama], [Deskripsi], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (8, NULL, N'Celana', N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'29c0df26-59c0-4d25-91be-a0cec7b5a132', 4, CAST(N'2018-07-10T10:14:46.283' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.283' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBProdukKategori] ([IDProdukKategori], [IDProdukKategoriParent], [Nama], [Deskripsi], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (9, NULL, N'Sepatu', N'', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'99b8f965-910d-450c-8d1c-8aed73834240', 5, CAST(N'2018-07-10T10:14:46.520' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.520' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TBProdukKategori] OFF
INSERT [dbo].[TBRelasiProdukKategoriProduk] ([IDProduk], [IDKategoriProduk]) VALUES (5, 5)
INSERT [dbo].[TBRelasiProdukKategoriProduk] ([IDProduk], [IDKategoriProduk]) VALUES (6, 6)
INSERT [dbo].[TBRelasiProdukKategoriProduk] ([IDProduk], [IDKategoriProduk]) VALUES (7, 6)
INSERT [dbo].[TBRelasiProdukKategoriProduk] ([IDProduk], [IDKategoriProduk]) VALUES (8, 7)
INSERT [dbo].[TBRelasiProdukKategoriProduk] ([IDProduk], [IDKategoriProduk]) VALUES (9, 8)
INSERT [dbo].[TBRelasiProdukKategoriProduk] ([IDProduk], [IDKategoriProduk]) VALUES (9, 9)
INSERT [dbo].[TBRelasiProdukKategoriProduk] ([IDProduk], [IDKategoriProduk]) VALUES (10, 10)
SET IDENTITY_INSERT [dbo].[TBStatusMeja] ON 

INSERT [dbo].[TBStatusMeja] ([IDStatusMeja], [Nama]) VALUES (1, N'Open')
INSERT [dbo].[TBStatusMeja] ([IDStatusMeja], [Nama]) VALUES (2, N'Close')
INSERT [dbo].[TBStatusMeja] ([IDStatusMeja], [Nama]) VALUES (3, N'Booked')
INSERT [dbo].[TBStatusMeja] ([IDStatusMeja], [Nama]) VALUES (4, N'Pre-Settlement')
SET IDENTITY_INSERT [dbo].[TBStatusMeja] OFF
SET IDENTITY_INSERT [dbo].[TBStatusTransaksi] ON 

INSERT [dbo].[TBStatusTransaksi] ([IDStatusTransaksi], [Urutan], [Nama]) VALUES (1, 1, N'Pending Shipping Cost')
INSERT [dbo].[TBStatusTransaksi] ([IDStatusTransaksi], [Urutan], [Nama]) VALUES (2, 2, N'Awaiting Payment')
INSERT [dbo].[TBStatusTransaksi] ([IDStatusTransaksi], [Urutan], [Nama]) VALUES (3, 3, N'Awaiting Payment Verification')
INSERT [dbo].[TBStatusTransaksi] ([IDStatusTransaksi], [Urutan], [Nama]) VALUES (4, 4, N'Payment Verified')
INSERT [dbo].[TBStatusTransaksi] ([IDStatusTransaksi], [Urutan], [Nama]) VALUES (5, 5, N'Complete')
INSERT [dbo].[TBStatusTransaksi] ([IDStatusTransaksi], [Urutan], [Nama]) VALUES (6, 6, N'Canceled')
INSERT [dbo].[TBStatusTransaksi] ([IDStatusTransaksi], [Urutan], [Nama]) VALUES (7, 7, N'Pending Production')
SET IDENTITY_INSERT [dbo].[TBStatusTransaksi] OFF
SET IDENTITY_INSERT [dbo].[TBStokProduk] ON 

INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (5, 1, 1, NULL, 30000.0000, 100000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (6, 1, 2, NULL, 30000.0000, 100000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (7, 1, 3, NULL, 30000.0000, 100000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (8, 1, 4, NULL, 30000.0000, 100000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (9, 1, 5, NULL, 40000.0000, 150000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (10, 1, 6, NULL, 40000.0000, 150000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (11, 1, 7, NULL, 40000.0000, 150000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (12, 1, 8, NULL, 40000.0000, 150000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (13, 1, 9, NULL, 120000.0000, 250000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (14, 1, 10, NULL, 120000.0000, 250000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (15, 1, 11, NULL, 120000.0000, 250000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (16, 1, 12, NULL, 80000.0000, 250000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (17, 1, 13, NULL, 80000.0000, 250000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (18, 1, 14, NULL, 80000.0000, 250000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (19, 1, 15, NULL, 80000.0000, 250000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (20, 1, 16, NULL, 80000.0000, 250000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (21, 1, 17, NULL, 200000.0000, 400000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (22, 1, 18, NULL, 200000.0000, 400000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (23, 1, 19, NULL, 200000.0000, 400000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (24, 1, 20, NULL, 200000.0000, 400000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
INSERT [dbo].[TBStokProduk] ([IDStokProduk], [IDTempat], [IDKombinasiProduk], [IDStokProdukPenyimpanan], [HargaBeli], [HargaJual], [PersentaseKonsinyasi], [EnumDiscountStore], [DiscountStore], [EnumDiscountKonsinyasi], [DiscountKonsinyasi], [PajakPersentase], [Jumlah], [JumlahMinimum], [Status]) VALUES (25, 1, 21, NULL, 200000.0000, 400000.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0.0000, 0, 0.0000, CAST(0.00 AS Decimal(18, 2)), 0, 0, 1)
SET IDENTITY_INSERT [dbo].[TBStokProduk] OFF
SET IDENTITY_INSERT [dbo].[TBStore] ON 

INSERT [dbo].[TBStore] ([IDStore], [IDWMS], [Nama], [Alamat], [Email], [KodePos], [Handphone], [TeleponLain], [Website], [SMTPServer], [SMTPPort], [SMTPUser], [SMTPPassword], [SecureSocketsLayer]) VALUES (1, N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'WIT.', N'', N'', N'', N'', N'', N'', N'mail.mypartitur.com', 26, N'wit.systems@mypartitur.com', N'Warmup987654321*', 0)
INSERT [dbo].[TBStore] ([IDStore], [IDWMS], [Nama], [Alamat], [Email], [KodePos], [Handphone], [TeleponLain], [Website], [SMTPServer], [SMTPPort], [SMTPUser], [SMTPPassword], [SecureSocketsLayer]) VALUES (2, N'122eef34-3743-4f5a-872a-f668bf1f16e1', N'Amble Footwear', N'', N'', N'', N'', N'', N'', N'mail.mypartitur.com', 26, N'wit.systems@mypartitur.com', N'Warmup987654321*', 0)
SET IDENTITY_INSERT [dbo].[TBStore] OFF
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'90486ab9-a085-4f4a-851e-03416fa68362', 1, 1, CAST(N'2018-04-22' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'b8dcc300-a17c-4ac5-b7ac-0893ac3edbf3', 1, 1, CAST(N'2019-05-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'21251050-1488-418c-91c7-185ef3b27d41', 1, NULL, CAST(N'2019-06-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'b2a0d79c-98bc-43e6-aac9-1a2222d4d5cb', 1, 1, CAST(N'2019-03-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'acf057d3-65da-4d95-acb5-22e5a8602c94', 1, 1, CAST(N'2018-03-22' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'f174d917-21f0-4f9c-b97a-24bf4456552b', 1, 1, CAST(N'2018-08-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'9b1f8953-d2f1-4c09-b475-336edac75103', 1, NULL, CAST(N'2020-02-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'318fc4a7-0207-49b1-a2fd-3f741d759302', 1, 1, CAST(N'2019-02-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'93761ce3-ffad-437a-9460-436ea090c08d', 1, NULL, CAST(N'2019-07-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'f6514deb-91ad-4926-a144-439da1bb2770', 1, 1, CAST(N'2018-12-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'74c6c572-a4ea-4c2d-a1ab-440283fa95a3', 1, 1, CAST(N'2018-01-22' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'2f6e20d1-b9c9-419b-909e-48602410ed79', 1, NULL, CAST(N'2019-09-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'f9cd31d5-d4b2-4de5-8052-4a7f810d79c2', 1, 1, CAST(N'2018-09-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'41be326a-d5b7-4890-b07f-503b039dfb62', 1, 1, CAST(N'2018-05-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'b774873d-1bb1-46cf-8c3a-53d0b9468881', 1, 1, CAST(N'2019-02-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'd7e8a7d5-7f88-44b2-ac6d-564a95c079ed', 1, 1, CAST(N'2019-03-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'3a6d56b9-8e1d-4821-b002-5cbd5cf7fa48', 1, 1, CAST(N'2018-04-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'424b91a5-c629-4462-8264-5f340e1dc0ee', 1, NULL, CAST(N'2019-11-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'e2ebe533-2ff3-47b6-8bb8-6a0e45a9eec8', 1, 1, CAST(N'2017-10-22' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'032c5512-919e-4e8d-9bcb-739ef8b27379', 1, 1, CAST(N'2018-06-22' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'07b5df3b-ce64-4b37-9456-740c40bbf328', 1, 1, CAST(N'2018-09-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'049d3bd1-0acd-4bc6-9499-75e5a85cfe57', 1, NULL, CAST(N'2019-12-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'ef52b814-203d-497f-8715-7be5b0e4b00b', 1, 1, CAST(N'2018-02-22' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'2a959a14-5b62-4314-80b7-7ec899baed7a', 1, NULL, CAST(N'2020-03-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'af3bf7cc-98f3-45be-88b5-8118e10eecb0', 1, 1, CAST(N'2018-10-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'b65da06f-9f96-4d94-a283-84659139cc92', 1, 1, CAST(N'2018-06-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'f6460012-0157-459a-ab9e-91a7820b4405', 1, NULL, CAST(N'2019-10-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'70015ad3-f0bb-4594-a029-97a0b75d18ba', 1, NULL, CAST(N'2019-08-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'd9eeab6b-0d1c-4b99-bea6-97ecb9f25741', 1, 1, CAST(N'2019-01-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'27d38bf1-ed6f-4c8d-bc2d-9828c5976221', 1, 1, CAST(N'2018-08-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'3bb1d987-a100-427e-9bef-9ecd571191e3', 1, 1, CAST(N'2018-07-22' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'2ab13cd8-a5c7-4d76-b8c4-a7c11fecdc1f', 1, 1, CAST(N'2019-01-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'e133c04f-7bd9-4578-8ee9-ae3a3fdc9ce8', 1, 1, CAST(N'2017-11-22' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'4bf6de5f-f71b-4b27-bb34-b40acf2d5aae', 1, 1, CAST(N'2018-10-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'3b50fe30-c7a9-4090-8318-b7f311371384', 1, 1, CAST(N'2018-11-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'cf126ce3-7351-42a3-9ccf-b885fb7343a2', 1, NULL, CAST(N'2020-05-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'c4c53d04-9ada-4c18-baee-bb4ee4d5be7c', 1, 1, CAST(N'2018-11-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'15b3d131-ea16-463e-b713-c1ecf223534b', 1, 1, CAST(N'2017-12-22' AS Date), CAST(N'2017-12-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'9a8e65ca-0678-4896-aa7a-c79379529315', 1, 1, CAST(N'2018-06-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'6d00f38e-7d72-4b47-8a37-cbb6cc2d67b3', 1, 1, CAST(N'2018-12-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'97e43f2b-585c-4f75-80a1-d3668ed8d220', 1, NULL, CAST(N'2020-01-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'25f27484-9c7a-4e3f-b13d-d6486d2eb7b0', 1, NULL, CAST(N'2020-04-25' AS Date), NULL, 0)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'9a9a40fd-e523-48b5-9683-e39f46092a21', 1, 1, CAST(N'2018-05-22' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'963009d4-944d-4cc9-bcc5-e4f92640d333', 1, 1, CAST(N'2018-07-03' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'39745925-f81a-440c-a26b-e9cea224beda', 1, 1, CAST(N'2019-04-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'8ed03ce9-6c71-4484-9250-ebe09509292d', 1, 1, CAST(N'2018-07-25' AS Date), CAST(N'2017-11-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'bad16bd1-f00a-4d70-9052-eff0d5f8cb0e', 1, 1, CAST(N'2017-08-22' AS Date), CAST(N'2017-10-06T10:56:40.067' AS DateTime), 1)
INSERT [dbo].[TBStoreKey] ([IDStoreKey], [IDStore], [IDPenggunaAktif], [TanggalKey], [TanggalAktif], [IsAktif]) VALUES (N'd39f42e1-c3c9-40ab-bf88-fe2f2a6dc9a8', 1, 1, CAST(N'2017-09-22' AS Date), CAST(N'2017-10-06T10:56:45.157' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[TBStoreKonfigurasi] ON 

INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (1, 1, N'Jam Buka', N'05:00')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (2, 1, N'Jam Tutup', N'03:00')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (3, 1, N'Minimum Stok', N'30%')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (4, 1, N'Tipe Point of Sales', N'1')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (5, 1, N'Hari Jatuh Tempo', N'21')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (6, 1, N'Pembulatan', N'True')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (7, 1, N'Email Report Sales', N'herdiawan.rendy@gmail.com')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (8, 1, N'Validasi Stok Produk Point of Sales', N'False')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (9, 1, N'URL WIT. Commerce', N'http://localhost:1947/')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (10, 1, N'Tempat Sync WIT. Commerce Express', N'1')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (11, 1, N'Batas Bulan Produk', N'3')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (12, 1, N'Security Token', N'')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (13, 1, N'Urutan Proses Tomahawk', N'2')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (16, 1, N'Jumlah Hari Sebelum Jatuh Tempo', N'7')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (17, 1, N'Mikrotik IP Address', N'')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (18, 1, N'Mikrotik Port', N'')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (19, 1, N'Mikrotik IP Address Radius', N'')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (20, 1, N'Mikrotik Username', N'')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (21, 1, N'Mikrotik Password', N'')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (22, 1, N'Mikrotik Nama Customer', N'')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (23, 1, N'Minimum Order Akses Wifi', N'20000')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (24, 1, N'Mikrotik Hotspot Profile', N'')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (25, 1, N'Tipe Penghitungan Consignment', N'2')
INSERT [dbo].[TBStoreKonfigurasi] ([IDStoreKonfigurasi], [IDStore], [Nama], [Pengaturan]) VALUES (26, 1, N'Fitur ECommerce', N'True')
SET IDENTITY_INSERT [dbo].[TBStoreKonfigurasi] OFF
SET IDENTITY_INSERT [dbo].[TBSyncData] ON 

INSERT [dbo].[TBSyncData] ([IDSyncData], [IDWMSSyncData], [IDWMSStore], [IDWMSTempat], [IDWMSPengguna], [TanggalUpload], [TanggalUploadFinish], [TanggalSync], [Data]) VALUES (7, N'38b829c3-a489-4948-947b-2f1ba87cb847', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', CAST(N'2017-06-30T22:51:39.630' AS DateTime), CAST(N'2017-06-30T22:55:18.990' AS DateTime), NULL, N'JSON-DATA')
INSERT [dbo].[TBSyncData] ([IDSyncData], [IDWMSSyncData], [IDWMSStore], [IDWMSTempat], [IDWMSPengguna], [TanggalUpload], [TanggalUploadFinish], [TanggalSync], [Data]) VALUES (6, N'9cc2379d-a976-410c-9ef1-41fab4d061d9', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', CAST(N'2017-06-30T22:46:53.093' AS DateTime), CAST(N'2017-06-30T22:56:40.790' AS DateTime), NULL, N'JSON-DATA')
INSERT [dbo].[TBSyncData] ([IDSyncData], [IDWMSSyncData], [IDWMSStore], [IDWMSTempat], [IDWMSPengguna], [TanggalUpload], [TanggalUploadFinish], [TanggalSync], [Data]) VALUES (9, N'1141ecc0-83e7-4aa7-b6d1-4a9e99f3c50e', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', CAST(N'2017-06-30T22:51:51.707' AS DateTime), NULL, NULL, N'JSON-DATA')
INSERT [dbo].[TBSyncData] ([IDSyncData], [IDWMSSyncData], [IDWMSStore], [IDWMSTempat], [IDWMSPengguna], [TanggalUpload], [TanggalUploadFinish], [TanggalSync], [Data]) VALUES (5, N'a4ef82b5-43d0-4e57-b46c-619dfb3d3e2b', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', CAST(N'2017-06-30T22:39:24.423' AS DateTime), CAST(N'2017-06-30T22:39:46.957' AS DateTime), NULL, N'JSON-DATA')
INSERT [dbo].[TBSyncData] ([IDSyncData], [IDWMSSyncData], [IDWMSStore], [IDWMSTempat], [IDWMSPengguna], [TanggalUpload], [TanggalUploadFinish], [TanggalSync], [Data]) VALUES (10, N'3486195a-9129-4c4c-9d87-915a34a669e1', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', CAST(N'2017-06-30T22:51:53.977' AS DateTime), CAST(N'2017-06-30T22:52:37.903' AS DateTime), NULL, N'JSON-DATA')
INSERT [dbo].[TBSyncData] ([IDSyncData], [IDWMSSyncData], [IDWMSStore], [IDWMSTempat], [IDWMSPengguna], [TanggalUpload], [TanggalUploadFinish], [TanggalSync], [Data]) VALUES (8, N'c83c2d4e-9f0a-483c-8385-a6bcaf318752', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', CAST(N'2017-06-30T22:51:46.837' AS DateTime), NULL, NULL, N'JSON-DATA')
INSERT [dbo].[TBSyncData] ([IDSyncData], [IDWMSSyncData], [IDWMSStore], [IDWMSTempat], [IDWMSPengguna], [TanggalUpload], [TanggalUploadFinish], [TanggalSync], [Data]) VALUES (3, N'f8793f46-8700-485e-a4b7-b24e497f9a3c', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', CAST(N'2017-06-30T22:39:13.510' AS DateTime), NULL, NULL, N'JSON-DATA')
INSERT [dbo].[TBSyncData] ([IDSyncData], [IDWMSSyncData], [IDWMSStore], [IDWMSTempat], [IDWMSPengguna], [TanggalUpload], [TanggalUploadFinish], [TanggalSync], [Data]) VALUES (4, N'800b8fc8-bfa3-43cd-9348-db0c81ca5f3f', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', CAST(N'2017-06-30T22:39:20.553' AS DateTime), NULL, NULL, N'JSON-DATA')
INSERT [dbo].[TBSyncData] ([IDSyncData], [IDWMSSyncData], [IDWMSStore], [IDWMSTempat], [IDWMSPengguna], [TanggalUpload], [TanggalUploadFinish], [TanggalSync], [Data]) VALUES (2, N'bfb8657e-5730-499e-8af8-e35109f8dcd4', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', N'723b568a-7ac3-4811-902e-94b14487bf5f', CAST(N'2017-06-30T22:18:17.520' AS DateTime), CAST(N'2017-06-30T22:53:17.913' AS DateTime), NULL, N'JSON-DATA')
SET IDENTITY_INSERT [dbo].[TBSyncData] OFF
SET IDENTITY_INSERT [dbo].[TBTempat] ON 

INSERT [dbo].[TBTempat] ([IDTempat], [IDStore], [IDKategoriTempat], [Kode], [Nama], [Alamat], [Email], [Telepon1], [Telepon2], [EnumBiayaTambahan1], [KeteranganBiayaTambahan1], [BiayaTambahan1], [EnumBiayaTambahan2], [KeteranganBiayaTambahan2], [BiayaTambahan2], [EnumBiayaTambahan3], [KeteranganBiayaTambahan3], [BiayaTambahan3], [EnumBiayaTambahan4], [KeteranganBiayaTambahan4], [BiayaTambahan4], [Latitude], [Longitude], [FooterPrint], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (1, 1, 1, N'1', N'Bandung', N'', N'', N'', N'', 2, N'Tax', 0.0000, 2, N'Service', 0.0000, 0, N'', 0.0000, 0, N'', 0.0000, N'0', N'0', N'', NULL, N'5e3aeaa1-c632-4d59-9d5d-3f75056c9ff6', NULL, CAST(N'2013-09-01T00:00:00.000' AS DateTime), 1, 1, CAST(N'2018-07-11T00:20:24.710' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TBTempat] OFF
SET IDENTITY_INSERT [dbo].[TBTemplateKeterangan] ON 

INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (1, N'Rare')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (2, N'Medium Rare')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (3, N'Medium Well')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (4, N'Well Done')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (5, N'Less Sugar')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (6, N'No Sugar')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (7, N'More Sugar')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (29, N'Level 1')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (30, N'Level 2')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (31, N'Level 3')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (32, N'Level 4')
INSERT [dbo].[TBTemplateKeterangan] ([IDTemplateKeterangan], [Isi]) VALUES (33, N'Level 5')
SET IDENTITY_INSERT [dbo].[TBTemplateKeterangan] OFF
SET IDENTITY_INSERT [dbo].[TBTransaksiECommerce] ON 

INSERT [dbo].[TBTransaksiECommerce] ([IDTransaksiECommerce], [IDPelanggan], [_IDWMSPelanggan], [_TanggalInsert]) VALUES (1, 6, N'ce8d6eda-302c-42b5-800a-ffa7d69734af', CAST(N'2018-07-10T10:16:36.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[TBTransaksiECommerce] OFF
SET IDENTITY_INSERT [dbo].[TBTransaksiECommerceDetail] ON 

INSERT [dbo].[TBTransaksiECommerceDetail] ([IDTransaksiECommerceDetail], [IDTransaksiECommerce], [IDStokProduk], [Quantity], [_TanggalInsert]) VALUES (4, 1, 5, 1, CAST(N'2018-07-10T15:51:57.410' AS DateTime))
INSERT [dbo].[TBTransaksiECommerceDetail] ([IDTransaksiECommerceDetail], [IDTransaksiECommerce], [IDStokProduk], [Quantity], [_TanggalInsert]) VALUES (7, 1, 5, 1, CAST(N'2018-07-11T00:18:38.457' AS DateTime))
SET IDENTITY_INSERT [dbo].[TBTransaksiECommerceDetail] OFF
SET IDENTITY_INSERT [dbo].[TBWarna] ON 

INSERT [dbo].[TBWarna] ([IDWarna], [Kode], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (5, N'', N'Putih', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'4a448eb4-c877-4e01-b3bf-0285e8c01579', 1, CAST(N'2018-07-10T10:14:45.433' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.433' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBWarna] ([IDWarna], [Kode], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (6, N'', N'Hitam', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'41db13b0-bb7f-4d1b-ae1a-d4a12f3793e4', 2, CAST(N'2018-07-10T10:14:45.973' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:45.973' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBWarna] ([IDWarna], [Kode], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (7, N'', N'Biru', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'5dd42348-cb60-4067-967b-6d641743789c', 3, CAST(N'2018-07-10T10:14:46.187' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.187' AS DateTime), 1, 1, 1)
INSERT [dbo].[TBWarna] ([IDWarna], [Kode], [Nama], [_IDWMSStore], [_IDWMS], [_Urutan], [_TanggalInsert], [_IDTempatInsert], [_IDPenggunaInsert], [_TanggalUpdate], [_IDTempatUpdate], [_IDPenggunaUpdate], [_IsActive]) VALUES (8, N'', N'Coklat', N'96220ac7-7ce3-41f7-a159-2e2e64a3a9ba', N'fea07903-641d-4532-8668-c331dcf37277', 4, CAST(N'2018-07-10T10:14:46.287' AS DateTime), 1, 1, CAST(N'2018-07-10T10:14:46.287' AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[TBWarna] OFF
ALTER TABLE [dbo].[TBAtribut] ADD  CONSTRAINT [DF_TBAtribut_IDWMS]  DEFAULT (newid()) FOR [IDWMS]
GO
ALTER TABLE [dbo].[TBAtributGrup] ADD  CONSTRAINT [DF_TBAtributGrup_IDWMS]  DEFAULT (newid()) FOR [IDWMS]
GO
ALTER TABLE [dbo].[TBAtributPilihan] ADD  CONSTRAINT [DF_TBAtributPilihan_IDWMS]  DEFAULT (newid()) FOR [IDWMS]
GO
ALTER TABLE [dbo].[TBAtributProduk] ADD  CONSTRAINT [DF_TBAtributProduk__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBAtributProduk] ADD  CONSTRAINT [DF_TBAtributProduk__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBAtributProduk] ADD  CONSTRAINT [DF_TBAtributProduk__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBAtributProduk] ADD  CONSTRAINT [DF_TBAtributProduk__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBAtributProduk] ADD  CONSTRAINT [DF_TBAtributProduk__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBAtributProduk] ADD  CONSTRAINT [DF_TBAtributProduk__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBAtributProduk] ADD  CONSTRAINT [DF_TBAtributProduk__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBAtributProduk] ADD  CONSTRAINT [DF_TBAtributProduk__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBAtributProdukGrup] ADD  CONSTRAINT [DF_Table_1__IDWMS]  DEFAULT (newid()) FOR [_IDWMSAtributProdukGrup]
GO
ALTER TABLE [dbo].[TBAtributProdukGrup] ADD  CONSTRAINT [DF_TBAtributProdukGrup__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBAtributProdukGrup] ADD  CONSTRAINT [DF_TBAtributProdukGrup__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBAtributProdukGrup] ADD  CONSTRAINT [DF_TBAtributProdukGrup__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBAtributProdukGrup] ADD  CONSTRAINT [DF_TBAtributProdukGrup__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBAtributProdukGrup] ADD  CONSTRAINT [DF_TBAtributProdukGrup__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBAtributProdukGrup] ADD  CONSTRAINT [DF_TBAtributProdukGrup__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBAtributProdukGrup] ADD  CONSTRAINT [DF_TBAtributProdukGrup__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBBahanBaku] ADD  CONSTRAINT [DF_TBBahanBaku_IDWMS_1]  DEFAULT (newid()) FOR [IDWMS]
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk] ADD  CONSTRAINT [DF_TBDiscountKombinasiProduk__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk] ADD  CONSTRAINT [DF_TBDiscountKombinasiProduk__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk] ADD  CONSTRAINT [DF_TBDiscountKombinasiProduk__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk] ADD  CONSTRAINT [DF_TBDiscountKombinasiProduk__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk] ADD  CONSTRAINT [DF_TBDiscountKombinasiProduk__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk] ADD  CONSTRAINT [DF_TBDiscountKombinasiProduk__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk] ADD  CONSTRAINT [DF_TBDiscountKombinasiProduk__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk] ADD  CONSTRAINT [DF_TBDiscountKombinasiProduk__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori] ADD  CONSTRAINT [DF_TBDiscountProdukKategori__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori] ADD  CONSTRAINT [DF_TBDiscountProdukKategori__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori] ADD  CONSTRAINT [DF_TBDiscountProdukKategori__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori] ADD  CONSTRAINT [DF_TBDiscountProdukKategori__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori] ADD  CONSTRAINT [DF_TBDiscountProdukKategori__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori] ADD  CONSTRAINT [DF_TBDiscountProdukKategori__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori] ADD  CONSTRAINT [DF_TBDiscountProdukKategori__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori] ADD  CONSTRAINT [DF_TBDiscountProdukKategori__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBGrupPelanggan] ADD  CONSTRAINT [DF_TBGrupPelanggan__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBGrupPelanggan] ADD  CONSTRAINT [DF_TBGrupPelanggan__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBGrupPelanggan] ADD  CONSTRAINT [DF_TBGrupPelanggan__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBGrupPelanggan] ADD  CONSTRAINT [DF_TBGrupPelanggan__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBGrupPelanggan] ADD  CONSTRAINT [DF_TBGrupPelanggan__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBGrupPelanggan] ADD  CONSTRAINT [DF_TBGrupPelanggan__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBGrupPelanggan] ADD  CONSTRAINT [DF_TBGrupPelanggan__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBGrupPelanggan] ADD  CONSTRAINT [DF_TBGrupPelanggan__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBKombinasiProduk] ADD  CONSTRAINT [DF_TBKombinasiProduk_IDWMS]  DEFAULT (newid()) FOR [IDWMS]
GO
ALTER TABLE [dbo].[TBKurir] ADD  CONSTRAINT [DF_TBKurir__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBKurir] ADD  CONSTRAINT [DF_TBKurir__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBKurir] ADD  CONSTRAINT [DF_TBKurir__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBKurir] ADD  CONSTRAINT [DF_TBKurir__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBKurir] ADD  CONSTRAINT [DF_TBKurir__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBKurir] ADD  CONSTRAINT [DF_TBKurir__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBKurir] ADD  CONSTRAINT [DF_TBKurir__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBKurir] ADD  CONSTRAINT [DF_TBKurir__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBKurirBiaya] ADD  CONSTRAINT [DF_TBKurirBiaya__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBKurirBiaya] ADD  CONSTRAINT [DF_TBKurirBiaya__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBKurirBiaya] ADD  CONSTRAINT [DF_TBKurirBiaya__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBKurirBiaya] ADD  CONSTRAINT [DF_TBKurirBiaya__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBKurirBiaya] ADD  CONSTRAINT [DF_TBKurirBiaya__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBKurirBiaya] ADD  CONSTRAINT [DF_TBKurirBiaya__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBKurirBiaya] ADD  CONSTRAINT [DF_TBKurirBiaya__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBKurirBiaya] ADD  CONSTRAINT [DF_TBKurirBiaya__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBNotifikasi] ADD  CONSTRAINT [DF_TBNotifikasi_IDWMS]  DEFAULT (newid()) FOR [IDWMS]
GO
ALTER TABLE [dbo].[TBPelanggan] ADD  CONSTRAINT [DF_TBPelanggan_IDPenggunaPIC]  DEFAULT ((1)) FOR [IDPenggunaPIC]
GO
ALTER TABLE [dbo].[TBPelanggan] ADD  CONSTRAINT [DF_TBPelanggan__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBPelanggan] ADD  CONSTRAINT [DF_TBPelanggan__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBPelanggan] ADD  CONSTRAINT [DF_TBPelanggan__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBPelanggan] ADD  CONSTRAINT [DF_TBPelanggan__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBPelanggan] ADD  CONSTRAINT [DF_TBPelanggan__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBPelanggan] ADD  CONSTRAINT [DF_TBPelanggan__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBPelanggan] ADD  CONSTRAINT [DF_TBPelanggan__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBPelanggan] ADD  CONSTRAINT [DF_TBPelanggan__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBPemilikProduk] ADD  CONSTRAINT [DF_TBPemilikProduk__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBPemilikProduk] ADD  CONSTRAINT [DF_TBPemilikProduk__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBPemilikProduk] ADD  CONSTRAINT [DF_TBPemilikProduk__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBPemilikProduk] ADD  CONSTRAINT [DF_TBPemilikProduk__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBPemilikProduk] ADD  CONSTRAINT [DF_TBPemilikProduk__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBPemilikProduk] ADD  CONSTRAINT [DF_TBPemilikProduk__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBPemilikProduk] ADD  CONSTRAINT [DF_TBPemilikProduk__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBPemilikProduk] ADD  CONSTRAINT [DF_TBPemilikProduk__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBPengguna] ADD  CONSTRAINT [DF_TBPengguna_TanggalLahir]  DEFAULT (getdate()) FOR [TanggalLahir]
GO
ALTER TABLE [dbo].[TBPengguna] ADD  CONSTRAINT [DF_TBPengguna_TanggalBekerja]  DEFAULT (getdate()) FOR [TanggalBekerja]
GO
ALTER TABLE [dbo].[TBPengguna] ADD  CONSTRAINT [DF_TBPengguna__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBPengguna] ADD  CONSTRAINT [DF_TBPengguna__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBPengguna] ADD  CONSTRAINT [DF_TBPengguna__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBPengguna] ADD  CONSTRAINT [DF_TBPengguna__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBPengguna] ADD  CONSTRAINT [DF_TBPengguna__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBPengguna] ADD  CONSTRAINT [DF_TBPengguna__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBPengguna] ADD  CONSTRAINT [DF_TBPengguna__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBPengguna] ADD  CONSTRAINT [DF_TBPengguna__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku] ADD  CONSTRAINT [DF_TBPerpindahanStokBahanBaku_IDWMS_1]  DEFAULT (newid()) FOR [IDWMS]
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku] ADD  CONSTRAINT [DF_TBPerpindahanStokBahanBaku_HargaBeli]  DEFAULT ((0)) FOR [HargaBeli]
GO
ALTER TABLE [dbo].[TBPerpindahanStokProduk] ADD  CONSTRAINT [DF_TBPerpindahanStokProduk_IDWMS]  DEFAULT (newid()) FOR [IDWMS]
GO
ALTER TABLE [dbo].[TBProduk] ADD  CONSTRAINT [DF_TBProduk_IDProdukKategori]  DEFAULT ((1)) FOR [IDProdukKategori]
GO
ALTER TABLE [dbo].[TBProduk] ADD  CONSTRAINT [DF_TBProduk_Dilihat]  DEFAULT ((0)) FOR [Dilihat]
GO
ALTER TABLE [dbo].[TBProduk] ADD  CONSTRAINT [DF_TBProduk_IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBProduk] ADD  CONSTRAINT [DF_TBProduk_TanggalDaftar]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBProduk] ADD  CONSTRAINT [DF_TBProduk__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBProduk] ADD  CONSTRAINT [DF_TBProduk__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBProduk] ADD  CONSTRAINT [DF_TBProduk_TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBProduk] ADD  CONSTRAINT [DF_TBProduk__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBProduk] ADD  CONSTRAINT [DF_TBProduk__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBProduk] ADD  CONSTRAINT [DF_TBProduk__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBProdukKategori] ADD  CONSTRAINT [DF_TBProdukKategori__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBProdukKategori] ADD  CONSTRAINT [DF_TBProdukKategori__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBProdukKategori] ADD  CONSTRAINT [DF_TBProdukKategori__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBProdukKategori] ADD  CONSTRAINT [DF_TBProdukKategori__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBProdukKategori] ADD  CONSTRAINT [DF_TBProdukKategori__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBProdukKategori] ADD  CONSTRAINT [DF_TBProdukKategori_IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBProdukKategori] ADD  CONSTRAINT [DF_TBProdukKategori_IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBProdukKategori] ADD  CONSTRAINT [DF_TBProdukKategori_IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBStokProduk] ADD  CONSTRAINT [DF_TBStokProduk_EnumDiscountStore]  DEFAULT ((0)) FOR [EnumDiscountStore]
GO
ALTER TABLE [dbo].[TBStokProduk] ADD  CONSTRAINT [DF_TBStokProduk_DiscountStore]  DEFAULT ((0)) FOR [DiscountStore]
GO
ALTER TABLE [dbo].[TBStokProduk] ADD  CONSTRAINT [DF_TBStokProduk_EnumDiscountBrand]  DEFAULT ((0)) FOR [EnumDiscountKonsinyasi]
GO
ALTER TABLE [dbo].[TBStokProduk] ADD  CONSTRAINT [DF_TBStokProduk_DiscountBrand]  DEFAULT ((0)) FOR [DiscountKonsinyasi]
GO
ALTER TABLE [dbo].[TBStokProduk] ADD  CONSTRAINT [DF_TBStokProduk_PajakPersentase]  DEFAULT ((0)) FOR [PajakPersentase]
GO
ALTER TABLE [dbo].[TBStokProdukMutasi] ADD  CONSTRAINT [DF_TBStokProdukMutasi_IDWMS]  DEFAULT (newid()) FOR [IDWMS]
GO
ALTER TABLE [dbo].[TBStore] ADD  CONSTRAINT [DF_TBStore_IDWMS]  DEFAULT (newid()) FOR [IDWMS]
GO
ALTER TABLE [dbo].[TBSupplier] ADD  CONSTRAINT [DF_TBSupplier_PersentaseTax]  DEFAULT ((0)) FOR [PersentaseTax]
GO
ALTER TABLE [dbo].[TBSyncData] ADD  CONSTRAINT [DF_TBSyncData_IDWMSSyncData]  DEFAULT (newid()) FOR [IDWMSSyncData]
GO
ALTER TABLE [dbo].[TBTempat] ADD  CONSTRAINT [DF_TBTempat_IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBTempat] ADD  CONSTRAINT [DF_TBTempat__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBTempat] ADD  CONSTRAINT [DF_TBTempat__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBTempat] ADD  CONSTRAINT [DF_TBTempat__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBTempat] ADD  CONSTRAINT [DF_TBTempat__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBTempat] ADD  CONSTRAINT [DF_TBTempat__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBTempat] ADD  CONSTRAINT [DF_TBTempat__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBTempat] ADD  CONSTRAINT [DF_TBTempat__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBTransaksi] ADD  CONSTRAINT [DF_TBTransaksi_BeratSubtotal]  DEFAULT ((0)) FOR [BeratSubtotal]
GO
ALTER TABLE [dbo].[TBTransaksi] ADD  CONSTRAINT [DF_TBTransaksi_BeratPembulatan]  DEFAULT ((0)) FOR [BeratPembulatan]
GO
ALTER TABLE [dbo].[TBTransaksiDetail] ADD  CONSTRAINT [DF_TBTransaksiDetail_Discount]  DEFAULT ((0)) FOR [DiscountStore]
GO
ALTER TABLE [dbo].[TBTransaksiDetail] ADD  CONSTRAINT [DF_TBTransaksiDetail_DiscountKonsinyasi]  DEFAULT ((0)) FOR [DiscountKonsinyasi]
GO
ALTER TABLE [dbo].[TBTransaksiDetail] ADD  CONSTRAINT [DF_TBTransaksiDetail_PajakNominal]  DEFAULT ((0)) FOR [PajakNominal]
GO
ALTER TABLE [dbo].[TBTransaksiDetailTemp] ADD  CONSTRAINT [DF_TBTransaksiDetailTemp_Discount]  DEFAULT ((0)) FOR [DiscountStore]
GO
ALTER TABLE [dbo].[TBTransaksiDetailTemp] ADD  CONSTRAINT [DF_TBTransaksiDetailTemp_DiscountKonsinyasi]  DEFAULT ((0)) FOR [DiscountKonsinyasi]
GO
ALTER TABLE [dbo].[TBTransaksiDetailTemp] ADD  CONSTRAINT [DF_TBTransaksiDetailTemp_PajakNominal]  DEFAULT ((0)) FOR [PajakNominal]
GO
ALTER TABLE [dbo].[TBTransaksiECommerce] ADD  CONSTRAINT [DF_TBTransaksiECommerce_TanggalTransaksi]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBTransaksiECommerceDetail] ADD  CONSTRAINT [DF_TBTransaksiECommerceDetail_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[TBTransaksiECommerceDetail] ADD  CONSTRAINT [DF_TBTransaksiECommerceDetail__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBTransaksiTemp] ADD  CONSTRAINT [DF_TBTransaksiTemp_BeratSubtotal]  DEFAULT ((0)) FOR [BeratSubtotal]
GO
ALTER TABLE [dbo].[TBTransaksiTemp] ADD  CONSTRAINT [DF_TBTransaksiTemp_BeratPembulatan]  DEFAULT ((0)) FOR [BeratPembulatan]
GO
ALTER TABLE [dbo].[TBVendor] ADD  CONSTRAINT [DF_TBVendor_PersentaseTax]  DEFAULT ((0)) FOR [PersentaseTax]
GO
ALTER TABLE [dbo].[TBWarna] ADD  CONSTRAINT [DF_TBWarna__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBWarna] ADD  CONSTRAINT [DF_TBWarna__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBWarna] ADD  CONSTRAINT [DF_TBWarna__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBWarna] ADD  CONSTRAINT [DF_TBWarna__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBWarna] ADD  CONSTRAINT [DF_TBWarna__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBWarna] ADD  CONSTRAINT [DF_TBWarna__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBWarna] ADD  CONSTRAINT [DF_TBWarna__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBWarna] ADD  CONSTRAINT [DF_TBWarna__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBWilayah] ADD  CONSTRAINT [DF_TBWilayah__IDWMS]  DEFAULT (newid()) FOR [_IDWMS]
GO
ALTER TABLE [dbo].[TBWilayah] ADD  CONSTRAINT [DF_TBWilayah__TanggalInsert]  DEFAULT (getdate()) FOR [_TanggalInsert]
GO
ALTER TABLE [dbo].[TBWilayah] ADD  CONSTRAINT [DF_TBWilayah__IDTempatInsert]  DEFAULT ((1)) FOR [_IDTempatInsert]
GO
ALTER TABLE [dbo].[TBWilayah] ADD  CONSTRAINT [DF_TBWilayah__IDPenggunaInsert]  DEFAULT ((1)) FOR [_IDPenggunaInsert]
GO
ALTER TABLE [dbo].[TBWilayah] ADD  CONSTRAINT [DF_TBWilayah__TanggalUpdate]  DEFAULT (getdate()) FOR [_TanggalUpdate]
GO
ALTER TABLE [dbo].[TBWilayah] ADD  CONSTRAINT [DF_TBWilayah__IDTempatUpdate]  DEFAULT ((1)) FOR [_IDTempatUpdate]
GO
ALTER TABLE [dbo].[TBWilayah] ADD  CONSTRAINT [DF_TBWilayah__IDPenggunaUpdate]  DEFAULT ((1)) FOR [_IDPenggunaUpdate]
GO
ALTER TABLE [dbo].[TBWilayah] ADD  CONSTRAINT [DF_TBWilayah__IsActive]  DEFAULT ((1)) FOR [_IsActive]
GO
ALTER TABLE [dbo].[TBAkun]  WITH CHECK ADD  CONSTRAINT [FK_TBAkun_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBAkun] CHECK CONSTRAINT [FK_TBAkun_TBTempat]
GO
ALTER TABLE [dbo].[TBAkun]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiAkun_TBAkuntansiGrupAkun] FOREIGN KEY([IDAkunGrup])
REFERENCES [dbo].[TBAkunGrup] ([IDAkunGrup])
GO
ALTER TABLE [dbo].[TBAkun] CHECK CONSTRAINT [FK_TBAkuntansiAkun_TBAkuntansiGrupAkun]
GO
ALTER TABLE [dbo].[TBAkunGrup]  WITH CHECK ADD  CONSTRAINT [FK_TBAkunGrup_TBAkunGrup] FOREIGN KEY([IDAkunGrupParent])
REFERENCES [dbo].[TBAkunGrup] ([IDAkunGrup])
GO
ALTER TABLE [dbo].[TBAkunGrup] CHECK CONSTRAINT [FK_TBAkunGrup_TBAkunGrup]
GO
ALTER TABLE [dbo].[TBAkunSaldoAwal]  WITH CHECK ADD  CONSTRAINT [FK_TBAkunSaldoAwal_TBAkun] FOREIGN KEY([IDAkun])
REFERENCES [dbo].[TBAkun] ([IDAkun])
GO
ALTER TABLE [dbo].[TBAkunSaldoAwal] CHECK CONSTRAINT [FK_TBAkunSaldoAwal_TBAkun]
GO
ALTER TABLE [dbo].[TBAkunSaldoAwal]  WITH CHECK ADD  CONSTRAINT [FK_TBAkunSaldoAwal_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBAkunSaldoAwal] CHECK CONSTRAINT [FK_TBAkunSaldoAwal_TBPengguna]
GO
ALTER TABLE [dbo].[TBAkunSaldoAwal]  WITH CHECK ADD  CONSTRAINT [FK_TBAkunSaldoAwal_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBAkunSaldoAwal] CHECK CONSTRAINT [FK_TBAkunSaldoAwal_TBTempat]
GO
ALTER TABLE [dbo].[TBAkuntansiAkun]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiAkun_TBAkuntansiAkun] FOREIGN KEY([IDAkuntansiAkunParent])
REFERENCES [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun])
GO
ALTER TABLE [dbo].[TBAkuntansiAkun] CHECK CONSTRAINT [FK_TBAkuntansiAkun_TBAkuntansiAkun]
GO
ALTER TABLE [dbo].[TBAkuntansiAkun]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiAkun_TBAkuntansiAkunTipe] FOREIGN KEY([IDAkuntansiAkunTipe])
REFERENCES [dbo].[TBAkuntansiAkunTipe] ([IDAkuntansiAkunTipe])
GO
ALTER TABLE [dbo].[TBAkuntansiAkun] CHECK CONSTRAINT [FK_TBAkuntansiAkun_TBAkuntansiAkunTipe]
GO
ALTER TABLE [dbo].[TBAkuntansiDokumen]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiDokumen_TBAkuntansiJurnal] FOREIGN KEY([IDAkuntansiJurnal])
REFERENCES [dbo].[TBAkuntansiJurnal] ([IDAkuntansiJurnal])
GO
ALTER TABLE [dbo].[TBAkuntansiDokumen] CHECK CONSTRAINT [FK_TBAkuntansiDokumen_TBAkuntansiJurnal]
GO
ALTER TABLE [dbo].[TBAkuntansiJurnal]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiJurnal_TBPengguna1] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBAkuntansiJurnal] CHECK CONSTRAINT [FK_TBAkuntansiJurnal_TBPengguna1]
GO
ALTER TABLE [dbo].[TBAkuntansiJurnal]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiJurnal_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBAkuntansiJurnal] CHECK CONSTRAINT [FK_TBAkuntansiJurnal_TBTempat]
GO
ALTER TABLE [dbo].[TBAkuntansiJurnalDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiJurnalDetail_TBAkuntansiAkun1] FOREIGN KEY([IDAkuntansiAkun])
REFERENCES [dbo].[TBAkuntansiAkun] ([IDAkuntansiAkun])
GO
ALTER TABLE [dbo].[TBAkuntansiJurnalDetail] CHECK CONSTRAINT [FK_TBAkuntansiJurnalDetail_TBAkuntansiAkun1]
GO
ALTER TABLE [dbo].[TBAkuntansiJurnalDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiJurnalDetail_TBAkuntansiJurnal1] FOREIGN KEY([IDAkuntansiJurnal])
REFERENCES [dbo].[TBAkuntansiJurnal] ([IDAkuntansiJurnal])
GO
ALTER TABLE [dbo].[TBAkuntansiJurnalDetail] CHECK CONSTRAINT [FK_TBAkuntansiJurnalDetail_TBAkuntansiJurnal1]
GO
ALTER TABLE [dbo].[TBAlamat]  WITH CHECK ADD  CONSTRAINT [FK_TBAlamat_TBPelanggan] FOREIGN KEY([IDPelanggan])
REFERENCES [dbo].[TBPelanggan] ([IDPelanggan])
GO
ALTER TABLE [dbo].[TBAlamat] CHECK CONSTRAINT [FK_TBAlamat_TBPelanggan]
GO
ALTER TABLE [dbo].[TBAlamat]  WITH CHECK ADD  CONSTRAINT [FK_TBAlamat_TBWilayah] FOREIGN KEY([IDNegara])
REFERENCES [dbo].[TBWilayah] ([IDWilayah])
GO
ALTER TABLE [dbo].[TBAlamat] CHECK CONSTRAINT [FK_TBAlamat_TBWilayah]
GO
ALTER TABLE [dbo].[TBAlamat]  WITH CHECK ADD  CONSTRAINT [FK_TBAlamat_TBWilayah1] FOREIGN KEY([IDProvinsi])
REFERENCES [dbo].[TBWilayah] ([IDWilayah])
GO
ALTER TABLE [dbo].[TBAlamat] CHECK CONSTRAINT [FK_TBAlamat_TBWilayah1]
GO
ALTER TABLE [dbo].[TBAlamat]  WITH CHECK ADD  CONSTRAINT [FK_TBAlamat_TBWilayah2] FOREIGN KEY([IDKota])
REFERENCES [dbo].[TBWilayah] ([IDWilayah])
GO
ALTER TABLE [dbo].[TBAlamat] CHECK CONSTRAINT [FK_TBAlamat_TBWilayah2]
GO
ALTER TABLE [dbo].[TBAtribut]  WITH CHECK ADD  CONSTRAINT [FK_TBAtribut_TBAtributGrup] FOREIGN KEY([IDAtributGrup])
REFERENCES [dbo].[TBAtributGrup] ([IDAtributGrup])
GO
ALTER TABLE [dbo].[TBAtribut] CHECK CONSTRAINT [FK_TBAtribut_TBAtributGrup]
GO
ALTER TABLE [dbo].[TBAtributPilihan]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihan_TBAtribut] FOREIGN KEY([IDAtribut])
REFERENCES [dbo].[TBAtribut] ([IDAtribut])
GO
ALTER TABLE [dbo].[TBAtributPilihan] CHECK CONSTRAINT [FK_TBAtributPilihan_TBAtribut]
GO
ALTER TABLE [dbo].[TBAtributPilihanBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanBahanBaku_TBAtributPilihan] FOREIGN KEY([IDAtributPilihan])
REFERENCES [dbo].[TBAtributPilihan] ([IDAtributPilihan])
GO
ALTER TABLE [dbo].[TBAtributPilihanBahanBaku] CHECK CONSTRAINT [FK_TBAtributPilihanBahanBaku_TBAtributPilihan]
GO
ALTER TABLE [dbo].[TBAtributPilihanBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanBahanBaku_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBAtributPilihanBahanBaku] CHECK CONSTRAINT [FK_TBAtributPilihanBahanBaku_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBAtributPilihanPelanggan]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanPelanggan_TBAtributPilihan] FOREIGN KEY([IDAtributPilihan])
REFERENCES [dbo].[TBAtributPilihan] ([IDAtributPilihan])
GO
ALTER TABLE [dbo].[TBAtributPilihanPelanggan] CHECK CONSTRAINT [FK_TBAtributPilihanPelanggan_TBAtributPilihan]
GO
ALTER TABLE [dbo].[TBAtributPilihanPelanggan]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanPelanggan_TBPelanggan] FOREIGN KEY([IDPelanggan])
REFERENCES [dbo].[TBPelanggan] ([IDPelanggan])
GO
ALTER TABLE [dbo].[TBAtributPilihanPelanggan] CHECK CONSTRAINT [FK_TBAtributPilihanPelanggan_TBPelanggan]
GO
ALTER TABLE [dbo].[TBAtributPilihanPengguna]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanPengguna_TBAtributPilihan] FOREIGN KEY([IDAtributPilihan])
REFERENCES [dbo].[TBAtributPilihan] ([IDAtributPilihan])
GO
ALTER TABLE [dbo].[TBAtributPilihanPengguna] CHECK CONSTRAINT [FK_TBAtributPilihanPengguna_TBAtributPilihan]
GO
ALTER TABLE [dbo].[TBAtributPilihanPengguna]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanPengguna_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBAtributPilihanPengguna] CHECK CONSTRAINT [FK_TBAtributPilihanPengguna_TBPengguna]
GO
ALTER TABLE [dbo].[TBAtributPilihanProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanProduk_TBAtributPilihan] FOREIGN KEY([IDAtributPilihan])
REFERENCES [dbo].[TBAtributPilihan] ([IDAtributPilihan])
GO
ALTER TABLE [dbo].[TBAtributPilihanProduk] CHECK CONSTRAINT [FK_TBAtributPilihanProduk_TBAtributPilihan]
GO
ALTER TABLE [dbo].[TBAtributPilihanProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanProduk_TBProduk] FOREIGN KEY([IDProduk])
REFERENCES [dbo].[TBProduk] ([IDProduk])
GO
ALTER TABLE [dbo].[TBAtributPilihanProduk] CHECK CONSTRAINT [FK_TBAtributPilihanProduk_TBProduk]
GO
ALTER TABLE [dbo].[TBAtributPilihanStore]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanStore_TBAtributPilihan] FOREIGN KEY([IDAtributPilihan])
REFERENCES [dbo].[TBAtributPilihan] ([IDAtributPilihan])
GO
ALTER TABLE [dbo].[TBAtributPilihanStore] CHECK CONSTRAINT [FK_TBAtributPilihanStore_TBAtributPilihan]
GO
ALTER TABLE [dbo].[TBAtributPilihanStore]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanStore_TBStore] FOREIGN KEY([IDStore])
REFERENCES [dbo].[TBStore] ([IDStore])
GO
ALTER TABLE [dbo].[TBAtributPilihanStore] CHECK CONSTRAINT [FK_TBAtributPilihanStore_TBStore]
GO
ALTER TABLE [dbo].[TBAtributPilihanTempat]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanTempat_TBAtributPilihan] FOREIGN KEY([IDAtributPilihan])
REFERENCES [dbo].[TBAtributPilihan] ([IDAtributPilihan])
GO
ALTER TABLE [dbo].[TBAtributPilihanTempat] CHECK CONSTRAINT [FK_TBAtributPilihanTempat_TBAtributPilihan]
GO
ALTER TABLE [dbo].[TBAtributPilihanTempat]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributPilihanTempat_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBAtributPilihanTempat] CHECK CONSTRAINT [FK_TBAtributPilihanTempat_TBTempat]
GO
ALTER TABLE [dbo].[TBAtributProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBAtributProduk_TBAtributProdukGrup] FOREIGN KEY([IDAtributProdukGrup])
REFERENCES [dbo].[TBAtributProdukGrup] ([IDAtributProdukGrup])
GO
ALTER TABLE [dbo].[TBAtributProduk] CHECK CONSTRAINT [FK_TBAtributProduk_TBAtributProdukGrup]
GO
ALTER TABLE [dbo].[TBBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBBahanBaku_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBBahanBaku] CHECK CONSTRAINT [FK_TBBahanBaku_TBSatuan]
GO
ALTER TABLE [dbo].[TBBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBBahanBaku_TBSatuan1] FOREIGN KEY([IDSatuanKonversi])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBBahanBaku] CHECK CONSTRAINT [FK_TBBahanBaku_TBSatuan1]
GO
ALTER TABLE [dbo].[TBDiscount]  WITH CHECK ADD  CONSTRAINT [FK_TBDiscount_TBDiscountEvent] FOREIGN KEY([IDDiscountEvent])
REFERENCES [dbo].[TBDiscountEvent] ([IDDiscountEvent])
GO
ALTER TABLE [dbo].[TBDiscount] CHECK CONSTRAINT [FK_TBDiscount_TBDiscountEvent]
GO
ALTER TABLE [dbo].[TBDiscount]  WITH CHECK ADD  CONSTRAINT [FK_TBDiscount_TBStokProduk] FOREIGN KEY([IDStokProduk])
REFERENCES [dbo].[TBStokProduk] ([IDStokProduk])
GO
ALTER TABLE [dbo].[TBDiscount] CHECK CONSTRAINT [FK_TBDiscount_TBStokProduk]
GO
ALTER TABLE [dbo].[TBDiscountEvent]  WITH CHECK ADD  CONSTRAINT [FK_TBDiscountEvent_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBDiscountEvent] CHECK CONSTRAINT [FK_TBDiscountEvent_TBPengguna]
GO
ALTER TABLE [dbo].[TBDiscountEvent]  WITH CHECK ADD  CONSTRAINT [FK_TBDiscountEvent_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBDiscountEvent] CHECK CONSTRAINT [FK_TBDiscountEvent_TBTempat]
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBDiscountKombinasiProduk_TBGrupPelanggan] FOREIGN KEY([IDGrupPelanggan])
REFERENCES [dbo].[TBGrupPelanggan] ([IDGrupPelanggan])
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk] CHECK CONSTRAINT [FK_TBDiscountKombinasiProduk_TBGrupPelanggan]
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBDiscountKombinasiProduk_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBDiscountKombinasiProduk] CHECK CONSTRAINT [FK_TBDiscountKombinasiProduk_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori]  WITH CHECK ADD  CONSTRAINT [FK_TBDiscountProdukKategori_TBGrupPelanggan] FOREIGN KEY([IDGrupPelanggan])
REFERENCES [dbo].[TBGrupPelanggan] ([IDGrupPelanggan])
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori] CHECK CONSTRAINT [FK_TBDiscountProdukKategori_TBGrupPelanggan]
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori]  WITH CHECK ADD  CONSTRAINT [FK_TBDiscountProdukKategori_TBProdukKategori] FOREIGN KEY([IDProdukKategori])
REFERENCES [dbo].[TBProdukKategori] ([IDProdukKategori])
GO
ALTER TABLE [dbo].[TBDiscountProdukKategori] CHECK CONSTRAINT [FK_TBDiscountProdukKategori_TBProdukKategori]
GO
ALTER TABLE [dbo].[TBForecast]  WITH CHECK ADD  CONSTRAINT [FK_TBForecast_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBForecast] CHECK CONSTRAINT [FK_TBForecast_TBTempat]
GO
ALTER TABLE [dbo].[TBFotoKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBFotoKombinasiProduk_TBFotoProduk] FOREIGN KEY([IDFotoProduk])
REFERENCES [dbo].[TBFotoProduk] ([IDFotoProduk])
GO
ALTER TABLE [dbo].[TBFotoKombinasiProduk] CHECK CONSTRAINT [FK_TBFotoKombinasiProduk_TBFotoProduk]
GO
ALTER TABLE [dbo].[TBFotoKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBGambarKombinasi_TBKombinasi] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBFotoKombinasiProduk] CHECK CONSTRAINT [FK_TBGambarKombinasi_TBKombinasi]
GO
ALTER TABLE [dbo].[TBFotoProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBGambarProduk_TBProduk] FOREIGN KEY([IDProduk])
REFERENCES [dbo].[TBProduk] ([IDProduk])
GO
ALTER TABLE [dbo].[TBFotoProduk] CHECK CONSTRAINT [FK_TBGambarProduk_TBProduk]
GO
ALTER TABLE [dbo].[TBHargaSupplier]  WITH CHECK ADD  CONSTRAINT [FK_TBHargaSupplier_TBStokBahanBaku] FOREIGN KEY([IDStokBahanBaku])
REFERENCES [dbo].[TBStokBahanBaku] ([IDStokBahanBaku])
GO
ALTER TABLE [dbo].[TBHargaSupplier] CHECK CONSTRAINT [FK_TBHargaSupplier_TBStokBahanBaku]
GO
ALTER TABLE [dbo].[TBHargaSupplier]  WITH CHECK ADD  CONSTRAINT [FK_TBHargaSupplier_TBSupplier] FOREIGN KEY([IDSupplier])
REFERENCES [dbo].[TBSupplier] ([IDSupplier])
GO
ALTER TABLE [dbo].[TBHargaSupplier] CHECK CONSTRAINT [FK_TBHargaSupplier_TBSupplier]
GO
ALTER TABLE [dbo].[TBHargaVendor]  WITH CHECK ADD  CONSTRAINT [FK_TBHargaVendor_TBStokProduk] FOREIGN KEY([IDStokProduk])
REFERENCES [dbo].[TBStokProduk] ([IDStokProduk])
GO
ALTER TABLE [dbo].[TBHargaVendor] CHECK CONSTRAINT [FK_TBHargaVendor_TBStokProduk]
GO
ALTER TABLE [dbo].[TBHargaVendor]  WITH CHECK ADD  CONSTRAINT [FK_TBHargaVendor_TBVendor] FOREIGN KEY([IDVendor])
REFERENCES [dbo].[TBVendor] ([IDVendor])
GO
ALTER TABLE [dbo].[TBHargaVendor] CHECK CONSTRAINT [FK_TBHargaVendor_TBVendor]
GO
ALTER TABLE [dbo].[TBJenisBiayaProyeksiDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBJenisBiayaProyeksiDetail_TBJenisBiayaProyeksi] FOREIGN KEY([IDJenisBiayaProyeksi])
REFERENCES [dbo].[TBJenisBiayaProyeksi] ([IDJenisBiayaProyeksi])
GO
ALTER TABLE [dbo].[TBJenisBiayaProyeksiDetail] CHECK CONSTRAINT [FK_TBJenisBiayaProyeksiDetail_TBJenisBiayaProyeksi]
GO
ALTER TABLE [dbo].[TBJenisPembayaran]  WITH CHECK ADD  CONSTRAINT [FK_TBJenisPembayaran_TBAkun] FOREIGN KEY([IDAkun])
REFERENCES [dbo].[TBAkun] ([IDAkun])
GO
ALTER TABLE [dbo].[TBJenisPembayaran] CHECK CONSTRAINT [FK_TBJenisPembayaran_TBAkun]
GO
ALTER TABLE [dbo].[TBJenisPembayaran]  WITH CHECK ADD  CONSTRAINT [FK_TBJenisPembayaran_TBJenisBebanBiaya] FOREIGN KEY([IDJenisBebanBiaya])
REFERENCES [dbo].[TBJenisBebanBiaya] ([IDJenisBebanBiaya])
GO
ALTER TABLE [dbo].[TBJenisPembayaran] CHECK CONSTRAINT [FK_TBJenisPembayaran_TBJenisBebanBiaya]
GO
ALTER TABLE [dbo].[TBJurnal]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiJurnal_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBJurnal] CHECK CONSTRAINT [FK_TBAkuntansiJurnal_TBPengguna]
GO
ALTER TABLE [dbo].[TBJurnal]  WITH CHECK ADD  CONSTRAINT [FK_TBJurnal_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBJurnal] CHECK CONSTRAINT [FK_TBJurnal_TBTempat]
GO
ALTER TABLE [dbo].[TBJurnalDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiJurnalDetail_TBAkuntansiAkun] FOREIGN KEY([IDAkun])
REFERENCES [dbo].[TBAkun] ([IDAkun])
GO
ALTER TABLE [dbo].[TBJurnalDetail] CHECK CONSTRAINT [FK_TBAkuntansiJurnalDetail_TBAkuntansiAkun]
GO
ALTER TABLE [dbo].[TBJurnalDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiJurnalDetail_TBAkuntansiJurnal] FOREIGN KEY([IDJurnal])
REFERENCES [dbo].[TBJurnal] ([IDJurnal])
GO
ALTER TABLE [dbo].[TBJurnalDetail] CHECK CONSTRAINT [FK_TBAkuntansiJurnalDetail_TBAkuntansiJurnal]
GO
ALTER TABLE [dbo].[TBJurnalDokumen]  WITH CHECK ADD  CONSTRAINT [FK_TBJurnalDokumen_TBJurnal] FOREIGN KEY([IDJurnal])
REFERENCES [dbo].[TBJurnal] ([IDJurnal])
GO
ALTER TABLE [dbo].[TBJurnalDokumen] CHECK CONSTRAINT [FK_TBJurnalDokumen_TBJurnal]
GO
ALTER TABLE [dbo].[TBJurnalHutangPiutang]  WITH CHECK ADD  CONSTRAINT [FK_TBAkuntansiJurnalHutang_TBAkuntansiJurnal] FOREIGN KEY([IDJurnal])
REFERENCES [dbo].[TBJurnal] ([IDJurnal])
GO
ALTER TABLE [dbo].[TBJurnalHutangPiutang] CHECK CONSTRAINT [FK_TBAkuntansiJurnalHutang_TBAkuntansiJurnal]
GO
ALTER TABLE [dbo].[TBKategoriBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBKategoriBahanBaku_TBKategoriBahanBaku] FOREIGN KEY([IDKategoriBahanBakuParent])
REFERENCES [dbo].[TBKategoriBahanBaku] ([IDKategoriBahanBaku])
GO
ALTER TABLE [dbo].[TBKategoriBahanBaku] CHECK CONSTRAINT [FK_TBKategoriBahanBaku_TBKategoriBahanBaku]
GO
ALTER TABLE [dbo].[TBKategoriKonten]  WITH CHECK ADD  CONSTRAINT [FK_TBKategoriKonten_TBKategoriKonten] FOREIGN KEY([IDKategoriKontenParent])
REFERENCES [dbo].[TBKategoriKonten] ([IDKategoriKonten])
GO
ALTER TABLE [dbo].[TBKategoriKonten] CHECK CONSTRAINT [FK_TBKategoriKonten_TBKategoriKonten]
GO
ALTER TABLE [dbo].[TBKategoriProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBKategoriProduk_TBKategoriProduk] FOREIGN KEY([IDKategoriProdukParent])
REFERENCES [dbo].[TBKategoriProduk] ([IDKategoriProduk])
GO
ALTER TABLE [dbo].[TBKategoriProduk] CHECK CONSTRAINT [FK_TBKategoriProduk_TBKategoriProduk]
GO
ALTER TABLE [dbo].[TBKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBKombinasi_TBProduk] FOREIGN KEY([IDProduk])
REFERENCES [dbo].[TBProduk] ([IDProduk])
GO
ALTER TABLE [dbo].[TBKombinasiProduk] CHECK CONSTRAINT [FK_TBKombinasi_TBProduk]
GO
ALTER TABLE [dbo].[TBKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBKombinasiProduk_TBAtributProduk] FOREIGN KEY([IDAtributProduk])
REFERENCES [dbo].[TBAtributProduk] ([IDAtributProduk])
GO
ALTER TABLE [dbo].[TBKombinasiProduk] CHECK CONSTRAINT [FK_TBKombinasiProduk_TBAtributProduk]
GO
ALTER TABLE [dbo].[TBKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBKombinasiProduk_TBAtributProduk1] FOREIGN KEY([IDAtributProduk1])
REFERENCES [dbo].[TBAtributProduk] ([IDAtributProduk])
GO
ALTER TABLE [dbo].[TBKombinasiProduk] CHECK CONSTRAINT [FK_TBKombinasiProduk_TBAtributProduk1]
GO
ALTER TABLE [dbo].[TBKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBKombinasiProduk_TBAtributProduk2] FOREIGN KEY([IDAtributProduk2])
REFERENCES [dbo].[TBAtributProduk] ([IDAtributProduk])
GO
ALTER TABLE [dbo].[TBKombinasiProduk] CHECK CONSTRAINT [FK_TBKombinasiProduk_TBAtributProduk2]
GO
ALTER TABLE [dbo].[TBKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBKombinasiProduk_TBAtributProduk3] FOREIGN KEY([IDAtributProduk3])
REFERENCES [dbo].[TBAtributProduk] ([IDAtributProduk])
GO
ALTER TABLE [dbo].[TBKombinasiProduk] CHECK CONSTRAINT [FK_TBKombinasiProduk_TBAtributProduk3]
GO
ALTER TABLE [dbo].[TBKomposisiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBKomposisiBahanBaku_TBBahanBaku] FOREIGN KEY([IDBahanBakuProduksi])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBKomposisiBahanBaku] CHECK CONSTRAINT [FK_TBKomposisiBahanBaku_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBKomposisiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBKomposisiBahanBaku_TBBahanBaku1] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBKomposisiBahanBaku] CHECK CONSTRAINT [FK_TBKomposisiBahanBaku_TBBahanBaku1]
GO
ALTER TABLE [dbo].[TBKomposisiKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBKomposisiKombinasiProduk_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBKomposisiKombinasiProduk] CHECK CONSTRAINT [FK_TBKomposisiKombinasiProduk_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBKomposisiKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBKomposisiKombinasiProduk_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBKomposisiKombinasiProduk] CHECK CONSTRAINT [FK_TBKomposisiKombinasiProduk_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBKonfigurasi]  WITH CHECK ADD  CONSTRAINT [FK_TBKonfigurasi_TBKonfigurasiKategori] FOREIGN KEY([IDKonfigurasiKategori])
REFERENCES [dbo].[TBKonfigurasiKategori] ([IDKonfigurasiKategori])
GO
ALTER TABLE [dbo].[TBKonfigurasi] CHECK CONSTRAINT [FK_TBKonfigurasi_TBKonfigurasiKategori]
GO
ALTER TABLE [dbo].[TBKonfigurasiAkun]  WITH CHECK ADD  CONSTRAINT [FK_TBKonfigurasiAkun_TBAkun] FOREIGN KEY([IDAkun])
REFERENCES [dbo].[TBAkun] ([IDAkun])
GO
ALTER TABLE [dbo].[TBKonfigurasiAkun] CHECK CONSTRAINT [FK_TBKonfigurasiAkun_TBAkun]
GO
ALTER TABLE [dbo].[TBKonfigurasiAkun]  WITH CHECK ADD  CONSTRAINT [FK_TBKonfigurasiAkun_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBKonfigurasiAkun] CHECK CONSTRAINT [FK_TBKonfigurasiAkun_TBTempat]
GO
ALTER TABLE [dbo].[TBKontenKategori]  WITH CHECK ADD  CONSTRAINT [FK_TBKontenKategori_TBKategoriKonten] FOREIGN KEY([IDKategoriKonten])
REFERENCES [dbo].[TBKategoriKonten] ([IDKategoriKonten])
GO
ALTER TABLE [dbo].[TBKontenKategori] CHECK CONSTRAINT [FK_TBKontenKategori_TBKategoriKonten]
GO
ALTER TABLE [dbo].[TBKontenKategori]  WITH CHECK ADD  CONSTRAINT [FK_TBKontenKategori_TBKonten] FOREIGN KEY([IDKonten])
REFERENCES [dbo].[TBKonten] ([IDKonten])
GO
ALTER TABLE [dbo].[TBKontenKategori] CHECK CONSTRAINT [FK_TBKontenKategori_TBKonten]
GO
ALTER TABLE [dbo].[TBKurirBiaya]  WITH CHECK ADD  CONSTRAINT [FK_TBKurirBiaya_TBKurir] FOREIGN KEY([IDKurir])
REFERENCES [dbo].[TBKurir] ([IDKurir])
GO
ALTER TABLE [dbo].[TBKurirBiaya] CHECK CONSTRAINT [FK_TBKurirBiaya_TBKurir]
GO
ALTER TABLE [dbo].[TBKurirBiaya]  WITH CHECK ADD  CONSTRAINT [FK_TBKurirBiaya_TBWilayah] FOREIGN KEY([IDWilayah])
REFERENCES [dbo].[TBWilayah] ([IDWilayah])
GO
ALTER TABLE [dbo].[TBKurirBiaya] CHECK CONSTRAINT [FK_TBKurirBiaya_TBWilayah]
GO
ALTER TABLE [dbo].[TBLogPengguna]  WITH CHECK ADD  CONSTRAINT [FK_TBLogPengguna_TBLogPenggunaTipe] FOREIGN KEY([IDLogPenggunaTipe])
REFERENCES [dbo].[TBLogPenggunaTipe] ([IDLogPenggunaTipe])
GO
ALTER TABLE [dbo].[TBLogPengguna] CHECK CONSTRAINT [FK_TBLogPengguna_TBLogPenggunaTipe]
GO
ALTER TABLE [dbo].[TBLogPengguna]  WITH CHECK ADD  CONSTRAINT [FK_TBLogPengguna_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBLogPengguna] CHECK CONSTRAINT [FK_TBLogPengguna_TBPengguna]
GO
ALTER TABLE [dbo].[TBMeja]  WITH CHECK ADD  CONSTRAINT [FK_TBMeja_TBStatusMeja] FOREIGN KEY([IDStatusMeja])
REFERENCES [dbo].[TBStatusMeja] ([IDStatusMeja])
GO
ALTER TABLE [dbo].[TBMeja] CHECK CONSTRAINT [FK_TBMeja_TBStatusMeja]
GO
ALTER TABLE [dbo].[TBMenu]  WITH CHECK ADD  CONSTRAINT [FK_TBMenu_TBMenu] FOREIGN KEY([IDMenuParent])
REFERENCES [dbo].[TBMenu] ([IDMenu])
GO
ALTER TABLE [dbo].[TBMenu] CHECK CONSTRAINT [FK_TBMenu_TBMenu]
GO
ALTER TABLE [dbo].[TBMenu]  WITH CHECK ADD  CONSTRAINT [FK_TBMenu_TBMenuGrup] FOREIGN KEY([IDMenuGrup])
REFERENCES [dbo].[TBMenuGrup] ([IDMenuGrup])
GO
ALTER TABLE [dbo].[TBMenu] CHECK CONSTRAINT [FK_TBMenu_TBMenuGrup]
GO
ALTER TABLE [dbo].[TBMenubar]  WITH CHECK ADD  CONSTRAINT [FK_TBMenubar_TBMenubar] FOREIGN KEY([IDMenubarParent])
REFERENCES [dbo].[TBMenubar] ([IDMenubar])
GO
ALTER TABLE [dbo].[TBMenubar] CHECK CONSTRAINT [FK_TBMenubar_TBMenubar]
GO
ALTER TABLE [dbo].[TBMenubarPenggunaGrup]  WITH CHECK ADD  CONSTRAINT [FK_TBMenubarPenggunaGrup_TBGrupPengguna] FOREIGN KEY([IDGrupPengguna])
REFERENCES [dbo].[TBGrupPengguna] ([IDGrupPengguna])
GO
ALTER TABLE [dbo].[TBMenubarPenggunaGrup] CHECK CONSTRAINT [FK_TBMenubarPenggunaGrup_TBGrupPengguna]
GO
ALTER TABLE [dbo].[TBMenubarPenggunaGrup]  WITH CHECK ADD  CONSTRAINT [FK_TBMenubarPenggunaGrup_TBMenubar] FOREIGN KEY([IDMenubar])
REFERENCES [dbo].[TBMenubar] ([IDMenubar])
GO
ALTER TABLE [dbo].[TBMenubarPenggunaGrup] CHECK CONSTRAINT [FK_TBMenubarPenggunaGrup_TBMenubar]
GO
ALTER TABLE [dbo].[TBNotifikasi]  WITH CHECK ADD  CONSTRAINT [FK_TBNotifikasi_TBPengguna] FOREIGN KEY([IDPenggunaPengirim])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBNotifikasi] CHECK CONSTRAINT [FK_TBNotifikasi_TBPengguna]
GO
ALTER TABLE [dbo].[TBNotifikasi]  WITH CHECK ADD  CONSTRAINT [FK_TBNotifikasi_TBPengguna1] FOREIGN KEY([IDPenggunaPenerima])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBNotifikasi] CHECK CONSTRAINT [FK_TBNotifikasi_TBPengguna1]
GO
ALTER TABLE [dbo].[TBPelanggan]  WITH CHECK ADD  CONSTRAINT [FK_TBPelanggan_TBGrupPelanggan] FOREIGN KEY([IDGrupPelanggan])
REFERENCES [dbo].[TBGrupPelanggan] ([IDGrupPelanggan])
GO
ALTER TABLE [dbo].[TBPelanggan] CHECK CONSTRAINT [FK_TBPelanggan_TBGrupPelanggan]
GO
ALTER TABLE [dbo].[TBPelanggan]  WITH CHECK ADD  CONSTRAINT [FK_TBPelanggan_TBPengguna] FOREIGN KEY([IDPenggunaPIC])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPelanggan] CHECK CONSTRAINT [FK_TBPelanggan_TBPengguna]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBaku_TBPengguna] FOREIGN KEY([IDPenggunaDatang])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBaku_TBPengguna]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBaku_TBPengguna1] FOREIGN KEY([IDPenggunaTerima])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBaku_TBPengguna1]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBaku_TBPOProduksiBahanBaku] FOREIGN KEY([IDPOProduksiBahanBaku])
REFERENCES [dbo].[TBPOProduksiBahanBaku] ([IDPOProduksiBahanBaku])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBaku_TBPOProduksiBahanBaku]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBaku_TBPOProduksiBahanBakuPenagihan] FOREIGN KEY([IDPOProduksiBahanBakuPenagihan])
REFERENCES [dbo].[TBPOProduksiBahanBakuPenagihan] ([IDPOProduksiBahanBakuPenagihan])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBaku_TBPOProduksiBahanBakuPenagihan]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBakuDetail_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBakuDetail] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBakuDetail_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBakuDetail_TBPenerimaanPOProduksiBahanBaku] FOREIGN KEY([IDPenerimaanPOProduksiBahanBaku])
REFERENCES [dbo].[TBPenerimaanPOProduksiBahanBaku] ([IDPenerimaanPOProduksiBahanBaku])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBakuDetail] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBakuDetail_TBPenerimaanPOProduksiBahanBaku]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBakuDetail_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiBahanBakuDetail] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiBahanBakuDetail_TBSatuan]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiProduk_TBPengguna] FOREIGN KEY([IDPenggunaDatang])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProduk] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiProduk_TBPengguna]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiProduk_TBPengguna1] FOREIGN KEY([IDPenggunaTerima])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProduk] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiProduk_TBPengguna1]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiProduk_TBPOProduksiProduk] FOREIGN KEY([IDPOProduksiProduk])
REFERENCES [dbo].[TBPOProduksiProduk] ([IDPOProduksiProduk])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProduk] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiProduk_TBPOProduksiProduk]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiProduk_TBPOProduksiProdukPenagihan] FOREIGN KEY([IDPOProduksiProdukPenagihan])
REFERENCES [dbo].[TBPOProduksiProdukPenagihan] ([IDPOProduksiProdukPenagihan])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProduk] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiProduk_TBPOProduksiProdukPenagihan]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProdukDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiProdukDetail_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProdukDetail] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiProdukDetail_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProdukDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPenerimaanPOProduksiProdukDetail_TBPenerimaanPOProduksiProduk] FOREIGN KEY([IDPenerimaanPOProduksiProduk])
REFERENCES [dbo].[TBPenerimaanPOProduksiProduk] ([IDPenerimaanPOProduksiProduk])
GO
ALTER TABLE [dbo].[TBPenerimaanPOProduksiProdukDetail] CHECK CONSTRAINT [FK_TBPenerimaanPOProduksiProdukDetail_TBPenerimaanPOProduksiProduk]
GO
ALTER TABLE [dbo].[TBPengguna]  WITH CHECK ADD  CONSTRAINT [FK_TBPengguna_TBJabatan] FOREIGN KEY([IDGrupPengguna])
REFERENCES [dbo].[TBGrupPengguna] ([IDGrupPengguna])
GO
ALTER TABLE [dbo].[TBPengguna] CHECK CONSTRAINT [FK_TBPengguna_TBJabatan]
GO
ALTER TABLE [dbo].[TBPengguna]  WITH CHECK ADD  CONSTRAINT [FK_TBPengguna_TBPengguna] FOREIGN KEY([IDPenggunaParent])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPengguna] CHECK CONSTRAINT [FK_TBPengguna_TBPengguna]
GO
ALTER TABLE [dbo].[TBPengguna]  WITH CHECK ADD  CONSTRAINT [FK_TBPengguna_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBPengguna] CHECK CONSTRAINT [FK_TBPengguna_TBTempat]
GO
ALTER TABLE [dbo].[TBPenggunaJadwal]  WITH CHECK ADD  CONSTRAINT [FK_TBPenggunaJadwal_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPenggunaJadwal] CHECK CONSTRAINT [FK_TBPenggunaJadwal_TBPengguna]
GO
ALTER TABLE [dbo].[TBPenggunaLogKehadiran]  WITH CHECK ADD  CONSTRAINT [FK_TBPenggunaLogKehadiran_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPenggunaLogKehadiran] CHECK CONSTRAINT [FK_TBPenggunaLogKehadiran_TBPengguna]
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPengirimanPOProduksiBahanBaku_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPengirimanPOProduksiBahanBaku_TBPengguna]
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPengirimanPOProduksiBahanBaku_TBPOProduksiBahanBaku] FOREIGN KEY([IDPOProduksiBahanBaku])
REFERENCES [dbo].[TBPOProduksiBahanBaku] ([IDPOProduksiBahanBaku])
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPengirimanPOProduksiBahanBaku_TBPOProduksiBahanBaku]
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPengirimanPOProduksiBahanBakuDetail_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiBahanBakuDetail] CHECK CONSTRAINT [FK_TBPengirimanPOProduksiBahanBakuDetail_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPengirimanPOProduksiBahanBakuDetail_TBPengirimanPOProduksiBahanBaku] FOREIGN KEY([IDPengirimanPOProduksiBahanBaku])
REFERENCES [dbo].[TBPengirimanPOProduksiBahanBaku] ([IDPengirimanPOProduksiBahanBaku])
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiBahanBakuDetail] CHECK CONSTRAINT [FK_TBPengirimanPOProduksiBahanBakuDetail_TBPengirimanPOProduksiBahanBaku]
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPengirimanPOProduksiBahanBakuDetail_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiBahanBakuDetail] CHECK CONSTRAINT [FK_TBPengirimanPOProduksiBahanBakuDetail_TBSatuan]
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPengirimanPOProduksiProduk_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiProduk] CHECK CONSTRAINT [FK_TBPengirimanPOProduksiProduk_TBPengguna]
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPengirimanPOProduksiProduk_TBPOProduksiProduk] FOREIGN KEY([IDPOProduksiProduk])
REFERENCES [dbo].[TBPOProduksiProduk] ([IDPOProduksiProduk])
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiProduk] CHECK CONSTRAINT [FK_TBPengirimanPOProduksiProduk_TBPOProduksiProduk]
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiProdukDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPengirimanPOProduksiProdukDetail_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiProdukDetail] CHECK CONSTRAINT [FK_TBPengirimanPOProduksiProdukDetail_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiProdukDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPengirimanPOProduksiProdukDetail_TBPengirimanPOProduksiProduk] FOREIGN KEY([IDPengirimanPOProduksiProduk])
REFERENCES [dbo].[TBPengirimanPOProduksiProduk] ([IDPengirimanPOProduksiProduk])
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiProdukDetail] CHECK CONSTRAINT [FK_TBPengirimanPOProduksiProdukDetail_TBPengirimanPOProduksiProduk]
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiProdukDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPengirimanPOProduksiProdukDetail_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBPengirimanPOProduksiProdukDetail] CHECK CONSTRAINT [FK_TBPengirimanPOProduksiProdukDetail_TBSatuan]
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPerpindahanStokBahanBaku_TBJenisPerpindahanStok] FOREIGN KEY([IDJenisPerpindahanStok])
REFERENCES [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok])
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku] CHECK CONSTRAINT [FK_TBPerpindahanStokBahanBaku_TBJenisPerpindahanStok]
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPerpindahanStokBahanBaku_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku] CHECK CONSTRAINT [FK_TBPerpindahanStokBahanBaku_TBPengguna]
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPerpindahanStokBahanBaku_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku] CHECK CONSTRAINT [FK_TBPerpindahanStokBahanBaku_TBSatuan]
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPerpindahanStokBahanBaku_TBStokBahanBaku] FOREIGN KEY([IDStokBahanBaku])
REFERENCES [dbo].[TBStokBahanBaku] ([IDStokBahanBaku])
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku] CHECK CONSTRAINT [FK_TBPerpindahanStokBahanBaku_TBStokBahanBaku]
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPerpindahanStokBahanBaku_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBPerpindahanStokBahanBaku] CHECK CONSTRAINT [FK_TBPerpindahanStokBahanBaku_TBTempat]
GO
ALTER TABLE [dbo].[TBPerpindahanStokProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPerpindahanStokProduk_TBJenisPerpindahanStok] FOREIGN KEY([IDJenisPerpindahanStok])
REFERENCES [dbo].[TBJenisPerpindahanStok] ([IDJenisPerpindahanStok])
GO
ALTER TABLE [dbo].[TBPerpindahanStokProduk] CHECK CONSTRAINT [FK_TBPerpindahanStokProduk_TBJenisPerpindahanStok]
GO
ALTER TABLE [dbo].[TBPerpindahanStokProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPerpindahanStokProduk_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPerpindahanStokProduk] CHECK CONSTRAINT [FK_TBPerpindahanStokProduk_TBPengguna]
GO
ALTER TABLE [dbo].[TBPerpindahanStokProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPerpindahanStokProduk_TBStokProduk] FOREIGN KEY([IDStokProduk])
REFERENCES [dbo].[TBStokProduk] ([IDStokProduk])
GO
ALTER TABLE [dbo].[TBPerpindahanStokProduk] CHECK CONSTRAINT [FK_TBPerpindahanStokProduk_TBStokProduk]
GO
ALTER TABLE [dbo].[TBPerpindahanStokProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPerpindahanStokProduk_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBPerpindahanStokProduk] CHECK CONSTRAINT [FK_TBPerpindahanStokProduk_TBTempat]
GO
ALTER TABLE [dbo].[TBPesanPrint]  WITH CHECK ADD  CONSTRAINT [FK_TBPesanPrint_TBKonfigurasiPrinter] FOREIGN KEY([IDKonfigurasiPrinter])
REFERENCES [dbo].[TBKonfigurasiPrinter] ([IDKonfigurasiPrinter])
GO
ALTER TABLE [dbo].[TBPesanPrint] CHECK CONSTRAINT [FK_TBPesanPrint_TBKonfigurasiPrinter]
GO
ALTER TABLE [dbo].[TBPesanPrint]  WITH CHECK ADD  CONSTRAINT [FK_TBPesanPrint_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPesanPrint] CHECK CONSTRAINT [FK_TBPesanPrint_TBPengguna]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBaku_TBJenisPembayaran] FOREIGN KEY([IDJenisPembayaran])
REFERENCES [dbo].[TBJenisPembayaran] ([IDJenisPembayaran])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPOProduksiBahanBaku_TBJenisPembayaran]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBaku_TBJenisPOProduksi] FOREIGN KEY([IDJenisPOProduksi])
REFERENCES [dbo].[TBJenisPOProduksi] ([IDJenisPOProduksi])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPOProduksiBahanBaku_TBJenisPOProduksi]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBaku_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPOProduksiBahanBaku_TBPengguna]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBaku_TBPengguna1] FOREIGN KEY([IDPenggunaPIC])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPOProduksiBahanBaku_TBPengguna1]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBaku_TBPengguna2] FOREIGN KEY([IDPenggunaDP])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPOProduksiBahanBaku_TBPengguna2]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBaku_TBPOProduksiBahanBakuPenagihan] FOREIGN KEY([IDPOProduksiBahanBakuPenagihan])
REFERENCES [dbo].[TBPOProduksiBahanBakuPenagihan] ([IDPOProduksiBahanBakuPenagihan])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPOProduksiBahanBaku_TBPOProduksiBahanBakuPenagihan]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBaku_TBProyeksi] FOREIGN KEY([IDProyeksi])
REFERENCES [dbo].[TBProyeksi] ([IDProyeksi])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPOProduksiBahanBaku_TBProyeksi]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBaku_TBSupplier] FOREIGN KEY([IDSupplier])
REFERENCES [dbo].[TBSupplier] ([IDSupplier])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPOProduksiBahanBaku_TBSupplier]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBaku_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBaku] CHECK CONSTRAINT [FK_TBPOProduksiBahanBaku_TBTempat]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuBiayaTambahan]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuBiayaTambahan_TBJenisBiayaProduksi] FOREIGN KEY([IDJenisBiayaProduksi])
REFERENCES [dbo].[TBJenisBiayaProduksi] ([IDJenisBiayaProduksi])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuBiayaTambahan] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuBiayaTambahan_TBJenisBiayaProduksi]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuBiayaTambahan]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuBiayaTambahan_TBPOProduksiBahanBaku] FOREIGN KEY([IDPOProduksiBahanBaku])
REFERENCES [dbo].[TBPOProduksiBahanBaku] ([IDPOProduksiBahanBaku])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuBiayaTambahan] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuBiayaTambahan_TBPOProduksiBahanBaku]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuDetail_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuDetail] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuDetail_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuDetail_TBPOProduksiBahanBaku] FOREIGN KEY([IDPOProduksiBahanBaku])
REFERENCES [dbo].[TBPOProduksiBahanBaku] ([IDPOProduksiBahanBaku])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuDetail] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuDetail_TBPOProduksiBahanBaku]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuDetail_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuDetail] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuDetail_TBSatuan]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuKomposisi]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuKomposisi_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuKomposisi] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuKomposisi_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuKomposisi]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuKomposisi_TBPOProduksiBahanBaku] FOREIGN KEY([IDPOProduksiBahanBaku])
REFERENCES [dbo].[TBPOProduksiBahanBaku] ([IDPOProduksiBahanBaku])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuKomposisi] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuKomposisi_TBPOProduksiBahanBaku]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuKomposisi]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuKomposisi_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuKomposisi] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuKomposisi_TBSatuan]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihan]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihan_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihan] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihan_TBPengguna]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihan]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihan_TBSupplier] FOREIGN KEY([IDSupplier])
REFERENCES [dbo].[TBSupplier] ([IDSupplier])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihan] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihan_TBSupplier]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihan]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihan_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihan] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihan_TBTempat]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihanDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihanDetail_TBJenisPembayaran] FOREIGN KEY([IDJenisPembayaran])
REFERENCES [dbo].[TBJenisPembayaran] ([IDJenisPembayaran])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihanDetail] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihanDetail_TBJenisPembayaran]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihanDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihanDetail_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihanDetail] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihanDetail_TBPengguna]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihanDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihanDetail_TBPOProduksiBahanBakuPenagihan] FOREIGN KEY([IDPOProduksiBahanBakuPenagihan])
REFERENCES [dbo].[TBPOProduksiBahanBakuPenagihan] ([IDPOProduksiBahanBakuPenagihan])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuPenagihanDetail] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuPenagihanDetail_TBPOProduksiBahanBakuPenagihan]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuRetur]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuRetur_TBPenerimaanPOProduksiBahanBaku] FOREIGN KEY([IDPenerimaanPOProduksiBahanBaku])
REFERENCES [dbo].[TBPenerimaanPOProduksiBahanBaku] ([IDPenerimaanPOProduksiBahanBaku])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuRetur] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuRetur_TBPenerimaanPOProduksiBahanBaku]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuRetur]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuRetur_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuRetur] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuRetur_TBPengguna]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuRetur]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuRetur_TBPOProduksiBahanBakuPenagihan] FOREIGN KEY([IDPOProduksiBahanBakuPenagihan])
REFERENCES [dbo].[TBPOProduksiBahanBakuPenagihan] ([IDPOProduksiBahanBakuPenagihan])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuRetur] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuRetur_TBPOProduksiBahanBakuPenagihan]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuRetur]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuRetur_TBSupplier] FOREIGN KEY([IDSupplier])
REFERENCES [dbo].[TBSupplier] ([IDSupplier])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuRetur] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuRetur_TBSupplier]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuRetur]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuRetur_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuRetur] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuRetur_TBTempat]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuReturDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuReturDetail_TBPOProduksiBahanBakuRetur] FOREIGN KEY([IDPOProduksiBahanBakuRetur])
REFERENCES [dbo].[TBPOProduksiBahanBakuRetur] ([IDPOProduksiBahanBakuRetur])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuReturDetail] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuReturDetail_TBPOProduksiBahanBakuRetur]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuReturDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuReturDetail_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuReturDetail] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuReturDetail_TBSatuan]
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuReturDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiBahanBakuReturDetail_TBStokBahanBaku] FOREIGN KEY([IDStokBahanBaku])
REFERENCES [dbo].[TBStokBahanBaku] ([IDStokBahanBaku])
GO
ALTER TABLE [dbo].[TBPOProduksiBahanBakuReturDetail] CHECK CONSTRAINT [FK_TBPOProduksiBahanBakuReturDetail_TBStokBahanBaku]
GO
ALTER TABLE [dbo].[TBPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProduk_TBJenisPembayaran] FOREIGN KEY([IDJenisPembayaran])
REFERENCES [dbo].[TBJenisPembayaran] ([IDJenisPembayaran])
GO
ALTER TABLE [dbo].[TBPOProduksiProduk] CHECK CONSTRAINT [FK_TBPOProduksiProduk_TBJenisPembayaran]
GO
ALTER TABLE [dbo].[TBPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProduk_TBJenisPOProduksi] FOREIGN KEY([IDJenisPOProduksi])
REFERENCES [dbo].[TBJenisPOProduksi] ([IDJenisPOProduksi])
GO
ALTER TABLE [dbo].[TBPOProduksiProduk] CHECK CONSTRAINT [FK_TBPOProduksiProduk_TBJenisPOProduksi]
GO
ALTER TABLE [dbo].[TBPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProduk_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiProduk] CHECK CONSTRAINT [FK_TBPOProduksiProduk_TBPengguna]
GO
ALTER TABLE [dbo].[TBPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProduk_TBPengguna1] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiProduk] CHECK CONSTRAINT [FK_TBPOProduksiProduk_TBPengguna1]
GO
ALTER TABLE [dbo].[TBPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProduk_TBPengguna2] FOREIGN KEY([IDPenggunaDP])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiProduk] CHECK CONSTRAINT [FK_TBPOProduksiProduk_TBPengguna2]
GO
ALTER TABLE [dbo].[TBPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProduk_TBPOProduksiProdukPenagihan] FOREIGN KEY([IDPOProduksiProdukPenagihan])
REFERENCES [dbo].[TBPOProduksiProdukPenagihan] ([IDPOProduksiProdukPenagihan])
GO
ALTER TABLE [dbo].[TBPOProduksiProduk] CHECK CONSTRAINT [FK_TBPOProduksiProduk_TBPOProduksiProdukPenagihan]
GO
ALTER TABLE [dbo].[TBPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProduk_TBProyeksi] FOREIGN KEY([IDProyeksi])
REFERENCES [dbo].[TBProyeksi] ([IDProyeksi])
GO
ALTER TABLE [dbo].[TBPOProduksiProduk] CHECK CONSTRAINT [FK_TBPOProduksiProduk_TBProyeksi]
GO
ALTER TABLE [dbo].[TBPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProduk_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBPOProduksiProduk] CHECK CONSTRAINT [FK_TBPOProduksiProduk_TBTempat]
GO
ALTER TABLE [dbo].[TBPOProduksiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProduk_TBVendor] FOREIGN KEY([IDVendor])
REFERENCES [dbo].[TBVendor] ([IDVendor])
GO
ALTER TABLE [dbo].[TBPOProduksiProduk] CHECK CONSTRAINT [FK_TBPOProduksiProduk_TBVendor]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukBiayaTambahan]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukBiayaTambahan_TBJenisBiayaProduksi] FOREIGN KEY([IDJenisBiayaProduksi])
REFERENCES [dbo].[TBJenisBiayaProduksi] ([IDJenisBiayaProduksi])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukBiayaTambahan] CHECK CONSTRAINT [FK_TBPOProduksiProdukBiayaTambahan_TBJenisBiayaProduksi]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukBiayaTambahan]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukBiayaTambahan_TBPOProduksiProduk] FOREIGN KEY([IDPOProduksiProduk])
REFERENCES [dbo].[TBPOProduksiProduk] ([IDPOProduksiProduk])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukBiayaTambahan] CHECK CONSTRAINT [FK_TBPOProduksiProdukBiayaTambahan_TBPOProduksiProduk]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukDetail_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukDetail] CHECK CONSTRAINT [FK_TBPOProduksiProdukDetail_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukDetail_TBPOProduksiProduk] FOREIGN KEY([IDPOProduksiProduk])
REFERENCES [dbo].[TBPOProduksiProduk] ([IDPOProduksiProduk])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukDetail] CHECK CONSTRAINT [FK_TBPOProduksiProdukDetail_TBPOProduksiProduk]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukKomposisi]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukKomposisi_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukKomposisi] CHECK CONSTRAINT [FK_TBPOProduksiProdukKomposisi_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukKomposisi]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukKomposisi_TBPOProduksiProduk] FOREIGN KEY([IDPOProduksiProduk])
REFERENCES [dbo].[TBPOProduksiProduk] ([IDPOProduksiProduk])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukKomposisi] CHECK CONSTRAINT [FK_TBPOProduksiProdukKomposisi_TBPOProduksiProduk]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukKomposisi]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukKomposisi_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukKomposisi] CHECK CONSTRAINT [FK_TBPOProduksiProdukKomposisi_TBSatuan]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihan]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukPenagihan_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihan] CHECK CONSTRAINT [FK_TBPOProduksiProdukPenagihan_TBPengguna]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihan]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukPenagihan_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihan] CHECK CONSTRAINT [FK_TBPOProduksiProdukPenagihan_TBTempat]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihan]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukPenagihan_TBVendor] FOREIGN KEY([IDVendor])
REFERENCES [dbo].[TBVendor] ([IDVendor])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihan] CHECK CONSTRAINT [FK_TBPOProduksiProdukPenagihan_TBVendor]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihanDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukPenagihanDetail_TBJenisPembayaran] FOREIGN KEY([IDJenisPembayaran])
REFERENCES [dbo].[TBJenisPembayaran] ([IDJenisPembayaran])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihanDetail] CHECK CONSTRAINT [FK_TBPOProduksiProdukPenagihanDetail_TBJenisPembayaran]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihanDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukPenagihanDetail_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihanDetail] CHECK CONSTRAINT [FK_TBPOProduksiProdukPenagihanDetail_TBPengguna]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihanDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukPenagihanDetail_TBPOProduksiProdukPenagihan] FOREIGN KEY([IDPOProduksiProdukPenagihan])
REFERENCES [dbo].[TBPOProduksiProdukPenagihan] ([IDPOProduksiProdukPenagihan])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukPenagihanDetail] CHECK CONSTRAINT [FK_TBPOProduksiProdukPenagihanDetail_TBPOProduksiProdukPenagihan]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukRetur]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukRetur_TBPenerimaanPOProduksiProduk] FOREIGN KEY([IDPenerimaanPOProduksiProduk])
REFERENCES [dbo].[TBPenerimaanPOProduksiProduk] ([IDPenerimaanPOProduksiProduk])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukRetur] CHECK CONSTRAINT [FK_TBPOProduksiProdukRetur_TBPenerimaanPOProduksiProduk]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukRetur]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukRetur_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukRetur] CHECK CONSTRAINT [FK_TBPOProduksiProdukRetur_TBPengguna]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukRetur]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukRetur_TBPOProduksiProdukPenagihan] FOREIGN KEY([IDPOProduksiProdukPenagihan])
REFERENCES [dbo].[TBPOProduksiProdukPenagihan] ([IDPOProduksiProdukPenagihan])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukRetur] CHECK CONSTRAINT [FK_TBPOProduksiProdukRetur_TBPOProduksiProdukPenagihan]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukRetur]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukRetur_TBSupplier] FOREIGN KEY([IDVendor])
REFERENCES [dbo].[TBVendor] ([IDVendor])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukRetur] CHECK CONSTRAINT [FK_TBPOProduksiProdukRetur_TBSupplier]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukRetur]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukRetur_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukRetur] CHECK CONSTRAINT [FK_TBPOProduksiProdukRetur_TBTempat]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukReturDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukReturDetail_TBPOProduksiProdukRetur] FOREIGN KEY([IDPOProduksiProdukRetur])
REFERENCES [dbo].[TBPOProduksiProdukRetur] ([IDPOProduksiProdukRetur])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukReturDetail] CHECK CONSTRAINT [FK_TBPOProduksiProdukReturDetail_TBPOProduksiProdukRetur]
GO
ALTER TABLE [dbo].[TBPOProduksiProdukReturDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBPOProduksiProdukReturDetail_TBStokProduk] FOREIGN KEY([IDStokProduk])
REFERENCES [dbo].[TBStokProduk] ([IDStokProduk])
GO
ALTER TABLE [dbo].[TBPOProduksiProdukReturDetail] CHECK CONSTRAINT [FK_TBPOProduksiProdukReturDetail_TBStokProduk]
GO
ALTER TABLE [dbo].[TBProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBProduk_TBPemilikProduk] FOREIGN KEY([IDPemilikProduk])
REFERENCES [dbo].[TBPemilikProduk] ([IDPemilikProduk])
GO
ALTER TABLE [dbo].[TBProduk] CHECK CONSTRAINT [FK_TBProduk_TBPemilikProduk]
GO
ALTER TABLE [dbo].[TBProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBProduk_TBProdukKategori] FOREIGN KEY([IDProdukKategori])
REFERENCES [dbo].[TBProdukKategori] ([IDProdukKategori])
GO
ALTER TABLE [dbo].[TBProduk] CHECK CONSTRAINT [FK_TBProduk_TBProdukKategori]
GO
ALTER TABLE [dbo].[TBProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBProduk_TBWarna] FOREIGN KEY([IDWarna])
REFERENCES [dbo].[TBWarna] ([IDWarna])
GO
ALTER TABLE [dbo].[TBProduk] CHECK CONSTRAINT [FK_TBProduk_TBWarna]
GO
ALTER TABLE [dbo].[TBProdukKategori]  WITH CHECK ADD  CONSTRAINT [FK_TBProdukKategori_TBProdukKategori] FOREIGN KEY([IDProdukKategoriParent])
REFERENCES [dbo].[TBProdukKategori] ([IDProdukKategori])
GO
ALTER TABLE [dbo].[TBProdukKategori] CHECK CONSTRAINT [FK_TBProdukKategori_TBProdukKategori]
GO
ALTER TABLE [dbo].[TBProject]  WITH CHECK ADD  CONSTRAINT [FK_TBProject_TBPelanggan] FOREIGN KEY([IDPelanggan])
REFERENCES [dbo].[TBPelanggan] ([IDPelanggan])
GO
ALTER TABLE [dbo].[TBProject] CHECK CONSTRAINT [FK_TBProject_TBPelanggan]
GO
ALTER TABLE [dbo].[TBProject]  WITH CHECK ADD  CONSTRAINT [FK_TBProject_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBProject] CHECK CONSTRAINT [FK_TBProject_TBPengguna]
GO
ALTER TABLE [dbo].[TBProject]  WITH CHECK ADD  CONSTRAINT [FK_TBProject_TBPengguna1] FOREIGN KEY([IDPenggunaUpdate])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBProject] CHECK CONSTRAINT [FK_TBProject_TBPengguna1]
GO
ALTER TABLE [dbo].[TBProject]  WITH CHECK ADD  CONSTRAINT [FK_TBProject_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBProject] CHECK CONSTRAINT [FK_TBProject_TBTempat]
GO
ALTER TABLE [dbo].[TBProject]  WITH CHECK ADD  CONSTRAINT [FK_TBProject_TBTransaksi] FOREIGN KEY([IDTransaksi])
REFERENCES [dbo].[TBTransaksi] ([IDTransaksi])
GO
ALTER TABLE [dbo].[TBProject] CHECK CONSTRAINT [FK_TBProject_TBTransaksi]
GO
ALTER TABLE [dbo].[TBProyeksi]  WITH CHECK ADD  CONSTRAINT [FK_TBProyeksi_TBPemilikProduk] FOREIGN KEY([IDPemilikProduk])
REFERENCES [dbo].[TBPemilikProduk] ([IDPemilikProduk])
GO
ALTER TABLE [dbo].[TBProyeksi] CHECK CONSTRAINT [FK_TBProyeksi_TBPemilikProduk]
GO
ALTER TABLE [dbo].[TBProyeksi]  WITH CHECK ADD  CONSTRAINT [FK_TBProyeksi_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBProyeksi] CHECK CONSTRAINT [FK_TBProyeksi_TBPengguna]
GO
ALTER TABLE [dbo].[TBProyeksi]  WITH CHECK ADD  CONSTRAINT [FK_TBProyeksi_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBProyeksi] CHECK CONSTRAINT [FK_TBProyeksi_TBTempat]
GO
ALTER TABLE [dbo].[TBProyeksiDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBProyeksiDetail_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBProyeksiDetail] CHECK CONSTRAINT [FK_TBProyeksiDetail_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBProyeksiDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBProyeksiDetail_TBProyeksi] FOREIGN KEY([IDProyeksi])
REFERENCES [dbo].[TBProyeksi] ([IDProyeksi])
GO
ALTER TABLE [dbo].[TBProyeksiDetail] CHECK CONSTRAINT [FK_TBProyeksiDetail_TBProyeksi]
GO
ALTER TABLE [dbo].[TBProyeksiKomposisi]  WITH CHECK ADD  CONSTRAINT [FK_TBProyeksiKomposisi_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBProyeksiKomposisi] CHECK CONSTRAINT [FK_TBProyeksiKomposisi_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBProyeksiKomposisi]  WITH CHECK ADD  CONSTRAINT [FK_TBProyeksiKomposisi_TBProyeksi] FOREIGN KEY([IDProyeksi])
REFERENCES [dbo].[TBProyeksi] ([IDProyeksi])
GO
ALTER TABLE [dbo].[TBProyeksiKomposisi] CHECK CONSTRAINT [FK_TBProyeksiKomposisi_TBProyeksi]
GO
ALTER TABLE [dbo].[TBProyeksiKomposisi]  WITH CHECK ADD  CONSTRAINT [FK_TBProyeksiKomposisi_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBProyeksiKomposisi] CHECK CONSTRAINT [FK_TBProyeksiKomposisi_TBSatuan]
GO
ALTER TABLE [dbo].[TBQuotation]  WITH CHECK ADD  CONSTRAINT [FK_TBQuotation_TBProject] FOREIGN KEY([IDProject])
REFERENCES [dbo].[TBProject] ([IDProject])
GO
ALTER TABLE [dbo].[TBQuotation] CHECK CONSTRAINT [FK_TBQuotation_TBProject]
GO
ALTER TABLE [dbo].[TBQuotationDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBQuotationDetail_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBQuotationDetail] CHECK CONSTRAINT [FK_TBQuotationDetail_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBQuotationDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBQuotationDetail_TBQuotation] FOREIGN KEY([IDQuotation])
REFERENCES [dbo].[TBQuotation] ([IDQuotation])
GO
ALTER TABLE [dbo].[TBQuotationDetail] CHECK CONSTRAINT [FK_TBQuotationDetail_TBQuotation]
GO
ALTER TABLE [dbo].[TBRekomendasiKategoriProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBRekomendasiKategoriProduk_TBKategoriProduk] FOREIGN KEY([IDKategoriProduk1])
REFERENCES [dbo].[TBKategoriProduk] ([IDKategoriProduk])
GO
ALTER TABLE [dbo].[TBRekomendasiKategoriProduk] CHECK CONSTRAINT [FK_TBRekomendasiKategoriProduk_TBKategoriProduk]
GO
ALTER TABLE [dbo].[TBRekomendasiKategoriProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBRekomendasiKategoriProduk_TBKategoriProduk1] FOREIGN KEY([IDKategoriProduk2])
REFERENCES [dbo].[TBKategoriProduk] ([IDKategoriProduk])
GO
ALTER TABLE [dbo].[TBRekomendasiKategoriProduk] CHECK CONSTRAINT [FK_TBRekomendasiKategoriProduk_TBKategoriProduk1]
GO
ALTER TABLE [dbo].[TBRekomendasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBRekomendasiProduk_TBProduk] FOREIGN KEY([IDProduk1])
REFERENCES [dbo].[TBProduk] ([IDProduk])
GO
ALTER TABLE [dbo].[TBRekomendasiProduk] CHECK CONSTRAINT [FK_TBRekomendasiProduk_TBProduk]
GO
ALTER TABLE [dbo].[TBRekomendasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBRekomendasiProduk_TBProduk1] FOREIGN KEY([IDProduk2])
REFERENCES [dbo].[TBProduk] ([IDProduk])
GO
ALTER TABLE [dbo].[TBRekomendasiProduk] CHECK CONSTRAINT [FK_TBRekomendasiProduk_TBProduk1]
GO
ALTER TABLE [dbo].[TBRelasiBahanBakuKategoriBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBRelasiBahanBakuKategoriBahanBaku_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBRelasiBahanBakuKategoriBahanBaku] CHECK CONSTRAINT [FK_TBRelasiBahanBakuKategoriBahanBaku_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBRelasiBahanBakuKategoriBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBRelasiBahanBakuKategoriBahanBaku_TBKategoriBahanBaku] FOREIGN KEY([IDKategoriBahanBaku])
REFERENCES [dbo].[TBKategoriBahanBaku] ([IDKategoriBahanBaku])
GO
ALTER TABLE [dbo].[TBRelasiBahanBakuKategoriBahanBaku] CHECK CONSTRAINT [FK_TBRelasiBahanBakuKategoriBahanBaku_TBKategoriBahanBaku]
GO
ALTER TABLE [dbo].[TBRelasiJenisBiayaProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBRelasiJenisBiayaProduksiBahanBaku_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBRelasiJenisBiayaProduksiBahanBaku] CHECK CONSTRAINT [FK_TBRelasiJenisBiayaProduksiBahanBaku_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBRelasiJenisBiayaProduksiBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBRelasiJenisBiayaProduksiBahanBaku_TBJenisBiayaProduksi] FOREIGN KEY([IDJenisBiayaProduksi])
REFERENCES [dbo].[TBJenisBiayaProduksi] ([IDJenisBiayaProduksi])
GO
ALTER TABLE [dbo].[TBRelasiJenisBiayaProduksiBahanBaku] CHECK CONSTRAINT [FK_TBRelasiJenisBiayaProduksiBahanBaku_TBJenisBiayaProduksi]
GO
ALTER TABLE [dbo].[TBRelasiJenisBiayaProduksiKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBRelasiJenisBiayaProduksiKombinasiProduk_TBJenisBiayaProduksi] FOREIGN KEY([IDJenisBiayaProduksi])
REFERENCES [dbo].[TBJenisBiayaProduksi] ([IDJenisBiayaProduksi])
GO
ALTER TABLE [dbo].[TBRelasiJenisBiayaProduksiKombinasiProduk] CHECK CONSTRAINT [FK_TBRelasiJenisBiayaProduksiKombinasiProduk_TBJenisBiayaProduksi]
GO
ALTER TABLE [dbo].[TBRelasiJenisBiayaProduksiKombinasiProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBRelasiJenisBiayaProduksiKombinasiProduk_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBRelasiJenisBiayaProduksiKombinasiProduk] CHECK CONSTRAINT [FK_TBRelasiJenisBiayaProduksiKombinasiProduk_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBRelasiPenggunaMenu]  WITH CHECK ADD  CONSTRAINT [FK_TBRelasiPenggunaMenu_TBMenu] FOREIGN KEY([IDMenu])
REFERENCES [dbo].[TBMenu] ([IDMenu])
GO
ALTER TABLE [dbo].[TBRelasiPenggunaMenu] CHECK CONSTRAINT [FK_TBRelasiPenggunaMenu_TBMenu]
GO
ALTER TABLE [dbo].[TBRelasiPenggunaMenu]  WITH CHECK ADD  CONSTRAINT [FK_TBRelasiPenggunaMenu_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBRelasiPenggunaMenu] CHECK CONSTRAINT [FK_TBRelasiPenggunaMenu_TBPengguna]
GO
ALTER TABLE [dbo].[TBRelasiProdukKategoriProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBRelasiProdukKategoriProduk_TBKategoriProduk] FOREIGN KEY([IDKategoriProduk])
REFERENCES [dbo].[TBKategoriProduk] ([IDKategoriProduk])
GO
ALTER TABLE [dbo].[TBRelasiProdukKategoriProduk] CHECK CONSTRAINT [FK_TBRelasiProdukKategoriProduk_TBKategoriProduk]
GO
ALTER TABLE [dbo].[TBRelasiProdukKategoriProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBRelasiProdukKategoriProduk_TBProduk] FOREIGN KEY([IDProduk])
REFERENCES [dbo].[TBProduk] ([IDProduk])
GO
ALTER TABLE [dbo].[TBRelasiProdukKategoriProduk] CHECK CONSTRAINT [FK_TBRelasiProdukKategoriProduk_TBProduk]
GO
ALTER TABLE [dbo].[TBRestaurantOrder]  WITH CHECK ADD  CONSTRAINT [FK_TBRestaurantOrder_TBMeja] FOREIGN KEY([IDMeja])
REFERENCES [dbo].[TBMeja] ([IDMeja])
GO
ALTER TABLE [dbo].[TBRestaurantOrder] CHECK CONSTRAINT [FK_TBRestaurantOrder_TBMeja]
GO
ALTER TABLE [dbo].[TBRestaurantOrder]  WITH CHECK ADD  CONSTRAINT [FK_TBRestaurantOrder_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBRestaurantOrder] CHECK CONSTRAINT [FK_TBRestaurantOrder_TBPengguna]
GO
ALTER TABLE [dbo].[TBRestaurantOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBRestaurantOrderDetail_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBRestaurantOrderDetail] CHECK CONSTRAINT [FK_TBRestaurantOrderDetail_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBRestaurantOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBRestaurantOrderDetail_TBRestaurantOrder1] FOREIGN KEY([NomorOrder])
REFERENCES [dbo].[TBRestaurantOrder] ([NomorOrder])
GO
ALTER TABLE [dbo].[TBRestaurantOrderDetail] CHECK CONSTRAINT [FK_TBRestaurantOrderDetail_TBRestaurantOrder1]
GO
ALTER TABLE [dbo].[TBSoal]  WITH CHECK ADD  CONSTRAINT [FK_TBSoal_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBSoal] CHECK CONSTRAINT [FK_TBSoal_TBPengguna]
GO
ALTER TABLE [dbo].[TBSoal]  WITH CHECK ADD  CONSTRAINT [FK_TBSoal_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBSoal] CHECK CONSTRAINT [FK_TBSoal_TBTempat]
GO
ALTER TABLE [dbo].[TBSoalJawaban]  WITH CHECK ADD  CONSTRAINT [FK_TBSoalJawaban_TBSoalPertanyaan] FOREIGN KEY([IDSoalPertanyaan])
REFERENCES [dbo].[TBSoalPertanyaan] ([IDSoalPertanyaan])
GO
ALTER TABLE [dbo].[TBSoalJawaban] CHECK CONSTRAINT [FK_TBSoalJawaban_TBSoalPertanyaan]
GO
ALTER TABLE [dbo].[TBSoalJawabanPelanggan]  WITH CHECK ADD  CONSTRAINT [FK_TBSoalJawabanPelanggan_TBPelanggan] FOREIGN KEY([IDPelanggan])
REFERENCES [dbo].[TBPelanggan] ([IDPelanggan])
GO
ALTER TABLE [dbo].[TBSoalJawabanPelanggan] CHECK CONSTRAINT [FK_TBSoalJawabanPelanggan_TBPelanggan]
GO
ALTER TABLE [dbo].[TBSoalJawabanPelanggan]  WITH CHECK ADD  CONSTRAINT [FK_TBSoalJawabanPelanggan_TBSoalJawaban] FOREIGN KEY([IDSoalJawaban])
REFERENCES [dbo].[TBSoalJawaban] ([IDSoalJawaban])
GO
ALTER TABLE [dbo].[TBSoalJawabanPelanggan] CHECK CONSTRAINT [FK_TBSoalJawabanPelanggan_TBSoalJawaban]
GO
ALTER TABLE [dbo].[TBSoalPertanyaan]  WITH CHECK ADD  CONSTRAINT [FK_TBSoalPertanyaan_TBSoal] FOREIGN KEY([IDSoal])
REFERENCES [dbo].[TBSoal] ([IDSoal])
GO
ALTER TABLE [dbo].[TBSoalPertanyaan] CHECK CONSTRAINT [FK_TBSoalPertanyaan_TBSoal]
GO
ALTER TABLE [dbo].[TBStokBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBStokBahanBaku_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBStokBahanBaku] CHECK CONSTRAINT [FK_TBStokBahanBaku_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBStokBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBStokBahanBaku_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBStokBahanBaku] CHECK CONSTRAINT [FK_TBStokBahanBaku_TBTempat]
GO
ALTER TABLE [dbo].[TBStokProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBStokProduk_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBStokProduk] CHECK CONSTRAINT [FK_TBStokProduk_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBStokProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBStokProduk_TBStokProdukPenyimpanan] FOREIGN KEY([IDStokProdukPenyimpanan])
REFERENCES [dbo].[TBStokProdukPenyimpanan] ([IDStokProdukPenyimpanan])
GO
ALTER TABLE [dbo].[TBStokProduk] CHECK CONSTRAINT [FK_TBStokProduk_TBStokProdukPenyimpanan]
GO
ALTER TABLE [dbo].[TBStokProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBStokProduk_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBStokProduk] CHECK CONSTRAINT [FK_TBStokProduk_TBTempat]
GO
ALTER TABLE [dbo].[TBStokProdukMutasi]  WITH CHECK ADD  CONSTRAINT [FK_TBStokProdukMutasi_TBJenisStokMutasi] FOREIGN KEY([IDJenisStokMutasi])
REFERENCES [dbo].[TBJenisStokMutasi] ([IDJenisStokMutasi])
GO
ALTER TABLE [dbo].[TBStokProdukMutasi] CHECK CONSTRAINT [FK_TBStokProdukMutasi_TBJenisStokMutasi]
GO
ALTER TABLE [dbo].[TBStokProdukMutasi]  WITH CHECK ADD  CONSTRAINT [FK_TBStokProdukMutasi_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBStokProdukMutasi] CHECK CONSTRAINT [FK_TBStokProdukMutasi_TBPengguna]
GO
ALTER TABLE [dbo].[TBStokProdukMutasi]  WITH CHECK ADD  CONSTRAINT [FK_TBStokProdukMutasi_TBStokProduk] FOREIGN KEY([IDStokProduk])
REFERENCES [dbo].[TBStokProduk] ([IDStokProduk])
GO
ALTER TABLE [dbo].[TBStokProdukMutasi] CHECK CONSTRAINT [FK_TBStokProdukMutasi_TBStokProduk]
GO
ALTER TABLE [dbo].[TBStoreKey]  WITH CHECK ADD  CONSTRAINT [FK_TBStoreKey_TBPengguna] FOREIGN KEY([IDPenggunaAktif])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBStoreKey] CHECK CONSTRAINT [FK_TBStoreKey_TBPengguna]
GO
ALTER TABLE [dbo].[TBStoreKey]  WITH CHECK ADD  CONSTRAINT [FK_TBStoreKey_TBStore] FOREIGN KEY([IDStore])
REFERENCES [dbo].[TBStore] ([IDStore])
GO
ALTER TABLE [dbo].[TBStoreKey] CHECK CONSTRAINT [FK_TBStoreKey_TBStore]
GO
ALTER TABLE [dbo].[TBStoreKonfigurasi]  WITH CHECK ADD  CONSTRAINT [FK_TBStoreKonfigurasi_TBStore] FOREIGN KEY([IDStore])
REFERENCES [dbo].[TBStore] ([IDStore])
GO
ALTER TABLE [dbo].[TBStoreKonfigurasi] CHECK CONSTRAINT [FK_TBStoreKonfigurasi_TBStore]
GO
ALTER TABLE [dbo].[TBSyncDatabaseLog]  WITH CHECK ADD  CONSTRAINT [FK_TBSyncDatabaseLog_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBSyncDatabaseLog] CHECK CONSTRAINT [FK_TBSyncDatabaseLog_TBPengguna]
GO
ALTER TABLE [dbo].[TBTempat]  WITH CHECK ADD  CONSTRAINT [FK_TBTempat_TBKategoriTempat] FOREIGN KEY([IDKategoriTempat])
REFERENCES [dbo].[TBKategoriTempat] ([IDKategoriTempat])
GO
ALTER TABLE [dbo].[TBTempat] CHECK CONSTRAINT [FK_TBTempat_TBKategoriTempat]
GO
ALTER TABLE [dbo].[TBTempat]  WITH CHECK ADD  CONSTRAINT [FK_TBTempat_TBStore] FOREIGN KEY([IDStore])
REFERENCES [dbo].[TBStore] ([IDStore])
GO
ALTER TABLE [dbo].[TBTempat] CHECK CONSTRAINT [FK_TBTempat_TBStore]
GO
ALTER TABLE [dbo].[TBTempatJarak]  WITH CHECK ADD  CONSTRAINT [FK_TBTempatJarak_TBTempat] FOREIGN KEY([IDTempatAwal])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTempatJarak] CHECK CONSTRAINT [FK_TBTempatJarak_TBTempat]
GO
ALTER TABLE [dbo].[TBTempatJarak]  WITH CHECK ADD  CONSTRAINT [FK_TBTempatJarak_TBTempat1] FOREIGN KEY([IDTempatTujuan])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTempatJarak] CHECK CONSTRAINT [FK_TBTempatJarak_TBTempat1]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBAlamat] FOREIGN KEY([IDAlamat])
REFERENCES [dbo].[TBAlamat] ([IDAlamat])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBAlamat]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBJenisBebanBiaya] FOREIGN KEY([IDJenisBebanBiaya])
REFERENCES [dbo].[TBJenisBebanBiaya] ([IDJenisBebanBiaya])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBJenisBebanBiaya]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBJenisPembayaran] FOREIGN KEY([IDJenisPembayaran])
REFERENCES [dbo].[TBJenisPembayaran] ([IDJenisPembayaran])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBJenisPembayaran]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBJenisTransaksi] FOREIGN KEY([IDJenisTransaksi])
REFERENCES [dbo].[TBJenisTransaksi] ([IDJenisTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBJenisTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBKurir] FOREIGN KEY([IDKurir])
REFERENCES [dbo].[TBKurir] ([IDKurir])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBKurir]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBMeja] FOREIGN KEY([IDMeja])
REFERENCES [dbo].[TBMeja] ([IDMeja])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBMeja]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBPelanggan] FOREIGN KEY([IDPelanggan])
REFERENCES [dbo].[TBPelanggan] ([IDPelanggan])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBPelanggan]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBPengguna] FOREIGN KEY([IDPenggunaTransaksi])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBPengguna]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBPengguna1] FOREIGN KEY([IDPenggunaPembayaran])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBPengguna1]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBPengguna2] FOREIGN KEY([IDPenggunaUpdate])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBPengguna2]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBPengguna3] FOREIGN KEY([IDPenggunaMarketing])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBPengguna3]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBPengguna4] FOREIGN KEY([IDPenggunaBatal])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBPengguna4]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBStatusTransaksi] FOREIGN KEY([IDStatusTransaksi])
REFERENCES [dbo].[TBStatusTransaksi] ([IDStatusTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBStatusTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBTempat]
GO
ALTER TABLE [dbo].[TBTransaksi]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksi_TBTempat1] FOREIGN KEY([IDTempatPengirim])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTransaksi] CHECK CONSTRAINT [FK_TBTransaksi_TBTempat1]
GO
ALTER TABLE [dbo].[TBTransaksiDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiDetail_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBTransaksiDetail] CHECK CONSTRAINT [FK_TBTransaksiDetail_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBTransaksiDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiDetail_TBStokProduk] FOREIGN KEY([IDStokProduk])
REFERENCES [dbo].[TBStokProduk] ([IDStokProduk])
GO
ALTER TABLE [dbo].[TBTransaksiDetail] CHECK CONSTRAINT [FK_TBTransaksiDetail_TBStokProduk]
GO
ALTER TABLE [dbo].[TBTransaksiDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiDetail_TBTransaksi] FOREIGN KEY([IDTransaksi])
REFERENCES [dbo].[TBTransaksi] ([IDTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksiDetail] CHECK CONSTRAINT [FK_TBTransaksiDetail_TBTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksiDetailTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiDetailTemp_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBTransaksiDetailTemp] CHECK CONSTRAINT [FK_TBTransaksiDetailTemp_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBTransaksiDetailTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiDetailTemp_TBStokProduk] FOREIGN KEY([IDStokProduk])
REFERENCES [dbo].[TBStokProduk] ([IDStokProduk])
GO
ALTER TABLE [dbo].[TBTransaksiDetailTemp] CHECK CONSTRAINT [FK_TBTransaksiDetailTemp_TBStokProduk]
GO
ALTER TABLE [dbo].[TBTransaksiDetailTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiDetailTemp_TBTransaksiTemp] FOREIGN KEY([IDTransaksiTemp])
REFERENCES [dbo].[TBTransaksiTemp] ([IDTransaksiTemp])
GO
ALTER TABLE [dbo].[TBTransaksiDetailTemp] CHECK CONSTRAINT [FK_TBTransaksiDetailTemp_TBTransaksiTemp]
GO
ALTER TABLE [dbo].[TBTransaksiECommerce]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiECommerce_TBPelanggan] FOREIGN KEY([IDPelanggan])
REFERENCES [dbo].[TBPelanggan] ([IDPelanggan])
GO
ALTER TABLE [dbo].[TBTransaksiECommerce] CHECK CONSTRAINT [FK_TBTransaksiECommerce_TBPelanggan]
GO
ALTER TABLE [dbo].[TBTransaksiECommerceDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiECommerceDetail_TBStokProduk] FOREIGN KEY([IDStokProduk])
REFERENCES [dbo].[TBStokProduk] ([IDStokProduk])
GO
ALTER TABLE [dbo].[TBTransaksiECommerceDetail] CHECK CONSTRAINT [FK_TBTransaksiECommerceDetail_TBStokProduk]
GO
ALTER TABLE [dbo].[TBTransaksiECommerceDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiECommerceDetail_TBTransaksiECommerce] FOREIGN KEY([IDTransaksiECommerce])
REFERENCES [dbo].[TBTransaksiECommerce] ([IDTransaksiECommerce])
GO
ALTER TABLE [dbo].[TBTransaksiECommerceDetail] CHECK CONSTRAINT [FK_TBTransaksiECommerceDetail_TBTransaksiECommerce]
GO
ALTER TABLE [dbo].[TBTransaksiJenisPembayaran]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiJenisPembayaran_TBJenisBebanBiaya] FOREIGN KEY([IDJenisBebanBiaya])
REFERENCES [dbo].[TBJenisBebanBiaya] ([IDJenisBebanBiaya])
GO
ALTER TABLE [dbo].[TBTransaksiJenisPembayaran] CHECK CONSTRAINT [FK_TBTransaksiJenisPembayaran_TBJenisBebanBiaya]
GO
ALTER TABLE [dbo].[TBTransaksiJenisPembayaran]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiJenisPembayaran_TBJenisPembayaran] FOREIGN KEY([IDJenisPembayaran])
REFERENCES [dbo].[TBJenisPembayaran] ([IDJenisPembayaran])
GO
ALTER TABLE [dbo].[TBTransaksiJenisPembayaran] CHECK CONSTRAINT [FK_TBTransaksiJenisPembayaran_TBJenisPembayaran]
GO
ALTER TABLE [dbo].[TBTransaksiJenisPembayaran]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiJenisPembayaran_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksiJenisPembayaran] CHECK CONSTRAINT [FK_TBTransaksiJenisPembayaran_TBPengguna]
GO
ALTER TABLE [dbo].[TBTransaksiJenisPembayaran]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiJenisPembayaran_TBTransaksi] FOREIGN KEY([IDTransaksi])
REFERENCES [dbo].[TBTransaksi] ([IDTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksiJenisPembayaran] CHECK CONSTRAINT [FK_TBTransaksiJenisPembayaran_TBTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksiPrint]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiPrint_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBTransaksiPrint] CHECK CONSTRAINT [FK_TBTransaksiPrint_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBTransaksiPrint]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiPrint_TBTransaksi] FOREIGN KEY([IDTransaksi])
REFERENCES [dbo].[TBTransaksi] ([IDTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksiPrint] CHECK CONSTRAINT [FK_TBTransaksiPrint_TBTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksiPrintECommerce]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiPrintECommerce_TBStatusTransaksi] FOREIGN KEY([IDStatusTransaksi])
REFERENCES [dbo].[TBStatusTransaksi] ([IDStatusTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksiPrintECommerce] CHECK CONSTRAINT [FK_TBTransaksiPrintECommerce_TBStatusTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksiPrintECommerce]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiPrintECommerce_TBTransaksi] FOREIGN KEY([IDTransaksi])
REFERENCES [dbo].[TBTransaksi] ([IDTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksiPrintECommerce] CHECK CONSTRAINT [FK_TBTransaksiPrintECommerce_TBTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksiPrintLog]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiPrintLog_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksiPrintLog] CHECK CONSTRAINT [FK_TBTransaksiPrintLog_TBPengguna]
GO
ALTER TABLE [dbo].[TBTransaksiPrintLog]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiPrintLog_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTransaksiPrintLog] CHECK CONSTRAINT [FK_TBTransaksiPrintLog_TBTempat]
GO
ALTER TABLE [dbo].[TBTransaksiPrintLog]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiPrintLog_TBTransaksi] FOREIGN KEY([IDTransaksi])
REFERENCES [dbo].[TBTransaksi] ([IDTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksiPrintLog] CHECK CONSTRAINT [FK_TBTransaksiPrintLog_TBTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBAlamat] FOREIGN KEY([IDAlamat])
REFERENCES [dbo].[TBAlamat] ([IDAlamat])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBAlamat]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBJenisTransaksi] FOREIGN KEY([IDJenisTransaksi])
REFERENCES [dbo].[TBJenisTransaksi] ([IDJenisTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBJenisTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBKurir] FOREIGN KEY([IDKurir])
REFERENCES [dbo].[TBKurir] ([IDKurir])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBKurir]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBMeja] FOREIGN KEY([IDMeja])
REFERENCES [dbo].[TBMeja] ([IDMeja])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBMeja]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBPelanggan] FOREIGN KEY([IDPelanggan])
REFERENCES [dbo].[TBPelanggan] ([IDPelanggan])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBPelanggan]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBPengguna] FOREIGN KEY([IDPenggunaTransaksi])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBPengguna]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBPengguna1] FOREIGN KEY([IDPenggunaUpdate])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBPengguna1]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBPengguna2] FOREIGN KEY([IDPenggunaMarketing])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBPengguna2]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBPengguna3] FOREIGN KEY([IDPenggunaBatal])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBPengguna3]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBStatusTransaksi] FOREIGN KEY([IDStatusTransaksi])
REFERENCES [dbo].[TBStatusTransaksi] ([IDStatusTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBStatusTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBTempat] FOREIGN KEY([IDTempat])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBTempat]
GO
ALTER TABLE [dbo].[TBTransaksiTemp]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiTemp_TBTempat1] FOREIGN KEY([IDTempatPengirim])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTransaksiTemp] CHECK CONSTRAINT [FK_TBTransaksiTemp_TBTempat1]
GO
ALTER TABLE [dbo].[TBTransaksiVoucher]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiVoucher_TBTransaksi] FOREIGN KEY([IDTransaksi])
REFERENCES [dbo].[TBTransaksi] ([IDTransaksi])
GO
ALTER TABLE [dbo].[TBTransaksiVoucher] CHECK CONSTRAINT [FK_TBTransaksiVoucher_TBTransaksi]
GO
ALTER TABLE [dbo].[TBTransaksiVoucher]  WITH CHECK ADD  CONSTRAINT [FK_TBTransaksiVoucher_TBVoucher] FOREIGN KEY([IDVoucher])
REFERENCES [dbo].[TBVoucher] ([IDVoucher])
GO
ALTER TABLE [dbo].[TBTransaksiVoucher] CHECK CONSTRAINT [FK_TBTransaksiVoucher_TBVoucher]
GO
ALTER TABLE [dbo].[TBTransferBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferBahanBaku_TBPengguna] FOREIGN KEY([IDPengirim])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransferBahanBaku] CHECK CONSTRAINT [FK_TBTransferBahanBaku_TBPengguna]
GO
ALTER TABLE [dbo].[TBTransferBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferBahanBaku_TBPengguna1] FOREIGN KEY([IDPenerima])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransferBahanBaku] CHECK CONSTRAINT [FK_TBTransferBahanBaku_TBPengguna1]
GO
ALTER TABLE [dbo].[TBTransferBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferBahanBaku_TBTempat] FOREIGN KEY([IDTempatPengirim])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTransferBahanBaku] CHECK CONSTRAINT [FK_TBTransferBahanBaku_TBTempat]
GO
ALTER TABLE [dbo].[TBTransferBahanBaku]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferBahanBaku_TBTempat1] FOREIGN KEY([IDTempatPenerima])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTransferBahanBaku] CHECK CONSTRAINT [FK_TBTransferBahanBaku_TBTempat1]
GO
ALTER TABLE [dbo].[TBTransferBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferBahanBakuDetail_TBBahanBaku] FOREIGN KEY([IDBahanBaku])
REFERENCES [dbo].[TBBahanBaku] ([IDBahanBaku])
GO
ALTER TABLE [dbo].[TBTransferBahanBakuDetail] CHECK CONSTRAINT [FK_TBTransferBahanBakuDetail_TBBahanBaku]
GO
ALTER TABLE [dbo].[TBTransferBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferBahanBakuDetail_TBSatuan] FOREIGN KEY([IDSatuan])
REFERENCES [dbo].[TBSatuan] ([IDSatuan])
GO
ALTER TABLE [dbo].[TBTransferBahanBakuDetail] CHECK CONSTRAINT [FK_TBTransferBahanBakuDetail_TBSatuan]
GO
ALTER TABLE [dbo].[TBTransferBahanBakuDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferBahanBakuDetail_TBTransferBahanBaku] FOREIGN KEY([IDTransferBahanBaku])
REFERENCES [dbo].[TBTransferBahanBaku] ([IDTransferBahanBaku])
GO
ALTER TABLE [dbo].[TBTransferBahanBakuDetail] CHECK CONSTRAINT [FK_TBTransferBahanBakuDetail_TBTransferBahanBaku]
GO
ALTER TABLE [dbo].[TBTransferProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferProduk_TBPengguna] FOREIGN KEY([IDPengirim])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransferProduk] CHECK CONSTRAINT [FK_TBTransferProduk_TBPengguna]
GO
ALTER TABLE [dbo].[TBTransferProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferProduk_TBPengguna1] FOREIGN KEY([IDPenerima])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBTransferProduk] CHECK CONSTRAINT [FK_TBTransferProduk_TBPengguna1]
GO
ALTER TABLE [dbo].[TBTransferProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferProduk_TBTempat] FOREIGN KEY([IDTempatPengirim])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTransferProduk] CHECK CONSTRAINT [FK_TBTransferProduk_TBTempat]
GO
ALTER TABLE [dbo].[TBTransferProduk]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferProduk_TBTempat1] FOREIGN KEY([IDTempatPenerima])
REFERENCES [dbo].[TBTempat] ([IDTempat])
GO
ALTER TABLE [dbo].[TBTransferProduk] CHECK CONSTRAINT [FK_TBTransferProduk_TBTempat1]
GO
ALTER TABLE [dbo].[TBTransferProdukDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferProdukDetail_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBTransferProdukDetail] CHECK CONSTRAINT [FK_TBTransferProdukDetail_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBTransferProdukDetail]  WITH CHECK ADD  CONSTRAINT [FK_TBTransferProdukDetail_TBTransferProduk] FOREIGN KEY([IDTransferProduk])
REFERENCES [dbo].[TBTransferProduk] ([IDTransferProduk])
GO
ALTER TABLE [dbo].[TBTransferProdukDetail] CHECK CONSTRAINT [FK_TBTransferProdukDetail_TBTransferProduk]
GO
ALTER TABLE [dbo].[TBVoucher]  WITH CHECK ADD  CONSTRAINT [FK_TBVoucher_TBPelanggan1] FOREIGN KEY([IDPelanggan])
REFERENCES [dbo].[TBPelanggan] ([IDPelanggan])
GO
ALTER TABLE [dbo].[TBVoucher] CHECK CONSTRAINT [FK_TBVoucher_TBPelanggan1]
GO
ALTER TABLE [dbo].[TBWaitingList]  WITH CHECK ADD  CONSTRAINT [FK_TBWaitingList_TBMeja] FOREIGN KEY([IDMeja])
REFERENCES [dbo].[TBMeja] ([IDMeja])
GO
ALTER TABLE [dbo].[TBWaitingList] CHECK CONSTRAINT [FK_TBWaitingList_TBMeja]
GO
ALTER TABLE [dbo].[TBWaitingList]  WITH CHECK ADD  CONSTRAINT [FK_TBWaitingList_TBPelanggan] FOREIGN KEY([IDPelanggan])
REFERENCES [dbo].[TBPelanggan] ([IDPelanggan])
GO
ALTER TABLE [dbo].[TBWaitingList] CHECK CONSTRAINT [FK_TBWaitingList_TBPelanggan]
GO
ALTER TABLE [dbo].[TBWaitingList]  WITH CHECK ADD  CONSTRAINT [FK_TBWaitingList_TBPengguna] FOREIGN KEY([IDPengguna])
REFERENCES [dbo].[TBPengguna] ([IDPengguna])
GO
ALTER TABLE [dbo].[TBWaitingList] CHECK CONSTRAINT [FK_TBWaitingList_TBPengguna]
GO
ALTER TABLE [dbo].[TBWholesale]  WITH CHECK ADD  CONSTRAINT [FK_TBWholesale_TBKombinasiProduk] FOREIGN KEY([IDKombinasiProduk])
REFERENCES [dbo].[TBKombinasiProduk] ([IDKombinasiProduk])
GO
ALTER TABLE [dbo].[TBWholesale] CHECK CONSTRAINT [FK_TBWholesale_TBKombinasiProduk]
GO
ALTER TABLE [dbo].[TBWilayah]  WITH CHECK ADD  CONSTRAINT [FK_TBWilayah_TBGrupWilayah] FOREIGN KEY([IDGrupWilayah])
REFERENCES [dbo].[TBGrupWilayah] ([IDGrupWilayah])
GO
ALTER TABLE [dbo].[TBWilayah] CHECK CONSTRAINT [FK_TBWilayah_TBGrupWilayah]
GO
ALTER TABLE [dbo].[TBWilayah]  WITH CHECK ADD  CONSTRAINT [FK_TBWilayah_TBWilayah] FOREIGN KEY([IDWilayahParent])
REFERENCES [dbo].[TBWilayah] ([IDWilayah])
GO
ALTER TABLE [dbo].[TBWilayah] CHECK CONSTRAINT [FK_TBWilayah_TBWilayah]
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertPenerimaanPOProduksiBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Proc_InsertPenerimaanPOProduksiBahanBaku]
	@IDPenerimaanPOProduksiBahanBaku VARCHAR(30) OUTPUT,
	@IDPOProduksiBahanBaku VARCHAR(30),
	@IDSupplier INT,
	@IDTempat INT,
	@IDPenggunaDatang INT,
	@TanggalDatang DATETIME
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'PNRB-' + CONVERT(VARCHAR,@IDTempat) + '-' + CONVERT(VARCHAR,@IDSupplier) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@TanggalDatang)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@TanggalDatang)),2) + CONVERT(VARCHAR, YEAR(@TanggalDatang))+ '-'
	
	SELECT  TOP 1 @IDSelanjutnya = IDPenerimaanPOProduksiBahanBaku
	FROM TBPenerimaanPOProduksiBahanBaku
	WHERE MONTH(TanggalDatang) = MONTH(@TanggalDatang) AND YEAR(TanggalDatang) = YEAR(@TanggalDatang) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBPenerimaanPOProduksiBahanBaku(IDPenerimaanPOProduksiBahanBaku, IDPOProduksiBahanBaku, IDPenggunaDatang, TanggalDatang)
	VALUES (@IDSelanjutnya, @IDPOProduksiBahanBaku, @IDPenggunaDatang, @TanggalDatang)

	SET @IDPenerimaanPOProduksiBahanBaku = @IDSelanjutnya

SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertPenerimaanPOProduksiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Proc_InsertPenerimaanPOProduksiProduk]
	@IDPenerimaanPOProduksiProduk VARCHAR(30) OUTPUT,
	@IDPOProduksiProduk VARCHAR(30),
	@IDVendor INT,
	@IDTempat INT,
	@IDPenggunaDatang INT,
	@TanggalDatang DATETIME
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'PNRP-' + CONVERT(VARCHAR,@IDTempat) + '-' + CONVERT(VARCHAR,@IDVendor) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@TanggalDatang)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@TanggalDatang)),2) + CONVERT(VARCHAR, YEAR(@TanggalDatang))+ '-'
	
	SELECT  TOP 1 @IDSelanjutnya = IDPenerimaanPOProduksiProduk
	FROM TBPenerimaanPOProduksiProduk
	WHERE MONTH(TanggalDatang) = MONTH(@TanggalDatang) AND YEAR(TanggalDatang) = YEAR(@TanggalDatang) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBPenerimaanPOProduksiProduk(IDPenerimaanPOProduksiProduk, IDPOProduksiProduk, IDPenggunaDatang, TanggalDatang)
	VALUES (@IDSelanjutnya, @IDPOProduksiProduk, @IDPenggunaDatang, @TanggalDatang)

	SET @IDPenerimaanPOProduksiProduk = @IDSelanjutnya

SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertPengirimanPOProduksiBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Proc_InsertPengirimanPOProduksiBahanBaku]
	@IDPengirimanPOProduksiBahanBaku VARCHAR(30) OUTPUT,
	@IDPOProduksiBahanBaku VARCHAR(30),
	@IDSupplier INT,
	@IDTempat INT,
	@IDPengguna INT,
	@Tanggal DATETIME
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'PNGB-' + CONVERT(VARCHAR,@IDTempat) + '-' + CONVERT(VARCHAR,@IDSupplier) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal))+ '-'
	
	SELECT  TOP 1 @IDSelanjutnya = IDPengirimanPOProduksiBahanBaku
	FROM TBPengirimanPOProduksiBahanBaku
	WHERE MONTH(Tanggal) = MONTH(@Tanggal) AND YEAR(Tanggal) = YEAR(@Tanggal) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBPengirimanPOProduksiBahanBaku(IDPengirimanPOProduksiBahanBaku, IDPOProduksiBahanBaku, IDPengguna, Tanggal)
	VALUES (@IDSelanjutnya, @IDPOProduksiBahanBaku, @IDPengguna, @Tanggal)

	SET @IDPengirimanPOProduksiBahanBaku = @IDSelanjutnya

SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertPengirimanPOProduksiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Proc_InsertPengirimanPOProduksiProduk]
	@IDPengirimanPOProduksiProduk VARCHAR(30) OUTPUT,
	@IDPOProduksiProduk VARCHAR(30),
	@IDVendor INT,
	@IDTempat INT,
	@IDPengguna INT,
	@Tanggal DATETIME
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'PNGB-' + CONVERT(VARCHAR,@IDTempat) + '-' + CONVERT(VARCHAR,@IDVendor) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal))+ '-'
	
	SELECT  TOP 1 @IDSelanjutnya = IDPengirimanPOProduksiProduk
	FROM TBPengirimanPOProduksiProduk
	WHERE MONTH(Tanggal) = MONTH(@Tanggal) AND YEAR(Tanggal) = YEAR(@Tanggal) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBPengirimanPOProduksiProduk(IDPengirimanPOProduksiProduk, IDPOProduksiProduk, IDPengguna, Tanggal)
	VALUES (@IDSelanjutnya, @IDPOProduksiProduk, @IDPengguna, @Tanggal)

	SET @IDPengirimanPOProduksiProduk = @IDSelanjutnya

SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertPOProduksiBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Proc_InsertPOProduksiBahanBaku]
	@IDPOProduksiBahanBaku VARCHAR(30) OUTPUT,
	@IDTempat INT,
	@IDPenggunaPending INT,
	@EnumJenisProduksi INT,
	@Tanggal DATETIME
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'PROB' + CONVERT(VARCHAR,@EnumJenisProduksi) + '-' + CONVERT(VARCHAR,@IDTempat) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal)) + '-'
	SELECT  TOP 1 @IDSelanjutnya = IDPOProduksiBahanBaku
	FROM TBPOProduksiBahanBaku
	WHERE IDTempat = @IDTempat AND EnumJenisProduksi = @EnumJenisProduksi AND MONTH(Tanggal) = MONTH(@Tanggal) AND YEAR(Tanggal) = YEAR(@Tanggal) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBPOProduksiBahanBaku (IDPOProduksiBahanBaku, IDTempat, IDPengguna, EnumJenisProduksi, Tanggal)
	VALUES (@IDSelanjutnya, @IDTempat, @IDPenggunaPending, @EnumJenisProduksi, @Tanggal)

	SET @IDPOProduksiBahanBaku = @IDSelanjutnya


SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertPOProduksiBahanBakuPenagihan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Proc_InsertPOProduksiBahanBakuPenagihan]
	@IDPOProduksiBahanBakuPenagihan VARCHAR(30) OUTPUT,
	@IDSupplier INT,
	@IDTempat INT,
	@IDPengguna INT,
	@Tanggal DATETIME
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'PNGB' + '-' + CONVERT(VARCHAR,@IDTempat) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal)) + '-'
	SELECT  TOP 1 @IDSelanjutnya = IDPOProduksiBahanBakuPenagihan
	FROM TBPOProduksiBahanBakuPenagihan
	WHERE IDTempat = @IDTempat AND MONTH(Tanggal) = MONTH(@Tanggal) AND YEAR(Tanggal) = YEAR(@Tanggal) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBPOProduksiBahanBakuPenagihan (IDPOProduksiBahanBakuPenagihan, IDSupplier, IDTempat, IDPengguna, Tanggal)
	VALUES (@IDSelanjutnya, @IDSupplier, @IDTempat, @IDPengguna, @Tanggal)

	SET @IDPOProduksiBahanBakuPenagihan = @IDSelanjutnya


SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertPOProduksiBahanBakuRetur]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Proc_InsertPOProduksiBahanBakuRetur]
	@IDProduksiBahanBakuRetur VARCHAR(50) OUTPUT,
	@IDTempat INT,
	@IDSupplier INT,
	@IDPengguna INT,
	@Tanggal DATETIME
AS
	DECLARE @kunci VARCHAR(30)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'RTRB-' + CONVERT(VARCHAR,@IDTempat) + '-' + CONVERT(VARCHAR,@IDTempat) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal)) + '-'
	
	SELECT  TOP 1 @IDSelanjutnya = IDPOProduksiBahanBakuRetur
	FROM TBPOProduksiBahanBakuRetur
	WHERE IDTempat = @IDTempat AND MONTH(TanggalRetur) = MONTH(@Tanggal) AND YEAR(TanggalRetur) = YEAR(@Tanggal) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBPOProduksiBahanBakuRetur(IDPOProduksiBahanBakuRetur, IDTempat, IDSupplier, IDPengguna, TanggalRetur)
	VALUES (@IDSelanjutnya, @IDTempat, @IDSupplier, @IDPengguna, @Tanggal)

	SET @IDProduksiBahanBakuRetur = @IDSelanjutnya

SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertPOProduksiProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Proc_InsertPOProduksiProduk]
	@IDPOProduksiProduk VARCHAR(30) OUTPUT,
	@IDTempat INT,
	@IDPengguna INT,
	@EnumJenisProduksi INT,
	@Tanggal DATETIME
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'PROP' + CONVERT(VARCHAR,@EnumJenisProduksi) + '-' + CONVERT(VARCHAR,@IDTempat) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal)) + '-'
	SELECT  TOP 1 @IDSelanjutnya = IDPOProduksiProduk
	FROM TBPOProduksiProduk
	WHERE IDTempat = @IDTempat AND EnumJenisProduksi = @EnumJenisProduksi AND MONTH(Tanggal) = MONTH(@Tanggal) AND YEAR(Tanggal) = YEAR(@Tanggal) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBPOProduksiProduk (IDPOProduksiProduk, IDTempat, IDPengguna, EnumJenisProduksi, Tanggal)
	VALUES (@IDSelanjutnya, @IDTempat, @IDPengguna, @EnumJenisProduksi, @Tanggal)

	SET @IDPOProduksiProduk = @IDSelanjutnya


SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertPOProduksiProdukPenagihan]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Proc_InsertPOProduksiProdukPenagihan]
	@IDPOProduksiProdukPenagihan VARCHAR(30) OUTPUT,
	@IDVendor INT,
	@IDTempat INT,
	@IDPengguna INT,
	@Tanggal DATETIME
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'PNGB' + '-' + CONVERT(VARCHAR,@IDTempat) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal)) + '-'
	SELECT  TOP 1 @IDSelanjutnya = IDPOProduksiProdukPenagihan
	FROM TBPOProduksiProdukPenagihan
	WHERE IDTempat = @IDTempat AND MONTH(Tanggal) = MONTH(@Tanggal) AND YEAR(Tanggal) = YEAR(@Tanggal) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBPOProduksiProdukPenagihan (IDPOProduksiProdukPenagihan, IDVendor, IDTempat, IDPengguna, Tanggal)
	VALUES (@IDSelanjutnya, @IDVendor, @IDTempat, @IDPengguna, @Tanggal)

	SET @IDPOProduksiProdukPenagihan = @IDSelanjutnya


SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertPOProduksiProdukRetur]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Proc_InsertPOProduksiProdukRetur]
	@IDProduksiProdukRetur VARCHAR(50) OUTPUT,
	@IDTempat INT,
	@IDVendor INT,
	@IDPengguna INT,
	@Tanggal DATETIME
AS
	DECLARE @kunci VARCHAR(30)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'RTRP-' + CONVERT(VARCHAR,@IDTempat) + '-' + CONVERT(VARCHAR,@IDTempat) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal)) + '-'
	
	SELECT  TOP 1 @IDSelanjutnya = IDPOProduksiProdukRetur
	FROM TBPOProduksiProdukRetur
	WHERE IDTempat = @IDTempat AND MONTH(TanggalRetur) = MONTH(@Tanggal) AND YEAR(TanggalRetur) = YEAR(@Tanggal) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBPOProduksiProdukRetur(IDPOProduksiProdukRetur, IDTempat, IDVendor, IDPengguna, TanggalRetur)
	VALUES (@IDSelanjutnya, @IDTempat, @IDVendor, @IDPengguna, @Tanggal)

	SET @IDProduksiProdukRetur = @IDSelanjutnya

SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertProject]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Proc_InsertProject]
	@IDProject VARCHAR(30) OUTPUT,
	@IDTempat INT,
	@Tanggal DATETIME,
	@IDPelanggan INT,
	@IDPengguna INT,
	@Nama VARCHAR(50),
	@Keterangan TEXT,
	@Status BIT,
	@Progress INT
	
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'PRJ-' + CONVERT(VARCHAR,@IDTempat) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal)) + '-'
	SELECT  TOP 1 @IDSelanjutnya = IDProject
	FROM TBProject
	WHERE IDTempat = @IDTempat AND MONTH(Tanggal) = MONTH(@Tanggal) AND YEAR(Tanggal) = YEAR(@Tanggal) ORDER BY Tanggal DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBProject (IDProject, IDWMS, IDTempat, Tanggal, IDPelanggan, IDPengguna, Nama, Keterangan,Status, Progress)
	VALUES (@IDSelanjutnya,NEWID(), @IDTempat, @Tanggal, @IDPelanggan, @IDPengguna, @Nama, @Keterangan, @Status, @Progress)

	SET @IDProject = @IDSelanjutnya


SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertProyeksi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Proc_InsertProyeksi]
	@IDProyeksi VARCHAR(30) OUTPUT,
	@IDTempat INT,
	@TanggalProyeksi DATETIME
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'PROY-' + CONVERT(VARCHAR,@IDTempat) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@TanggalProyeksi)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@TanggalProyeksi)),2) + CONVERT(VARCHAR, YEAR(@TanggalProyeksi)) + '-'
	SELECT  TOP 1 @IDSelanjutnya = IDProyeksi
	FROM TBProyeksi
	WHERE IDTempat = @IDTempat AND MONTH(@TanggalProyeksi) = MONTH(@TanggalProyeksi) AND YEAR(@TanggalProyeksi) = YEAR(@TanggalProyeksi) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBProyeksi (IDProyeksi, IDTempat, TanggalProyeksi)
	VALUES (@IDSelanjutnya, @IDTempat, @TanggalProyeksi)

	SET @IDProyeksi = @IDSelanjutnya


SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertQuotation]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Proc_InsertQuotation]
	@IDQuotation VARCHAR(30) OUTPUT,
	@IDTempat INT,
	@Tanggal DATETIME
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'QUO-' + CONVERT(VARCHAR,@IDTempat) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal)) + '-'
	SELECT  TOP 1 @IDSelanjutnya = IDQuotation
	FROM TBQuotation
	WHERE MONTH(Tanggal) = MONTH(@Tanggal) AND YEAR(Tanggal) = YEAR(@Tanggal) ORDER BY Tanggal DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBQuotation(IDQuotation, Tanggal)
	VALUES (@IDSelanjutnya, @Tanggal)

	SET @IDQuotation = @IDSelanjutnya


SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertRestaurantOrder]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Proc_InsertRestaurantOrder]
	@NomorOrder VARCHAR(30) OUTPUT,
	@IDMeja INT,
	@IDPengguna INT,
	@Kategori INT,
	@Tanggal DATETIME,
	@JenisMeja INT
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)

	SET @kunci = 'ORD/' + RIGHT('0' + CONVERT(VARCHAR, DAY(@Tanggal)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@Tanggal)),2) + CONVERT(VARCHAR, YEAR(@Tanggal)) + '/'
	SELECT  TOP 1 @IDSelanjutnya = NomorOrder
	FROM TBRestaurantOrder
	WHERE DAY(Tanggal) = DAY(@Tanggal) AND MONTH(Tanggal) = MONTH(@Tanggal) AND YEAR(Tanggal) = YEAR(@Tanggal) ORDER BY CONVERT(INT,isnull(RIGHT(NomorOrder, CHARINDEX('/', REVERSE(NomorOrder)) - 1), 0)) DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,isnull(RIGHT(@IDSelanjutnya, CHARINDEX('/', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO TBRestaurantOrder (NomorOrder, IDMeja, IDPengguna, Kategori, Tanggal, JenisMeja, StatusOrder)
	VALUES (@IDSelanjutnya, @IDMeja, @IDPengguna, @Kategori, @Tanggal, @JenisMeja, 1)

	SET @NomorOrder = @IDSelanjutnya


SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertTransaksi]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Proc_InsertTransaksi]
	@IDTempat INT
	,@TanggalTransaksi DATETIME
	,@IDTransaksi VARCHAR(30) OUTPUT
	,@Nomor INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO [dbo].[TBTransaksi]
           ([IDTransaksi]
           ,[IDTempat]
		   ,[IDPelanggan]
           ,[TanggalTransaksi])
     VALUES
           (@IDTransaksi
           ,@IDTempat
		   ,1 -- GENERAL CUSTOMER
           ,@TanggalTransaksi)

		   IF(@IDTransaksi = ' ')
			BEGIN
				SET @IDTransaksi = dbo.Func_IDTransaksi(@IDTempat, SCOPE_IDENTITY(), @TanggalTransaksi)
			END

			SET @Nomor = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertTransferBahanBaku]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Proc_InsertTransferBahanBaku]
	@IDTransferBahanBaku VARCHAR(30) OUTPUT,
	@IDPengirim INT,
	@IDTempatPengirim INT,
	@IDTempatPenerima INT,
	@Keterangan VARCHAR(MAX)
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)
	DECLARE @TanggalDaftar DATETIME

	SET @TanggalDaftar = GETDATE()

	SET @kunci = 'TRANB-' + CONVERT(VARCHAR,@IDTempatPengirim) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@TanggalDaftar)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@TanggalDaftar)),2) + CONVERT(varchar, YEAR(@TanggalDaftar))+ '-'
	
	SELECT TOP 1 @IDSelanjutnya = IDTransferBahanBaku
	FROM TBTransferBahanBaku
	WHERE IDTempatPengirim = @IDTempatPengirim AND MONTH(TanggalDaftar) = MONTH(@TanggalDaftar) AND YEAR(TanggalDaftar) = YEAR(@TanggalDaftar) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,ISNULL(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO [dbo].[TBTransferBahanBaku]
           ([IDTransferBahanBaku]
           ,[IDPengirim]
           ,[IDTempatPengirim]
           ,[IDTempatPenerima]
           ,[TanggalDaftar]
           ,[TanggalUpdate]
           ,[EnumJenisTransfer]
           ,[TanggalKirim]
           ,[TotalJumlah]
           ,[GrandTotal]
           ,[Keterangan])
     VALUES
           (@IDSelanjutnya
           ,@IDPengirim
           ,@IDTempatPengirim
           ,@IDTempatPenerima
           ,GETDATE()
           ,GETDATE()
           ,5 --5 : TRANSFER PENDING
           ,@TanggalDaftar
           ,0
           ,0
           ,@Keterangan)

	SET @IDTransferBahanBaku = @IDSelanjutnya
GO
/****** Object:  StoredProcedure [dbo].[Proc_InsertTransferProduk]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Proc_InsertTransferProduk]
	@IDTransferProduk VARCHAR(30) OUTPUT,
	@IDPengirim INT,
	@IDTempatPengirim INT,
	@IDTempatPenerima INT,
	@Keterangan VARCHAR(MAX)
AS
	DECLARE @kunci VARCHAR(25)
	DECLARE @IDSelanjutnya VARCHAR(30)
	DECLARE @TanggalDaftar DATETIME

	SET @TanggalDaftar = GETDATE()

	SET @kunci = 'TRANP-' + CONVERT(VARCHAR,@IDTempatPengirim) + '-' + RIGHT('0' + CONVERT(VARCHAR, DAY(@TanggalDaftar)),2) + RIGHT('0' + CONVERT(VARCHAR, MONTH(@TanggalDaftar)),2) + CONVERT(varchar, YEAR(@TanggalDaftar))+ '-'
	
	SELECT TOP 1 @IDSelanjutnya = IDTransferProduk
	FROM TBTransferProduk
	WHERE IDTempatPengirim = @IDTempatPengirim AND MONTH(TanggalDaftar) = MONTH(@TanggalDaftar) AND YEAR(TanggalDaftar) = YEAR(@TanggalDaftar) ORDER BY Nomor DESC
	
	SET @IDSelanjutnya = @kunci + CONVERT(VARCHAR,ISNULL(RIGHT(@IDSelanjutnya, CHARINDEX('-', REVERSE(@IDSelanjutnya)) - 1), 0) + 1)

	INSERT INTO [dbo].[TBTransferProduk]
           ([IDTransferProduk]
           ,[IDPengirim]
           ,[IDTempatPengirim]
           ,[IDTempatPenerima]
           ,[TanggalDaftar]
           ,[TanggalUpdate]
           ,[EnumJenisTransfer]
           ,[TanggalKirim]
           ,[TotalJumlah]
           ,[GrandTotalHargaBeli]
           ,[GrandTotalHargaJual]
           ,[Keterangan])
     VALUES
           (@IDSelanjutnya
           ,@IDPengirim
           ,@IDTempatPengirim
           ,@IDTempatPenerima
           ,GETDATE()
           ,GETDATE()
           ,5 --5 : TRANSFER PENDING
           ,@TanggalDaftar
           ,0
           ,0
           ,0
           ,@Keterangan)

	SET @IDTransferProduk = @IDSelanjutnya
GO
/****** Object:  Trigger [dbo].[TBTransaksi_Insert]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[TBTransaksi_Insert]
   ON  [dbo].[TBTransaksi]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

    DECLARE @IDTransaksi varchar(30)
	SELECT @IDTransaksi = IDTransaksi
	FROM INSERTED

	IF(@IDTransaksi = ' ')
	BEGIN
		UPDATE TBTransaksi SET TBTransaksi.IDTransaksi = dbo.Func_IDTransaksi(TBTransaksi.IDTempat, TBTransaksi.Nomor, TBTransaksi.TanggalTransaksi)
		FROM TBTransaksi INNER JOIN INSERTED ON TBTransaksi.Nomor = INSERTED.Nomor
	END
END
GO
ALTER TABLE [dbo].[TBTransaksi] ENABLE TRIGGER [TBTransaksi_Insert]
GO
/****** Object:  Trigger [dbo].[TBTransaksiTemp_Insert]    Script Date: 7/11/2018 12:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[TBTransaksiTemp_Insert]
   ON  [dbo].[TBTransaksiTemp]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

    DECLARE @IDTransaksiTemp varchar(30)
	SELECT @IDTransaksiTemp = IDTransaksiTemp
	FROM INSERTED

	IF(@IDTransaksiTemp = ' ')
	BEGIN
		UPDATE TBTransaksiTemp SET TBTransaksiTemp.IDTransaksiTemp = dbo.Func_IDTransaksi(TBTransaksiTemp.IDTempat, TBTransaksiTemp.Nomor, TBTransaksiTemp.TanggalTransaksi)
		FROM TBTransaksiTemp INNER JOIN INSERTED ON TBTransaksiTemp.Nomor = INSERTED.Nomor
	END
END
GO
ALTER TABLE [dbo].[TBTransaksiTemp] ENABLE TRIGGER [TBTransaksiTemp_Insert]
GO
USE [master]
GO
ALTER DATABASE [DBWITEnterpriseSystem] SET  READ_WRITE 
GO
