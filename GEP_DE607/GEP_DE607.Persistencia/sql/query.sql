USE [DBD_GEP]

-- TRUNCATE TABLE [dbo].[Projeto];
-- TRUNCATE TABLE [dbo].[Sprint];
-- TRUNCATE TABLE [dbo].[Tarefa];
-- TRUNCATE TABLE [dbo].[Defeito];
-- TRUNCATE TABLE [dbo].[Relato];
-- DELETE FROM [dbo].[Funcionario];
-- TRUNCATE TABLE ItemBacklog;

-- SELECT * FROM [dbo].[Projeto];
-- SELECT * FROM [dbo].[Sprint];
-- select * from Sprint order by nome, dtInicio;
-- SELECT * FROM [dbo].[Tarefa];
-- SELECT * FROM [dbo].[Defeito];
-- SELECT * FROM [dbo].[Relato];
-- SELECT * FROM [dbo].[Funcionario] order by lotacao, nome;

-- select * from ItemBacklog;
-- SELECT * FROM Sprint where projeto in (282805) Order by nome;
-- SELECT * FROM Projeto WHERE sistema = 'DCTF-Web' 

-- UPDATE [dbo].[Funcionario] SET lotacao = 'SUPDE/DEBHE/DE605' where codigo in (81, 85, 86, 89);
-- update relato set responsavel = 56 where responsavel = 91;
-- select * from relato where responsavel = 91;
-- DELETE [dbo].[Funcionario] WHERE codigo = 91;


SELECT * FROM Projeto WHERE sistema = 'eSocial';

SELECT aprop.codigo, aprop.nome, aprop.data, aprop.hora, aprop.tarefa, tar.titulo, aprop.macroatividade, aprop.mnemonico, aprop.projeto
	FROM Apropriacao aprop inner join Tarefa tar on aprop.tarefa = tar.id
	where aprop.nome = 'Filipe Montenegro Campos de Albuquerque' 
	and convert(date, aprop.data) >= convert(date, '2016-08-01');
