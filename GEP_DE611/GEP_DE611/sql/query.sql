select * from [dbo].[Sprint]

-- delete from [dbo].[Tarefa]

SELECT * FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01' 
	and dataColeta in (SELECT distinct MAX (dataColeta)  
		FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01')

SELECT distinct MAX (dataColeta)  
		FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01'

-- UPDATE Funcionario set lotacao = 'DEBHE/DE604' WHERE codigo = 14;

-- UPDATE TAREFA set estimaticaCorrigida = 7.6 where codigo = 3439

select * from [dbo].[Tarefa]


SELECT distinct (status) from TAREFA


SELECT distinct (dataColeta) FROM Tarefa 
	WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01            ' 
	and tempoGasto <> 0  ORDER BY dataColeta ASC 


SELECT * FROM Tarefa 
  WHERE pai in (SELECT distinct(pai) from tarefa where codigo = 3542)

--	WHERE pai like '%313216%'

