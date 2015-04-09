CREATE TABLE [dbo].[Projeto] (
    [codigo]   INT        IDENTITY (1, 1) NOT NULL,
    [nome]     NCHAR (40) NOT NULL,
    [id]       INT        NOT NULL,
    [dtInicio] DATETIME   NULL,
    [dtFinal]  DATETIME   NULL,
    PRIMARY KEY CLUSTERED ([codigo] ASC)
);

