CREATE TABLE [dbo].[Funcionario] (
    [codigo]  INT          IDENTITY (1, 1) NOT NULL,
    [lotacao] VARCHAR (30) NULL,
    [nome]    VARCHAR (80) NULL,
    PRIMARY KEY CLUSTERED ([codigo] ASC)
);

