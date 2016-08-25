USE [DBD_GEP]

-- DELETE [dbo].[Funcionario] where codigo = 96;
-- INSERT INTO Tarefa (tipo, id, titulo, responsavel, status, planejadoPara, pai, dataModificacao, projeto,  classificacao, estimativa, tempoGasto)  VALUES (@tipo, @id, @titulo, @responsavel, @status, @planejadoPara, @pai, @dataModificacao, @projeto,  @classificacao, @estimativa, @tempoGasto)

SELECT [codigo],[lotacao],[nome] FROM [dbo].[Funcionario];
SELECT [codigo],[tipo],[id],[titulo],[responsavel],[status],[planejadoPara],[pai],[dataModificacao],[projeto],[estimativa],[tempoGasto] FROM [DBD_GEP].[dbo].[Tarefa];
