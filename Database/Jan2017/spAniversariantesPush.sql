
create procedure [dbo].[spObterAnivesariantesPush]
@DataInicial char(5),
@DataFinal char(5)
as
 BEGIN
  SELECT Nome,[user_id]
  FROM Pessoa 
 WHERE 	RIGHT(CONVERT(VARCHAR,DataNascimento,112),4)
   between RIGHT(@DataInicial,2) + LEFT(@DataInicial,2) 
	AND RIGHT(@DataFinal,2) + LEFT(@DataFinal,2)
	and [user_id] is not null and [user_id] !=''
 END

