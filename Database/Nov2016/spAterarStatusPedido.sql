create procedure spAlteraPedidoStatusMovimento
@CodPedido int,
@CodStatus int,
@CodUsuario int,
@DataAlteracao datetime
as
 update PedidoStatusMovimento set
 DataAlteracao=@DataAlteracao,
 CodUsuario = @CodUsuario
 where CodPedido=@CodPedido and CodStatus=@CodStatus
 