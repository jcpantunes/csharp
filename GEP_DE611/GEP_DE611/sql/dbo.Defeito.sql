CREATE TABLE [dbo].[Defeito] (
    [codigo]				INT             IDENTITY (1, 1) NOT NULL,
    [tipo]					VARCHAR (20)    NULL,
    [id]					INT             NULL,
    [titulo]				TEXT            NULL,
    [status]				VARCHAR (20)    NULL,
    [planejadoPara]			VARCHAR (40)    NULL,
    [dataColeta]			DATETIME        NULL,
    [encontradoProjeto]     VARCHAR (40)    NULL,
    [tipoRelato]			VARCHAR (10)    NULL,
    [resolucao]				VARCHAR (20)    NULL,
    [pai]					VARCHAR (10)    NULL,
	[projeto]               VARCHAR (10)    NULL,
    [responsavel]			INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([codigo] ASC),
    CONSTRAINT [FK_Defeito_ToFuncionario] FOREIGN KEY ([responsavel]) REFERENCES [dbo].[Funcionario] ([codigo])
);
