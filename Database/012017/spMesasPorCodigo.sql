create procedure spObterMesasPorCodigo
@Codigo int
as
 select * from Mesas where Codigo=@Codigo