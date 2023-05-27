USE [master]
GO
/****** Object:  Database [portal]    Script Date: 26/5/2023 00:03:00 ******/
CREATE DATABASE [portal]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'portal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\portal.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'portal_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\portal_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [portal] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [portal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [portal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [portal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [portal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [portal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [portal] SET ARITHABORT OFF 
GO
ALTER DATABASE [portal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [portal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [portal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [portal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
CREATE DATABASE portal
GO
USE [portal]
GO
/****** Object:  Table [dbo].[busquedas]    Script Date: 26/5/2023 00:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busquedas](
	[id_busqueda] [int] IDENTITY(1,1) NOT NULL,
	[id_oferta] [int] NULL,
	[id_usuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_busqueda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[comentario]    Script Date: 26/5/2023 00:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comentario](
	[id_det_oferta] [int] IDENTITY(1,1) NOT NULL,
	[comentario] [varchar](150) NULL,
	[id_usuario_null] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_det_oferta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[det_oferta]    Script Date: 26/5/2023 00:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[det_oferta](
	[id_det_oferta] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_oferta] [int] NULL,
	[curriculum] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_det_oferta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[oferta]    Script Date: 26/5/2023 00:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[oferta](
	[id_oferta] [int] IDENTITY(1,1) NOT NULL,
	[tipo_trabajo] [varchar](50) NULL,
	[experiencia] [varchar](50) NULL,
	[salario] [float] NULL,
	[tipo_contrato] [varchar](100) NULL,
	[ubicacion] [varchar](50) NULL,
	[id_empresa] [int] NULL,
	[foto] [varbinary](max) NULL,
	[fecha_publicacion] [datetime] NULL,
	[estado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_oferta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 26/5/2023 00:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[nombre_usuario] [varchar](50) NOT NULL,
	[empresa] [int] NULL,
	[direccion] [varchar](100) NULL,
	[telefono] [varchar](9) NULL,
	[foto] [varbinary](max) NULL,
	[correo] [varchar](50) NULL,
	[password] [varchar](16) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[oferta] ON 

INSERT [dbo].[oferta] ([id_oferta], [tipo_trabajo], [experiencia], [salario], [tipo_contrato], [ubicacion], [id_empresa], [foto], [fecha_publicacion], [estado]) VALUES (1, N'Desarrollador', N'cero', 500, N'anual', N'San sivar', 2, NULL, CAST(N'2023-05-25T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[oferta] ([id_oferta], [tipo_trabajo], [experiencia], [salario], [tipo_contrato], [ubicacion], [id_empresa], [foto], [fecha_publicacion], [estado]) VALUES (2, N'Grafico', N'cero', 500, N'mensual', N'San sivar', 2, NULL, CAST(N'2023-05-21T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[oferta] ([id_oferta], [tipo_trabajo], [experiencia], [salario], [tipo_contrato], [ubicacion], [id_empresa], [foto], [fecha_publicacion], [estado]) VALUES (1002, N'Desarrollador full stack', N'1 anio', 1000, N'anual', N'San Sivar', 2, NULL, CAST(N'2023-04-15T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[oferta] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([id_usuario], [nombre], [nombre_usuario], [empresa], [direccion], [telefono], [foto], [correo], [password]) VALUES (1, N'prueba', N'pru', 0, N'Santa Ana', N'7770-7997', NULL, N'asd@da.com', N'123')
INSERT [dbo].[usuario] ([id_usuario], [nombre], [nombre_usuario], [empresa], [direccion], [telefono], [foto], [correo], [password]) VALUES (2, N'prueba2', N'as', 1, N'Santa Ana', N'7989-909', NULL, N'adf@.com', N'123')
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__usuario__D4D22D742C0ED533]    Script Date: 26/5/2023 00:03:00 ******/
ALTER TABLE [dbo].[usuario] ADD UNIQUE NONCLUSTERED 
(
	[nombre_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[busquedas]  WITH CHECK ADD  CONSTRAINT [fk_oferta] FOREIGN KEY([id_oferta])
REFERENCES [dbo].[oferta] ([id_oferta])
GO
ALTER TABLE [dbo].[busquedas] CHECK CONSTRAINT [fk_oferta]
GO
ALTER TABLE [dbo].[busquedas]  WITH CHECK ADD  CONSTRAINT [fk_usuario_busqueda] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[busquedas] CHECK CONSTRAINT [fk_usuario_busqueda]
GO
ALTER TABLE [dbo].[det_oferta]  WITH CHECK ADD  CONSTRAINT [fk_oferta_det_oferta] FOREIGN KEY([id_oferta])
REFERENCES [dbo].[oferta] ([id_oferta])
GO
ALTER TABLE [dbo].[det_oferta] CHECK CONSTRAINT [fk_oferta_det_oferta]
GO
ALTER TABLE [dbo].[det_oferta]  WITH CHECK ADD  CONSTRAINT [fk_usuario_det_oferta] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[det_oferta] CHECK CONSTRAINT [fk_usuario_det_oferta]
GO
ALTER TABLE [dbo].[oferta]  WITH CHECK ADD  CONSTRAINT [fk_usuario] FOREIGN KEY([id_empresa])
REFERENCES [dbo].[usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[oferta] CHECK CONSTRAINT [fk_usuario]
GO
USE [master]
GO
ALTER DATABASE [portal] SET  READ_WRITE 
GO
