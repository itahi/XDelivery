
ALTER procedure [dbo].[spObterCodigoMesa]
@NumeroMesa nvarchar(100)
 as 
   begin
    select Codigo,NumeroMesa from Mesas
	  where NumeroMesa = @NumeroMesa
   end
