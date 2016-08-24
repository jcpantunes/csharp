USE [DBD_GEP]
GO
/****** Object:  Table [dbo].[Defeito]    Script Date: 24/08/2016 18:41:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Defeito](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [varchar](20) NULL,
	[id] [int] NULL,
	[titulo] [text] NULL,
	[status] [varchar](20) NULL,
	[planejadoPara] [varchar](40) NULL,
	[dataColeta] [datetime] NULL,
	[encontradoProjeto] [varchar](40) NULL,
	[tipoRelato] [varchar](10) NULL,
	[resolucao] [varchar](20) NULL,
	[pai] [varchar](10) NULL,
	[codigoProjeto] [int] NOT NULL,
	[responsavel] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Funcionario]    Script Date: 24/08/2016 18:41:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Funcionario](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[lotacao] [varchar](30) NULL,
	[nome] [varchar](80) NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ItemBacklog]    Script Date: 24/08/2016 18:41:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ItemBacklog](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [varchar](20) NULL,
	[id] [int] NULL,
	[titulo] [text] NULL,
	[status] [varchar](20) NULL,
	[planejadoPara] [varchar](40) NULL,
	[dataColeta] [datetime] NULL,
	[valorNegocio] [int] NULL,
	[tamanho] [int] NULL,
	[complexidade] [int] NULL,
	[pf] [decimal](10, 1) NULL,
	[codigoProjeto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Projeto]    Script Date: 24/08/2016 18:41:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Projeto](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](40) NOT NULL,
	[id] [int] NOT NULL,
	[dtInicio] [datetime] NULL,
	[dtFinal] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sprint]    Script Date: 24/08/2016 18:41:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sprint](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](40) NULL,
	[dtInicio] [datetime] NULL,
	[dtFinal] [datetime] NULL,
	[codigoProjeto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tarefa]    Script Date: 24/08/2016 18:41:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tarefa](
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
/****** Object:  Table [dbo].[TarefaHistorico]    Script Date: 24/08/2016 18:41:56 ******/
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
ALTER TABLE [dbo].[Defeito]  WITH CHECK ADD  CONSTRAINT [FK_Defeito_ToFuncionario] FOREIGN KEY([responsavel])
REFERENCES [dbo].[Funcionario] ([codigo])
GO
ALTER TABLE [dbo].[Defeito] CHECK CONSTRAINT [FK_Defeito_ToFuncionario]
GO
ALTER TABLE [dbo].[ItemBacklog]  WITH CHECK ADD  CONSTRAINT [FK_ItemBacklog_ToProjeto] FOREIGN KEY([codigoProjeto])
REFERENCES [dbo].[Projeto] ([codigo])
GO
ALTER TABLE [dbo].[ItemBacklog] CHECK CONSTRAINT [FK_ItemBacklog_ToProjeto]
GO
ALTER TABLE [dbo].[Sprint]  WITH CHECK ADD  CONSTRAINT [FK_Sprint_ToProjeto] FOREIGN KEY([codigoProjeto])
REFERENCES [dbo].[Projeto] ([codigo])
GO
ALTER TABLE [dbo].[Sprint] CHECK CONSTRAINT [FK_Sprint_ToProjeto]
GO
ALTER TABLE [dbo].[Tarefa]  WITH CHECK ADD  CONSTRAINT [FK_Tarefa_ToFuncionario] FOREIGN KEY([responsavel])
REFERENCES [dbo].[Funcionario] ([codigo])
GO
ALTER TABLE [dbo].[Tarefa] CHECK CONSTRAINT [FK_Tarefa_ToFuncionario]
GO
ALTER TABLE [dbo].[TarefaHistorico]  WITH CHECK ADD  CONSTRAINT [FK_TarefaHistorico_ToFuncionario] FOREIGN KEY([responsavel])
REFERENCES [dbo].[Funcionario] ([codigo])
GO
ALTER TABLE [dbo].[TarefaHistorico] CHECK CONSTRAINT [FK_TarefaHistorico_ToFuncionario]
GO
