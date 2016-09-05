USE [DBD_GEP]

GO

DROP table [dbo].[ItemBacklog]

GO

/****** Object:  Table [dbo].[ItemBacklog]    Script Date: 25/08/2016 17:13:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ItemBacklog] (
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [varchar](20) NULL,
	[id] [int] NULL,
	[titulo] [text] NULL,
	[responsavel] [int] NOT NULL,
	[status] [varchar](20) NULL,
	[planejadoPara] [varchar](60) NULL,
	[pai] [varchar](10) NULL,
	[dataModificacao] [datetime] NULL,
	[projeto] [int] NULL,
	[valorNegocio] [int] NULL,
	[tamanho] [int] NULL,
	[complexidade] [int] NULL,
	[pf] [decimal](10, 1) NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ItemBacklog]  WITH CHECK ADD  CONSTRAINT [FK_ItemBacklog_ToFuncionario] FOREIGN KEY([responsavel])
REFERENCES [dbo].[Funcionario] ([codigo])
GO

ALTER TABLE [dbo].[ItemBacklog] CHECK CONSTRAINT [FK_ItemBacklog_ToFuncionario]
GO
