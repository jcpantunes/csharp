select * from [dbo].[Sprint]

-- delete from [dbo].[Tarefa]
-- delete from [dbo].[TarefaHistorico]
-- delete from [dbo].[ItemBacklog]


SELECT SUM(estimativa) FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01'
	and estimativaCorrigida = 0 and dataColeta in (SELECT distinct MAX (dataColeta)  
		FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01') 
union  
SELECT SUM(estimativaCorrigida) FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01'
	and estimativaCorrigida > 0 and dataColeta in (SELECT distinct MAX (dataColeta) 
		FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01')


SELECT dataColeta, SUM(tempoGasto) FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01' 
	and tempoGasto > 0	
	and ( dataColeta = '23/03/2015 00:00:00' or dataColeta = '24/03/2015 00:00:00')
	GROUP BY dataColeta ORDER BY dataColeta ASC
	

SELECT * FROM ItemBacklog

SELECT * FROM ItemBacklog WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01'

SELECT * FROM ItemBacklog WHERE planejadoPara = 'eSocial-281573-1.0.0-CONS-01'



SELECT * FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01' 
	and dataColeta in (SELECT distinct MAX (dataColeta)  
		FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01')

SELECT DISTINCT MAX (dataColeta)  
		FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01'

-- UPDATE Funcionario set lotacao = 'DEBHE/DE604' WHERE codigo = 14;

-- UPDATE TAREFA set estimaticaCorrigida = 7.6 where codigo = 3439

SELECT * FROM [dbo].[Tarefa]


SELECT distinct (status) from TAREFA


SELECT distinct (dataColeta) FROM Tarefa 
	WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01            ' 
	and tempoGasto <> 0  ORDER BY dataColeta ASC 


SELECT * FROM Tarefa 
  WHERE pai in (SELECT distinct(pai) from tarefa where codigo = 3542)


UPDATE ItemBacklog
	SET tipo = 'Item de Backlog', 
	id = 300778, 
	titulo = '(N) F01.01 Identificar Usuário',
	status = 'Pronto', 
	planejadoPara = 'eSocial-281573-1.0.0-CONS-01', 
	valorNegocio = 990, 
	tamanho = 13, 
	complexidade = 60, 
	pf = 10,
	codigoProjeto = 1
	WHERE codigo = 132;


SELECT SUM(estimativa) FROM TarefaHistorico 
	WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01' and estimativaCorrigida = 0 and dataColeta in 
		(SELECT distinct MAX (dataColeta)  FROM TarefaHistorico WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01')
union  
SELECT SUM(estimativaCorrigida) FROM TarefaHistorico 
	WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01' and estimativaCorrigida > 0 and dataColeta in 
		(SELECT distinct MAX (dataColeta)  FROM TarefaHistorico WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01')