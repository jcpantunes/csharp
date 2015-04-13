CREATE TABLE [dbo].[ItemBacklog] (
    [codigo]        INT             IDENTITY (1, 1) NOT NULL,
    [tipo]          VARCHAR (20)    NULL,
    [id]            INT             NULL,
    [titulo]        TEXT            NULL,
    [status]        VARCHAR (20)    NULL,
    [planejadoPara] VARCHAR (40)    NULL,
    [dataColeta]    DATETIME        NULL,
    [valorNegocio]  INT             NULL,
    [tamanho]       INT             NULL,
    [complexidade]  INT             NULL,
    [pf]            DECIMAL (10, 1) NULL,
    [codigoProjeto] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([codigo] ASC),
    CONSTRAINT [FK_ItemBacklog_ToProjeto] FOREIGN KEY ([codigoProjeto]) REFERENCES [dbo].[Projeto] ([codigo])
);

