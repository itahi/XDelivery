create procedure spObterAnivesariantes
@DataInicial datetime,
@DataFinal datetime
as
 BEGIN
  SELECT Telefone,Nome
  FROM Pessoa 
  where DataNascimento BETWEEN @DataInicial and @DataFinal
 END
 
