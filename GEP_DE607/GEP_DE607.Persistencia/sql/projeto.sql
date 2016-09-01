USE [DBD_GEP]

GO

DROP table [dbo].[Projeto];

GO

/****** Object:  Table [dbo].[Projeto]    Script Date: 30/08/2016 22:48:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- int codigo, string tipo, int id, string nome, string sistema, string linguagem, string processo, string tipoProjeto, 
-- string situacao, int conclusividade, decimal pfprev, decimal pfreal, decimal apropriacao, DateTime dtInicio, DateTime dtEntrega, DateTime dtFim

CREATE TABLE [dbo].[Projeto](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[ss] [int] NOT NULL,
	[tipo] [varchar](20) NOT NULL,
	[id] [int] NOT NULL,
	[nome] [varchar](60) NOT NULL,
	[sistema] [varchar](20) NOT NULL,
	[linguagem] [varchar](10) NOT NULL,
	[processo] [varchar](20) NULL,
	[tipoProjeto] [varchar](20) NOT NULL,
	[situacao] [varchar](20) NOT NULL,
	[conclusividade] [int] NOT NULL,
	[pfprev] [decimal] (10, 1) NOT NULL,
	[pfreal] [decimal] (10, 1) NOT NULL,
	[apropriacao] [decimal] (10, 1) NOT NULL,
	[dtInicio] [datetime] NOT NULL,
	[dtEntrega] [datetime] NOT NULL,
	[dtFinal] [datetime] NOT NULL,
	PRIMARY KEY CLUSTERED
(
	[codigo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
