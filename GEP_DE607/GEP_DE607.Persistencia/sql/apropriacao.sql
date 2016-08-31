USE [DBD_GEP]
GO

/****** Object:  Table [dbo].[Apropriacao]    Script Date: 30/08/2016 22:43:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Apropriacao](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](40) NULL,
	[data] [datetime] NULL,
	[tarefa] [int] NULL,
	[hora] [decimal](10, 1) NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
