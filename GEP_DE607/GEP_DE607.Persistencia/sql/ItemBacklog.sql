USE [DBD_GEP]
GO

/****** Object:  Table [dbo].[ItemBacklog]    Script Date: 31/08/2016 09:27:54 ******/
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

ALTER TABLE [dbo].[ItemBacklog]  WITH CHECK ADD  CONSTRAINT [FK_ItemBacklog_ToProjeto] FOREIGN KEY([codigoProjeto])
REFERENCES [dbo].[Projeto] ([codigo])
GO

ALTER TABLE [dbo].[ItemBacklog] CHECK CONSTRAINT [FK_ItemBacklog_ToProjeto]
GO


