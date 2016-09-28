USE [DBD_GEP]
GO

DROP table [dbo].[Siscop];

GO
/****** Object:  Table [dbo].[Siscop]    Script Date: 31/08/2016 09:28:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Siscop] (
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[responsavel] [int] NOT NULL,
	[data] [datetime] NULL,
	[batida] [varchar](80) NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
