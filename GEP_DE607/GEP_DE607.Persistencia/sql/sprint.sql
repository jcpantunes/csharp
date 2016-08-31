USE [DBD_GEP]
GO

/****** Object:  Table [dbo].[Sprint]    Script Date: 31/08/2016 09:28:38 ******/
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

ALTER TABLE [dbo].[Sprint]  WITH CHECK ADD  CONSTRAINT [FK_Sprint_ToProjeto] FOREIGN KEY([codigoProjeto])
REFERENCES [dbo].[Projeto] ([codigo])
GO

ALTER TABLE [dbo].[Sprint] CHECK CONSTRAINT [FK_Sprint_ToProjeto]
GO


