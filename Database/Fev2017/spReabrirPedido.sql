create procedure spReabrirPedido
@CodPedido int
as
begin
delete from CaixaMOvimento where NumeroDocumento =@CodPedido
delete from Pedido_Finalizacao where CodPedido=@CodPedido;
delete from Pedido_OpcoesProduto where CodPedido=@CodPedido;
delete from HistoricoCancelamentos where CodPedido=@CodPedido;
update Pedido set Finalizado=0 , [status]='Aberto' , RealizadoEm=GetDate() where Codigo=@CodPedido;
update Mesas set StatusMesa =2 where Codigo=(select top 1 CodigoMesa from Pedido where Codigo=@CodPedido order by RealizadoEm desc);

end
