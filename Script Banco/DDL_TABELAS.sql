USE [Artigo]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 08/07/2009 18:07:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cliente](
	[Pessoa] [int] NOT NULL CONSTRAINT [DF__Cliente__Pessoa__0F975522]  DEFAULT ((0)),
	[CPF] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[RG] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[DataNascimento] [datetime] NULL,
	[Sexo] [char](1) COLLATE Latin1_General_CI_AS NULL,
	[Ativo] [char](1) COLLATE Latin1_General_CI_AS NULL,
	[Anexo] [image] NULL,
 CONSTRAINT [aaaaaCliente_PK] PRIMARY KEY NONCLUSTERED 
(
	[Pessoa] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 08/07/2009 18:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pessoa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Endereco] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Bairro] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Cidade] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[UF] [char](2) COLLATE Latin1_General_CI_AS NULL,
	[CEP] [nvarchar](10) COLLATE Latin1_General_CI_AS NULL,
	[Telefone] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Email] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [aaaaaPessoa_PK] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF