select * from [dbo].[Sprint]

select * from [dbo].[Tarefa]

-- delete from [dbo].[Tarefa]

SELECT * FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01' 
	and dataColeta in (SELECT distinct MAX (dataColeta)  
		FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01')

SELECT distinct MAX (dataColeta)  
		FROM Tarefa WHERE planejadoPara = 'eSocial-281573-1.0.1-CONS-01'

-- UPDATE Funcionario set lotacao = 'DEBHE/DE604' WHERE codigo = 14;

SELECT distinct (status) from TAREFA