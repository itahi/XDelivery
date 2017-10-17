
ALTER procedure [dbo].[spObterAnivesariantes]
@DataInicial char(5),
@DataFinal char(5)
as
 BEGIN
  SELECT 
  Telefone,
  Nome,
  (select top 1 Telefone from Empresa) as 'SeuTelefone',
  FROM Pessoa 
 WHERE RIGHT(CONVERT(VARCHAR,DataNascimento,112),4)
   between RIGHT(@DataInicial,2) + LEFT(@DataInicial,2) 
	AND RIGHT(@DataFinal,2) + LEFT(@DataFinal,2)
 END




