CREATE TABLE [dbo].[Tarefa] (
    [codigo]              INT          IDENTITY (1, 1) NOT NULL,
    [tipo]                VARCHAR (20) NULL,
    [id]                  INT          NULL,
    [titulo]              TEXT         NULL,
    [status]              VARCHAR (20) NULL,
    [planejadoPara]       VARCHAR (40) NULL,
    [pai]                 VARCHAR (10) NULL,
    [dataColeta]          DATETIME     NULL,
    [estimativa]          VARCHAR (20) NULL,
    [estimaticaCorrigida] VARCHAR (20) NULL,
    [tempoGasto]          VARCHAR (20) NULL,
    [responsavel]         INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([codigo] ASC),
    CONSTRAINT [FK_Tarefa_ToFuncionario] FOREIGN KEY ([responsavel]) REFERENCES [dbo].[Funcionario] ([codigo])
);

