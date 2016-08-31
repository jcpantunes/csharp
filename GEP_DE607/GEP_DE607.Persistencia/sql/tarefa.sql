USE [DBD_GEP]

GO

DROP table [dbo].[Tarefa]

GO

/****** Object:  Table [dbo].[Tarefa]    Script Date: 25/08/2016 17:13:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tarefa] (
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
	[classificacao] [varchar](20) NULL,
	[estimativa] [decimal](10, 1) NULL,
	[tempoGasto] [decimal](10, 1) NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Tarefa]  WITH CHECK ADD  CONSTRAINT [FK_Tarefa_ToFuncionario] FOREIGN KEY([responsavel])
REFERENCES [dbo].[Funcionario] ([codigo])
GO

ALTER TABLE [dbo].[Tarefa] CHECK CONSTRAINT [FK_Tarefa_ToFuncionario]
GO


