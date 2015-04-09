CREATE TABLE [dbo].[Sprint] (
    [codigo]   INT        IDENTITY (1, 1) NOT NULL,
    [nome]     NCHAR (40) NULL,
    [dtInicio] DATETIME   NULL,
    [dtFinal]  DATETIME   NULL,
    [codigoProjeto] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([codigo] ASC), 
    CONSTRAINT [FK_Sprint_ToProjeto] FOREIGN KEY ([codigoProjeto]) REFERENCES [Projeto]([codigo])
);

