CREATE DATABASE portal;
go
use portal;
go
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
/****** Object:  Table [dbo].[comentario]    Script Date: 30/5/2023 13:47:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comentario](
	[id_det_oferta] [int] IDENTITY(1,1) NOT NULL,
	[Comentario] [varchar](150) NULL,
	[id_usuario_null] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_det_oferta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[det_oferta]    Script Date: 30/5/2023 13:47:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[det_oferta](
	[id_det_oferta] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_oferta] [int] NULL,
	[curriculum] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_det_oferta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[oferta]    Script Date: 30/5/2023 13:47:19 ******/
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
	[foto] [varchar](max) NULL,
	[fecha_publicacion] [datetime] NULL,
	[fecha_contratacion] [varchar](100) NULL,
	[estado] [int] NULL,
	[requisitos] [varchar](max) NULL,
	[habilidades] [varchar](max) NULL,
	[rango_edad] [varchar](max) NULL,
	[nivel_academico] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_oferta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 30/5/2023 13:47:19 ******/
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
	[foto] [varchar](max) NULL,
	[correo] [varchar](50) NULL,
	[password] [varchar](16) NULL,
	[tecnologias] [varchar](max) NULL,
	[titulos] [varchar](max) NULL,
	[trabajos] [varchar](max) NULL,
	[cursos] [varchar](max) NULL,
	[habilidades] [varchar](max) NULL,
	[aptitudes] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[oferta] ON 

INSERT [dbo].[oferta] ([id_oferta], [tipo_trabajo], [experiencia], [salario], [tipo_contrato], [ubicacion], [id_empresa], [foto], [fecha_publicacion], [fecha_contratacion], [estado], [requisitos], [habilidades], [rango_edad], [nivel_academico]) VALUES (1, N'Desarrollador', N'cero', 500, N'anual', N'San sivar', 2, NULL, CAST(N'2023-05-25T00:00:00.000' AS DateTime), NULL, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[oferta] ([id_oferta], [tipo_trabajo], [experiencia], [salario], [tipo_contrato], [ubicacion], [id_empresa], [foto], [fecha_publicacion], [fecha_contratacion], [estado], [requisitos], [habilidades], [rango_edad], [nivel_academico]) VALUES (2, N'Grafico', N'cero', 500, N'mensual', N'San sivar', 2, NULL, CAST(N'2023-05-21T00:00:00.000' AS DateTime), NULL, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[oferta] ([id_oferta], [tipo_trabajo], [experiencia], [salario], [tipo_contrato], [ubicacion], [id_empresa], [foto], [fecha_publicacion], [fecha_contratacion], [estado], [requisitos], [habilidades], [rango_edad], [nivel_academico]) VALUES (1002, N'Desarrollador full stack', N'1 anio', 1000, N'anual', N'San Sivar', 2, NULL, CAST(N'2023-04-15T00:00:00.000' AS DateTime), NULL, 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[oferta] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([id_usuario], [nombre], [nombre_usuario], [empresa], [direccion], [telefono], [foto], [correo], [password], [tecnologias], [titulos], [trabajos], [cursos], [habilidades], [aptitudes]) VALUES (1, N'prueba', N'pru', 0, N'Santa Ana', N'7770-7997', NULL, N'asd@da.com', N'123', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[usuario] ([id_usuario], [nombre], [nombre_usuario], [empresa], [direccion], [telefono], [foto], [correo], [password], [tecnologias], [titulos], [trabajos], [cursos], [habilidades], [aptitudes]) VALUES (2, N'prueba2', N'as', 1, N'Santa Ana', N'7989-909', NULL, N'adf@.com', N'123', NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__usuario__D4D22D74CFF94E87]    Script Date: 30/5/2023 13:47:19 ******/
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
