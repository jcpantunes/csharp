﻿USE [DBD_GEP]

GO

DROP TABLE [dbo].[Apropriacao]

GO

/****** Object:  Table [dbo].[Apropriacao]    Script Date: 30/08/2016 22:43:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

-- Nome	Data	Horas	Tarefa	Macroatividade	Mnemonico	Projeto
-- JULIO CESAR PEREIRA ANTUNES	2016-08-01	8.000000	995994	Gestão de Projeto de Software	eSocial-281573	282935
-- (int codigo, string nome, DateTime data, decimal hora, int tarefa, string macroatividade, string mnemonico, int projeto)
CREATE TABLE [dbo].[Apropriacao](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](60) NOT NULL,
	[data] [datetime] NOT NULL,
	[hora] [decimal](10, 1) NOT NULL,
	[tarefa] [int] NOT NULL,
	[macroatividade] [varchar](60) NULL,
	[mnemonico] [varchar](60) NULL,
	[projeto] [int] NULL,
	
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
