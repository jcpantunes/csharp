USE [DBD_GEP]

GO

DROP table [dbo].[Defeito];

DROP table [dbo].[Relato];

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Defeito] (
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [varchar](20) NULL,
	[id] [int] NULL,
	[titulo] [text] NULL,
	[responsavel] [int] NOT NULL,
	[status] [varchar](20) NULL,
	[planejadoPara] [varchar](40) NULL,
	[pai] [varchar](10) NULL,
	[dataModificacao] [datetime] NULL,
	[projeto] [int] NULL,
	[criadoPor] [varchar](60) NULL,
	[encontradoProjeto] [varchar](40) NULL,
	[tipoRelato] [varchar](10) NULL,
	[resolucao] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Defeito]  WITH CHECK ADD  CONSTRAINT [FK_Defeito_ToFuncionario] FOREIGN KEY([responsavel])
REFERENCES [dbo].[Funcionario] ([codigo])
GO

ALTER TABLE [dbo].[Defeito] CHECK CONSTRAINT [FK_Defeito_ToFuncionario]
GO

GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Relato] (
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [varchar](20) NULL,
	[id] [int] NULL,
	[titulo] [text] NULL,
	[responsavel] [int] NOT NULL,
	[status] [varchar](20) NULL,
	[planejadoPara] [varchar](40) NULL,
	[pai] [varchar](10) NULL,
	[dataModificacao] [datetime] NULL,
	[projeto] [int] NULL,
	[criadoPor] [varchar](60) NULL,
	[encontradoProjeto] [varchar](40) NULL,
	[tipoRelato] [varchar](10) NULL,
	[resolucao] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Relato]  WITH CHECK ADD  CONSTRAINT [FK_Relato_ToFuncionario] FOREIGN KEY([responsavel])
REFERENCES [dbo].[Funcionario] ([codigo])
GO

ALTER TABLE [dbo].[Relato] CHECK CONSTRAINT [FK_Relato_ToFuncionario]
GO

