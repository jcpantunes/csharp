USE [DBD_GEP]
GO

/****** Object:  Table [dbo].[TarefaHistorico]    Script Date: 31/08/2016 09:25:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TarefaHistorico](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [varchar](20) NULL,
	[id] [int] NULL,
	[titulo] [text] NULL,
	[status] [varchar](20) NULL,
	[planejadoPara] [varchar](40) NULL,
	[pai] [varchar](10) NULL,
	[dataColeta] [datetime] NULL,
	[estimativa] [decimal](10, 1) NULL,
	[estimativaCorrigida] [decimal](10, 1) NULL,
	[tempoGasto] [decimal](10, 1) NULL,
	[responsavel] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TarefaHistorico]  WITH CHECK ADD  CONSTRAINT [FK_TarefaHistorico_ToFuncionario] FOREIGN KEY([responsavel])
REFERENCES [dbo].[Funcionario] ([codigo])
GO

ALTER TABLE [dbo].[TarefaHistorico] CHECK CONSTRAINT [FK_TarefaHistorico_ToFuncionario]
GO


