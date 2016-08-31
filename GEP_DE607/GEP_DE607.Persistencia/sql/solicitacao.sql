USE [DBD_GEP]
GO

DROP table [dbo].[Solicitacao];

GO

/****** Object:  Table [dbo].[Projeto]    Script Date: 30/08/2016 23:06:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

--ID	Demanda	Sistema	Tipo da Demanda	Assunto	Destinatário	Status	Data de Criação	Data de Entrega Prevista	Data de Resolução

CREATE TABLE [dbo].[Solicitacao](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[id] [int] NOT NULL,
	[demanda] [varchar](20) NOT NULL,
	[sistema] [varchar](20) NOT NULL,
	[tipoDemanda] [varchar](20) NOT NULL,
	[assunto] [text] NULL,
	[destinatario] [varchar](60) NOT NULL,
	[status] [varchar](20) NOT NULL,
	[dtInicio] [datetime] NOT NULL,
	[dtEntrega] [datetime] NOT NULL,
	[dtFinal] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

