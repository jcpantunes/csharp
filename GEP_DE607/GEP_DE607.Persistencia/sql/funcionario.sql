USE [DBD_GEP]

GO

DROP TABLE [dbo].[Funcionario]

GO

/****** Object:  Table [dbo].[Funcionario]    Script Date: 25/08/2016 17:19:44 ******/
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

insert into funcionario values ('DEBHE/DE607', 'Julio Cesar Pereira Antunes');
insert into funcionario values ('DEBHE/DE607', 'Andre Pereira Viveiros');
insert into funcionario values ('DEBHE/DE607', 'Brian Luppi Pimentel');
insert into funcionario values ('DEBHE/DE607', 'Claudia Helena Souza e Silva Santos Behring');
insert into funcionario values ('DEBHE/DE607', 'Eduardo Resende Gomes');
insert into funcionario values ('DEBHE/DE607', 'Filipe Montenegro Campos de Albuquerque');
insert into funcionario values ('DEBHE/DE607', 'Francisco Dimas Santos Coimbra');
insert into funcionario values ('DEBHE/DE607', 'Gisely Cristina Maciel');
insert into funcionario values ('DEBHE/DE607', 'Guilherme Schmidt Bacelar');
insert into funcionario values ('DEBHE/DE607', 'Herbert Sidney Resende');
insert into funcionario values ('DEBHE/DE607', 'Jose Maurilio Gonçalves Dias');
insert into funcionario values ('DEBHE/DE607', 'Lisiene Bruna de Lima Pio Santos');
insert into funcionario values ('DEBHE/DE607', 'Mauricio Pereira Reis');
insert into funcionario values ('DEBHE/DE607', 'Raphael Schumman');
insert into funcionario values ('DEBHE/DE607', 'Sergio Marcio Magalhaes Teixeira');
insert into funcionario values ('DEBHE/DE607', 'Solange Leal Costa');

GO

