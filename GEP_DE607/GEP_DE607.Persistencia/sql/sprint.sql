USE [DBD_GEP]
GO

DROP table [dbo].[Sprint];

GO
/****** Object:  Table [dbo].[Sprint]    Script Date: 31/08/2016 09:28:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Sprint] (
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](40) NULL,
	[dtInicio] [datetime] NULL,
	[dtFinal] [datetime] NULL,
	[projeto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.0-CONS-01', Convert(date, '2014-09-04'), Convert(date, '2014-09-26'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.0-CONS-02', Convert(date, '2014-09-01'), Convert(date, '2014-10-17'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.0-CONS-03', Convert(date, '2014-10-20'), Convert(date, '2014-11-10'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.0-CONS-04', Convert(date, '2014-11-11'), Convert(date, '2014-12-04'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.0-CONS-05', Convert(date, '2014-12-05'), Convert(date, '2015-01-09'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.0-CONS-06', Convert(date, '2015-01-12'), Convert(date, '2015-02-20'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.0-CONS-07', Convert(date, '2015-02-23'), Convert(date, '2015-03-20'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.0-TRANS', Convert(date, '2015-03-23'), Convert(date, '2015-03-27'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.1-CONS-01', Convert(date, '2015-03-23'), Convert(date, '2015-04-24'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.1-CONS-02', Convert(date, '2015-04-27'), Convert(date, '2015-05-22'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.1-CONS-03', Convert(date, '2015-05-25'), Convert(date, '2015-06-26'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.1-CONS-04', Convert(date, '2015-06-29'), Convert(date, '2015-07-24'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.1-CONS-05', Convert(date, '2015-03-27'), Convert(date, '2015-08-21'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.1-CONS-06', Convert(date, '2015-08-24'), Convert(date, '2015-09-04'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.1-TRANS', Convert(date, '2015-09-08'), Convert(date, '2015-11-01'), 282935);

INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.2-CONS-01', Convert(date, '2015-11-02'), Convert(date, '2015-11-26'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.2-CONS-02', Convert(date, '2015-11-27'), Convert(date, '2015-12-15'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.2-TRANS', Convert(date, '2015-11-26'), Convert(date, '2015-11-30'), 282935);

INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.3-CONS', Convert(date, '2015-12-01'), Convert(date, '2015-12-21'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.3-TRANS', Convert(date, '2015-12-22'), Convert(date, '2015-12-23'), 282935);

INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.4-CONS', Convert(date, '2015-12-28'), Convert(date, '2016-02-12'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.4-TRANS', Convert(date, '2016-02-15'), Convert(date, '2016-03-07'), 282935);

INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.5-CONS', Convert(date, '2016-03-01'), Convert(date, '2016-04-01'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.5-TRANS', Convert(date, '2016-04-04'), Convert(date, '2016-05-09'), 282935);

INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.6-CONS', Convert(date, '2016-05-09'), Convert(date, '2016-06-17'), 282935);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-281573-1.0.6-TRANS', Convert(date, '2016-06-20'), Convert(date, '2016-07-29'), 282935);

INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-921077-1.7.0-CONS', Convert(date, '2016-07-05'), Convert(date, '2016-08-19'), 994361);
INSERT INTO [dbo].[Sprint] ([nome],[dtInicio],[dtFinal],[projeto]) VALUES('eSocial-921077-1.7.0-TRANS', Convert(date, '2016-08-22'), Convert(date, '2016-09-09'), 994361);
