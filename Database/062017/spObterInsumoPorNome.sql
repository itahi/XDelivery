create procedure spObterInsumoPorNome
@Nome nvarchar(max)
as
 begin
   select Codigo,Nome from Insumo
   where AtivoSN=1 and Nome Like '%'+@Nome+'%'
 end